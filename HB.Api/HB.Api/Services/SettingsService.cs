using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HB.Api.Models;
using HB.Api.Repositories;

namespace HB.Api.Services
{
    public class SettingsService
    {
        ISettingsRespository _settingsRepository;

        public SettingsService(ISettingsRespository respository) => _settingsRepository = respository;

        public IEnumerable<Setting> GetSettings(int boardId){
            return _settingsRepository.GetSettings(boardId);
        }
    }
}