using SmartDb.NetCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartDb.MySql.NetCore
{
   public class MySqlDbContext : SqlDbContext
    {

        public MySqlDbContext(string connectionString = "")
        {
            var dbFactory = new MySqlFactory();
            DbHelper.ConnectionString = connectionString;
            DbHelper.DbFactory = dbFactory;
            DbBuilder = new MySqlBuilder();
            DbBuilder.DbFactory = dbFactory;
            DbBuilder.CurrentDbType = SmartDbTypes.MySql;
        }

    }
}
