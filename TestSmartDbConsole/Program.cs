using SmartDb.MySql.NetCore;
using SmartDb.NetCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestSmartDbConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            TestMySql();
            //TestSqlite();
            Console.ReadLine();
        }

        public static void TestMySql()
        {
            string connectString = "server=localhost;User Id=root;password=123456;Database=testdb;SslMode=None;";
            SqlDbContext db = new MySqlDbContext(connectString);

            //数据执行回调函数
            db.AopAction = (dbAopEntity) => {
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

            var dbTest = new DbTest(db);
            dbTest.DeleteAll();
            dbTest.Insert();
            dbTest.Delete();
            dbTest.Update();
            dbTest.Query();
            dbTest.OrtherQuery();
            dbTest.OrtherNoneQuery();
        }

        //public static void TestSqlServer()
        //{
        //    string connectString = "server=localhost;user id=sa;password=123456;database=testdb;pooling=true;min pool size=5;max pool size=512;connect timeout = 60;";
        //    SqlDbContext db = new SqlServerDbContext(connectString);

        //    //数据执行回调函数
        //    db.ExecuteDbCallBack = (cmdText, dbParms) => {
        //        StringBuilder stringBuilder = new StringBuilder();
        //        stringBuilder.Append("sql:" + cmdText);
        //        if (dbParms != null)
        //        {
        //            foreach (IDbDataParameter param in dbParms)
        //            {
        //                stringBuilder.Append("paramName:" + param.ParameterName + ",paramValue:" + param.Value.ToString());
        //            }
        //        }
        //        stringBuilder.Append("\n\n");
        //        Console.Write(stringBuilder.ToString());
        //    };

        //    var dbTest = new DbTest(db);
        //    dbTest.DeleteAll();
        //    dbTest.Insert();
        //    dbTest.Delete();
        //    dbTest.Update();
        //    dbTest.Query();
        //    dbTest.OrtherQuery();
        //    dbTest.OrtherNoneQuery();
        //}

        //public static void TestSqlite()
        //{
        //     string dbPath = "f:\\testdb.sqlite";
        //     string connectString = "Data Source=f:\\testdb.sqlite;Pooling=true;FailIfMissing=false;";
        //    if (File.Exists(dbPath))
        //    {
        //        File.Delete(dbPath);
        //    }
        //    SqlDbContext db = new SQLiteDbContext(connectString);

        //    //数据执行回调函数
        //    db.ExecuteDbCallBack = (cmdText, dbParms) => {
        //        StringBuilder stringBuilder = new StringBuilder();
        //        stringBuilder.Append("sql:" + cmdText);
        //        if (dbParms != null)
        //        {
        //            foreach (IDbDataParameter param in dbParms)
        //            {
        //                stringBuilder.Append("paramName:" + param.ParameterName + ",paramValue:" + param.Value.ToString());
        //            }
        //        }
        //        stringBuilder.Append("\n\n");
        //        Console.Write(stringBuilder.ToString());
        //    };

        //    //var sql = "create  table UserInfo(UserId int identity(1,1) primary key,UserName  varchar(50),Age int,Email varchar(50))";
        //    var sql = "create  table UserInfo(UserId int  primary key,UserName  varchar(50),Age int,Email varchar(50))";
        //    db.ExecuteNoneQuery(sql, null);

        //    var dbTest = new DbTest(db);
        //    dbTest.DeleteAll();
        //    dbTest.Insert();
        //    dbTest.Delete();
        //    dbTest.Update();
        //    dbTest.Query();
        //    dbTest.OrtherQuery();
        //    dbTest.OrtherNoneQuery();
        //}

        //public static void TestPostgreSql()
        //{
        //    string connectString = "server=192.168.58.131;port=5432;user id=xiaozhang1;password=123456;database=testdb;";
        //    SqlDbContext db = new PostgreSqlDbContext(connectString);

        //    //数据执行回调函数
        //    db.ExecuteDbCallBack = (cmdText, dbParms) => {
        //        StringBuilder stringBuilder = new StringBuilder();
        //        stringBuilder.Append("sql:" + cmdText);
        //        if (dbParms != null)
        //        {
        //            foreach (IDbDataParameter param in dbParms)
        //            {
        //                stringBuilder.Append("paramName:" + param.ParameterName + ",paramValue:" + param.Value.ToString());
        //            }
        //        }
        //        stringBuilder.Append("\n\n");
        //        Console.Write(stringBuilder.ToString());
        //    };

        //    var dbTest = new DbTest(db);
        //    dbTest.DeleteAll();
        //    dbTest.Insert();
        //    dbTest.Delete();
        //    dbTest.Update();
        //    dbTest.Query();
        //    dbTest.OrtherQuery();
        //    dbTest.OrtherNoneQuery();
        //}
     }
}
