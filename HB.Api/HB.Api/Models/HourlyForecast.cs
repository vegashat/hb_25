using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HB.Api.Models
{
    using System;

    public class HourlyForecast
    {
        public int hour {get; set;}
        public decimal temp { get; set; }
        public decimal feelsLike { get; set; }
        public int  precip_percent { get; set; }
        public string weather_code {get; set;}
    }


}