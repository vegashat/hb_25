using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HB.Api.Models;

namespace HB.Api.Repositories
{
    public interface IWeatherRespository
    {
        public Board GetBoard(int boardId);

        public CurrentWeather GetWeatherForecast(int zipCode);

        public void UpdateWeatherForecast(int zipCode, string weatherData);
    }
}