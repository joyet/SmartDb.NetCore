using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartDb.Mapper.NetCore;

namespace SmartDb.NetCore
{
    public abstract class SqlDbContext
    {
        #region private protected public  variables

        /// <summary>
        /// DbHelper
        /// </summary>
        public DbUtility DbHelper { get; set; }

        /// <summary>
        /// 基础SqlBuilder
        /// </summary>
        public SqlBuilder DbBuilder { get; set; }

        #endregion

        public SqlDbContext()
        {
            DbHelper = new DbUtility();
        }

        #region public   Methods

        /// <summary>
        /// 执行增、删、改操作,返回影响行数
        /// </summary>
        /// <param name="cmdText">SQL语句/存储过程/参数化SQL语句</param>
        /// <param name="dbParams">IDbDataParameter参数列表</param>
        /// <param name="cmdType">命令类型:SQL语句/存储过程</param>
        /// <returns>int</returns>
        public virtual int ExecuteNoneQuery(string cmdText, List<IDbDataParameter> dbParams = null, CommandType cmdType = CommandType.Text)
        {
            int result = 0;
            result = DbHelper.ExecuteNonQuery(cmdText, dbParams, cmdType);
            return result;
        }

        /// <summary>
        /// 执行增、删、改操作,返回影响行数
        /// </summary>
        /// <param name="cmdText">sql语句/参数化SQL语句/存储过程</param>
        /// <param name="objParam">字段名及字段值参数,例:new {Uname="joyet",Age = 110}</param>
        /// <param name="cmdType">命令类型:SQL语句/存储过程</param>
        /// <returns>int</returns>
        public virtual int ExecuteNoneQueryWithObjParam(string cmdText, object objParam = null, CommandType cmdType = CommandType.Text)
        {
            int result = 0;
            List<IDbDataParameter> dbParams = DbHelper.DbFactory.GetDbParamList(objParam);
            result = DbHelper.ExecuteNonQuery(cmdText, dbParams, cmdType);
            return result;
        }

        /// <summary>
        /// 执行查询操作,返回DataSet
        /// </summary>
        /// <param name="cmdText">SQL语句/存储过程/参数化SQL语句</param>
        /// <param name="dbParams">IDbDataParameter参数列表</param>
        /// <param name="cmdType">命令类型:SQL语句/存储过程</param>
        /// <returns>DataSet</returns>
        public virtual DataSet ExecuteQuery(string cmdText, List<IDbDataParameter> dbParams = null, CommandType cmdType = CommandType.Text)
        {
            DataSet result = null;
            result = DbHelper.ExecuteQuery(cmdText, dbParams, cmdType);
            return result;
        }

        /// <summary>
        /// 执行查询操作,返回DataSet
        /// </summary>
        /// <param name="cmdText">sql语句/参数化SQL语句/存储过程</param>
        /// <param name="objParam">字段名及字段值参数,例:new {Uname="joyet",Age = 110}</param>
        /// <param name="cmdType">命令类型:SQL语句/存储过程</param>
        /// <returns>DataSet</returns>
        public virtual DataSet ExecuteQueryWithObjParam(string cmdText, object objParam = null, CommandType cmdType = CommandType.Text)
        {
            DataSet result = null;
            List<IDbDataParameter> dbParams = DbHelper.DbFactory.GetDbParamList(objParam);
            result = DbHelper.ExecuteQuery(cmdText, dbParams, cmdType);
            return result;
        }

        /// <summary>
        /// 执行查询操作,返回IDataReader
        /// </summary>
        /// <param name="cmdText">SQL语句/存储过程/参数化SQL语句</param>
        /// <param name="dbParams">IDbDataParameter参数列表</param>
        /// <param name="cmdType">命令类型:SQL语句/存储过程</param>
        /// <returns>IDataReader</returns>
        public virtual IDataReader ExecuteReader(string cmdText, List<IDbDataParameter> dbParams = null, CommandType cmdType = CommandType.Text)
        {
            IDataReader result = null;
            result = DbHelper.ExecuteReader(cmdText, dbParams, cmdType);
            return result;
        }

