using SmartDb.MySql.NetCore;
using SmartDb.NetCore;
using SmartDb.PostgreSql.NetCore;
using SmartDb.SQLite.NetCore;
using SmartDb.SqlServer.NetCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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

            //SmartDb框架提供一个记录日志委托用来记录执行SQL及参数信息，ConsoleWriteInfo在控制台输出，大家可以根据自己需要自己定义方法传给DbHelper.logAction会自动进行调用
            db.DbHelper.logAction = db.DbHelper.ConsoleWriteInfo;

            var dbTest = new DbTest(db);
            dbTest.DeleteAll();
            dbTest.Insert();
            dbTest.Delete();
            dbTest.Update();
            dbTest.Query();
            dbTest.OrtherQuery();
            dbTest.OrtherNoneQuery();
        }

        public static void TestSqlServer()
        {
            string connectString = "server=localhost;user id=sa;password=123456;database=testdb;pooling=true;min pool size=5;max pool size=512;connect timeout = 60;";
            SqlDbContext db = new SqlServerDbContext(connectString);

            //SmartDb框架提供一个记录日志委托用来记录执行SQL及参数信息，ConsoleWriteInfo在控制台输出，大家可以根据自己需要自己定义方法传给DbHelper.logAction会自动进行调用
            db.DbHelper.logAction = db.DbHelper.ConsoleWriteInfo;

            var dbTest = new DbTest(db);
            //dbTest.DeleteAll();
            //dbTest.Insert();
            dbTest.Delete();
            //dbTest.Update();
            //dbTest.Query();
            //dbTest.OrtherQuery();
            //dbTest.OrtherNoneQuery();
        }

        public static void TestSqlite()
        {
             string dbPath = "f:\\testdb.sqlite";
             string connectString = "Data Source=f:\\testdb.sqlite;Pooling=true;FailIfMissing=false;";
            if (File.Exists(dbPath))
            {
                File.Delete(dbPath);
            }
            SqlDbContext db = new SQLiteDbContext(connectString);

            //SmartDb框架提供一个记录日志委托用来记录执行SQL及参数信息，ConsoleWriteInfo在控制台输出，大家可以根据自己需要自己定义方法传给DbHelper.logAction会自动进行调用
            db.DbHelper.logAction = db.DbHelper.ConsoleWriteInfo;

            var sql = "create  table UserInfo(UserId int not null,UserName  varchar(50),Age int,Email varchar(50))";
            db.ExecuteNoneQuery(sql, null);

            var dbTest = new DbTest(db);
            dbTest.DeleteAll();
            dbTest.Insert();
            dbTest.Delete();
            dbTest.Update();
            dbTest.Query();
            dbTest.OrtherQuery();
            dbTest.OrtherNoneQuery();
        }

        public static void TestPostgreSql()
        {
            string connectString = "server=192.168.58.131;port=5432;user id=xiaozhang1;password=123456;database=testdb;";
            SqlDbContext db = new PostgreSqlDbContext(connectString);

            //SmartDb框架提供一个记录日志委托用来记录执行SQL及参数信息，ConsoleWriteInfo在控制台输出，大家可以根据自己需要自己定义方法传给DbHelper.logAction会自动进行调用
            db.DbHelper.logAction = db.DbHelper.ConsoleWriteInfo;

            var dbTest = new DbTest(db);
            dbTest.DeleteAll();
            dbTest.Insert();
            dbTest.Delete();
            dbTest.Update();
            dbTest.Query();
            dbTest.OrtherQuery();
            dbTest.OrtherNoneQuery();
        }
     }
}
