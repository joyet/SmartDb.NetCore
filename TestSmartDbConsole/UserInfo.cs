using SmartDb;
using SmartDb.NetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSmartDbConsole
{
    [Table(TableName="userinfo")]
   public class UserInfo
    {
        [TableColumn(IsPrimaryKey = true)]
        public int UserId { get; set; }

        public string UserName { get; set; }

        public int Age { get; set; }

        public string Email { get; set; }
    }
}
