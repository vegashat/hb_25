using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HB.Api.Models
{
    public class Nest
    {
        public int AmbientTemperature { get; set; }
        public int Humidity { get; set; }
        public int TargetTemperature { get; set; }
        public int TargetTemperatureHigh { get; set; }
        public int TargetTemperatureLow { get; set; }
        public string HVACMode { get; set; }
        public string HVACState { get; set; }

        public bool IsAway {get; set;}
    }
}