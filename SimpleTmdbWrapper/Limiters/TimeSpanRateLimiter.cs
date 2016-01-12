using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NLog;

namespace SimpleTmdbWrapper.Limiters
{
    public sealed class TimeSpanRateLimiter : IRateLimiter
    {
        private readonly Logger _log = LogManager.GetCurrentClassLogger();
        public static readonly IRateLimiter Default = new TimeSpanRateLimiter(40, TimeSpan.FromSeconds(10D));

        private readonly SemaphoreSlim _semaphore;

        private readonly ConcurrentQueue<DateTime> _startTimes;

        public int Requests { get; private set; }

        public TimeSpan Period { get; private set; }

        public TimeSpanRateLimiter(int requests, TimeSpan period)
        {
            Requests = requests;
            Period = period;
            _semaphore = new SemaphoreSlim(Requests, Requests);
            _startTimes = new ConcurrentQueue<DateTime>();
        }

        public void Limit(Task task)
        {
             _semaphore.Wait();

            var hasStarted = false;

            while (!hasStarted)
            {
                var startTime = DateTime.UtcNow;

                if (_startTimes.Count < Requests || (startTime - _startTimes.Min()) > Period)
                {
                    _startTimes.Enqueue(startTime);
                    hasStarted = true;

                    try
                    {
                        _log.Debug($"Starting task at {startTime}.  Current number of tasks: {_startTimes.Count}");
                        task.Start();
                        task.Wait();
                    }
                    catch(AggregateException ag)
                    {
                        _log.Error(ag.Flatten());
                    }
                    finally
                    {
                        _log.Debug($"Task finished at {DateTime.UtcNow}");
                    }

                    if (_startTimes.Count > Requests)
                    {
                        DateTime removedTime;
                        _startTimes.TryDequeue(out removedTime);
                    }

                    _semaphore.Release();
                }
                else
                {
                    var delay = Period - (startTime - _startTimes.Min());
                    _log.Info($"Delaying for {delay.ToString()}");
                    Task.Delay(delay);
                }
            }
        }
    }
}