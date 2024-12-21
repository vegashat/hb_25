using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace HBTaskRunner.Util
{
    public class Config
    {

        private  Settings? _settings;
        static IConfigurationRoot? config;

        public Config() {
           config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();

            _settings = config.GetRequiredSection("Settings").Get<Settings>();
        }

        public Settings Settings {
            get {
                return _settings;
            }
        }
    }
}