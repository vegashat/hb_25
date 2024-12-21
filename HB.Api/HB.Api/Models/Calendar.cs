using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HB.Api.Models
{
    public class Calendar
    {
        public Calendar()
        {
        }

        public string Description { get; set; }
        public string Link { get; set; }
        public int Board_Id { get; set; }
    }
}