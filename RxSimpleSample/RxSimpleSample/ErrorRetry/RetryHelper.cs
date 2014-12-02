using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace RxSimpleSample.ErrorRetry
{
    public static class RetryHelper
    {
        public static void Do(Action action, TimeSpan retryInterval, int retryCount = 3)
        {
            Do<object>(() =>
            {
                action();
                return null;
            }, retryInterval, retryCount);
        }

        public static void Do(Action action, int retryCount = 3)
        {
            Do<object>(() =>
            {
                action();
                return null;
            }, TimeSpan.Zero, retryCount);
        }

        public static T Do<T>(Func<T> func, int retryCount = 3)
        {
            return Do<T>(() =>
            {
                return func();

            }, TimeSpan.Zero, retryCount);
        }

        public static T Do<T>(Func<T> action, TimeSpan retryInterval, int retryCount = 3)
        {
            var exceptions = new List<Exception>();

            for (int retry = 0; retry < retryCount; retry++)
            {
                try
                {
                    return action();
                }
                catch (Exception ex)
                {
                    exceptions.Add(ex);
                    Thread.Sleep(retryInterval);
                }
            }

            throw new AggregateException(exceptions);
        }
    }
}
