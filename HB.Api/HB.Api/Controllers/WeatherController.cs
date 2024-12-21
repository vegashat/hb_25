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
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherRespository _weatherRepository;

        public WeatherController(IWeatherRespository weatherRespository){
            _weatherRepository = weatherRespository;
        }

        [HttpGet("{boardId}")]
        public async Task<CurrentWeather> GetWeather(int boardId){
            try {
                return await new WeatherService(_weatherRepository).GetWeather(boardId);
            }catch(Exception ex){
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }
    }
}