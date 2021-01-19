using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PHCoreWebAPI.DAL.DbFactory
{
    public class SqlDbFactory:IDbFactory
    {

        private readonly IConfiguration _configuration;

        public SqlDbFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private DbSetting _dbWriteSetting => new DbSetting
        {
            Connection = _configuration.GetValue<string>("ConnectionStrings:Mssql")
        };


        public SqlConnection CreateConnection()
        {
            return new SqlConnection(_dbWriteSetting.Connection);
        }

        public async ValueTask<SqlConnection> OpenConnectionAsync(string setting)
        {
            if (setting == null || setting == string.Empty)
                throw new InvalidOperationException();

            SqlConnection connection = new SqlConnection(setting);
            await connection.OpenAsync();
            return connection;
        }

        public async ValueTask<SqlConnection> OpenConnectionAsync()
        {
            SqlConnection connection = CreateConnection();
            await connection.OpenAsync();
            return connection;
        }
    }
}
