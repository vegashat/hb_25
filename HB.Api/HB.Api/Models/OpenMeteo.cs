using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HB.Api.Models
{
// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Current
    {
        public string time { get; set; }
        public int interval { get; set; }
        public decimal temperature_2m { get; set; }
        public int relative_humidity_2m { get; set; }
        public decimal apparent_temperature { get; set; }
        public decimal precipitation { get; set; }
        public decimal rain { get; set; }
        public decimal showers { get; set; }
        public decimal snowfall { get; set; }
        public int weather_code { get; set; }
        public int cloud_cover { get; set; }
        public decimal wind_speed_10m { get; set; }
        public int wind_direction_10m { get; set; }
        public decimal wind_gusts_10m { get; set; }
        public string sunrise {get; set;}
        public string sunset {get; set;}
    }

    public class CurrentUnits
    {
        public string time { get; set; }
        public string interval { get; set; }
        public string temperature_2m { get; set; }
        public string relative_humidity_2m { get; set; }
        public string apparent_temperature { get; set; }
        public string precipitation { get; set; }
        public string rain { get; set; }
        public string showers { get; set; }
        public string snowfall { get; set; }
        public string weather_code { get; set; }
        public string cloud_cover { get; set; }
        public string wind_speed_10m { get; set; }
        public string wind_direction_10m { get; set; }
        public string wind_gusts_10m { get; set; }
    }

    public class Daily
    {
        public List<string> time { get; set; }
        public List<int> weather_code { get; set; }
        public List<decimal> temperature_2m_max { get; set; }
        public List<decimal> temperature_2m_min { get; set; }
        public List<decimal> apparent_temperature_max { get; set; }
        public List<decimal> apparent_temperature_min { get; set; }
        public List<string> sunrise { get; set; }
        public List<string> sunset { get; set; }
        public List<int> precipitation_probability_max { get; set; }
        public List<decimal> wind_speed_10m_max { get; set; }
        public List<decimal> wind_gusts_10m_max { get; set; }
    }

    public class DailyUnits
    {
        public string time { get; set; }
        public string weather_code { get; set; }
        public string temperature_2m_max { get; set; }
        public string temperature_2m_min { get; set; }
        public string apparent_temperature_max { get; set; }
        public string apparent_temperature_min { get; set; }
        public string sunrise { get; set; }
        public string sunset { get; set; }
        public string precipitation_probability_max { get; set; }
        public string wind_speed_10m_max { get; set; }
        public string wind_gusts_10m_max { get; set; }
    }

    public class Hourly
    {
        public List<string> time { get; set; }
        public List<decimal> temperature_2m { get; set; }
        public List<int> relative_humidity_2m { get; set; }
        public List<decimal> apparent_temperature { get; set; }
        public List<int> precipitation_probability { get; set; }
        public List<decimal> rain { get; set; }
        public List<decimal> showers { get; set; }
        public List<decimal> snowfall { get; set; }
        public List<int> weather_code { get; set; }
        public List<int> cloud_cover { get; set; }
        public List<decimal> wind_speed_10m { get; set; }
        public List<int> wind_direction_10m { get; set; }
    }

    public class HourlyUnits
    {
        public string time { get; set; }
        public string temperature_2m { get; set; }
        public string relative_humidity_2m { get; set; }
        public string apparent_temperature { get; set; }
        public string precipitation_probability { get; set; }
        public string rain { get; set; }
        public string showers { get; set; }
        public string snowfall { get; set; }
        public string weather_code { get; set; }
        public string cloud_cover { get; set; }
        public string wind_speed_10m { get; set; }
        public string wind_direction_10m { get; set; }
    }

    public class OpenMeteoForecast
    {
        public decimal latitude { get; set; }
        public decimal longitude { get; set; }
        public decimal generationtime_ms { get; set; }
        public int utc_offset_seconds { get; set; }
        public string timezone { get; set; }
        public string timezone_abbreviation { get; set; }
        public decimal elevation { get; set; }
        public CurrentUnits current_units { get; set; }
        public Current current { get; set; }
        public HourlyUnits hourly_units { get; set; }
        public Hourly hourly { get; set; }
        public DailyUnits daily_units { get; set; }
        public Daily daily { get; set; }
    }


}