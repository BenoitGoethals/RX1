using System.Reactive.Linq;

namespace RX1
{
    internal class Program
    {
        private static Sensor sensor = new Sensor();
        static void Main(string[] args)
        {
            var data = DataMessarment;
      
           
            data.Subscribe(x => Console.WriteLine("xs: " + x.Temp));
            sensor.Start();


        }

        public static IObservable<Messarement> DataMessarment
        {
            get
            {
                return Observable
                    .FromEventPattern<SensorEventArgs>(
                        h => sensor.SensorEvents += h,
                        h => sensor.SensorEvents -= h)
                    .Select(x => x.EventArgs.MessarmentTaken);
            }
        }

       
    }
}