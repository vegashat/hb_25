using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HB.Api.Models
{
    public class CurrentWeather : Forecast
    {
        public decimal currentTemp { get; set; }
        public decimal feelsLike { get; set; }
        public decimal humidity { get; set; }
        public decimal windSpeed { get; set; }
        public decimal windGusts {get; set;}
        public string windDirection { get; set; }
        public DateTime sunrise { get; set; }
        public DateTime sunset { get; set; }
        public IList<Forecast> forecasts { get; set; }
        public IList<HourlyForecast> hourly { get; set; }
    }
}