using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HB.Api.Models
{
    public class CalendarItem
    {
        private DateTime startTime;

        public CalendarItem(DateTime startTime, DateTime endTime, string description, bool isAllDay)
        {
            StartTime = startTime;
            EndTime = endTime;
            Description = description;
            IsAllDay = isAllDay;
        }
        public DateTime StartTime
        {
            get;
            set;
        }

        public DateTime EndTime
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public bool IsAllDay
        {
            get;
            set;
        }
    }
}