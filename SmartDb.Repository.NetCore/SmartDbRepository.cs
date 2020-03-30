using SmartDb.NetCore;
using SmartDb.Repository.NetCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace TestSmartDbRepository
{
   public class SmartDbRepository<T, IdType>:ISmartDbRepository<T, IdType>
    {
       public SqlDbContext SmartDbContext { get; set; }

        /// <summary>
        /// 添加单条数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public object Insert(T entity)
        {
            var result = SmartDbContext.Insert(entity);
            return result;
        }

        /// <summary>
        /// 批量添加数据
        /// </summary>
        /// <param name="entityList"></param>
        /// <returns></returns>
        public IList<object> Insert(List<T> entityList)
        {
            var result = new List<object>();
            foreach (var entity in entityList)
            {
                var insertResult=SmartDbContext.Insert(entity);
                result.Add(insertResult);
            }
            return result;
        }

        /// <summary>
        /// 根据主键值删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Delete(IdType id)
        {
            var result = SmartDbContext.Delete<T>(id);
            return result;
        }

        /// <summary>
        /// 根据主键值列表删除数据
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        public int Delete(List<IdType> idList)
        {
            var result = 0;
            foreach (var id in idList)
            {
                SmartDbContext.Delete<T>(id);
                result++;
            }
            return result;
        }

        /// <summary>
        /// 根据过滤条件Sql和过滤条件参数删除数据
        /// </summary>
        /// <param name="whereSql">过滤条件Sql</param>
        /// <param name="whereParam">过滤条件参数(参数名和参数值,例:new {Uname="joyet",Age = 110})</param>
        /// <returns></returns>
        public int Delete(string whereSql, object whereParam)
        {
            var result = SmartDbContext.Delete<T>(whereSql, whereParam);
            return result;
        }

        /// <summary>
        /// 根据实体主键值修改其它字段数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Update(T entity)
        {
            var result = SmartDbContext.Update(entity);
            return result;
        }

        /// <summary>
        ///  根据修改字段参数、主键值修改数据
        /// </summary>
        /// <param name="updateParam">修改字段参数(参数名和参数值,例:new {Uname="joyet",Age = 110})</param>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Update(object updateParams, IdType id)
        {
            var result = SmartDbContext.Update<T>(updateParams, id);
            return result;
        }

        /// <summary>
        /// 根据修改字段参数、过滤条件Sql、过滤条件参数修改数据
        /// </summary>
        /// <param name="updateParam">修改字段参数(参数名和参数值,例:new {Uname="joyet",Age = 110})</param>
        /// <param name="whereSql">过滤条件Sql</param>
        /// <param name="whereParam">过滤条件参数(参数名和参数值,例:new {UserId=1})</param>
        /// <returns></returns>
        public int Update(object updateParam, string whereSql, object whereParam)
        {
            var result = SmartDbContext.Update<T>(updateParam, whereSql,whereParam);
            return result;
        }

        /// <summary>
        ///根据主键值查询数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T Query(IdType id)
        {
            var result = SmartDbContext.Query<T>(id);
            return result;
        }

        /// <summary>
        /// 根据查询字段、过滤条件Sql、过滤条件参数查询数据
        /// </summary>
        /// <param name="queryColumns">要查询字段</param>
        /// <param name="whereSql">过滤条件Sql</param>
        /// <param name="whereParam">过滤条件参数(参数名和参数值,例:new {Uname="joyet",Age = 110})</param>
        /// <returns></returns>
        public List<T> Query(string queryColumns, string whereSql, object whereParam)
        {
            var result = SmartDbContext.Query<T>(queryColumns, whereSql, whereParam);
            return result;
        }

        /// <summary>
        /// 根据sql语句、过滤条件参数查询数据
        /// </summary>
        /// <param name="sql">sql语句/参数化SQL语句/存储过程</param>
        /// <param name="objParam">过滤条件参数(参数名和参数值,例:new {Uname="joyet",Age = 110})</param>
        /// <returns></returns>
        public List<T2> Query<T2>(string sql, object objParam)
        {
            var result = SmartDbContext.Query<T2>(sql, objParam);
            return result;
        }

        /// <summary>
        /// 单表分页数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryColumns">要查询字段</param>
        /// <param name="sortColumn">排序字段</param>
        /// <param name="sortType">排序方式</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">第几页</param>
        /// <param name="whereSql">过滤条件Sql</param>
        /// <param name="whereParam">过滤条件参数(参数名和参数值,例:new {Uname="joyet",Age = 110})</param>
        /// <returns></returns>
        public PageResultEntity QueryPageList(string queryColumns, string sortColumn, string sortType, int pageSize, int pageIndex, string whereSql, object whereParam)
        {
            var result = SmartDbContext.QueryPageList<T>(queryColumns, sortColumn, sortType, pageSize, pageIndex, whereSql, whereParam);
            return result;
        }

        /// <summary>
        /// 创建数据库参数对象
        /// </summary>
        /// <param name="dbParamName"></param>
        /// <param name="dbParamValue"></param>
        /// <param name="dbPramDbType"></param>
        /// <param name="dbPramDbTypeLength"></param>
        /// <returns></returns>
        public IDbDataParameter GetDbParam(string dbParamName, object dbParamValue, object dbPramDbType = null, int dbPramDbTypeLength = 0)
        {
            var result = SmartDbContext.DbBuilder.DbFactory.GetDbParam(dbParamName, dbParamValue, dbPramDbType, dbPramDbTypeLength);
            return result;
        }

        /// <summary>
        /// 返回数据库参数对象列表
        /// </summary>
        /// <param name="objParam"></param>
        /// <returns></returns>
        public  List<IDbDataParameter> GetDbParamList(object objParam)
        {
            var result = SmartDbContext.DbBuilder.DbFactory.GetDbParamList(objParam);
            return result;
        }

        /// <summary>
        /// 创建数据库参数化关键字操作符
        /// </summary>
        /// <returns></returns>
        public  string GetDbParamOperator()
        {
            return SmartDbContext.DbBuilder.DbFactory.GetDbParamOperator();
        }
    }
}
