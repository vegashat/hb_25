using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using HB.Api.Models;

namespace HB.Api.Repositories
{
    public class CalendarRepository : ICalendarRespository 
    {
        private readonly HBContext _weatherContext;

        public CalendarRepository(HBContext weatherContext)
        {
            _weatherContext = weatherContext;
        }
        public IEnumerable<Calendar> GetCalendars(int boardId)
        {

            using (var connection = _weatherContext.CreateConnection())
            {
                var parms = new DynamicParameters();
                parms.Add("@board_id", boardId);
                var calendars = connection.Query<Calendar>("dbo.usp_calendar_select", parms, commandType: CommandType.StoredProcedure);

                return calendars;
            }
        }
        
    }
}