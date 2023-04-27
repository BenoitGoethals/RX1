namespace RX1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Sensor sensor = new Sensor();
            sensor.SensorEvents += Sensor_SensorEvents;

            sensor.Start();

        }

        private static void Sensor_SensorEvents(object? sender, SensorEventArgs e)
        {
           Console.WriteLine(e.MessarmentTaken);
        }
    }
}