using System.Reactive.Linq;

namespace RX1
{
    internal class Program
    {
        private static readonly Sensor sensor = new Sensor();
        static void Main(string[] args)
        {
           


            DataMeasurement.Subscribe(x => Console.WriteLine("xs: " + x.Temp));
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