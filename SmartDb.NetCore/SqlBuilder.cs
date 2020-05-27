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
        public  SqlDbFactory DbFactory { get; set; }

        /// <summary>
        /// SmartDb数据库类型
        /// </summary>
        public  SmartDbTypes CurrentDbType { get; set; }

        /// <summary>
        /// 添加单条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="isGetAutoIncrementValue"></param>
        /// <returns></returns>
        public virtual DbEntity Insert<T>(T entity, bool isGetAutoIncrementValue = false)
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
            var noAutoIncrementColumns = columns.Where(a => !a.IsAutoIncrement&&!a.IsIgnoreAdd).ToList();
            var dbOperator = DbFactory.GetDbParamOperator();
            var dbParams = new List<IDbDataParameter>();
            var sqlBuilder = new StringBuilder("insert into {tableName}({columnNames}) values({columnValues});");
            sqlBuilder.Replace("{tableName}", tableEntity.TableName);

            #region 处理添加字段及参数SQL语句
            var addItem = HandleAddColumnValueParam(noAutoIncrementColumns, sqlBuilder,dbParams);
            sqlBuilder = addItem.Item1;
            dbParams = addItem.Item2;
            #endregion

            #region 处理自动增长列Sql
            var autoIncrementColumn = columns.Where(a => a.IsAutoIncrement).FirstOrDefault();
            if (autoIncrementColumn != null&& isGetAutoIncrementValue)
            {
                var dbTypeValue = Convert.ToInt32(CurrentDbType);
                switch (dbTypeValue)
                {
                    case 1:
                    case 2:
                        sqlBuilder.Append(GetAutoIncrementSql());
                        break;
                    case 3:
                        sqlBuilder.AppendFormat(GetAutoIncrementSql(), autoIncrementColumn.ColumnName);
                        break;
                    case 4:
                        sqlBuilder.AppendFormat(GetAutoIncrementSql(), tableEntity.TableName);
                        break;
                }
            }
            #endregion

            dbEntity = new DbEntity()
            {
                TableEntity = tableEntity,
                CommandText = sqlBuilder.ToString(),
                DbParams = dbParams
            };
            return dbEntity;
        }

        /// <summary>
        /// 根据主键值删除数据
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
            StringBuilder sqlBuilder = new StringBuilder("delete from {tableName} {whereCriteria}");
            sqlBuilder.Replace("{tableName}", tableEntity.TableName);

            //处理过滤字段参数
            var whereItem = HandleWhereParam("",new List<TableColumnAttribute>() { pkColumn }, sqlBuilder, dbParams);
            sqlBuilder = whereItem.Item1;
            dbParams = whereItem.Item2;

            dbEntity = new DbEntity()
            {
                TableEntity = tableEntity,
                CommandText = sqlBuilder.ToString(),
                DbParams = dbParams
            };
            return dbEntity;
        }

        /// <summary>
        /// 根据过滤条件参数(参数名和参数值)删除数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="whereSql">过滤条件Sql</param>
        /// <param name="whereParam">过滤条件参数(参数名和参数值),例:new {Uname="joyet",Age = 110}</param>
        /// <returns></returns>
        public virtual DbEntity Delete<T>(string whereSql,object whereParam)
        {
            DbEntity dbEntity = null;
            Type type = typeof(T);
            var attributeBuilder = new AttributeBuilder();
            var tableEntity = attributeBuilder.GetTableInfo(type);
            var dbOperatore = DbFactory.GetDbParamOperator();
            var dbParams = new List<IDbDataParameter>();
            var whereColumns = attributeBuilder.GetColumnInfos(whereParam);
            StringBuilder sqlBuilder = new StringBuilder("delete from {tableName} {whereCriteria}");
            sqlBuilder.Replace("{tableName}", tableEntity.TableName);

            //处理过滤字段参数
            var whereItem = HandleWhereParam(whereSql, whereColumns, sqlBuilder, dbParams);
            sqlBuilder = whereItem.Item1;
            dbParams = whereItem.Item2;

            dbEntity = new DbEntity()
            {
                TableEntity = tableEntity,
                CommandText = sqlBuilder.ToString(),
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
            if (columns.Count <0)
            {
                return dbEntity;
            }
            var pkColumn = columns.Where(a => a.IsPrimaryKey).FirstOrDefault();
            var updateColumns = columns.Where(a => !a.IsPrimaryKey).ToList();
            if (pkColumn == null|| updateColumns.Count<0)
            {
                return dbEntity;
            }
            var dbOperator = DbFactory.GetDbParamOperator();
            var dbParams = new List<IDbDataParameter>();
            var sqlBuilder = new StringBuilder("update {tableName} set {updateCriteria} {whereCriteria}");
            sqlBuilder.Replace("{tableName}", tableEntity.TableName);

            //处理更改字段参数
            var updateItem = HandleUpdateParam(updateColumns, sqlBuilder, dbParams);
            sqlBuilder = updateItem.Item1;
            dbParams = updateItem.Item2;

            //处理过滤字段参数
            var whereItem = HandleWhereParam("",new List<TableColumnAttribute>() { pkColumn }, sqlBuilder, dbParams);
            sqlBuilder = whereItem.Item1;
            dbParams = whereItem.Item2;

            dbEntity = new DbEntity()
            {
                TableEntity = tableEntity,
                CommandText = sqlBuilder.ToString(),
                DbParams = dbParams
            };
            return dbEntity;
        }

        /// <summary>
        ///  根据修改字段参数(参数名和参数值)、主键值修改数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="updateParam">修改字段参数(参数名和参数值),例:new {Uname="joyet",Age = 110}</param>
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
            var sqlBuilder = new StringBuilder("update {tableName} set {updateCriteria} {whereCriteria}");
            sqlBuilder.Replace("{tableName}", tableEntity.TableName);

            //处理更改字段参数
            var updateItem = HandleUpdateParam(updateColumns, sqlBuilder, dbParams);
            sqlBuilder = updateItem.Item1;
            dbParams = updateItem.Item2;

            //处理过滤字段参数
            var whereItem = HandleWhereParam("",new List<TableColumnAttribute>() { pkColumn }, sqlBuilder, dbParams);
            sqlBuilder = whereItem.Item1;
            dbParams = whereItem.Item2;

            dbEntity = new DbEntity()
            {
                TableEntity = tableEntity,
                CommandText = sqlBuilder.ToString(),
                DbParams = dbParams,
            };
            return dbEntity;
        }

        /// <summary>
        /// 根据要修改字段参数(参数名和参数值)、过滤字段参数(参数名和参数值)修改数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="updateParam">要修改字段参数(参数名和参数值),例:new {Uname="joyet",Age = 110}</param>
        /// <param name="whereSql">过滤条件Sql</param>
        /// <param name="whereParam">过滤字段参数(参数名和参数值),例:new {UserId=1}</param>
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
            var sqlBuilder = new StringBuilder("update {tableName} set {updateCriteria} {whereCriteria}");
            sqlBuilder.Replace("{tableName}", tableEntity.TableName);

            //处理更改字段参数
            var updateItem = HandleUpdateParam(updateColumns, sqlBuilder, dbParams);
            sqlBuilder = updateItem.Item1;
            dbParams = updateItem.Item2;

            //处理过滤字段参数
            var whereItem = HandleWhereParam(whereSql,whereColumns, sqlBuilder, dbParams);
            sqlBuilder = whereItem.Item1;
            dbParams = whereItem.Item2;

            dbEntity = new DbEntity()
            {
                TableEntity = tableEntity,
                CommandText = sqlBuilder.ToString(),
                DbParams = dbParams,
            };
            return dbEntity;
        }

        /// <summary>
        ///根据主键值查询数据
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
            StringBuilder sqlBuilder = new StringBuilder("select * from {tableName} {whereCriteria}");
            sqlBuilder.Replace("{tableName}", tableEntity.TableName); 

            //处理过滤字段参数
            var whereItem = HandleWhereParam("",new List<TableColumnAttribute>() { pkColumn }, sqlBuilder, dbParams);
            sqlBuilder = whereItem.Item1;
            dbParams = whereItem.Item2;

            dbEntity = new DbEntity()
            {
                TableEntity = tableEntity,
                CommandText = sqlBuilder.ToString(),
                DbParams = dbParams,
            };
            return dbEntity;
        }

        /// <summary>
        /// 根据查询字段、过滤条件参数(参数名和参数值)查询数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryColumns">要查询字段</param>
        /// <param name="whereSql">过滤条件Sql</param>
        /// <param name="whereParam">过滤条件参数(参数名和参数值),例:new {Uname="joyet",Age = 110}</param>
        /// <returns></returns>
        public virtual DbEntity Query<T>(string queryColumns, string whereSql, object whereParam)
        {
            DbEntity dbEntity = null;
            if (string.IsNullOrEmpty(queryColumns))
            {
                queryColumns = "*";
            }
            Type type = typeof(T);
            var attributeBuilder = new AttributeBuilder();
            var tableEntity = attributeBuilder.GetTableInfo(type);
            string dbOperator = DbFactory.GetDbParamOperator();
            var dbParams = new List<IDbDataParameter>();
            List<TableColumnAttribute> whereColumns = new AttributeBuilder().GetColumnInfos(whereParam);
            StringBuilder sqlBuilder = new StringBuilder("select {queryColumns} from {tableName} {whereCriteria}");
            sqlBuilder.Replace("{tableName}", tableEntity.TableName);

            //处理查询字段参数
            var queryColumnItem = HandleQueryColumnParam(queryColumns, "", sqlBuilder);
            sqlBuilder = queryColumnItem.Item1;

            //处理过滤字段参数
            var whereItem = HandleWhereParam(whereSql,whereColumns, sqlBuilder, dbParams);
            sqlBuilder = whereItem.Item1;
            dbParams = whereItem.Item2;

            dbEntity = new DbEntity()
            {
                TableEntity = tableEntity,
                CommandText = sqlBuilder.ToString(),
                DbParams = dbParams,
            };
            return dbEntity;
        }

        /// <summary>
        /// 根据sql语句、过滤条件参数(参数名和参数值)查询数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">sql语句/参数化SQL语句/存储过程</param>
        /// <param name="objParam">过滤条件参数(参数名和参数值),例:new {Uname="joyet",Age = 110}</param>
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
        /// <param name="whereParam">过滤条件参数(参数名和参数值),例:new {Uname="joyet",Age = 110}</param>
        /// <returns></returns>
        public virtual DbEntity QueryPageList<T>(string queryColumns,string sortColumn, string sortType, int pageSize, int pageIndex, string whereSql, object whereParam)
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
        /// 根据过滤条件参数(参数名和参数值)查询数据总数量
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="whereSql">过滤条件Sql</param>
        /// <param name="whereParam">过滤条件参数(参数名和参数值),例:new {Uname="joyet",Age = 110}</param>
        /// <returns></returns>
        public virtual DbEntity QueryTotalPageCount<T>(string whereSql, object whereParam)
        {
            DbEntity dbEntity = null;
            Type type = typeof(T);
            var attributeBuilder = new AttributeBuilder();
            var tableEntity = attributeBuilder.GetTableInfo(type);
            var dbParams = new List<IDbDataParameter>();
            string dbOperator = DbFactory.GetDbParamOperator();
            List<TableColumnAttribute> whereColumns = attributeBuilder.GetColumnInfos(whereParam);
            StringBuilder sqlBuilder = new StringBuilder("select count(*) from  {tableName} {whereCriteria}");
            sqlBuilder.Replace("{tableName}", tableEntity.TableName);

            //处理过滤字段参数
            var whereItem = HandleWhereParam(whereSql,whereColumns, sqlBuilder, dbParams);
            sqlBuilder = whereItem.Item1;
            dbParams = whereItem.Item2;

            dbEntity = new DbEntity()
            {
                TableEntity = tableEntity,
                CommandText = sqlBuilder.ToString(),
                DbParams = dbParams,
            };
            return dbEntity;
        }

        /// <summary>
        /// 设置数据库自动增长sql
        /// </summary>
        /// <returns></returns>
        public virtual string GetAutoIncrementSql()
        {
            return "";
        }

        /// <summary>
        /// 处理查询字段
        /// </summary>
        /// <param name="queryColumns"></param>
        /// <param name="tableAlias"></param>
        /// <param name="sqlBuild"></param>
        /// <returns></returns>
        public Tuple<StringBuilder> HandleQueryColumnParam(string queryColumns, string tableAlias, StringBuilder sqlBuild)
        {
            var queryColumnBuilder = new StringBuilder();
            if (string.IsNullOrEmpty(queryColumns))
            {
                queryColumns = "*";
            }
            if (queryColumns.IndexOf(',') > 0)
            {
                var columnNameList = queryColumns.Split(',').Where(a => !string.IsNullOrEmpty(a)).ToList();
                for (var i = 0; i < columnNameList.Count; i++)
                {
                    var queryColumnName = columnNameList[i];
                    queryColumnBuilder.Append(string.IsNullOrEmpty(tableAlias) ? queryColumnName : tableAlias + "." + queryColumnName);
                    if (i != columnNameList.Count - 1)
                    {
                        queryColumnBuilder.Append(",");
                    }
                }
            }
            else
            {
                queryColumnBuilder.Append(string.IsNullOrEmpty(tableAlias) ? queryColumns : tableAlias + "." + queryColumns);
            }
            sqlBuild.Replace("{queryColumns}", queryColumnBuilder.ToString());
            return new Tuple<StringBuilder>(sqlBuild);
        }

        /// <summary>
        /// 处理updateParam参数
        /// </summary>
        /// <param name="updateColumns"></param>
        /// <param name="sqlBuild"></param>
        /// <param name="dbParams"></param>
        /// <returns></returns>
        public Tuple<StringBuilder, List<IDbDataParameter>> HandleUpdateParam(List<TableColumnAttribute> updateColumns,StringBuilder sqlBuild, List<IDbDataParameter> dbParams)
        {
            StringBuilder updateSqlBuilder = new StringBuilder();
            var dbOperator = DbFactory.GetDbParamOperator();
            for (int i = 0; i < updateColumns.Count; i++)
            {
                var columnItem = updateColumns[i];
                updateSqlBuilder.AppendFormat("{0}={1}{0}", columnItem.ColumnName, dbOperator);
                if (i != updateColumns.Count - 1)
                {
                    updateSqlBuilder.Append(",");
                }
                dbParams.Add(DbFactory.GetDbParam(columnItem));
            }
            sqlBuild.Replace("{updateCriteria}", updateSqlBuilder.ToString());
            return new Tuple<StringBuilder, List<IDbDataParameter>>(sqlBuild, dbParams);
        }

        /// <summary>
        /// 处理whereParam参数
        /// </summary>
        /// <param name="whereSql">过滤SQL</param>
        /// <param name="whereColumns"></param>
        /// <param name="sqlBuild"></param>
        /// <param name="dbParams"></param>
        /// <returns></returns>
        public Tuple<StringBuilder, List<IDbDataParameter>> HandleWhereParam(string whereSql, List<TableColumnAttribute> whereColumns,StringBuilder sqlBuild, List<IDbDataParameter> dbParams)
        {
            StringBuilder wherSqlBuild = new StringBuilder();
            var dbOperator = DbFactory.GetDbParamOperator();
            if (string.IsNullOrEmpty(whereSql)&& whereColumns.Count<=0)
            {
                sqlBuild.Replace("{whereCriteria}", wherSqlBuild.ToString());
                return new Tuple<StringBuilder, List<IDbDataParameter>>(sqlBuild, dbParams);
            }
            wherSqlBuild.Append(" where ");
            if (string.IsNullOrEmpty(whereSql) && whereColumns.Count > 0)
            {
                for (int i = 0; i < whereColumns.Count; i++)
                {
                    var columnItem = whereColumns[i];
                    if (i != 0)
                    {
                        wherSqlBuild.Append("and ");
                    }
                    wherSqlBuild.AppendFormat("{0}={1}{0} ", columnItem.ColumnName, dbOperator);
                    dbParams.Add(DbFactory.GetDbParam(columnItem));
                }
            }
            else if (!string.IsNullOrEmpty(whereSql) && whereColumns.Count<=0)
            {
                wherSqlBuild.Append(whereSql);
            }
            else if (!string.IsNullOrEmpty(whereSql) && whereColumns.Count>0)
            {
                wherSqlBuild.Append(whereSql);
                for (int i = 0; i < whereColumns.Count; i++)
                {
                    var columnItem = whereColumns[i];
                    dbParams.Add(DbFactory.GetDbParam(columnItem));
                }
            }
            sqlBuild.Replace("{whereCriteria}", wherSqlBuild.ToString());
            return new Tuple<StringBuilder, List<IDbDataParameter>>(sqlBuild, dbParams);
        }

        /// <summary>
        /// 处理添加字段值及参数
        /// </summary>
        /// <param name="noAutoIncrementColumns"></param>
        /// <param name="sqlBuild"></param>
        /// <param name="dbParams"></param>
        /// <returns></returns>
        public Tuple<StringBuilder,List<IDbDataParameter>> HandleAddColumnValueParam(List<TableColumnAttribute> noAutoIncrementColumns, StringBuilder sqlBuild, List<IDbDataParameter> dbParams)
        {
            var noDefaultColumns = noAutoIncrementColumns.Where(a => !a.IsSetDefaultValue).ToList();
            var defaultColumns = noAutoIncrementColumns.Where(a => a.IsSetDefaultValue && a.DefaultValue != null).ToList();
            List<TableColumnAttribute> columnList = new List<TableColumnAttribute>();
            if (noDefaultColumns.Count > 0)
            {
                columnList.AddRange(noDefaultColumns);
            }
            if (defaultColumns.Count > 0)
            {
                foreach (var defaultColumn in defaultColumns)
                {
                    defaultColumn.ColumnValue = defaultColumn.DefaultValue;
                    columnList.Add(defaultColumn);
                }
            }
            var dbOperator = DbFactory.GetDbParamOperator();
            var columnNameBuilder = new StringBuilder();
            var columnValueBuilder = new StringBuilder();
            for (int i = 0; i < columnList.Count; i++)
            {
                var column = columnList[i];
                columnNameBuilder.Append(column.ColumnName);
                columnValueBuilder.AppendFormat("{0}{1}",dbOperator,column.ColumnName);
                if (i != columnList.Count - 1)
                {
                    columnNameBuilder.Append(",");
                    columnValueBuilder.Append(",");
                }
                dbParams.Add(DbFactory.GetDbParam(column));
            }
            sqlBuild.Replace("{columnNames}", columnNameBuilder.ToString());
            sqlBuild.Replace("{columnValues}", columnValueBuilder.ToString());
            return new Tuple<StringBuilder, List<IDbDataParameter>>(sqlBuild, dbParams);
        }

    }
}
