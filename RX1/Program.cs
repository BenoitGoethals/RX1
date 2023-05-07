using System.Reactive.Linq;
using rx.core;

namespace RX1
{
    internal class Program
    {
        private static readonly Sensor Sensor = new();
        private static readonly SensorObs SensorObs = new();

        static void Main(string[] args)
        {
          
           
            // Event based sensor
            DataMeasurement.Buffer<Measurement>(TimeSpan.FromSeconds(10)).Subscribe(x =>
            {
                foreach (var measurement in x)
                {
                    Console.WriteLine("Event Temp: " + measurement.Temp);
                }

               
            });
            DataMeasurementWindSpeed.Subscribe(x => Console.WriteLine("Event windspeed : " + x.WindSpeed));
       
            // With observable class sensor

            var obs = SensorObs.Where(x => x.WindSpeed > 5).Buffer<Measurement>(TimeSpan.FromSeconds(10)).Subscribe(x =>
            {
                foreach (var measurement in x)
                {
                    Console.WriteLine("obs temp: " + measurement.Temp);
                }

            });
            SensorObs.Start();
            Sensor.Start();

        }


        //Event pattern
        public static IObservable<Measurement> DataMeasurement
        {
            get
            {
                return Observable
                    .FromEventPattern<SensorEventArgs>(
                        h => Sensor.SensorEvents += h,
                        h => Sensor.SensorEvents -= h)
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
                        h => Sensor.SensorEvents += h,
                        h => Sensor.SensorEvents -= h)
                    .Where(x => x.EventArgs.MeasurementTaken.WindSpeed > 5)

                    .Select(x => x.EventArgs.MeasurementTaken);
            }
        }

        

    }
}