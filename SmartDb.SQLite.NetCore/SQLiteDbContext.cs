using SmartDb;
using SmartDb.NetCore;
using System;
using System.Data;

namespace SmartDb.SQLite.NetCore
{
    public class SQLiteDbContext:SqlDbContext
    {
       
        public SQLiteDbContext(string connectionString="")
        {
            var dbFactory = new SQLiteDbFactory();
            DbHelper.ConnectionString = connectionString;
            DbHelper.DbFactory = dbFactory;
            DbBuilder = new SQLiteBuilder();
            DbBuilder.DbFactory = dbFactory;
            DbBuilder.CurrentDbType = SmartDbTypes.SQLite;
        }

    }
}
