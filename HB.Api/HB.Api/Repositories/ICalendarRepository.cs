using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HB.Api.Models;

namespace HB.Api.Repositories
{
    public interface ICalendarRespository
    {
        public IEnumerable<Calendar> GetCalendars(int boardId);
    }
}