        /// <summary>
        /// 执行查询操作,返回IDataReader
        /// </summary>
        /// <param name="cmdText">sql语句/参数化SQL语句/存储过程</param>
        /// <param name="objParam">字段名及字段值参数,例:new {Uname="joyet",Age = 110}</param>
        /// <param name="cmdType">命令类型:SQL语句/存储过程</param>
        /// <returns>IDataReader</returns>
        public virtual IDataReader ExecuteReaderWithObjParam(string cmdText, object objParam = null, CommandType cmdType = CommandType.Text)
        {
            IDataReader result = null;
            List<IDbDataParameter> dbParams = DbHelper.DbFactory.GetDbParamList(objParam);
            result = DbHelper.ExecuteReader(cmdText, dbParams, cmdType);
            return result;
        }

        /// <summary>
        /// 执行查询操作,返回object
        /// </summary>
        /// <param name="cmdText">SQL语句/存储过程/参数化SQL语句</param>
        /// <param name="dbParams">IDbDataParameter参数列表</param>
        /// <param name="cmdType">命令类型:SQL语句/存储过程</param>
        /// <returns>object</returns>
        public virtual object ExecuteScalar(string cmdText, List<IDbDataParameter> dbParams = null, CommandType cmdType = CommandType.Text)
        {
            object result = null;
            result = DbHelper.ExecuteScalar(cmdText, dbParams, cmdType);
            return result;
        }

        /// <summary>
        /// 执行查询操作,返回object
        /// </summary>
        /// <param name="cmdText">sql语句/参数化SQL语句/存储过程</param>
        /// <param name="objParam">字段名及字段值参数,例:new {Uname="joyet",Age = 110}</param>
        /// <param name="cmdType">命令类型:SQL语句/存储过程</param>
        /// <returns>object</returns>
        public virtual object ExecuteScalarWithObjParam(string cmdText, object objParam = null, CommandType cmdType = CommandType.Text)
        {
            object result = null;
            List<IDbDataParameter> dbParams = DbHelper.DbFactory.GetDbParamList(objParam);
            result = DbHelper.ExecuteScalar(cmdText, dbParams, cmdType);
            return result;
        }

        /// <summary>
        /// 添加单条实体对象数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual int Insert<T>(T entity)
        {
            int result = 0;
            var dbEntity = DbBuilder.Insert<T>(entity);
            if (dbEntity == null)
            {
                return result;
            }
            result = DbHelper.ExecuteNonQuery(dbEntity.CommandText, dbEntity.DbParams);
            return result;
        }

        /// <summary>
        /// 根据主键删除实体对应数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual int Delete<T>(object id)
        {
            int result = 0;
            var dbEntity = DbBuilder.Delete<T>(id);
            if (dbEntity == null)
            {
                return result;
            }
            result = DbHelper.ExecuteNonQuery(dbEntity.CommandText, dbEntity.DbParams);
            return result;
        }

        /// <summary>
        /// 根据过滤条件Sql、过滤条件字段名及字段值参数删除实体对应数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="whereSql">过滤条件Sql</param>
        /// <param name="whereParam">过滤条件字段名及字段值参数,例:new {Uname="joyet",Age = 110}</param>
        /// <returns></returns>
        public virtual int Delete<T>(string whereSql, object whereParam)
        {
            int result = 0;
            var dbEntity = DbBuilder.Delete<T>(whereSql,whereParam);
            if (dbEntity == null)
            {
                return result;
            }
            result = DbHelper.ExecuteNonQuery(dbEntity.CommandText, dbEntity.DbParams);
            return result;
        }

        /// <summary>
        /// 根据实体主键值修改其它字段数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual int Update<T>(T entity)
        {
            int result = 0;
            var dbEntity = DbBuilder.Update<T>(entity);
            if (dbEntity == null)
            {
                return result;
            }
            result = DbHelper.ExecuteNonQuery(dbEntity.CommandText, dbEntity.DbParams);
            return result;
        }

