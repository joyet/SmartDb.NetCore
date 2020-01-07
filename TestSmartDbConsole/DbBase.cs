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
