using SmartDb.NetCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace SmartDb.Repository.NetCore
{
    public interface IBaseRepository<T,IdType>
    {
        SqlDbContext DbContext { get; set; }

        #region 添加数据相关方法

        /// <summary>
        /// 添加单条数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Insert(T entity);

        /// <summary>
        /// 添加单条数据异步方法
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<int> InsertAsync(T entity);

        /// <summary>
        /// 添加单条数据,返回自动增长值或影响行数
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="isGetAutoIncrementValue"></param>
        /// <returns></returns>
        object Insert(T entity, bool isGetAutoIncrementValue);

        /// <summary>
        /// 添加单条数据异步方法,返回自动增长值或影响行数
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="isGetAutoIncrementValue"></param>
        /// <returns></returns>
        Task<object> InsertAsync(T entity, bool isGetAutoIncrementValue);

        /// <summary>
        /// 批量添加数据
        /// </summary>
        /// <param name="entityList"></param>
        /// <returns></returns>
        int Insert(IList<T> entityList);

        /// <summary>
        /// 批量添加数据异步方法
        /// </summary>
        /// <param name="entityList"></param>
        /// <returns></returns>
        Task<int> InsertAsync(IList<T> entityList);

        /// <summary>
        /// 批量添加数据,返回自动增长值或影响行数列表
        /// </summary>
        /// <param name="entityList"></param>
        /// <param name="isGetAutoIncrementValue"></param>
        /// <returns></returns>
        IList<object> Insert(IList<T> entityList, bool isGetAutoIncrementValue);

        /// <summary>
        /// 批量添加数据异步方法,返回自动增长值或影响行数列表
        /// </summary>
        /// <param name="entityList"></param>
        /// <param name="isGetAutoIncrementValue"></param>
        /// <returns></returns>
        Task<IList<object>> InsertAsync(IList<T> entityList, bool isGetAutoIncrementValue);

        #endregion

        #region 删除数据相关方法

        /// <summary>
        /// 根据主键值删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int Delete(IdType id);

        /// <summary>
        /// 根据主键值删除数据异步方法
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<int> DeleteAsync(IdType id);

        /// <summary>
        /// 根据主键值列表删除数据
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        int Delete(IList<IdType> idList);

        // <summary>
        /// 根据主键值列表删除数据异步方法
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        Task<int> DeleteAsync(IList<IdType> idList);

        /// <summary>
        /// 根据过滤条件Sql和过滤条件参数删除数据
        /// </summary>
        /// <param name="whereSql">过滤条件Sql</param>
        /// <param name="whereParam">过滤条件参数(参数名和参数值,例:new {Uname="joyet",Age = 110})</param>
        /// <returns></returns>
        int Delete(string whereSql, object whereParam);

        // <summary>
        /// 根据过滤条件Sql和过滤条件参数删除数据异步方法
        /// </summary>
        /// <param name="whereSql">过滤条件Sql</param>
        /// <param name="whereParam">过滤条件参数(参数名和参数值,例:new {Uname="joyet",Age = 110})</param>
        /// <returns></returns>
        Task<int> DeleteAsync(string whereSql, object whereParam);

        #endregion

        #region 修改数据相关方法

        /// <summary>
        /// 根据实体主键值修改其它字段数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Update(T entity);

        /// <summary>
        /// 根据实体主键值修改其它字段数据异步方法
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<int> UpdateAsync(T entity);

        /// <summary>
        ///  根据修改字段参数、主键值修改数据
        /// </summary>
        /// <param name="updateParam">修改字段参数(参数名和参数值,例:new {Uname="joyet",Age = 110})</param>
        /// <param name="id"></param>
        /// <returns></returns>
        int Update(object updateParam, IdType id);

        /// <summary>
        ///  根据修改字段参数、主键值修改数据异步方法
        /// </summary>
        /// <param name="updateParam">修改字段参数(参数名和参数值,例:new {Uname="joyet",Age = 110})</param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<int> UpdateAsync(object updateParam, IdType id);

        /// <summary>
        /// 根据修改字段参数、过滤条件Sql、过滤条件参数修改数据
        /// </summary>
        /// <param name="updateParam">修改字段参数(参数名和参数值,例:new {Uname="joyet",Age = 110})</param>
        /// <param name="whereSql">过滤条件Sql</param>
        /// <param name="whereParam">过滤条件参数(参数名和参数值,例:new {UserId=1})</param>
        /// <returns></returns>
        int Update(object updateParam, string whereSql, object whereParam);

        /// <summary>
        /// 根据修改字段参数、过滤条件Sql、过滤条件参数修改数据异步方法
        /// </summary>
        /// <param name="updateParam">修改字段参数(参数名和参数值,例:new {Uname="joyet",Age = 110})</param>
        /// <param name="whereSql">过滤条件Sql</param>
        /// <param name="whereParam">过滤条件参数(参数名和参数值,例:new {UserId=1})</param>
        /// <returns></returns>
        Task<int> UpdateAsync(object updateParam, string whereSql, object whereParam);

        #endregion

        #region 查询数据相关方法

        /// <summary>
        ///根据主键值查询数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T Query(IdType id);

        /// <summary>
        ///根据主键值查询数据异步方法
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> QueryAsync(IdType id);

        /// <summary>
        /// 根据查询字段、过滤条件Sql、过滤条件参数查询数据
        /// </summary>
        /// <param name="queryColumns">要查询字段</param>
        /// <param name="whereSql">过滤条件Sql</param>
        /// <param name="whereParam">过滤条件参数(参数名和参数值,例:new {Uname="joyet",Age = 110})</param>
        /// <returns></returns>
        IList<T> Query(string queryColumns, string whereSql, object whereParam);

        /// <summary>
        /// 根据查询字段、过滤条件Sql、过滤条件参数查询数据异步方法
        /// </summary>
        /// <param name="queryColumns">要查询字段</param>
        /// <param name="whereSql">过滤条件Sql</param>
        /// <param name="whereParam">过滤条件参数(参数名和参数值,例:new {Uname="joyet",Age = 110})</param>
        /// <returns></returns>
        Task<IList<T>> QueryAsync(string queryColumns, string whereSql, object whereParam);

        /// <summary>
        /// 根据sql语句、过滤条件参数查询数据
        /// </summary>
        /// <param name="sql">sql语句/参数化SQL语句/存储过程</param>
        /// <param name="objParam">过滤条件参数(参数名和参数值,例:new {Uname="joyet",Age = 110})</param>
        /// <returns></returns>
        IList<T2> Query<T2>(string sql, object objParam);

        /// <summary>
        /// 根据sql语句、过滤条件参数查询数据异步方法
        /// </summary>
        /// <param name="sql">sql语句/参数化SQL语句/存储过程</param>
        /// <param name="objParam">过滤条件参数(参数名和参数值,例:new {Uname="joyet",Age = 110})</param>
        /// <returns></returns>
        Task<IList<T2>> QueryAsync<T2>(string sql, object objParam);

        /// <summary>
        /// 单表分页数据
        /// </summary>
        /// <param name="queryColumns">要查询字段</param>
        /// <param name="sortColumn">排序字段</param>
        /// <param name="sortType">排序方式</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">第几页</param>
        /// <param name="whereSql">过滤条件Sql</param>
        /// <param name="whereParam">过滤条件参数(参数名和参数值,,例:new {Uname="joyet",Age = 110})</param>
        /// <returns></returns>
        PageResultEntity QueryPageList(string queryColumns, string sortColumn, string sortType, int pageSize, int pageIndex, string whereSql, object whereParam);

        /// <summary>
        /// 单表分页数据异步方法
        /// </summary>
        /// <param name="queryColumns">要查询字段</param>
        /// <param name="sortColumn">排序字段</param>
        /// <param name="sortType">排序方式</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">第几页</param>
        /// <param name="whereSql">过滤条件Sql</param>
        /// <param name="whereParam">过滤条件参数(参数名和参数值,,例:new {Uname="joyet",Age = 110})</param>
        /// <returns></returns>

        Task<PageResultEntity> QueryPageListAsync(string queryColumns, string sortColumn, string sortType, int pageSize, int pageIndex, string whereSql, object whereParam);

        #endregion
    }
}
