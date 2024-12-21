using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace HB.Api.Repositories
{
    public class HBContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public HBContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("Homeboard");
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
        }
        public IDbConnection CreateConnection()
            => new SqlConnection(_connectionString);

    }
}