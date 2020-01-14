using SmartDb;
using SmartDb.NetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSmartDbConsole
{
    //[Table(TableName = "userinfo")]
    //[Table(TableName="userinfo",IsGetAutoIncrementValue =true)]
    public class UserInfo
    {
        [TableColumn(IsPrimaryKey = true)]
        //[TableColumn(IsPrimaryKey = true,IsAutoIncrement =true)]
        public int UserId { get; set; }

        public string UserName { get; set; }

        //[TableColumn(IsSetDefaultValue=true, DefaultValue= 50)]
        public int Age { get; set; }

        public string Email { get; set; }
    }
}
