using System.Reactive.Linq;

namespace RX1
{
    internal class Program
    {
        private static readonly Sensor sensor = new();
        static void Main(string[] args)
        {
            var t = DataMeasurement;
            
            
            DataMeasurement.Buffer<Measurement>(TimeSpan.FromSeconds(10)).Subscribe(x =>
            {
                foreach (var measurement in x)
                {
                    Console.WriteLine("xs: " + measurement.Temp);
                }

               
            });
            DataMeasurementWindSpeed.Subscribe(x => Console.WriteLine("windspeed : " + x.WindSpeed));
            sensor.Start();
        }

        public static IObservable<Measurement> DataMeasurement
        {
            get
            {
                return Observable
                    .FromEventPattern<SensorEventArgs>(
                        h => sensor.SensorEvents += h,
                        h => sensor.SensorEvents -= h)
                    .Where(x=>x.EventArgs.MeasurementTaken.Temp>40)

                    .Select(x => x.EventArgs.MeasurementTaken);
            }
        }


        public static IObservable<Measurement> DataMeasurementWindSpeed
        {
            get
            {
                return Observable
                    .FromEventPattern<SensorEventArgs>(
                        h => sensor.SensorEvents += h,
                        h => sensor.SensorEvents -= h)
                    .Where(x => x.EventArgs.MeasurementTaken.WindSpeed > 5)

                    .Select(x => x.EventArgs.MeasurementTaken);
            }
        }


    }
}