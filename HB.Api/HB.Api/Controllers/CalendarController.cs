using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HB.Api.Models;
using HB.Api.Repositories;
using HB.Api.Services;

namespace HB.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CalendarController : ControllerBase
    {
        private readonly ICalendarRespository _calendarRepository;

        public CalendarController(ICalendarRespository calendarRespository)
        {
            _calendarRepository = calendarRespository;
        }

        [HttpGet("{boardId}")]
        public async Task<IEnumerable<BoardCalendar>> GetCalendar(int boardId)
        {
            Console.WriteLine($"Getting Calendar for board {boardId}");
            return await new CalendarService(_calendarRepository).GetCalendars(boardId);
        }
    }
}