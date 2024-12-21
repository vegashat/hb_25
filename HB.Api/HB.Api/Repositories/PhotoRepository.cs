using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using HB.Api.Models;

namespace HB.Api.Repositories
{
    public class PhotoRepository : IPhotoRespository 
    {
        private readonly HBContext _photoContext;

        public PhotoRepository(HBContext photoContext)
        {
            _photoContext = photoContext;
        }
        public string GetPhotoGuid(int boardId)
        {

            using (var connection = _photoContext.CreateConnection())
            {
                var parms = new DynamicParameters();
                parms.Add("@board_id", boardId);
                var guid = connection.QuerySingle<string>("dbo.usp_photo_select", parms, commandType: CommandType.StoredProcedure);

                return guid;
            }
        }
        
    }
}