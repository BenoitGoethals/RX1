using Newtonsoft.Json;

namespace rx.core.openweather;

public class OpenWeatherHelper
{
    // returns a list of cities
    public static List<location> GetCities()
    {
        List<location> Cities = new List<location>();
        Cities.Add(new location() { CityName = "Marlboro", StateAbbrev = "MA", CountryCode = "US" });
        Cities.Add(new location() { CityName = "San Diego", StateAbbrev = "CA", CountryCode = "US" });
        Cities.Add(new location() { CityName = "Cheyenne", StateAbbrev = "WY", CountryCode = "US" });
        Cities.Add(new location() { CityName = "Anchorage", StateAbbrev = "AK", CountryCode = "US" });
        Cities.Add(new location() { CityName = "Austin", StateAbbrev = "TX", CountryCode = "US" });
        Cities.Add(new location() { CityName = "Orlando", StateAbbrev = "FL", CountryCode = "US" });
        Cities.Add(new location() { CityName = "Seattle", StateAbbrev = "WA", CountryCode = "US" });
        Cities.Add(new location() { CityName = "Cleveland", StateAbbrev = "OH", CountryCode = "US" });
        Cities.Add(new location() { CityName = "Portland", StateAbbrev = "ME", CountryCode = "US" });
        Cities.Add(new location() { CityName = "Honolulu", StateAbbrev = "HI", CountryCode = "US" });
        return Cities;
    }
    // day averages
    public class DayAverages
    {
        public DateTime Day { get; set; }
        public double AveTemp { get; set; }
        public bool Precipitation { get; set; }
    }

    // location
    public class location
    {
        public string CityName { get; set; }
        public string StateAbbrev { get; set; }
        public string CountryCode { get; set; }
        public OpenWeatherAPIObject WeatherInfo { get; set; }
    }

    //classes for the OpenWeatherMap Object
    #region weatherClasses
    public class OpenWeatherAPIObject
    {
        public string cod { get; set; }
        public string message { get; set; }
        public int cnt { get; set; }
        public List<List> list { get; set; }
        public City city { get; set; }
    }

 
    public class Coord
    {
        public float lat { get; set; }
        public float lon { get; set; }
    }

    public class City
    {
        public int id { get; set; }
        public string name { get; set; }
        public Coord coord { get; set; }
        public string country { get; set; }
        public int population { get; set; }
        public int timezone { get; set; }
        public int sunrise { get; set; }
        public int sunset { get; set; }
    }

    public class Clouds
    {
        public int all { get; set; }
    }


    public class List
    {
        public int dt { get; set; }
        public Main main { get; set; }
        public List<Weather> weather { get; set; }
        public Clouds clouds { get; set; }
        public Wind wind { get; set; }
        public int visibility { get; set; }
        public double pop { get; set; }
        public Sys sys { get; set; }
        public string dt_txt { get; set; }
        public Rain rain { get; set; }
    }

    public class Main
    {
        public double temp { get; set; }
        public double feels_like { get; set; }
        public double temp_min { get; set; }
        public double temp_max { get; set; }
        public int pressure { get; set; }
        public int sea_level { get; set; }
        public int grnd_level { get; set; }
        public int humidity { get; set; }
        public double temp_kf { get; set; }
    }

    public class Rain
    {
        [JsonProperty("3h")]
        public double _3h { get; set; }
    }

    public class Root
    {
        public string cod { get; set; }
        public int message { get; set; }
        public int cnt { get; set; }
        public List<List> list { get; set; }
        public City city { get; set; }
    }

    public class Sys
    {
        public string pod { get; set; }
    }

    public class Weather
    {
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }

    public class Wind
    {
        public double speed { get; set; }
        public int deg { get; set; }
        public double gust { get; set; }
    }



    #endregion
}
