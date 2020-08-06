using SmartDb.MySql.NetCore;
using SmartDb.NetCore;
using SmartDb.Repository.NetCore;
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
            DbContext = new MySqlDbContext(connectString);

            DbContext.AopAction = (dbAopEntity) => {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendLine("sql:" + dbAopEntity.CommandText);
                if (dbAopEntity.DbParams != null)
                {
                    foreach (IDbDataParameter param in dbAopEntity.DbParams)
                    {
                        stringBuilder.AppendLine("paramName:" + param.ParameterName + ",paramValue:" + param.Value.ToString());
                    }
                }
                stringBuilder.AppendLine("执行时间:" + dbAopEntity.Elapsed.ToString());
                stringBuilder.Append("\n");
                Console.Write(stringBuilder.ToString());
            };
        }

    }
}