        /// <summary>
        ///  修改字段名及字段值参数、主键值修改实体对应数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="updateParam">修改字段名及字段值参数,例:new {Uname="joyet",Age = 110}</param>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual int Update<T>(object updateParams, object id)
        {
            int result = 0;
            var dbEntity = DbBuilder.Update<T>(updateParams, id);
            if (dbEntity == null)
            {
                return result;
            }
            result = DbHelper.ExecuteNonQuery(dbEntity.CommandText, dbEntity.DbParams);
            return result;
        }

        /// <summary>
        /// 根据修改字段名及字段值参数、过滤条件Sql、过滤条件字段名及字段值参数修改实体对应数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="updateParam">修改字段名及字段值参数,例:new {Uname="joyet",Age = 110}</param>
        /// <param name="whereSql">过滤条件Sql</param>
        /// <param name="whereParam">过滤条件字段名及字段值参数,例:new {UserId=1}</param>
        /// <returns></returns>
        public virtual int Update<T>(object updateParams,string whereSql, object whereParams)
        {
            int result = 0;
            var dbEntity = DbBuilder.Update<T>(updateParams, whereSql, whereParams);
            if (dbEntity == null)
            {
                return result;
            }
            result = DbHelper.ExecuteNonQuery(dbEntity.CommandText, dbEntity.DbParams);
            return result;
        }

        /// <summary>
        /// 根据主键查询实体对应数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T Query<T>(object id)
        {
            T result = default(T);
            var dbEntity = DbBuilder.Query<T>(id);
            if (dbEntity == null)
            {
                return result;
            }
            using (var reader = DbHelper.ExecuteReader(dbEntity.CommandText, dbEntity.DbParams))
            {
                result = DataReaderToEntity<T>(reader);
            }
            return result;
        }

        /// <summary>
        /// 根据查询字段、过滤条件Sql、过滤条件字段名及字段值参数修改实体对应数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryColumns">要查询字段</param>
        /// <param name="whereSql">过滤条件Sql</param>
        /// <param name="whereParam">过滤条件字段名及字段值参数,例:new {Uname="joyet",Age = 110}</param>
        /// <returns></returns>
        public virtual List<T> Query<T>(string queryColumns,string whereSql, object whereParam)
        {
            List<T> result = null;
            var dbEntity = DbBuilder.Query<T>(queryColumns, whereSql, whereParam);
            if (dbEntity == null)
            {
                return result;
            }
            using (var reader = DbHelper.ExecuteReader(dbEntity.CommandText, dbEntity.DbParams))
            {
                result = DataReaderToEntityList<T>(reader);
            }
            return result;
        }

