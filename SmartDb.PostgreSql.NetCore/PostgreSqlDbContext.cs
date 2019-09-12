using SmartDb;
using SmartDb.NetCore;
using System;
using System.Data;

namespace SmartDb.PostgreSql.NetCore
{
    public class PostgreSqlDbContext:SqlDbContext
    {
       
        public PostgreSqlDbContext(string connectionString="")
        {
            var dbFactory = new PostgreSqlFactory();
            DbHelper.ConnectionString = connectionString;
            DbHelper.DbFactory = dbFactory;
            DbBuilder = new PostgreSqlBuilder();
            DbBuilder.DbFactory = dbFactory;
        }

    }
}
