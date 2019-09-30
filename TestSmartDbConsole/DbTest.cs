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

        public DbTest(SqlDbContext db)
        {
            _db = db;
            _dbFactory = _db.DbBuilder.DbFactory;
            _dbOperator = _dbFactory.GetDbParamOperator();
        }

        ///// <summary>
        ///// 写入数据
        ///// </summary>
        //public void Insert()
        //{
        //    _db.BeginTransaction();
        //    for (int i = 1; i <= 10; i++)
        //    {
        //        _db.Insert<UserInfo>(new UserInfo() { UserId = i, UserName = "joyet" + i.ToString(), Age = 110, Email = "joyet" + i.ToString() + "@qq.com" });
        //    }
        //    _db.CommitTransaction();
        //}

        ///// <summary>
        ///// 删除所有数据
        ///// </summary>
        //public void DeleteAll()
        //{
        //    var result = _db.Delete<UserInfo>("", null);
        //}

        ///// <summary>
        ///// 删除数据
        ///// </summary>
        //public void Delete()
        //{
        //    //根据实体主键值查询数据
        //    var result = _db.Delete<UserInfo>(1);

        //    //根据过滤SQL、object参数查询数据列表1
        //    result = _db.Delete<UserInfo>("UserId = 1", null);
        //    result = _db.Delete<UserInfo>(string.Format("UserId={0}UserId", _dbOperator), new { UserId = 1 });
        //}

        ///// <summary>
        ///// 修改数据
        ///// </summary>
        //public void Update()
        //{
        //    //根据修改字段参数及值、过滤SQL、object参数修改数据
        //    var data = _db.Query<UserInfo>(2);
        //    data.UserName = "joyet22";
        //    var result = _db.Update<UserInfo>(data);
        //    result = _db.Update<UserInfo>(new { UserName = "joyet222" }, 2);
        //}

        ///// <summary>
        ///// 查询数据
        ///// </summary>
        //public void Query()
        //{
        //    //根据实体主键参数值查询数据
        //    var data = _db.Query<UserInfo>(3);

        //    //根据查询字段、过滤SQL、object参数查询数据列表
        //    var dataList1 = _db.Query<UserInfo>("UserId,UserName", "UserId=3", null);
        //    var dataList2 = _db.Query<UserInfo>("UserId,UserName", string.Format("UserId={0}UserId", _dbOperator), new { UserId = 3 });

        //    //根据查询参数化SQL、object参数查询数据列表
        //    var dataList3 = _db.Query<UserInfo>("select * from UserInfo where UserId=3", null);
        //    var dataList4 = _db.Query<UserInfo>(string.Format("select * from UserInfo where UserId={0}UserId", _dbOperator), new { UserId = 3 });

        //    //分页查询列表
        //    var pageDataList1 = _db.QueryPageList<UserInfo>("UserId,UserName", "UserId", "asc", 10, 1, string.Format("UserId>{0}UserId", _dbOperator), new { UserId = 3 });
        //    var pageDataList2 = _db.QueryPageList<UserInfo>("*", "UserId", "asc", 10, 2, "UserId>2", null);
        //}

        ///// <summary>
        ///// 执行非查询操作
        ///// </summary>
        //public void OrtherNoneQuery()
        //{
        //    var sql = "delete from UserInfo where UserId=4";
        //    var paramSql = string.Format("delete from UserInfo where UserId={0}UserId", _dbOperator);
        //    var dbParams = new List<IDbDataParameter>();
        //    dbParams.Add(_dbFactory.GetDbParam("UserId", 4));

        //    //根据SQL语句、IDbDataParameter参数列表删除数据用法1(无参)
        //    var result = _db.ExecuteNoneQuery(sql, null);

        //    //根据SQL语句、IDbDataParameter参数列表删除数据用法2(有参)
        //    result = _db.ExecuteNoneQuery(paramSql, dbParams);

        //    //根据SQL语句、object参数删除数据用法1(无参)
        //    result = _db.ExecuteNoneQueryWithObjParam(sql);

        //    //根据SQL语句、object参数删除数据用法2(有参)
        //    result = _db.ExecuteNoneQueryWithObjParam(paramSql, new { UserId = 4 });

        //}

        ///// <summary>
        ///// 执行查询操作
        ///// </summary>
        //public void OrtherQuery()
        //{
        //    var sql = "select * from UserInfo where UserId=5";
        //    var paramSql = string.Format("select * from UserInfo where UserId={0}UserId", _dbOperator);
        //    List<UserInfo> dataList;
        //    var dbParams = new List<IDbDataParameter>();
        //    dbParams.Add(_dbFactory.GetDbParam("UserId", 5));

        //    //根据SQL语句、IDbDataParameter参数列表删除数据用法2(无参)
        //    using (var dataReader = _db.ExecuteReader(sql, null))
        //    {
        //        dataList = _db.DataReaderToEntityList<UserInfo>(dataReader);
        //    }

        //    //根据SQL语句、IDbDataParameter参数列表删除数据用法2(有参)
        //    using (var dataReader = _db.ExecuteReader(sql, dbParams))
        //    {
        //        dataList = _db.DataReaderToEntityList<UserInfo>(dataReader);
        //    }

        //    //根据SQL语句、object参数查询数据用法1(无参)
        //    using (var dataReader = _db.ExecuteReaderWithObjParam(sql, null))
        //    {
        //        dataList = _db.DataReaderToEntityList<UserInfo>(dataReader);
        //    }

        //    //根据SQL语句、object参数查询数据用法1(有参)
        //    using (var dataReader = _db.ExecuteReaderWithObjParam(paramSql, new { UserId = 5 }))
        //    {
        //        dataList = _db.DataReaderToEntityList<UserInfo>(dataReader);
        //    }
        //}
    }
}
