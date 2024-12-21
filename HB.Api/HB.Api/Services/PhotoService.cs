using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HB.Api.Repositories;

namespace HB.Api.Services
{
    public class PhotoService
    {
        IPhotoRespository _photoRepository;

        public PhotoService(IPhotoRespository respository) => _photoRepository = respository;

        public string GetPhotoGuid(int boardId){
            return _photoRepository.GetPhotoGuid(boardId);
        }
    }
}