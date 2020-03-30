using SmartDb.NetCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SmartDb.Repository.NetCore
{
    public interface ISmartDbRepository<T,IdType>
    {
        /// <summary>
        /// 添加单条数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        object Insert(T entity);

        /// <summary>
        /// 批量添加数据
        /// </summary>
        /// <param name="entityList"></param>
        /// <returns></returns>
        IList<object> Insert(List<T> entityList);

        /// <summary>
        /// 根据主键值删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int Delete(IdType id);

        /// <summary>
        /// 根据主键值列表删除数据
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        int Delete(List<IdType> idList);

        /// <summary>
        /// 根据过滤条件Sql和过滤条件参数删除数据
        /// </summary>
        /// <param name="whereSql">过滤条件Sql</param>
        /// <param name="whereParam">过滤条件参数(参数名和参数值,例:new {Uname="joyet",Age = 110})</param>
        /// <returns></returns>
        int Delete(string whereSql, object whereParam);

        /// <summary>
        /// 根据实体主键值修改其它字段数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Update(T entity);

        /// <summary>
        ///  根据修改字段参数、主键值修改数据
        /// </summary>
        /// <param name="updateParam">修改字段参数(参数名和参数值,例:new {Uname="joyet",Age = 110})</param>
        /// <param name="id"></param>
        /// <returns></returns>
        int Update(object updateParams, IdType id);

        /// <summary>
        /// 根据修改字段参数、过滤条件Sql、过滤条件参数修改数据
        /// </summary>
        /// <param name="updateParam">修改字段参数(参数名和参数值,例:new {Uname="joyet",Age = 110})</param>
        /// <param name="whereSql">过滤条件Sql</param>
        /// <param name="whereParam">过滤条件参数(参数名和参数值,例:new {UserId=1})</param>
        /// <returns></returns>
        int Update(object updateParam, string whereSql, object whereParam);

        /// <summary>
        ///根据主键值查询数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T Query(IdType id);

        /// <summary>
        /// 根据查询字段、过滤条件Sql、过滤条件参数查询数据
        /// </summary>
        /// <param name="queryColumns">要查询字段</param>
        /// <param name="whereSql">过滤条件Sql</param>
        /// <param name="whereParam">过滤条件参数(参数名和参数值,例:new {Uname="joyet",Age = 110})</param>
        /// <returns></returns>
        List<T> Query(string queryColumns, string whereSql, object whereParam);

        /// <summary>
        /// 根据sql语句、过滤条件参数查询数据
        /// </summary>
        /// <param name="sql">sql语句/参数化SQL语句/存储过程</param>
        /// <param name="objParam">过滤条件参数(参数名和参数值,例:new {Uname="joyet",Age = 110})</param>
        /// <returns></returns>
        List<T2> Query<T2>(string sql, object objParam);

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
        /// 创建数据库参数对象
        /// </summary>
        /// <param name="dbParamName"></param>
        /// <param name="dbParamValue"></param>
        /// <param name="dbPramDbType"></param>
        /// <param name="dbPramDbTypeLength"></param>
        /// <returns></returns>
        IDbDataParameter GetDbParam(string dbParamName, object dbParamValue, object dbPramDbType = null, int dbPramDbTypeLength = 0);
       
        /// <summary>
        /// 返回数据库参数对象列表
        /// </summary>
        /// <param name="objParam"></param>
        /// <returns></returns>
        List<IDbDataParameter> GetDbParamList(object objParam);

        /// <summary>
        /// 创建数据库参数化关键字操作符
        /// </summary>
        /// <returns></returns>
        string GetDbParamOperator();
    }
}
