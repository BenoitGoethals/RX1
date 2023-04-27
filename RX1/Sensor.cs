using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RX1
{
    public class Sensor
    {
        private Random RandomInt=new Random();
        public event EventHandler<SensorEventArgs> SensorEvents;
        private   CancellationToken CancellationToken=new CancellationToken();
        private  readonly ConcurrentBag<Messarement> _messarments = new ConcurrentBag<Messarement>();


        private  Action Run()
        {
            while (!CancellationToken.IsCancellationRequested)
            {
                var mes = new Messarement()
                {
                    Id = Guid.NewGuid(),
                    Temp = RandomInt.Next(60),
                    TimeCreated = DateTime.Now
                };
                _messarments.Add(mes);
                SensorEvents.Invoke(this, new SensorEventArgs() { MessarmentTaken = mes });

             
            }
            return () => { };
        }
        
        public  void Start()
        {
         
            Task.Factory.StartNew(Run(), CancellationToken);
        }

    }

    public class SensorEventArgs : EventArgs
    {
        public Messarement MessarmentTaken { get; set; }
    }
}
