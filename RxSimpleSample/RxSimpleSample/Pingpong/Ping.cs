using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;

namespace RxSimpleSample.Pingpong
{
    public class Ping : ISubject<Pong, Ping>
    {
        #region Implementation of IObserver<Pong>

     
        public void OnNext(Pong value)
        {
            Console.WriteLine("Ping received Pong.");
        }

     
        public void OnError(Exception exception)
        {
            Console.WriteLine("Ping experienced an exception and had to quit playing.");
        }

      
        public void OnCompleted()
        {
            Console.WriteLine("Ping finished.");
        }

        #endregion

        #region Implementation of IObservable<Ping>

      
        public IDisposable Subscribe(IObserver<Ping> observer)
        {
            return Observable.Interval(TimeSpan.FromSeconds(2))
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
