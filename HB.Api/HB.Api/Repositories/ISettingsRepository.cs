using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HB.Api.Models;

namespace HB.Api.Repositories
{
    public interface ISettingsRespository
    {
        public IEnumerable<Setting> GetSettings(int boardId);

        public void UpdateSetting(Setting setting);
    }
}