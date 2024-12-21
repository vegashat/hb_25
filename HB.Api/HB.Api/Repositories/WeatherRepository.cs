using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Dapper;
using Microsoft.Extensions.Configuration;
using HB.Api.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text.Json;
using Newtonsoft.Json;

namespace HB.Api.Repositories
{
    public class WeatherRepository : IWeatherRespository
    {
        private readonly HBContext _weatherContext;

        public WeatherRepository(HBContext weatherContext)
        {
            _weatherContext = weatherContext;
        }
        public Board GetBoard(int boardId)
        {

            using (var connection = _weatherContext.CreateConnection())
            {
                var parms = new DynamicParameters();
                parms.Add("@board_id", boardId);
                Board board = connection.QuerySingle<Board>("dbo.usp_board_select", parms, commandType: CommandType.StoredProcedure);

                return board;
            }
        }
        public CurrentWeather GetWeatherForecast(int zipCode)
        {
            using (var connection = _weatherContext.CreateConnection())
            {
                var parms = new DynamicParameters();
                parms.Add("@zip_code", zipCode);
                var weatherJson = connection.Query<String>("dbo.usp_weather_select", parms, commandType: CommandType.StoredProcedure).FirstOrDefault();

                if (weatherJson != null && weatherJson.Length > 0)
                {
                    var forecast = JsonConvert.DeserializeObject<CurrentWeather>(weatherJson);
                    return forecast;
                }
                else
                {
                    return null;
                }
            }
        }

        public void UpdateWeatherForecast(int zipCode, string weatherData)
        {
            using (var connection = _weatherContext.CreateConnection())
            {
                var parms = new DynamicParameters();
                parms.Add("@zip_code", zipCode);
                parms.Add("@weather_data", weatherData);

                connection.Execute("dbo.usp_weather_upsert", parms, commandType: CommandType.StoredProcedure);
            }
        }
    }
}