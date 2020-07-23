using SmartDb.NetCore;
using SmartDb.Repository.NetCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace SmartDb.Repository.NetCore
{
   public class BaseRepository<T, IdType>:IBaseRepository<T,IdType>
    {
       public SqlDbContext DbContext { get; set; }

        #region 添加数据相关方法

        /// <summary>
        /// 添加单条数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Insert(T entity)
        {
            var result = DbContext.Insert(entity);
            return result;
        }

        /// <summary>
        /// 添加单条数据异步方法
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> InsertAsync(T entity)
        {
            var result = await Task.Run(() =>
            {
                return DbContext.Insert(entity); 
            });
            return result;
        }

        /// <summary>
        /// 添加单条数据(返回自动增长值)
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="isGetAutoIncrementValue"></param>
        /// <returns></returns>
        public object Insert(T entity, bool isGetAutoIncrementValue)
        {
            var result = DbContext.Insert(entity, isGetAutoIncrementValue);
            return result;
        }

        /// <summary>
        /// 添加单条数据异步方法(返回自动增长值)
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="isGetAutoIncrementValue"></param>
        /// <returns></returns>
        public async Task<object> InsertAsync(T entity, bool isGetAutoIncrementValue)
        {
            object result=await Task.Run(() =>
            {
                return DbContext.Insert(entity, isGetAutoIncrementValue);
            });
            return result;
        }

        /// <summary>
        ///  批量添加数据
        /// </summary>
        /// <param name="entityList"></param>
        /// <returns></returns>
        public int Insert(IList<T> entityList)
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
        /// 批量添加数据异步方法
        /// </summary>
        /// <param name="entityList"></param>
        /// <returns></returns>
        public async Task<int> InsertAsync(IList<T> entityList)
        {
            var result= await Task.Run(() => {
                var i = 0;
                foreach (var entity in entityList)
                {
                    DbContext.Insert(entity);
                    i++;
                }
                return i;
            });
            return result;
        }

        /// <summary>
        /// 批量添加数据(返回自动增长值列表)
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
        /// 批量添加数据异步方法(返回自动增长值列表)
        /// </summary>
        /// <param name="entityList"></param>
        /// <param name="isGetAutoIncrementValue"></param>
        /// <returns></returns>
        public async Task<IList<object>> InsertAsync(IList<T> entityList, bool isGetAutoIncrementValue)
        {
            var result = await Task.Run(() =>
            {
                IList<object> items = new List<object>();
                foreach (var entity in entityList)
                {
                    items.Add(Insert(entity, isGetAutoIncrementValue));
                }
                return items;
            });
            return result;
        }

        #endregion

        #region 删除数据相关方法

        /// <summary>
        /// 删除单条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Delete(IdType id)
        {
            int result = DbContext.Delete<T>(id);
            return result;
        }

        /// <summary>
        /// 删除单条数据异步方法
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<int> DeleteAsync(IdType id)
        {
            var result=await Task.Run(() => {
               return DbContext.Delete<T>(id); 
            });
            return result;
        }

        /// <summary>
        /// 批量删除数据
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        public int Delete(IList<IdType> idList)
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
        /// 批量删除数据异步方法
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        public async Task<int> DeleteAsync(IList<IdType> idList)
        {
            var result=await Task.Run(() => {
                var i = 0;
                foreach (var id in idList)
                {
                    DbContext.Delete<T>(id);
                    i++;
                }
                return i;
            });
            return result;
        }

        /// <summary>
        /// 删除一或多条数据
        /// </summary>
        /// <param name="whereSql">过滤条件Sql</param>
        /// <param name="whereParam">过滤条件参数(参数名和参数值,例:new {Uname="joyet",Age = 110})</param>
        /// <returns></returns>
        public int Delete(string whereSql, object whereParam)
        {
            var result = 0;
            result=DbContext.Delete<T>(whereSql, whereParam);
            return result;
        }

        /// <summary>
        ///删除一或多条数据异步方法
        /// </summary>
        /// <param name="whereSql">过滤条件Sql</param>
        /// <param name="whereParam">过滤条件参数(参数名和参数值,例:new {Uname="joyet",Age = 110})</param>
        /// <returns></returns>
        public async Task<int> DeleteAsync(string whereSql, object whereParam)
        {
            var result=await Task.Run(() => {
               return DbContext.Delete<T>(whereSql, whereParam); ;
            });
            return result;
        }
        #endregion

        #region 修改数据相关方法

        /// <summary>
        /// 修改单条数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Update(T entity)
        {
            var result = 0;
            result=DbContext.Update(entity);
            return result;
        }

        /// <summary>
        /// 修改单条数据异步方法
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> UpdateAsync(T entity)
        {
            var result = await Task.Run(() => {
                return DbContext.Update(entity); 
            });
            return result;
        }

        /// <summary>
        ///  修改单条数据
        /// </summary>
        /// <param name="updateParam">修改字段参数(参数名和参数值,例:new {Uname="joyet",Age = 110})</param>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Update(object updateParam, IdType id)
        {
            var result = 0;
            result = DbContext.Update<T>(updateParam, id);
            return result;
        }

        /// <summary>
        ///  修改单条数据异步方法
        /// </summary>
        /// <param name="updateParam">修改字段参数(参数名和参数值,例:new {Uname="joyet",Age = 110})</param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<int> UpdateAsync(object updateParam, IdType id)
        {
            var result=await Task.Run(() => {
               return DbContext.Update<T>(updateParam, id);
            });
            return result;
        }

        /// <summary>
        /// 批量修改数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Update(List<T> entityList)
        {
            var result = 0;
            foreach (var entity in entityList)
            {
                 DbContext.Update(entity);
                 result++;
            }
            return result;
        }

        /// <summary>
        /// 批量修改数据异步方法
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> UpdateAsync(List<T> entityList)
        {
            var result=await Task.Run(() => {
                var i = 0;
                foreach (var entity in entityList)
                {
                    DbContext.Update(entity);
                    i++;
                }
                return i;
            });
            return result;
        }

        /// <summary>
        /// 修改一或多条数据
        /// </summary>
        /// <param name="updateParam">修改字段参数(参数名和参数值,例:new {Uname="joyet",Age = 110})</param>
        /// <param name="whereSql">过滤条件Sql</param>
        /// <param name="whereParam">过滤条件参数(参数名和参数值,例:new {UserId=1})</param>
        /// <returns></returns>
        public int Update(object updateParam, string whereSql, object whereParam)
        {
            var result = 0;
            result=DbContext.Update<T>(updateParam, whereSql,whereParam);
            return result;
        }

        /// <summary>
        /// 修改一或多条数据异步方法
        /// </summary>
        /// <param name="updateParam">修改字段参数(参数名和参数值,例:new {Uname="joyet",Age = 110})</param>
        /// <param name="whereSql">过滤条件Sql</param>
        /// <param name="whereParam">过滤条件参数(参数名和参数值,例:new {UserId=1})</param>
        /// <returns></returns>
        public async Task<int> UpdateAsync(object updateParam, string whereSql, object whereParam)
        {
            var result = await Task.Run(() => {
                return DbContext.Update<T>(updateParam, whereSql, whereParam);
            });
            return result;
        }

        #endregion

        #region 查询数据相关方法

        /// <summary>
        ///查询单条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T Query(IdType id)
        {
            var result = DbContext.Query<T>(id);
            return result;
        }

        /// <summary>
        ///查询单条数据异步方法
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> QueryAsync(IdType id)
        {
            var result= await Task.Run(() =>
            {
                return DbContext.Query<T>(id); 
            });
            return result;
        }

        /// <summary>
        /// 查询一或多条数据
        /// </summary>
        /// <param name="queryColumns">要查询字段</param>
        /// <param name="whereSql">过滤条件Sql</param>
        /// <param name="whereParam">过滤条件参数(参数名和参数值,例:new {Uname="joyet",Age = 110})</param>
        /// <returns></returns>
        public IList<T> Query(string queryColumns, string whereSql, object whereParam)
        {
            var result = DbContext.Query<T>(queryColumns, whereSql, whereParam);
            return result;
        }

        /// <summary>
        /// 查询一或多条数据异步方法
        /// </summary>
        /// <param name="queryColumns">要查询字段</param>
        /// <param name="whereSql">过滤条件Sql</param>
        /// <param name="whereParam">过滤条件参数(参数名和参数值,例:new {Uname="joyet",Age = 110})</param>
        /// <returns></returns>
        public async Task<IList<T>> QueryAsync(string queryColumns, string whereSql, object whereParam)
        {
            var result =await Task.Run(() =>
            {
                return DbContext.Query<T>(queryColumns, whereSql, whereParam);
            });
            return result;
        }

        /// <summary>
        ///  查询一或多条数据
        /// </summary>
        /// <param name="sql">sql语句/参数化SQL语句/存储过程</param>
        /// <param name="objParam">过滤条件参数(参数名和参数值,例:new {Uname="joyet",Age = 110})</param>
        /// <returns></returns>
        public IList<T2> Query<T2>(string sql, object objParam)
        {
            var result = DbContext.Query<T2>(sql, objParam);
            return result;
        }

        /// <summary>
        /// 查询一或多条数据异步方法
        /// </summary>
        /// <param name="sql">sql语句/参数化SQL语句/存储过程</param>
        /// <param name="objParam">过滤条件参数(参数名和参数值,例:new {Uname="joyet",Age = 110})</param>
        /// <returns></returns>
        public async Task<IList<T2>> QueryAsync<T2>(string sql, object objParam)
        {
            var result = await Task.Run(() =>
            {
                return DbContext.Query<T2>(sql, objParam); 
            });
            return result;
        }

        /// <summary>
        /// 单表分页数据查询
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
            var result = DbContext.QueryPageList<T>(queryColumns, sortColumn, sortType, pageSize, pageIndex, whereSql, whereParam);
            return result;
        }

        /// <summary>
        /// 单表分页数据查询异步方法
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
            var result = await Task.Run(() =>
            {
               return DbContext.QueryPageList<T>(queryColumns, sortColumn, sortType, pageSize, pageIndex, whereSql, whereParam);
            });
            return result;
        }

        #endregion
    }
}
