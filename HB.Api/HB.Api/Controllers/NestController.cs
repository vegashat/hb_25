using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using HB.Api.Repositories;
using HB.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HB.Api.Controllers
{
    [Route("api/[controller]")]
    public class NestController : Controller
    {

        ISettingsRespository _settingsRepository;
        public NestController(ISettingsRespository settingsRespository)
        {
            _settingsRepository = settingsRespository;
        }


        [HttpGet("{boardId}")]
        public object GetNest(int boardId)
        {
            return new NestService(_settingsRepository).GetNest(boardId);
        }
    }
}