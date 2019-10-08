using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartDb.NetCore
{
   public abstract class SqlBuilder
    {
        /// <summary>
        /// 数据库工厂
        /// </summary>
        public virtual SqlDbFactory DbFactory { get; set; }

        /// <summary>
        /// 添加当前实体对应数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public virtual DbEntity Insert<T>(T entity)
        {
            DbEntity dbEntity = null;
            if (entity == null)
            {
                return dbEntity;
            }
            Type type = typeof(T);
            var attributeBuilder = new AttributeBuilder();
            TableAttribute tableEntity= attributeBuilder.GetTableInfo(type);
            var columns = new AttributeBuilder().GetColumnInfos(type, entity);
            if (columns==null||columns.Count<0)
            {
                return dbEntity;
            }
            if (columns.Where(a => !a.IsAutoIncrement).FirstOrDefault() == null)
            {
                return dbEntity;
            }
            var noneAutoIncrementColumns = columns.Where(a => !a.IsAutoIncrement).ToList();
            var dbOperator = DbFactory.GetDbParamOperator();
            var dbParams = new List<IDbDataParameter>();
            var sqlBuild = new StringBuilder("insert into {tableName}({columnNames}) values({columnValues})");
            sqlBuild.Replace("{tableName}", tableEntity.TableName);
            var columnNameList = new List<string>();
            var paramColumnList = new List<string>();
            foreach (var column in noneAutoIncrementColumns)
            {
                columnNameList.Add(column.ColumnName);
                paramColumnList.Add(dbOperator + column.ColumnName);
                dbParams.Add(DbFactory.GetDbParam(column));
            }
            sqlBuild.Replace("{columnNames}", string.Join(",", columnNameList));
            sqlBuild.Replace("{columnValues}",string.Join(",", paramColumnList));
            dbEntity = new DbEntity()
            {
                TableEntity = tableEntity,
                CommandText = sqlBuild.ToString(),
                DbParams = dbParams
            };
            return dbEntity;
        }

        /// <summary>
        /// 根据主键删除实体对应数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual DbEntity Delete<T>(object id)
        {
            DbEntity dbEntity = null;
            if (id == null)
            {
                return dbEntity;
            }
            Type type = typeof(T);
            var attributeBuilder = new AttributeBuilder();
            var tableEntity = attributeBuilder.GetTableInfo(type);
            var pkColumn = attributeBuilder.GetPkColumnInfo(type);
            if (pkColumn == null)
            {
                return dbEntity;
            }
            pkColumn.ColumnValue = id;
            string dbOperator = DbFactory.GetDbParamOperator();
            var dbParams = new List<IDbDataParameter>();
            StringBuilder sqlBuild = new StringBuilder("delete from {tableName} where {pkColumn}={dbOperator}{pkColumn}");
            sqlBuild.Replace("{tableName}", tableEntity.TableName);
            sqlBuild.Replace("{pkColumn}", pkColumn.ColumnName);
            sqlBuild.Replace("{dbOperator}", dbOperator);
            dbParams.Add(DbFactory.GetDbParam(pkColumn));
            dbEntity = new DbEntity()
            {
                TableEntity = tableEntity,
                CommandText = sqlBuild.ToString(),
                DbParams = dbParams
            };
            return dbEntity;
        }

        /// <summary>
        /// 根据过滤条件Sql、过滤条件字段名及字段值参数删除实体对应数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="whereSql">过滤条件Sql</param>
        /// <param name="whereParam">过滤条件字段名及字段值参数,例:new {Uname="joyet",Age = 110}</param>
        /// <returns></returns>
        public virtual DbEntity Delete<T>(string whereSql, object whereParam)
        {
            DbEntity dbEntity = null;
            Type type = typeof(T);
            var attributeBuilder = new AttributeBuilder();
            var tableEntity = attributeBuilder.GetTableInfo(type);
            var dbOperatore = DbFactory.GetDbParamOperator();
            var dbParams = new List<IDbDataParameter>();
            var whereColumns = attributeBuilder.GetColumnInfos(whereParam);
            StringBuilder sqlBuild = new StringBuilder("delete from {tableName} {whereCriteria}");
            sqlBuild.Replace("{tableName}", tableEntity.TableName);
            HandleWhereParam(whereSql, whereColumns, ref sqlBuild, ref dbParams);
            dbEntity = new DbEntity()
            {
                TableEntity = tableEntity,
                CommandText = sqlBuild.ToString(),
                DbParams = dbParams
            };
            return dbEntity;
        }

        /// <summary>
        /// 根据实体主键值修改其它字段数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual DbEntity Update<T>(T entity)
        {
            DbEntity dbEntity = null;
            if (entity == null)
            {
                return dbEntity;
            }
            var type = typeof(T);
            var attributeBuilder = new AttributeBuilder();
            var tableEntity = attributeBuilder.GetTableInfo(type);
            var columns = attributeBuilder.GetColumnInfos(type, entity);
            if (columns==null||columns.Count <0)
            {
                return dbEntity;
            }
            var pkColumn = columns.Where(a => a.IsPrimaryKey).FirstOrDefault();
            if (pkColumn == null|| columns.Where(a => !a.IsPrimaryKey).FirstOrDefault() == null)
            {
                return dbEntity;
            }
            var updateColumns = columns.Where(a => !a.IsPrimaryKey).ToList();
            var dbOperator = DbFactory.GetDbParamOperator();
            var dbParams = new List<IDbDataParameter>();
            var sqlBuild = new StringBuilder("update {tableName} set {updateCriteria} where {pkColumn}={dbOperator}{pkColumn}");
            sqlBuild.Replace("{tableName}", tableEntity.TableName);
            sqlBuild.Replace("{pkColumn}", pkColumn.ColumnName);
            sqlBuild.Replace("{dbOperator}", dbOperator);
            HandleUpdateParam(updateColumns, ref sqlBuild, ref dbParams);
            dbParams.Add(DbFactory.GetDbParam(pkColumn));
            dbEntity = new DbEntity()
            {
                TableEntity = tableEntity,
                CommandText = sqlBuild.ToString(),
                DbParams = dbParams
            };
            return dbEntity;
        }

        /// <summary>
        ///  修改字段名及字段值参数、主键值修改实体对应数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="updateParam">修改字段名及字段值参数,例:new {Uname="joyet",Age = 110}</param>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual DbEntity Update<T>(object updateParam, object id)
        {
            DbEntity dbEntity = null;
            if (updateParam == null|| id == null)
            {
                return dbEntity;
            }
            Type type = typeof(T);
            var attributeBuilder = new AttributeBuilder();
            var tableEntity = attributeBuilder.GetTableInfo(type);
            var updateColumns = attributeBuilder.GetColumnInfos(updateParam);
            var pkColumn = attributeBuilder.GetPkColumnInfo(type);
            if (updateColumns.Count < 0|| pkColumn == null)
            {
                return dbEntity;
            }
            pkColumn.ColumnValue = id;
            var dbOperator = DbFactory.GetDbParamOperator();
            var dbParams = new List<IDbDataParameter>();
            StringBuilder sqlBuild = new StringBuilder("update {tableName} set {updateCriteria}  where {pkColumn}={dbOperator}{pkColumn}");
            sqlBuild.Replace("{tableName}", tableEntity.TableName);
            sqlBuild.Replace("{pkColumn}", pkColumn.ColumnName);
            sqlBuild.Replace("{dbOperator}", dbOperator);
            HandleUpdateParam(updateColumns, ref sqlBuild, ref dbParams);
            dbParams.Add(DbFactory.GetDbParam(pkColumn));
            dbEntity = new DbEntity()
            {
                TableEntity = tableEntity,
                CommandText = sqlBuild.ToString(),
                DbParams = dbParams,
            };
            return dbEntity;
        }

        /// <summary>
        /// 根据修改字段名及字段值参数、过滤条件Sql、过滤条件字段名及字段值参数修改实体对应数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="updateParam">修改字段名及字段值参数,例:new {Uname="joyet",Age = 110}</param>
        /// <param name="whereSql">过滤条件Sql</param>
        /// <param name="whereParam">过滤条件字段名及字段值参数,例:new {UserId=1}</param>
        /// <returns></returns>
        public virtual DbEntity Update<T>(object updateParam, string whereSql, object whereParam)
        {
            DbEntity dbEntity = null;
            Type type = typeof(T);
            if (updateParam == null)
            {
                return dbEntity;
            }
            var attributeBuilder = new AttributeBuilder();
            var tableEntity = attributeBuilder.GetTableInfo(type);
            List<TableColumnAttribute> updateColumns= attributeBuilder.GetColumnInfos(updateParam);
            if (updateColumns.Count<0)
            {
                return dbEntity;
            }
            string dbOperator = DbFactory.GetDbParamOperator();
            var dbParams = new List<IDbDataParameter>();
            List<TableColumnAttribute> whereColumns = attributeBuilder.GetColumnInfos(whereParam);
            StringBuilder sqlBuild = new StringBuilder("update {tableName} set {updateCriteria} {whereCriteria}");
            sqlBuild.Replace("{tableName}", tableEntity.TableName);
            HandleUpdateParam(updateColumns, ref sqlBuild, ref dbParams);
            HandleWhereParam(whereSql, whereColumns, ref sqlBuild, ref dbParams);
            dbEntity = new DbEntity()
            {
                TableEntity = tableEntity,
                CommandText = sqlBuild.ToString(),
                DbParams = dbParams,
            };
            return dbEntity;
        }

        /// <summary>
        ///根据主键查询实体对应数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual DbEntity Query<T>(object id)
        {
            DbEntity dbEntity = null;
            Type type = typeof(T);
            var attributeBuilder = new AttributeBuilder();
            var tableEntity = attributeBuilder.GetTableInfo(type);
            TableColumnAttribute pkColumn =attributeBuilder.GetPkColumnInfo(type);
            if (pkColumn == null)
            {
                return dbEntity;
            }
            pkColumn.ColumnValue = id;
            string dbOperator = DbFactory.GetDbParamOperator();
            List<IDbDataParameter> dbParams = new List<IDbDataParameter>();
            StringBuilder sqlBuild = new StringBuilder("select * from {tableName} where {pkColumn}={dbOperator}{pkColumn}");
            sqlBuild.Replace("{tableName}", tableEntity.TableName);
            sqlBuild.Replace("{pkColumn}", pkColumn.ColumnName);
            sqlBuild.Replace("{dbOperator}", dbOperator);
            dbParams.Add(DbFactory.GetDbParam(pkColumn));
            dbEntity = new DbEntity()
            {
                TableEntity = tableEntity,
                CommandText = sqlBuild.ToString(),
                DbParams = dbParams,
            };
            return dbEntity;
        }

        /// <summary>
        /// 根据查询字段、过滤条件Sql、过滤条件字段名及字段值参数修改实体对应数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryColumns">要查询字段</param>
        /// <param name="whereSql">过滤条件Sql</param>
        /// <param name="whereParam">过滤条件字段名及字段值参数,例:new {Uname="joyet",Age = 110}</param>
        /// <returns></returns>
        public virtual DbEntity Query<T>(string queryColumns,string whereSql, object whereObjParam)
        {
            DbEntity dbEntity = null;
            if (string.IsNullOrEmpty(queryColumns))
            {
                return dbEntity;
            }
            Type type = typeof(T);
            var attributeBuilder = new AttributeBuilder();
            var tableEntity = attributeBuilder.GetTableInfo(type);
            string dbOperator = DbFactory.GetDbParamOperator();
            var dbParams = new List<IDbDataParameter>();
            List<TableColumnAttribute> whereColumns = new AttributeBuilder().GetColumnInfos(whereObjParam);
            StringBuilder sqlBuild = new StringBuilder("select {queryColumns} from {tableName} {whereCriteria}");
            sqlBuild.Replace("{tableName}", tableEntity.TableName);
            HandleQueryColumParam(queryColumns,"",ref sqlBuild);
            HandleWhereParam(whereSql, whereColumns,ref sqlBuild,ref dbParams);
            dbEntity = new DbEntity()
            {
                TableEntity = tableEntity,
                CommandText = sqlBuild.ToString(),
                DbParams = dbParams,
            };
            return dbEntity;
        }

        /// <summary>
        /// 根据sql语句、字段名及字段值参数查询实体对应数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">sql语句/参数化SQL语句/存储过程</param>
        /// <param name="objParam">字段名及字段值参数,例:new {Uname="joyet",Age = 110}</param>
        /// <returns></returns>
        public virtual DbEntity Query<T>(string sql, object objParam)
        {
            DbEntity dbEntity = null;
            if (string.IsNullOrEmpty(sql))
            {
                return dbEntity;
            }
            Type type = typeof(T);
            var attributeBuilder = new AttributeBuilder();
            var tableEntity = attributeBuilder.GetTableInfo(type);
            dbEntity = new DbEntity()
            {
                TableEntity = tableEntity,
                CommandText = sql,
                DbParams = DbFactory.GetDbParamList(objParam)
            };
            return dbEntity;
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
        public virtual DbEntity QueryPageList<T>(string queryColumns,string sortColumn, string sortType, long pageSize, long pageIndex, string whereSql, object whereObjParam)
        {
            DbEntity dbEntity = null;
            if (string.IsNullOrEmpty(sortColumn) || string.IsNullOrEmpty(sortType))
            {
                return dbEntity;
            }
            if (pageSize <= 0 || pageIndex <= 0)
            {
                return dbEntity;
            }
            dbEntity = new DbEntity();
            return dbEntity;
        }

        /// <summary>
        /// 根据查询条件、where参数查询实体对应数据总数量
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="whereSql">过滤条件Sql</param>
        /// <param name="whereParam">过滤条件字段名及字段值参数,例:new {Uname="joyet",Age = 110}</param>
        /// <returns></returns>
        public virtual DbEntity QueryTotalPageCount<T>(string whereSql, object whereObjParam)
        {
            DbEntity dbEntity = null;
            Type type = typeof(T);
            var attributeBuilder = new AttributeBuilder();
            var tableEntity = attributeBuilder.GetTableInfo(type);
            var dbParams = new List<IDbDataParameter>();
            string dbOperator = DbFactory.GetDbParamOperator();
            List<TableColumnAttribute> whereColumns = attributeBuilder.GetColumnInfos(whereObjParam);
            StringBuilder sqlBuild = new StringBuilder("select count(*) from  {tableName} {whereCriteria}");
            sqlBuild.Replace("{tableName}", tableEntity.TableName);
            HandleWhereParam(whereSql, whereColumns, ref sqlBuild, ref dbParams);
            dbEntity = new DbEntity()
            {
                TableEntity = tableEntity,
                CommandText = sqlBuild.ToString(),
                DbParams = dbParams,
            };
            return dbEntity;
        }

        /// <summary>
        /// 处理查询字段
        /// </summary>
        /// <param name="queryColumns"></param>
        /// <param name="sqlBuild"></param>
        public  void HandleQueryColumParam(string queryColumns, string tableAlias, ref StringBuilder sqlBuild)
        {
            var queryColumnList = new List<string>();
            if (queryColumns.IndexOf(',') > 0)
            {
                queryColumnList = queryColumns.Split(',').Where(a => !string.IsNullOrEmpty(a)).ToList();
                for (int i = 0; i < queryColumnList.Count; i++)
                {
                    queryColumnList[i] = string.IsNullOrEmpty(tableAlias) ? queryColumnList[i] : tableAlias + "." + queryColumnList[i];
                }
            }
            else
            {
                queryColumnList.Add(string.IsNullOrEmpty(tableAlias)? queryColumns: tableAlias+"."+ queryColumns);
            }
            sqlBuild.Replace("{queryColumns}", string.Join(",", queryColumnList));
        }

        /// <summary>
        /// 处理实体处理updateParam参数
        /// </summary>
        /// <param name="updateColumns"></param>
        /// <param name="sqlBuild"></param>
        /// <param name="dbParams"></param>
        public  void HandleUpdateParam(List<TableColumnAttribute> updateColumns, ref StringBuilder sqlBuild, ref List<IDbDataParameter> dbParams)
        {
            StringBuilder updateSqlBuild = new StringBuilder();
            if (updateColumns.Count > 0)
            {
                var dbOperator = DbFactory.GetDbParamOperator();
                var paramColumnList = new List<string>();
                foreach (var columnItem in updateColumns)
                {
                    var paramColumn = string.Format("{0}={1}{0}", columnItem.ColumnName, dbOperator);
                    paramColumnList.Add(paramColumn);
                    dbParams.Add(DbFactory.GetDbParam(columnItem));
                }
                updateSqlBuild.Append(string.Join(",", paramColumnList));
            }
            sqlBuild.Replace("{updateCriteria}", updateSqlBuild.ToString());
        }

        /// <summary>
        /// 处理实体whereParam参数
        /// </summary>
        /// <param name="whereColumns"></param>
        /// <param name="sqlBuild"></param>
        /// <param name="dbParams"></param>
        public  void HandleWhereParam(string whereSql, List<TableColumnAttribute> whereColumns, ref StringBuilder sqlBuild, ref List<IDbDataParameter> dbParams)
        {
            StringBuilder wherSqlBuild = new StringBuilder();
            if (!string.IsNullOrEmpty(whereSql))
            {
                wherSqlBuild.AppendFormat(" where " + whereSql);
            }
            if (whereColumns.Count>0)
            {
                foreach (var columnItem in whereColumns)
                {
                    dbParams.Add(DbFactory.GetDbParam(columnItem));
                }
            } 
            sqlBuild.Replace("{whereCriteria}", wherSqlBuild.ToString());
        }

    }
}
