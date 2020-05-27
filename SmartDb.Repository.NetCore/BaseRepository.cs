using SmartDb.NetCore;
using SmartDb.Repository.NetCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace TestSmartDbRepository
{
   public class BaseRepository<T, IdType>:IBaseRepository<T,IdType>
    {
       public SqlDbContext SmartDbContext { get; set; }

        #region 添加数据相关方法

        /// <summary>
        /// 添加单条数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void Insert(T entity)
        {
           SmartDbContext.Insert(entity);
        }

        /// <summary>
        /// 添加单条数据异步方法
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task InsertAsync(T entity)
        {
            await Task.Run(() =>
            {
                SmartDbContext.Insert(entity);
            });
        }

        /// <summary>
        /// 添加单条数据,返回自动增长值或影响行数
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="isGetAutoIncrementValue"></param>
        /// <returns></returns>
        public object Insert(T entity, bool isGetAutoIncrementValue)
        {
            var result = SmartDbContext.Insert(entity, isGetAutoIncrementValue);
            return result;
        }

        /// <summary>
        /// 添加单条数据异步方法,返回自动增长值或影响行数
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="isGetAutoIncrementValue"></param>
        /// <returns></returns>
        public async Task<object> InsertAsync(T entity, bool isGetAutoIncrementValue)
        {
            object result = null;
            await Task.Run(() =>
            {
                result=SmartDbContext.Insert(entity, isGetAutoIncrementValue);
            });
            return result;
        }

        /// <summary>
        /// 批量添加数据
        /// </summary>
        /// <param name="entityList"></param>
        /// <returns></returns>
        public void Insert(IList<T> entityList)
        {
            foreach (var entity in entityList)
            {
                SmartDbContext.Insert(entity);
            }
        }

        /// <summary>
        /// 批量添加数据异步方法
        /// </summary>
        /// <param name="entityList"></param>
        /// <returns></returns>
        public async Task InsertAsync(IList<T> entityList)
        {
           await Task.Run(() => {
                foreach (var entity in entityList)
                {
                    SmartDbContext.Insert(entity);
                }
            });
        }

        /// <summary>
        /// 批量添加数据,返回自动增长值或影响行数列表
        /// </summary>
        /// <param name="entityList"></param>
        /// <param name="isGetAutoIncrementValue"></param>
        /// <returns></returns>
        public IList<object> Insert(IList<T> entityList, bool isGetAutoIncrementValue)
        {
            IList<object> result=new List<object>();
            foreach (var entity in entityList)
            {
                result.Add(Insert(entity, isGetAutoIncrementValue));
            }
            return result;
        }

        /// <summary>
        /// 批量添加数据异步方法,返回自动增长值或影响行数列表
        /// </summary>
        /// <param name="entityList"></param>
        /// <param name="isGetAutoIncrementValue"></param>
        /// <returns></returns>
        public async Task<IList<object>> InsertAsync(IList<T> entityList, bool isGetAutoIncrementValue)
        {
            IList<object> result=new List<object>();
            await Task.Run(() =>
            {
                foreach (var entity in entityList)
                {
                    result.Add(Insert(entity, isGetAutoIncrementValue));
                }
            });
            return result;
        }

        #endregion

        #region 删除数据相关方法

        /// <summary>
        /// 根据主键值删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public void Delete(IdType id)
        {
            SmartDbContext.Delete<T>(id);
        }

        /// <summary>
        /// 根据主键值删除数据异步方法
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteAsync(IdType id)
        {
            await Task.Run(() => {
                SmartDbContext.Delete<T>(id);
            });
        }

        /// <summary>
        /// 根据主键值列表删除数据
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        public void Delete(IList<IdType> idList)
        {
            foreach (var id in idList)
            {
                SmartDbContext.Delete<T>(id);
            }
        }

        /// <summary>
        /// 根据主键值列表删除数据异步方法
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        public async Task DeleteAsync(IList<IdType> idList)
        {
           await Task.Run(() => {
                foreach (var id in idList)
                {
                    SmartDbContext.Delete<T>(id);
                }
            });
        }

        /// <summary>
        /// 根据过滤条件Sql和过滤条件参数删除数据
        /// </summary>
        /// <param name="whereSql">过滤条件Sql</param>
        /// <param name="whereParam">过滤条件参数(参数名和参数值,例:new {Uname="joyet",Age = 110})</param>
        /// <returns></returns>
        public void Delete(string whereSql, object whereParam)
        {
            SmartDbContext.Delete<T>(whereSql, whereParam);
        }

        /// <summary>
        /// 根据过滤条件Sql和过滤条件参数删除数据异步方法
        /// </summary>
        /// <param name="whereSql">过滤条件Sql</param>
        /// <param name="whereParam">过滤条件参数(参数名和参数值,例:new {Uname="joyet",Age = 110})</param>
        /// <returns></returns>
        public async Task DeleteAsync(string whereSql, object whereParam)
        {
            await Task.Run(() => {
                SmartDbContext.Delete<T>(whereSql, whereParam);
            });
        }
        #endregion

        #region 修改数据相关方法

        /// <summary>
        /// 根据实体主键值修改其它字段数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void Update(T entity)
        {
            SmartDbContext.Update(entity);
        }

        /// <summary>
        /// 根据实体主键值修改其它字段数据异步方法
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task UpdateAsync(T entity)
        {
           await Task.Run(() => {
                SmartDbContext.Update(entity);
            });
        }

        /// <summary>
        ///  根据修改字段参数、主键值修改数据
        /// </summary>
        /// <param name="updateParam">修改字段参数(参数名和参数值,例:new {Uname="joyet",Age = 110})</param>
        /// <param name="id"></param>
        /// <returns></returns>
        public void Update(object updateParam, IdType id)
        {
            SmartDbContext.Update<T>(updateParam, id);
        }

        /// <summary>
        ///  根据修改字段参数、主键值修改数据异步方法
        /// </summary>
        /// <param name="updateParam">修改字段参数(参数名和参数值,例:new {Uname="joyet",Age = 110})</param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task UpdateAsync(object updateParam, IdType id)
        {
           await Task.Run(() => {
                SmartDbContext.Update<T>(updateParam, id);
            });
        }

        /// <summary>
        /// 根据修改字段参数、过滤条件Sql、过滤条件参数修改数据
        /// </summary>
        /// <param name="updateParam">修改字段参数(参数名和参数值,例:new {Uname="joyet",Age = 110})</param>
        /// <param name="whereSql">过滤条件Sql</param>
        /// <param name="whereParam">过滤条件参数(参数名和参数值,例:new {UserId=1})</param>
        /// <returns></returns>
        public void Update(object updateParam, string whereSql, object whereParam)
        {
            SmartDbContext.Update<T>(updateParam, whereSql,whereParam);
        }

        /// <summary>
        /// 根据修改字段参数、过滤条件Sql、过滤条件参数修改数据异步方法
        /// </summary>
        /// <param name="updateParam">修改字段参数(参数名和参数值,例:new {Uname="joyet",Age = 110})</param>
        /// <param name="whereSql">过滤条件Sql</param>
        /// <param name="whereParam">过滤条件参数(参数名和参数值,例:new {UserId=1})</param>
        /// <returns></returns>
        public async Task UpdateAsync(object updateParam, string whereSql, object whereParam)
        {
           await Task.Run(() => {
                SmartDbContext.Update<T>(updateParam, whereSql, whereParam);
            });
        }
        #endregion

        #region 查询数据相关方法

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
        ///根据主键值查询数据异步方法
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> QueryAsync(IdType id)
        {
            T result = default(T);
            await Task.Run(() =>
            {
                result = SmartDbContext.Query<T>(id);
            });
            return result;
        }

        /// <summary>
        /// 根据查询字段、过滤条件Sql、过滤条件参数查询数据
        /// </summary>
        /// <param name="queryColumns">要查询字段</param>
        /// <param name="whereSql">过滤条件Sql</param>
        /// <param name="whereParam">过滤条件参数(参数名和参数值,例:new {Uname="joyet",Age = 110})</param>
        /// <returns></returns>
        public IList<T> Query(string queryColumns, string whereSql, object whereParam)
        {
            var result = SmartDbContext.Query<T>(queryColumns, whereSql, whereParam);
            return result;
        }


        /// <summary>
        /// 根据查询字段、过滤条件Sql、过滤条件参数查询数据
        /// </summary>
        /// <param name="queryColumns">要查询字段</param>
        /// <param name="whereSql">过滤条件Sql</param>
        /// <param name="whereParam">过滤条件参数(参数名和参数值,例:new {Uname="joyet",Age = 110})</param>
        /// <returns></returns>
        public async Task<IList<T>> QueryAsync(string queryColumns, string whereSql, object whereParam)
        {
            IList<T> result = new List<T>();
            await Task.Run(() =>
            {
                result = SmartDbContext.Query<T>(queryColumns, whereSql, whereParam);
            });
            return result;
        }

        /// <summary>
        /// 根据sql语句、过滤条件参数查询数据
        /// </summary>
        /// <param name="sql">sql语句/参数化SQL语句/存储过程</param>
        /// <param name="objParam">过滤条件参数(参数名和参数值,例:new {Uname="joyet",Age = 110})</param>
        /// <returns></returns>
        public IList<T2> Query<T2>(string sql, object objParam)
        {
            var result = SmartDbContext.Query<T2>(sql, objParam);
            return result;
        }

        /// <summary>
        /// 根据sql语句、过滤条件参数查询数据
        /// </summary>
        /// <param name="sql">sql语句/参数化SQL语句/存储过程</param>
        /// <param name="objParam">过滤条件参数(参数名和参数值,例:new {Uname="joyet",Age = 110})</param>
        /// <returns></returns>
        public async Task<IList<T2>> QueryAsync<T2>(string sql, object objParam)
        {
            IList<T2> result = new List<T2>();
            await Task.Run(() =>
            {
                result = SmartDbContext.Query<T2>(sql, objParam);
            });
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
        public async Task<PageResultEntity> QueryPageListAsync(string queryColumns, string sortColumn, string sortType, int pageSize, int pageIndex, string whereSql, object whereParam)
        {
            var result = new PageResultEntity();
            await Task.Run(() =>
            {
                result = SmartDbContext.QueryPageList<T>(queryColumns, sortColumn, sortType, pageSize, pageIndex, whereSql, whereParam);
            });
            return result;
        }

        #endregion
    }
}
