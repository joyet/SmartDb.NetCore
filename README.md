#### SmartDb.NetCore是一套基于ADO.Net和DoNetCore对SqlServer、MySql、SQLite、PostgreSql数据库支持的快速开发和轻量级ORM框架.

SmartDb.NetCore框架特点如下：
   * 支持.NF和DoNetCore框架。
   * 轻量级半ORM框架，封装基于单表CRUD等操作，同时支持事务、SQL语句、参数化SQL语句、存储过程操作。
   * 提供基于Emit将IDataReader、DataTable转化为实体对象。
   * 支持非参数化SQL语句、原生参数化SQL语句及类似Dapper参数化功能，提供原生ADO.Net对CRUD操作功能。

本源码提供SmartDb.NetCore对SqlServer、MySql、SQLite、PostgreSql调用一些测试代码，大家根据测试项目配置自己创建测试数据库和测试数据表。  
SmartDb.MySql.NetCore是此框架对MySql支持的Nuget包，Nuget包地址如下：[SmartDb.MySql.NetCore](https://www.nuget.org/packages/SmartDb.MySql.NetCore)  
SmartDb.SqlServer.NetCore是此框架对SqlServer支持的Nuget包，Nuget包地址如下：[SmartDb.SqlServer.NetCore](https://www.nuget.org/packages/SmartDb.SqlServer.NetCore)  
SmartDb.SQLite.NetCore是此框架对SQLite支持的Nuget包，Nuget包地址如下：[SmartDb.SQLite.NetCore](https://www.nuget.org/packages/SmartDb.SQLite.NetCore)  
SmartDb.PostgreSql.NetCore是此框架对PostgreSql支持的Nuget包，Nuget包地址如下：[SmartDb.PostgreSql.NetCore](https://www.nuget.org/packages/SmartDb.PostgreSql.NetCore)  

框架Git开源地址：https://github.com/joyet/SmartDb.NetCore  
框架博客地址：https://www.cnblogs.com/joyet-john/articles/9295985.html#4021708  
联系邮箱：joyet@qq.com  

实体类：
``` 
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

```

封装调用SmartDb.NetCore的封装类
```
 using SmartDb.NetCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace TestSmartDbConsole
{
   public class DbBase<Entity>
    {
        public SqlDbContext DbContext { get; set; }

        /// <summary>
        /// 写入数据
        /// </summary>
        public void Insert()
        {
            DbContext.BeginTransaction();
            for (int i = 1; i <= 5; i++)
            {
               var result= DbContext.Insert<UserInfo>(new UserInfo() { UserId = i, UserName = "joyet" + i.ToString(), Age = 110, Email = "joyet" + i.ToString() + "@qq.com" });
            }
            DbContext.CommitTransaction();
        }

        /// <summary>
        /// 删除所有数据
        /// </summary>
        public void DeleteAll()
        {
            var result = DbContext.Delete<Entity>("", null);
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        public void Delete()
        {
            var dbFactory = DbContext.DbBuilder.DbFactory;
            var dbOperator = dbFactory.GetDbParamOperator();

            //根据主键值删除数据(此方法会采用SQL参数化)
            var result = DbContext.Delete<Entity>(1);

            ////根据过滤条件Sql和参数删除数据
            result = DbContext.Delete<Entity>("UserId=1", null); //用法1
            result = DbContext.Delete<Entity>("", new { UserId = 1 }); //用法2(此方法会采用SQL参数化)
            result = DbContext.Delete<Entity>(string.Format("UserId={0}UserId", dbOperator), new { UserId = 1 }); //用法3(此方法会采用SQL参数化)
        }

        /// <summary>
        /// 修改数据
        /// </summary>
        public void Update()
        {
            var dbFactory = DbContext.DbBuilder.DbFactory;
            var dbOperator = dbFactory.GetDbParamOperator();

            var item = DbContext.Query<Entity>(2);

            //根据实体对象修改数据根据用法(此方法会采用SQL参数化)
            var result = DbContext.Update<Entity>(item);

            //根据修改字段参数、主键值修改数据用法(此方法会采用SQL参数化)
            result = DbContext.Update<Entity>(new { UserName = "joyet2X" }, 2);

            //根据要修改字段参数、过滤条件Sql、过滤参数修改数据
            result = DbContext.Update<Entity>(new { UserName = "joyet2XX" }, "UserId=2", null);  //用法1
            result = DbContext.Update<Entity>(new { UserName = "joyet2XXX" }, "", new { UserId = 2 }); //用法2(此方法会采用SQL参数化)
            result = DbContext.Update<Entity>(new { UserName = "joyet2XXXX" }, string.Format("UserId={0}UserId", dbOperator), new { UserId = 2 }); //用法3(此方法会采用SQL参数化)
        }

        /// <summary>
        /// 查询数据
        /// </summary>
        public void Query()
        {
            var dbFactory = DbContext.DbBuilder.DbFactory;
            var dbOperator = dbFactory.GetDbParamOperator();

            //根据主键值查询数据(此方法会采用SQL参数化)
            var data = DbContext.Query<Entity>(3);

            //根据查询字段、过滤条件Sql、过滤条件参数查询数据
            var dataList = DbContext.Query<Entity>("UserId,UserName", "UserId=3", null); //用法1
            dataList = DbContext.Query<Entity>("UserId,UserName", "", new { UserId = 3 }); //用法2(此方法会采用SQL参数化)
            dataList = DbContext.Query<Entity>("UserId,UserName", string.Format("UserId={0}UserId", dbOperator), new { UserId = 3 }); //用法3(此方法会采用SQL参数化)

            //根据sql语句、过滤条件参数查询数据
            dataList = DbContext.Query<Entity>("select * from UserInfo where UserId=3", null); //用法1
            dataList = DbContext.Query<Entity>(string.Format("select * from UserInfo where UserId={0}UserId", dbOperator), new { UserId = 3 }); //用法1(此方法会采用SQL参数化)

            //分页查询列表
            var pageDataList = DbContext.QueryPageList<Entity>("UserId,UserName", "UserId", "asc", 10, 1, "UserId=3", null);  //用法1
            pageDataList = DbContext.QueryPageList<Entity>("UserId,UserName", "UserId", "asc", 10, 1, "", new { UserId = 3 });  //用法2(此方法会采用SQL参数化)
            pageDataList = DbContext.QueryPageList<Entity>("UserId,UserName", "UserId", "asc", 10, 1, string.Format("UserId>{0}UserId", dbOperator), new { UserId = 3 });  //用法3(此方法会采用SQL参数化)
        }

        /// <summary>
        /// 执行非查询操作
        /// </summary>
        public void OrtherNoneQuery()
        {
            var dbFactory = DbContext.DbBuilder.DbFactory;
            var dbOperator = dbFactory.GetDbParamOperator();

            var sql = "delete from UserInfo where UserId=4";
            var paramSql = string.Format("delete from UserInfo where UserId={0}UserId", dbOperator);

            //根据SQL语句、IDbDataParameter参数删除数据
            var result = DbContext.ExecuteNoneQuery(sql, null); //用法1(无参)

            var dbParams = new List<IDbDataParameter>();
            dbParams.Add(dbFactory.GetDbParam("UserId", 4));
            result = DbContext.ExecuteNoneQuery(paramSql, dbParams); //用法2(有参)

            //根据SQL语句、object参数删除数据
            result = DbContext.ExecuteNoneQueryWithObjParam(sql); //用法1(无参)
            result = DbContext.ExecuteNoneQueryWithObjParam(paramSql, new { UserId = 4 }); //用法2(有参)
        }

        /// <summary>
        /// 执行查询操作
        /// </summary>
        public void OrtherQuery()
        {
            var dbFactory = DbContext.DbBuilder.DbFactory;
            var dbOperator = dbFactory.GetDbParamOperator();

            var sql = "select * from UserInfo where UserId=5";
            var paramSql = string.Format("select * from UserInfo where UserId={0}UserId", dbOperator);
            List<Entity> dataList;

            //根据SQL语句、IDbDataParameter参数删除数据
            using (var dataReader = DbContext.ExecuteReader(sql, null)) //用法1(无参)
            {
                dataList = DbContext.DataReaderToEntityList<Entity>(dataReader);
            }

            var dbParams = new List<IDbDataParameter>();
            dbParams.Add(dbFactory.GetDbParam("UserId", 5));
            using (var dataReader = DbContext.ExecuteReader(paramSql, dbParams)) //用法2(有参)
            {
                dataList = DbContext.DataReaderToEntityList<Entity>(dataReader);
            }

            //根据SQL语句、object参数查询数据
            using (var dataReader = DbContext.ExecuteReaderWithObjParam(sql, null)) //用法1(无参)
            {
                dataList = DbContext.DataReaderToEntityList<Entity>(dataReader);
            }
            using (var dataReader = DbContext.ExecuteReaderWithObjParam(paramSql, new { UserId = 5 })) //用法2(有参)
            {
                dataList = DbContext.DataReaderToEntityList<Entity>(dataReader);
            }
        }
    }
}

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

```
控制台调用示例代码：
```
 using SmartDb.MySql.NetCore;
using SmartDb.NetCore;
using SmartDb.PostgreSql.NetCore;
using SmartDb.SQLite.NetCore;
using SmartDb.SqlServer.NetCore;
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
            db.ExecuteDbCallBack = (cmdText, dbParms) => {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendFormat("sql:{0}\n", cmdText);
                if (dbParms != null)
                {
                    foreach (IDbDataParameter param in dbParms)
                    {
                        stringBuilder.AppendFormat("paramName:{0},paramValue:{1}\n", param.ParameterName,param.Value.ToString());
                    }
                }
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

        public static void TestSqlServer()
        {
            string connectString = "server=localhost;user id=sa;password=123456;database=testdb;pooling=true;min pool size=5;max pool size=512;connect timeout = 60;";
            SqlDbContext db = new SqlServerDbContext(connectString);

            //数据执行回调函数
            db.ExecuteDbCallBack = (cmdText, dbParms) => {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("sql:" + cmdText);
                if (dbParms != null)
                {
                    foreach (IDbDataParameter param in dbParms)
                    {
                        stringBuilder.Append("paramName:" + param.ParameterName + ",paramValue:" + param.Value.ToString());
                    }
                }
                stringBuilder.Append("\n\n");
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

        public static void TestSqlite()
        {
             string dbPath = "f:\\testdb.sqlite";
             string connectString = "Data Source=f:\\testdb.sqlite;Pooling=true;FailIfMissing=false;";
            if (File.Exists(dbPath))
            {
                File.Delete(dbPath);
            }
            SqlDbContext db = new SQLiteDbContext(connectString);

            //数据执行回调函数
            db.ExecuteDbCallBack = (cmdText, dbParms) => {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("sql:" + cmdText);
                if (dbParms != null)
                {
                    foreach (IDbDataParameter param in dbParms)
                    {
                        stringBuilder.Append("paramName:" + param.ParameterName + ",paramValue:" + param.Value.ToString());
                    }
                }
                stringBuilder.Append("\n\n");
                Console.Write(stringBuilder.ToString());
            };

            //var sql = "create  table UserInfo(UserId int identity(1,1) primary key,UserName  varchar(50),Age int,Email varchar(50))";
            var sql = "create  table UserInfo(UserId int  primary key,UserName  varchar(50),Age int,Email varchar(50))";
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

            //数据执行回调函数
            db.ExecuteDbCallBack = (cmdText, dbParms) => {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("sql:" + cmdText);
                if (dbParms != null)
                {
                    foreach (IDbDataParameter param in dbParms)
                    {
                        stringBuilder.Append("paramName:" + param.ParameterName + ",paramValue:" + param.Value.ToString());
                    }
                }
                stringBuilder.Append("\n\n");
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
     }
}

```
