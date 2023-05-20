using Newtonsoft.Json;
using rx.core.openweather;


namespace rx.core.sensor.openweather;

public class OpenWeatherData : ISensorData
{
    private readonly OpenWeatherHelper.location _loc;

    public OpenWeatherData(OpenWeatherHelper.location loc)
    {
        _loc = loc;
    }

    public Measurement? GetData()
    {
        try
        {
            string openWeatherKey = GetApiKey();
            string byCity = GetUrl(_loc.CityName, _loc.StateAbbrev, _loc.CountryCode, openWeatherKey);
            OpenWeatherHelper.Root? weather;
            using (var client = new HttpClient())
            using (var response = client.GetAsync(byCity))
            using (var content = response.Result)
            {
                var cityData = content.Content.ReadAsStringAsync().Result;

                weather = JsonConvert.DeserializeObject<OpenWeatherHelper.Root>(cityData);
            }

            if (weather != null)
            {
                Measurement? measurement = new Measurement() { TimeCreated = DateTime.Now, Temp = (int)weather.list[0].main.temp, Humidity = weather.list[0].main.humidity, WindSpeed = (int)weather.list[0].wind.speed };
                return measurement;
            }
        }
        catch (Exception e)
        {

            return default;
        }

        return null;
    }

    // returns the OpenWeatherMap Api Key
    private static string GetApiKey()
    {
        string openWeatherKey = "03abf0d0b72e5c8b41d6b4dc9fcdd6c9";
        return openWeatherKey;
    }

    // returns the OpenWeatherMap Url
    private static string GetUrl(string city, string state, string country, string apiKey)
    {
        var openWeatherUrl = $"http://api.openweathermap.org/data/2.5/forecast?q={city},{state},{country}&appid={apiKey}&units=imperial";
        return openWeatherUrl;
    }


}