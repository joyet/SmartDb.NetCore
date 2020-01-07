using SmartDb.NetCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSmartDbConsole
{
    public class DbTest:DbBase<UserInfo>
    {
        public DbTest(SqlDbContext dbContext)
        {
            DbContext = dbContext;
        }
    }
}
