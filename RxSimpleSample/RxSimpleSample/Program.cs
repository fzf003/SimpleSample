using RxSimpleSample.ErrorRetry;
using RxSimpleSample.ObservableColl;
using RxSimpleSample.Pingpong;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;

namespace RxSimpleSample
{
    class Program
    {
        static void Main(string[] args)
        {
            //PingPongSetUp.Start();

            //WatchForNewCustomers.Start();
            LongRunningOperationAsync(Guid.NewGuid().ToString()).Subscribe((p) => {
                Console.WriteLine(p);
            });

            Console.ReadKey();
            
        }

        public static IObservable<DateTime> LongRunningOperationAsync(string param)
        {
            return Observable.Create<DateTime>(
                o => Observable.ToAsync<string, DateTime>(DoLongRunningOperation)(param).Subscribe(o)
            );

        }

        public static DateTime DoLongRunningOperation(string param)
        {
            return DateTime.Now; 
        }
    }
}
