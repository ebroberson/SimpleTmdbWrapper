﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleTmdbWrapper.Limiters
{
    public sealed class PerSecondRateLimiter : IRateLimiter
    {
        public static readonly IRateLimiter Default = new PerSecondRateLimiter(30, TimeSpan.FromSeconds(10D));
        
        private readonly SemaphoreSlim _semaphore;

        public int Requests
        {
            get;
            private set;
        }

        public TimeSpan Period
        {
            get;
            private set;
        }

        public int RequestsPerSecond
        {
            get
            {
                return Requests / (int)Period.TotalSeconds;
            }
        }

        private DateTime _currentBatchStart = DateTime.MinValue;
        public DateTime CurrentBatchStart
        {
            get
            {
                return _currentBatchStart;
            }
            private set
            {
                _currentBatchStart = value;
                NextBatchAvailable = _currentBatchStart.Add(Period);
            }
        }

        public DateTime NextBatchAvailable
        {
            get;
            private set;
        }

        public PerSecondRateLimiter(int requests, TimeSpan period)
        {
            Requests = requests;
            Period = period;
            _semaphore = new SemaphoreSlim(1, Requests);
        }

        public async Task LimitAsync(Task task)
        {
            var allowedToWork = await _semaphore.WaitAsync((int)Period.TotalMilliseconds);

            if (allowedToWork)
            {
                CurrentBatchStart = DateTime.UtcNow;
                await task;
            }
        }
    }
}
