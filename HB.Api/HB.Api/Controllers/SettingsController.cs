using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HB.Api.Repositories;
using HB.Api.Services;
using HB.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace HB.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SettingsController : ControllerBase
    {
        
        private readonly ISettingsRespository _settingsRepository;

        public SettingsController(ISettingsRespository settingsRespository)
        {
            _settingsRepository = settingsRespository;
        }

        [HttpGet("{boardId}")]
        public IEnumerable<Setting> GetSettings(int boardId)
        {
            return new SettingsService(_settingsRepository).GetSettings(boardId);
        }
    }
}