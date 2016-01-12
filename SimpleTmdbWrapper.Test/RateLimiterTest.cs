using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleTmdbWrapper.Limiters;

namespace SimpleTmdbWrapper.Test
{
    [TestClass]
    public class RateLimiterTest
    {
        [TestMethod]
        public void RateLimitWithColdTask()
        {
            var limiter = new TimeSpanRateLimiter(10, TimeSpan.FromSeconds(2D));
            Duplicate(() => limiter.Limit(new Task(TaskToRun)), 30);
        }

        private void Duplicate(Action action, int times)
        {
            for (int i = 0; i < times; i++)
            {
                action();
            }
        }

        private void TaskToRun()
        {
            Console.WriteLine(DateTime.UtcNow.ToString());
        }
    }
}
