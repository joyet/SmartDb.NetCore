using SmartDb.MySql.NetCore;
using SmartDb.NetCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSmartDbRepository
{
    public class UserInfoRepository: BaseRepository<UserInfo,int>, IUserInfoRepository
    {
        public UserInfoRepository()
        {
            string connectString = "server=localhost;User Id=root;password=123456;Database=testdb;SslMode=None;";
            SmartDbContext = new MySqlDbContext(connectString);

            SmartDbContext.ExecuteDbCallBack = (cmdText, dbParms) => {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendFormat("sql:{0}\n", cmdText);
                if (dbParms != null)
                {
                    foreach (IDbDataParameter param in dbParms)
                    {
                        stringBuilder.AppendFormat("paramName:{0},paramValue:{1}\n", param.ParameterName, param.Value.ToString());
                    }
                }
                stringBuilder.Append("\n");
                Console.Write(stringBuilder.ToString());
            };
        }

    }
}
