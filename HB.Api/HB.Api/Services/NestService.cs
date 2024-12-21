using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using HB.Api.Models;
using HB.Api.Repositories;
using Newtonsoft.Json;
using NodaTime.Extensions;

namespace HB.Api.Services
{
    public class NestService
    {

        ISettingsRespository _settingsRepository;
        IList<Setting> _settings;
        private const string  NEST_REFRESH_TOKEN_URL = @"https://www.googleapis.com/oauth2/v4/token?client_id={0}&client_secret={1}&refresh_token={2}&grant_type=refresh_token";
        private const string NEST_DEVICE_URL = @"https://smartdevicemanagement.googleapis.com/v1/enterprises/{0}/devices";
        private  HttpClient _client;
        public NestService(ISettingsRespository settingsRespository){
            _settingsRepository = settingsRespository;
        }
       public  Nest GetNest(int boardId) {
            _settings = _settingsRepository.GetSettings(boardId).ToList();
            var lastUpdated = DateTime.Parse(_settings.First(s => s.Key == "TokenLastUpdated").Value, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal).ToLocalDateTime();
            var oneHourAgo = DateTime.Now.AddHours(-1).ToLocalDateTime();

            if(lastUpdated < oneHourAgo)
            {
                var access_token = RefreshToken();
                _settings.First(s => s.Key == "NestAuthToken").Value = access_token.Result;
                _settingsRepository.UpdateSetting(_settings.First(s => s.Key == "NestAuthToken"));
                _settingsRepository.UpdateSetting(new Setting(){BoardId = boardId, Key="TokenLastUpdated", Value=DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss")});
            }

            var nestStructure = GetNestData().Result;

            var thermostat = nestStructure.devices.First(d => d.parentRelations[0].displayName == "Hallway");
/*
i        public double AmbientTemperature { get; set; }
        public double Humidity { get; set; }
        public double TargetTemperature { get; set; }
        public double TargetTemperatureHigh { get; set; }
        public double TargetTemperatureLow { get; set; }
        public string HVACMode { get; set; }
        public string HVACState { get; set; }

        public bool IsAway {get; set;}
*/
            var nest = new Nest()
            {
                 AmbientTemperature = (int) CelsiusToFahrenheit(thermostat.traits.sdmdevicestraitsTemperature.ambientTemperatureCelsius)
                ,Humidity = thermostat.traits.sdmdevicestraitsHumidity.ambientHumidityPercent
                ,HVACMode = thermostat.traits.sdmdevicestraitsThermostatMode.mode
                ,HVACState = thermostat.traits.sdmdevicestraitsThermostatHvac.status
                ,TargetTemperatureHigh = (int)CelsiusToFahrenheit(thermostat.traits.sdmdevicestraitsThermostatTemperatureSetpoint.coolCelsius.Value)
                ,TargetTemperatureLow = (int)CelsiusToFahrenheit(thermostat.traits.sdmdevicestraitsThermostatTemperatureSetpoint.heatCelsius.Value)
                ,IsAway = thermostat.traits.sdmdevicestraitsThermostatEco.mode == "MANUAL_ECO"
            } ;

            switch(nest.HVACMode)
            {
                case "COOL":
                    nest.TargetTemperature = nest.TargetTemperatureLow;
                    break;
                case "HEAT":
                    nest.TargetTemperature = nest.TargetTemperatureHigh; 
                    break;
                case "HEATCOOL":
                    if(nest.AmbientTemperature < nest.TargetTemperatureLow)
                    {
                        nest.TargetTemperature = nest.TargetTemperatureLow;
                    } else if (nest.TargetTemperature > nest.TargetTemperatureHigh)
                    {
                        nest.TargetTemperature = nest.TargetTemperatureHigh;
                    } else {
                        nest.TargetTemperature = nest.AmbientTemperature;
                    }
                    break;
            }
            return nest;
       } 

       private double CelsiusToFahrenheit(double celsius){
        return (celsius * 1.8) + 32;
       }

       public async Task<string> RefreshToken()
       {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
            );

            var authUrl = string.Format(NEST_REFRESH_TOKEN_URL
                , _settings.First(s => s.Key == "NestClientId").Value
                , _settings.First(s => s.Key == "NestClientSecret").Value
                , _settings.First(s => s.Key == "NestRefreshToken").Value
                );

            using(var response = await _client.PostAsync(authUrl, null))
            {
                if(response.IsSuccessStatusCode){
                    var stream = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<AuthResponse>(stream);

                    return data.access_token;

                }
            }

            throw new Exception("Refreshing Token Failed");
        }

        public async Task<NestStructure> GetNestData()
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
            );
            var authToken = _settings.First(s => s.Key == "NestAuthToken").Value;
            var projectId = _settings.First(s => s.Key == "NestProjectId").Value;
            _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {authToken}");

            var authUrl = string.Format(NEST_DEVICE_URL, projectId);

            using(var response = await _client.GetAsync(authUrl))
            {
                if(response.IsSuccessStatusCode){
                    var stream = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<NestStructure>(stream);

                    return data;
                }
            }

            throw new Exception("Retrieving Nest Data Failed");
        }
    }
}