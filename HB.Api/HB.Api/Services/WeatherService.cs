using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Schema;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using HB.Api.Models;
using HB.Api.Repositories;

namespace HB.Api.Services
{
    public class WeatherService
    {
        private readonly IWeatherRespository _repository;
        private  HttpClient _client;
        private const string  WEATHER_URL_BASE = @"http://api.open-meteo.com/v1/forecast?";
        private const string WEATHER_OPTIONS = @"&current=temperature_2m,relative_humidity_2m,apparent_temperature,precipitation,rain,showers,snowfall,weather_code,cloud_cover,wind_speed_10m,wind_direction_10m,wind_gusts_10m&hourly=temperature_2m,relative_humidity_2m,apparent_temperature,precipitation_probability,rain,showers,snowfall,weather_code,cloud_cover,wind_speed_10m,wind_direction_10m&daily=weather_code,temperature_2m_max,temperature_2m_min,apparent_temperature_max,apparent_temperature_min,sunrise,sunset,precipitation_probability_max,wind_speed_10m_max,wind_gusts_10m_max&temperature_unit=fahrenheit&wind_speed_unit=mph&precipitation_unit=inch&timezone=America%2FChicago";
        
        public WeatherService(IWeatherRespository weatherRespository){
            this._repository = weatherRespository;

            _client = new HttpClient();
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/geo+json")
            );
            _client.DefaultRequestHeaders.Add("User-Agent", ".net application development");
        }
        public async Task<CurrentWeather> GetWeather(int boardId){

            var board = _repository.GetBoard(boardId);

            var weather = _repository.GetWeatherForecast(board.Zip);

            if(weather == null){
                weather = await LoadForecast(board);
            }

            return weather;
        }


        private async Task<CurrentWeather> LoadForecast(Board board){

            var weather = new CurrentWeather();
            var url = $"{WEATHER_URL_BASE}latitude={board.Latitude}&longitude={board.Longitude}{WEATHER_OPTIONS}";
            using(var response = await _client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead))
            {
                if(response.IsSuccessStatusCode){
                    var stream = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<OpenMeteoForecast>(stream);
                    weather = ConvertForecast(data);
                }
            }

            return weather;
        }

        private CurrentWeather ConvertForecast(OpenMeteoForecast forecast){

            CurrentWeather weather = new CurrentWeather();

            weather.date = DateTime.Parse(forecast.current.time);
            weather.currentTemp = forecast.current.temperature_2m;
            weather.feelsLike = forecast.current.apparent_temperature;
            weather.high = forecast.daily.temperature_2m_max[0];
            weather.low = forecast.daily.temperature_2m_min[0];
            weather.humidity = forecast.current.relative_humidity_2m;
            weather.precipPercent = forecast.daily.precipitation_probability_max[0];
            weather.windDirection = GetWindDirection(forecast.current.wind_direction_10m);
            weather.windSpeed = forecast.current.wind_speed_10m;
            weather.windGusts = forecast.current.wind_gusts_10m;
            weather.weatherCode = forecast.current.weather_code.ToString();

            var daily = forecast.daily;
            weather.forecasts = new List<Forecast>();

            for(var i = 0; i < forecast.daily.time.Count; i++)
            {
                var dailyForecast = new Forecast()
                {
                     date = DateTime.Parse(daily.time[i])
                    ,high = daily.temperature_2m_max[i]
                    ,low = daily.temperature_2m_min[i]
                    ,precipPercent = daily.precipitation_probability_max[i]
                    ,weatherCode = daily.weather_code[i].ToString()
                };
                if(i == 0){
                    weather.sunrise = DateTime.Parse(daily.sunrise[0]);
                    weather.sunset = DateTime.Parse(daily.sunset[0]);
                }

                weather.forecasts.Add(dailyForecast);
            }

            var hourly = forecast.hourly;
            var tomorrow = DateTime.Today.AddDays(1);
            weather.hourly = new List<HourlyForecast>();

            for(var i = 0; i <= 24; i++)
            {
                var hourlyForecast = new HourlyForecast()
                {
                     hour = DateTime.Parse(hourly.time[i]).Hour
                    ,temp = hourly.temperature_2m[i]
                    ,feelsLike = hourly.apparent_temperature[i]
                    ,precip_percent = hourly.precipitation_probability[i]
                    ,weather_code = hourly.weather_code[i].ToString()
                      
                };

                weather.hourly.Add(hourlyForecast);
            }

            return weather;
        }

        private string GetWindDirection(int degree){
            switch(degree)
            {
                case int n when (n >= 348):
                    return "N";
                case int n when (n >= 1 && n <= 11):
                    return "N";
                case int n when (n > 11 && n <= 33):
                    return "NNE";
                case int n when (n > 33 && n <= 56):
                    return "NE";
                case int n when (n > 56 && n <= 78):
                    return "ENE";
                case int n when (n > 78 && n <= 101):
                    return "E";
                case int n when (n > 101 && n <= 123):
                    return "ESE";
                case int n when (n > 123 && n <= 146):
                    return "SE";
                case int n when (n > 146 && n <= 168):
                    return "SSE";
                case int n when (n > 168 && n <= 191):
                    return "S";
                case int n when (n > 191 && n <= 213):
                    return "SSW";
                case int n when (n > 213 && n <= 236):
                    return "SW";
                case int n when (n > 236 && n <= 258):
                    return "WSW";
                case int n when (n > 258 && n <= 281):
                    return "W";
                case int n when (n > 281 && n <= 303):
                    return "WNW";
                case int n when (n > 303 && n <= 326):
                    return "NW";
                case int n when (n > 326 && n <= 348):
                    return "NNW";
                default:
                    return "";
            }
        }
    }
}