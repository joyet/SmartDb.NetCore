using SmartDb.MySql.NetCore;
using SmartDb.NetCore;
//using SmartDb.PostgreSql.NetCore;
//using SmartDb.SQLite.NetCore;
//using SmartDb.SqlServer.NetCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestSmartDbRepository
{
    class Program
    {
        static void Main(string[] args)
        {
            TestMySql2();
            //TestSqlite();
            Console.ReadLine();
        }

        public static void TestMySql()
        {
            var userInfoRepository = new UserInfoRepository();

            //删除所有数据
            userInfoRepository.Delete("", null);

            //插入数据
            for (int i = 1; i <= 5; i++)
            {
              userInfoRepository.Insert(new UserInfo() { UserId = i, UserName = "joyet" + i.ToString(), Age = 110, Email = "joyet" + i.ToString() + "@qq.com" });
            }

            //删除数据
            userInfoRepository.Delete(1);

            //删除数据
            userInfoRepository.Delete("",new { UserId=2 });

            //根据主键查询数据
            var item = userInfoRepository.Query(3);

            //根据实体主键修改数据
            item.UserName = "joyet3XX";
            userInfoRepository.Update(item);

            //根据查询字段、过滤条件Sql、过滤条件参数查询数据
            var dataList = userInfoRepository.Query("UserId,UserName", "UserId=3", null);

            //分页查询列表
            var pageDataList = userInfoRepository.QueryPageList("UserId,UserName", "UserId", "asc", 10, 1, "UserId=3", null);  
        }

        public static async void TestMySql2()
        {
            var userInfoRepository = new UserInfoRepository();

            //删除所有数据
            await userInfoRepository.DeleteAsync("", null);

            //插入数据
            for (int i = 1; i <= 5; i++)
            {
                await userInfoRepository.InsertAsync(new UserInfo() { UserId = i, UserName = "joyet" + i.ToString(), Age = 110, Email = "joyet" + i.ToString() + "@qq.com" });
            }

            //删除数据
            await userInfoRepository.DeleteAsync(1);

            //删除数据
            await userInfoRepository.DeleteAsync("", new { UserId = 2 });

            //根据主键查询数据
            var item =await userInfoRepository.QueryAsync(3);

            //根据实体主键修改数据
            item.UserName = "joyet3XX";
            await userInfoRepository.UpdateAsync(item);

            //根据查询字段、过滤条件Sql、过滤条件参数查询数据
            var dataList = await userInfoRepository.QueryAsync("UserId,UserName", "UserId=3", null);

            //分页查询列表
            var pageDataList = await userInfoRepository.QueryPageListAsync("UserId,UserName", "UserId", "asc", 10, 1, "UserId=3", null);
        }
    }
}
