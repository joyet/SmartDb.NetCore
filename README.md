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
 [Table(TableName="userinfo")]
 public class UserInfo
 {
      [TableColumn(IsPrimaryKey = true)]
      public int UserId { get; set; }

      public string UserName { get; set; }

      public int Age { get; set; }

      public string Email { get; set; }
 }
```

封装调用SmartDb.NetCore的封装类
```
  public class DbTest
    {
        
        private SqlDbContext _db;
        private SqlDbFactory _dbFactory;
        private string _dbOperator;
        
        public DbTest(SqlDbContext db)
        {
            _db = db;
            _dbFactory = _db.DbBuilder.DbFactory;
            _dbOperator = _dbFactory.GetDbParamOperator();
        }

        /// <summary>
        /// 写入数据
        /// </summary>
        public void Insert()
        {
            _db.BeginTransaction();
            for (int i = 1; i <= 10; i++)
            {
                _db.Insert<UserInfo>(new UserInfo() { UserId = i, UserName = "joyet" + i.ToString(), Age = 110, Email = "joyet" + i.ToString() + "@qq.com" });
            }
            _db.CommitTransaction();
        }

        /// <summary>
        /// 删除所有数据
        /// </summary>
        public void DeleteAll()
        {
            var result = _db.Delete<UserInfo>("", null);
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        public void Delete()
        {
            //根据实体主键值查询数据
            var result = _db.Delete<UserInfo>(1);

            //根据过滤SQL、object参数查询数据列表1
            result = _db.Delete<UserInfo>("UserId = 1", null);
            result = _db.Delete<UserInfo>(string.Format("UserId={0}UserId", _dbOperator), new { UserId = 1 });
        }

        /// <summary>
        /// 修改数据
        /// </summary>
        public void Update()
        {
            //根据修改字段参数及值、过滤SQL、object参数修改数据
            var data = _db.Query<UserInfo>(2);
            data.UserName = "joyet22";
            var result = _db.Update<UserInfo>(data);
            result = _db.Update<UserInfo>(new { UserName = "joyet222" },2);
        }

        /// <summary>
        /// 查询数据
        /// </summary>
        public void Query()
        {
            //根据实体主键参数值查询数据
            var data = _db.Query<UserInfo>(3);

            //根据查询字段、过滤SQL、object参数查询数据列表
            var dataList1 = _db.Query<UserInfo>("UserId,UserName", "UserId=3", null);
            var dataList2 = _db.Query<UserInfo>("UserId,UserName", string.Format("UserId={0}UserId", _dbOperator), new { UserId = 3 });

            //根据查询参数化SQL、object参数查询数据列表
            var dataList3 = _db.Query<UserInfo>("select * from UserInfo where UserId=3", null);
            var dataList4 = _db.Query<UserInfo>(string.Format("select * from UserInfo where UserId={0}UserId", _dbOperator), new { UserId = 3 });

            //分页查询列表
            var pageDataList1 = _db.QueryPageList<UserInfo>("UserId,UserName", "UserId", "asc", 10, 1, string.Format("UserId>{0}UserId", _dbOperator), new { UserId = 3 });
            var pageDataList2 = _db.QueryPageList<UserInfo>("*", "UserId", "asc", 10, 2, "UserId>2", null);
        }

        /// <summary>
        /// 执行非查询操作
        /// </summary>
        public void OrtherNoneQuery()
        {
            var sql = "delete from UserInfo where UserId=4";
            var paramSql = string.Format("delete from UserInfo where UserId={0}UserId", _dbOperator);
            var dbParams = new List<IDbDataParameter>();
            dbParams.Add(_dbFactory.GetDbParam("UserId", 4));

            //根据SQL语句、IDbDataParameter参数列表删除数据用法1(无参)
            var result = _db.ExecuteNoneQuery(sql, null);

            //根据SQL语句、IDbDataParameter参数列表删除数据用法2(有参)
            result = _db.ExecuteNoneQuery(paramSql, dbParams);

            //根据SQL语句、object参数删除数据用法1(无参)
            result = _db.ExecuteNoneQueryWithObjParam(sql);

            //根据SQL语句、object参数删除数据用法2(有参)
            result = _db.ExecuteNoneQueryWithObjParam(paramSql, new { UserId = 4 });

        }

        /// <summary>
        /// 执行查询操作
        /// </summary>
        public void OrtherQuery()
        {
            var sql = "select * from UserInfo where UserId=5";
            var paramSql = string.Format("select * from UserInfo where UserId={0}UserId", _dbOperator);
            List<UserInfo> dataList;
            var dbParams = new List<IDbDataParameter>();
            dbParams.Add(_dbFactory.GetDbParam("UserId", 5));

            //根据SQL语句、IDbDataParameter参数列表删除数据用法2(无参)
            using (var dataReader = _db.ExecuteReader(sql, null))
            {
                dataList = _db.DataReaderToEntityList<UserInfo>(dataReader);
            }

            //根据SQL语句、IDbDataParameter参数列表删除数据用法2(有参)
            using (var dataReader = _db.ExecuteReader(sql, dbParams))
            {
                dataList = _db.DataReaderToEntityList<UserInfo>(dataReader);
            }

            //根据SQL语句、object参数查询数据用法1(无参)
            using (var dataReader = _db.ExecuteReaderWithObjParam(sql, null))
            {
                dataList = _db.DataReaderToEntityList<UserInfo>(dataReader);
            }

            //根据SQL语句、object参数查询数据用法1(有参)
            using (var dataReader = _db.ExecuteReaderWithObjParam(paramSql, new { UserId = 5 }))
            {
                dataList = _db.DataReaderToEntityList<UserInfo>(dataReader);
            }
        }
    }
```
控制台调用示例代码：
```
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
```
