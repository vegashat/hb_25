using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HB.Api.Models
{
    public class BoardCalendar
    {
        public BoardCalendar()
        {
        }
        public DateTime Date
        {
            get;
            set;
        }

        public List<CalendarItem> Entries
        {
            get;
            set;
        }
    }
}