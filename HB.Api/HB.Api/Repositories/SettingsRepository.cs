using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using HB.Api.Models;

namespace HB.Api.Repositories
{
    public class SettingsRepository : ISettingsRespository 
    {
        private readonly HBContext _settingsContext;

        public SettingsRepository(HBContext settingsContext)
        {
            _settingsContext = settingsContext;
        }
        public IEnumerable<Setting> GetSettings(int boardId)
        {

            using (var connection = _settingsContext.CreateConnection())
            {
                var parms = new DynamicParameters();
                parms.Add("@board_id", boardId);
                var settings = connection.Query<Setting>("dbo.usp_settings_select", parms, commandType: CommandType.StoredProcedure);

                return settings;
            }
        }

        public void UpdateSetting(Setting setting)
        {

            using (var connection = _settingsContext.CreateConnection())
            {
                var parms = new DynamicParameters();
                parms.Add("@board_id", setting.BoardId);
                parms.Add("@key", setting.Key);
                parms.Add("@value", setting.Value);

                connection.Execute("dbo.usp_settings_upsert", parms, commandType: CommandType.StoredProcedure);
            }
        }
        
    }
}