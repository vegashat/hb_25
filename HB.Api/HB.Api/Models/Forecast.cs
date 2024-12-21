using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HB.Api.Models
{
    using System;

    public class Forecast
    {
        public DateTime date { get; set; }
        public decimal high { get; set; }
        public decimal low { get; set; }
        public int  precipPercent { get; set; }
        public string weatherCode {get; set;}
    }


}