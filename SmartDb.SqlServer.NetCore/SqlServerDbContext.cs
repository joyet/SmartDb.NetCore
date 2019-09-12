using SmartDb;
using SmartDb.NetCore;
using System;
using System.Data;
using System.Data.SqlClient;

namespace SmartDb.SqlServer.NetCore
{
    public class SqlServerDbContext : SqlDbContext
    {
       
        public SqlServerDbContext(string connectionString="")
        {
            var dbFactory = new SqlServerFactory();
            DbHelper.ConnectionString = connectionString;
            DbHelper.DbFactory = dbFactory;
            DbBuilder = new SqlServerBuilder();
            DbBuilder.DbFactory = dbFactory;
        }

    }
}
