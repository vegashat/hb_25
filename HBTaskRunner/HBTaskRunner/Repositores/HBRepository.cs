using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using HBTaskRunner.Models;
using HBTaskRunner.Util;
using Microsoft.Data.SqlClient;

namespace HBTaskRunner.Repositores
{
    public class HBRepository
    {
        private Settings _settings;

        public HBRepository()
        {
            _settings = new Config().Settings;
            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public IEnumerable<PhotoAlbum> GetActivePhotoAlbums()
        {
            string sql = @"select p.board_id, p.token 
                             from photo_album p
                       inner join board b on p.board_id = b.board_id
                            where b.is_active = 1;
                          ";

            using var connection = new SqlConnection(_settings.ConnectionString);

            return connection.Query<PhotoAlbum>(sql);
        }

        public IEnumerable<PhotoAlbum> GetCurrentPhotos(int boardId)
        {
            string sql = @"select board_id, guid, has_been_displayed
                             from photo
                            where board_id =  @boardId;
                          ";

            using var connection = new SqlConnection(_settings.ConnectionString);

            return connection.Query<PhotoAlbum>(sql, param: new { boardId });
        }

        public void InsertPhoto(int boardId, string guid)
        {
            string sql = @"insert into photo (board_id, guid, has_been_displayed)
                           values (@boardId, @guid, 0)";

            using (var connection = new SqlConnection(_settings.ConnectionString))
            {
                connection.Execute(
                   sql: sql,
                   param: new
                   {
                       boardId,
                       guid,
                   });
            }
        }
    }
}