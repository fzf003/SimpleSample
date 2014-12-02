using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RxSimpleSample.Pingpong
{
    public class PingPongSetUp
    {

        static CancellationTokenSource cts = new CancellationTokenSource();

        public static void Start()
        {
            Task.Factory.StartNew((token) => { 
            
                 
                    var ping = new Ping();

                    var pong = new Pong();

                    ping.Subscribe(pong);

                    pong.Subscribe(ping);
                

            },cts.Token,TaskCreationOptions.LongRunning);
           

        }

        public static void Stop()
        {
            cts.Cancel();
            cts.Dispose();
        }
    }
}
