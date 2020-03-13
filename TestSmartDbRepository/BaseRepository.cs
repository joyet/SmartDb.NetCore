using SmartDb.NetCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace TestSmartDbRepository
{
   public class BaseRepository<T, IdType> : IBaseRepository<T, IdType>
    {
       public SqlDbContext DbContext { get; set; }

        /// <summary>
        /// 添加单条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public object Insert(T entity)
        {
            var result = DbContext.Insert(entity);
            return result;
        }

        /// <summary>
        /// 批量添加数据
        /// </summary>
        /// <param name="entityList"></param>
        /// <returns></returns>
        public object Insert(List<T> entityList)
        {
            var result = 0;
            foreach (var entity in entityList)
            {
                DbContext.Insert(entity);
                result++;
            }
            return result;
        }

        /// <summary>
        /// 根据主键值删除数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Delete(IdType id)
        {
            var result = DbContext.Delete<T>(id);
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
                DbContext.Delete<T>(id);
                result++;
            }
            return result;
        }

        /// <summary>
        /// 根据过滤条件Sql和参数(参数名和参数值)删除数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="whereSql">过滤条件Sql</param>
        /// <param name="whereParam">过滤条件参数(参数名和参数值),例:new {Uname="joyet",Age = 110}</param>
        /// <returns></returns>
        public int Delete(string whereSql, object whereParam)
        {
            var result = DbContext.Delete<T>(whereSql, whereParam);
            return result;
        }

        /// <summary>
        /// 根据实体主键值修改其它字段数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Update(T entity)
        {
            var result = DbContext.Update(entity);
            return result;
        }

        /// <summary>
        ///  根据修改字段参数(参数名和参数值)、主键值修改数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="updateParam">修改字段参数(参数名和参数值),例:new {Uname="joyet",Age = 110}</param>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Update(object updateParams, IdType id)
        {
            var result = DbContext.Update<T>(updateParams, id);
            return result;
        }

        /// <summary>
        /// 根据要修改字段参数(参数名和参数值)、过滤条件Sql、过滤字段参数(参数名和参数值)修改数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="updateParam">要修改字段参数(参数名和参数值),例:new {Uname="joyet",Age = 110}</param>
        /// <param name="whereSql">过滤条件Sql</param>
        /// <param name="whereParam">过滤字段参数(参数名和参数值),例:new {UserId=1}</param>
        /// <returns></returns>
        public int Update(object updateParam, string whereSql, object whereParam)
        {
            var result = DbContext.Update<T>(updateParam, whereSql,whereParam);
            return result;
        }

        /// <summary>
        ///根据主键值查询数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public T Query(IdType id)
        {
            var result = DbContext.Query<T>(id);
            return result;
        }

        /// <summary>
        /// 根据查询字段、过滤条件Sql、过滤条件参数(参数名和参数值)查询数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryColumns">要查询字段</param>
        /// <param name="whereSql">过滤条件Sql</param>
        /// <param name="whereParam">过滤条件参数(参数名和参数值),例:new {Uname="joyet",Age = 110}</param>
        /// <returns></returns>
        public List<T> Query(string queryColumns, string whereSql, object whereParam)
        {
            var result = DbContext.Query<T>(queryColumns, whereSql, whereParam);
            return result;
        }

        /// <summary>
        /// 根据sql语句、过滤条件参数(参数名和参数值)查询数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">sql语句/参数化SQL语句/存储过程</param>
        /// <param name="objParam">过滤条件参数(参数名和参数值),例:new {Uname="joyet",Age = 110}</param>
        /// <returns></returns>
        public List<T2> Query<T2>(string sql, object objParam)
        {
            var result = DbContext.Query<T2>(sql, objParam);
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
        /// <param name="whereParam">过滤条件字段名及字段值参数,例:new {Uname="joyet",Age = 110}</param>
        /// <returns></returns>
        public PageResultEntity QueryPageList(string queryColumns, string sortColumn, string sortType, int pageSize, int pageIndex, string whereSql, object whereParam)
        {
            var result = DbContext.QueryPageList<T>(queryColumns, sortColumn, sortType, pageSize, pageIndex, whereSql, whereParam);
            return result;
        }

        /// <summary>
        /// 开启事务
        /// </summary>
        public  void BeginTransaction()
        {
            DbContext.BeginTransaction();
        }

        /// <summary>
        /// 提交事务
        /// </summary>
        public  void CommitTransaction()
        {
            DbContext.CommitTransaction();
        }

        /// <summary>
        /// 回滚事务
        /// </summary>
        public  void RollbackTransaction()
        {
            DbContext.RollbackTransaction();
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
            var result = DbContext.DbBuilder.DbFactory.GetDbParam(dbParamName, dbParamValue, dbPramDbType, dbPramDbTypeLength);
            return result;
        }

        /// <summary>
        /// 返回数据库参数对象列表
        /// </summary>
        /// <param name="objParam"></param>
        /// <returns></returns>
        public  List<IDbDataParameter> GetDbParamList(object objParam)
        {
            var result = DbContext.DbBuilder.DbFactory.GetDbParamList(objParam);
            return result;
        }

        /// <summary>
        /// 创建数据库参数化关键字操作符
        /// </summary>
        /// <returns></returns>
        public  string GetDbParamOperator()
        {
            return DbContext.DbBuilder.DbFactory.GetDbParamOperator();
        }
    }
}
