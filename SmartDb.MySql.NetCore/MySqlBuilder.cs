﻿using SmartDb.NetCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq;


namespace SmartDb.MySql.NetCore
{
    public class MySqlBuilder : SqlBuilder
    {
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
        public override DbEntity QueryPageList<T>(string  queryColumns, string sortColumn, string sortType, int pageSize, int pageIndex,string whereSql, object whereParam)
        {
            DbEntity dbEntity = null;
            dbEntity = base.QueryPageList<T>(queryColumns,sortColumn,sortType,pageSize,pageIndex, whereSql, whereParam);
            if (dbEntity == null)
            {
                return dbEntity;
            }
            var type = typeof(T);
            var attributeBuilder = new AttributeBuilder();
            var tableEntity = attributeBuilder.GetTableInfo(type);
            dbEntity.TableEntity = tableEntity;
            var startNum = pageSize * (pageIndex - 1);
            var dbOperator = DbFactory.GetDbParamOperator();
            var whereColumns = attributeBuilder.GetColumnInfos(whereParam);
            var dbParams = new List<IDbDataParameter>();

            //分页查询模板
            var sqlBuilder = new StringBuilder();
            sqlBuilder.AppendLine("select  {queryColumns} from");
            sqlBuilder.AppendLine("(");
            sqlBuilder.AppendLine("select {sortColumn} from {tableName} {whereCriteria} order by {sortColumn} {sortType} limit {startNum},{pageSize}");
            sqlBuilder.AppendLine(")");
            sqlBuilder.AppendLine("a inner join {tableName} b on a.{sortColumn}=b.{sortColumn} order by b.{sortColumn} {sortType}");
            sqlBuilder.Replace("{sortColumn}", sortColumn);
            sqlBuilder.Replace("{tableName}", tableEntity.TableName);
            sqlBuilder.Replace("{sortType}", sortType);
            sqlBuilder.Replace("{startNum}", startNum.ToString());
            sqlBuilder.Replace("{pageSize}", pageSize.ToString());

            //处理查询字段参数
            var queryColumnItem = HandleQueryColumnParam(queryColumns, "b", sqlBuilder);
            sqlBuilder = queryColumnItem.Item1;

            //处理过滤字段参数
            var whereItem = HandleWhereParam(whereSql, whereColumns, sqlBuilder, dbParams);
            sqlBuilder = whereItem.Item1;
            dbParams = whereItem.Item2;

            dbEntity.CommandText = sqlBuilder.ToString();
            dbEntity.DbParams = dbParams;
            return dbEntity;
        }

        /// <summary>
        /// 设置数据库自动增长sql
        /// </summary>
        /// <returns></returns>
        public override string GetAutoIncrementSql()
        {
            string sql = "select last_insert_id();";
            return sql;
        }
    }
}