        /// <summary>
        /// 根据sql语句、字段名及字段值参数查询实体对应数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">sql语句/参数化SQL语句/存储过程</param>
        /// <param name="objParam">字段名及字段值参数,例:new {Uname="joyet",Age = 110}</param>
        /// <returns></returns>
        public virtual List<T> Query<T>(string sql, object objParam)
        {
            List<T> result = null;
            var dbEntity = DbBuilder.Query<T>(sql, objParam);
            if (dbEntity == null)
            {
                return result;
            }
            using (var reader = DbHelper.ExecuteReader(dbEntity.CommandText, dbEntity.DbParams))
            {
                result = DataReaderToEntityList<T>(reader);
            }
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
        public virtual PageResultEntity QueryPageList<T>(string queryColumns,string sortColumn,string sortType, long pageSize, long pageIndex,string whereSql, object whereObjParam)
        {
            PageResultEntity result = new PageResultEntity();
            if (pageSize <= 0|| pageIndex<=0)
            {
                return result;
            }
            result.PageSize = pageSize;
            result.CurrentPageIndex = pageIndex;

            #region 为了分页查询效率，查询第一页时才会查询所有条数
            if (result.CurrentPageIndex==1)
            {
                var totalPageDbEntity = DbBuilder.QueryTotalPageCount<T>(whereSql, whereObjParam);
                if (totalPageDbEntity == null)
                {
                    return result;
                }
                var objTotalCount = DbHelper.ExecuteScalar(totalPageDbEntity.CommandText, totalPageDbEntity.DbParams);
                if (objTotalCount == null)
                {
                    return result;
                }
                result.TotalCount = Convert.ToInt64(objTotalCount);
                if (result.TotalCount <= 0)
                {
                    return result;
                }
            }
            #endregion

            var dbEntity = DbBuilder.QueryPageList<T>(queryColumns,sortColumn,sortType,pageSize,pageIndex, whereSql, whereObjParam);
            if (dbEntity == null)
            {
                return result;
            }
            using (var reader = DbHelper.ExecuteReader(dbEntity.CommandText, dbEntity.DbParams))
            {
                var datas = DataReaderToEntityList<T>(reader);
                result.Data = datas;
                result = SetPageListResult<T>(result);
            }
            return result;
        }

        /// <summary>
        /// 开启事务
        /// </summary>
        public virtual void BeginTransaction()
        {
            DbHelper.BeginTransaction();
        }

        /// <summary>
        /// 提交事务
        /// </summary>
        public virtual void CommitTransaction()
        {
            DbHelper.CommitTransaction();
        }

        /// <summary>
        /// 回滚事务
        /// </summary>
        public virtual void RollbackTransaction()
        {
            DbHelper.RollbackTransaction();
        }

        /// <summary>
        /// IDataReader转化为实体
        /// </summary>
        /// <typeparam name="Entity"></typeparam>
        /// <param name="reader"></param>
        /// <returns></returns>
        public virtual T DataReaderToEntity<T>(IDataReader reader)
        {
            T entity = default(T);
            List<T> entityList = DataReaderToEntityList<T>(reader);
            if (entityList == null)
            {
                return entity;
            }
            if (entityList.Count == 0)
            {
                return entity;
            }
            entity = entityList[0];
            return entity;
        }

        /// <summary>
        /// IDataReader转化为实体列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader"></param>
        /// <returns></returns>
        public virtual List<T> DataReaderToEntityList<T>(IDataReader reader)
        {
            List<T> entityList = null;
            if (reader == null)
            {
                return entityList;
            }
            entityList = new List<T>();
            DataReaderMapper<T> readBuild = DataReaderMapper<T>.GetInstance(reader);
            while (reader.Read())
            {
                entityList.Add(readBuild.Map(reader));
            }
            return entityList;
        }

        /// <summary>
        /// DataTable转化为实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public virtual T DataTableToEntity<T>(DataTable dataTable)
        {
            T entity = default(T);
            List<T> entityList = DataTableToEntityList<T>(dataTable);
            if (entityList == null)
            {
                return entity;
            }
            if (entityList.Count == 0)
            {
                return entity;
            }
            entity = entityList[0];
            return entity;
        }

        /// <summary>
        ///  DataTable转化为实体列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public virtual List<T> DataTableToEntityList<T>(DataTable dt)
        {
            List<T> entityList = null;
            if (dt == null)
            {
                return entityList;
            }
            entityList = new List<T>();
            foreach (DataRow dataRow in dt.Rows)
            {
                DataTableMapper<T> dataRowMapper = DataTableMapper<T>.GetInstance(dataRow);
                var entity = dataRowMapper.Map(dataRow);
                if (entity != null)
                {
                    entityList.Add(dataRowMapper.Map(dataRow));
                }
            }
            return entityList;
        }

        /// <summary>
        /// 设置分页结果
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pageResult"></param>
        /// <returns></returns>
        public virtual PageResultEntity SetPageListResult<T>(PageResultEntity pageResult)
        {
            if (pageResult.Data == null)
            {
                return pageResult;
            }
            var dataList = pageResult.Data as List<T>;
            if (dataList.Count<= 0)
            {
                pageResult.TotalPageIndex = 0;
                return pageResult;
            }
            else if (pageResult.TotalCount>0&&pageResult.TotalCount <= pageResult.PageSize)
            {
                pageResult.TotalPageIndex = 1;
                return pageResult;
            }
            else
            {
                if (pageResult.TotalCount % pageResult.PageSize == 0)
                {
                    pageResult.TotalPageIndex = pageResult.TotalCount / pageResult.PageSize;
                }
                else
                {
                    pageResult.TotalPageIndex = pageResult.TotalCount / pageResult.PageSize + 1;
                }
            }
            return pageResult;
        }

        #endregion
    }
}
