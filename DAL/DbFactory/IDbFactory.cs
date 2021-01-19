using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;


namespace PHCoreWebAPI.DAL.DbFactory
{
 public interface IDbFactory
    {

        ValueTask<SqlConnection> OpenConnectionAsync(string connStr);
        ValueTask<SqlConnection> OpenConnectionAsync();
        SqlConnection CreateConnection();
    }
}
