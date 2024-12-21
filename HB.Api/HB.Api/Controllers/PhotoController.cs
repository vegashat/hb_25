using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HB.Api.Repositories;
using HB.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace HB.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PhotoController : ControllerBase
    {
        
        private readonly IPhotoRespository _photoRepository;

        public PhotoController(IPhotoRespository photoRespository)
        {
            _photoRepository = photoRespository;
        }

        [HttpGet("{boardId}")]
        public object GetPhotoGuid(int boardId)
        {
            var guid = new PhotoService(_photoRepository).GetPhotoGuid(boardId);

            return (new {guid = guid});
        }
    }
}