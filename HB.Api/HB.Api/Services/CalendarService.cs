using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Ical.Net.CalendarComponents;
using HB.Api.Models;
using HB.Api.Repositories;

namespace HB.Api.Services
{
        public class CalendarService
    {
        private ICalendarRespository _calendarRepository;
        static HttpClient client = new HttpClient();

        public CalendarService(ICalendarRespository _repository){
            this._calendarRepository = _repository;
        }

        public async Task<IList<BoardCalendar>> GetCalendars(int boardId)
        {
            var calendars = new List<BoardCalendar>();
            int days = 14;

            var calendarEntries = _calendarRepository.GetCalendars(boardId);

            foreach(var calendar in calendarEntries)
            {
                DownloadCalendar(calendars, calendar.Link, days);
            }

            return FilterCalendar(calendars, days);
        }

        private IList<BoardCalendar> DownloadCalendar(List<BoardCalendar> calendars, string link, int days)
        {
            var uri = new Uri(link);
            var response = client.GetAsync(uri).Result;
            string result = response.Content.ReadAsStringAsync().Result;

            var regex = new Regex(@"X-APPLE-STRUCTURED-LOCATION[\s\S]*?(?=UID:|DTSTAMP:|ORGANIZER;|DTSTART:|DTEND:|SUMMARY:|GEO:|END:)");
            var contentWithoutApple = regex.Replace(result, "");

            Ical.Net.Calendar calendar = null;
            try
            {
                calendar = Ical.Net.Calendar.Load(contentWithoutApple);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            TimeZoneInfo cstZone = null;
            try
            {
                cstZone = TimeZoneInfo.FindSystemTimeZoneById("America/Chicago");
            }
            catch //on a windows machine
            {
                cstZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
            }
            var today = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, cstZone);

            var startDate = today;
            var endDate = today.AddDays(14);

            var events = from e in calendar.GetOccurrences(startDate, endDate.AddDays(1))
                         select new CalendarItem(e.Period.StartTime.Value, e.Period.EndTime.Value, ((RecurringComponent)e.Source).Summary, ((CalendarEvent)e.Source).IsAllDay);

            var allEvents = events.ToList().OrderBy(e => e.StartTime);

            while (startDate.Date <= endDate.Date)
            {
                if (calendars.Any(c => c.Date == startDate.Date))
                {
                    calendars.First(c => c.Date == startDate.Date).Entries.AddRange(allEvents.Where(e => e.StartTime.Date == startDate.Date).OrderBy(e => e.StartTime).ToList());
                }
                else
                {
                    var day = new BoardCalendar()
                    {
                        Date = startDate.Date,
                        Entries = allEvents.Where(e => e.StartTime.Date == startDate.Date).OrderBy(e => e.StartTime).ToList()
                    };

                    calendars.Add(day);
                }

                startDate = startDate.AddDays(1);
            }

            return calendars;
        }
        
        private IList<BoardCalendar> FilterCalendar(IList<BoardCalendar> calendars, int days)
        {
            var calendarDaysWithEntries = new List<BoardCalendar>();
            int entriesAdded = 0;
            DateTime today = DateTime.Today;

            foreach(var calendar in calendars.OrderBy(c => c.Date))
            {
                if(calendar.Entries.Any())
                {
                    calendarDaysWithEntries.Add(calendar);
                    entriesAdded++;
                }

                if(entriesAdded == days)
                {
                    break;
                }
            }

            return calendarDaysWithEntries;
        }
    }
}