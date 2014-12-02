using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;

namespace RxSimpleSample.Pingpong
{
    public class Pong : ISubject<Ping, Pong>
    {
        #region Implementation of IObserver<Ping>

    
        public void OnNext(Ping value)
        {
            Console.WriteLine("Pong received Ping.");
        }

      
        public void OnError(Exception exception)
        {
            Console.WriteLine("Pong experienced an exception and had to quit playing.");
        }

       
        public void OnCompleted()
        {
            Console.WriteLine("Pong finished.");
        }

        #endregion

        #region Implementation of IObservable<Pong>

   
        public IDisposable Subscribe(IObserver<Pong> observer)
        {
            return Observable.Interval(TimeSpan.FromSeconds(1.5))
                .Where(n => n < 10)
                .Select(n => this)
                .Subscribe(observer);
        }

        #endregion

        #region Implementation of IDisposable

      
        public void Dispose()
        {
            OnCompleted();
        }

        #endregion
    }
}
