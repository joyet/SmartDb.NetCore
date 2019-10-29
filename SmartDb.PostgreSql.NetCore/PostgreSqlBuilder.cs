using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Collections;
using System.Linq;
using SmartDb;
using SmartDb.NetCore;

namespace SmartDb.PostgreSql.NetCore
{
   public class PostgreSqlBuilder : SqlBuilder
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
        public override DbEntity QueryPageList<T>(string queryColumns, string sortColumn, string sortType, long pageSize, long pageIndex, string whereSql, object whereParam)
        {
            DbEntity dbEntity = null;
            Type type = typeof(T);
            var attributeBuilder = new AttributeBuilder();
            var tableEntity = attributeBuilder.GetTableInfo(type);
            dbEntity = new DbEntity()
            {
                TableEntity = tableEntity
            };
            var startNum = pageSize * (pageIndex - 1);
            var dbOperator = DbFactory.GetDbParamOperator();
            List<TableColumnAttribute> whereColumns = attributeBuilder.GetColumnInfos(whereParam);
            var dbParams = new List<IDbDataParameter>();

            //分页查询模板
            var queryTemplate = @"select  {queryColumns} from 
(
	select {sortColumn} from {tableName} {whereCriteria} order by {sortColumn} {sortType} limit {pageSize} offset {startNum}
) 
a inner join {tableName} b on a.{sortColumn}=b.{sortColumn} order by b.{sortColumn} {sortType};";
            StringBuilder sqlBuild = new StringBuilder(queryTemplate);
            sqlBuild.Replace("{sortColumn}", sortColumn);
            sqlBuild.Replace("{tableName}", tableEntity.TableName);
            sqlBuild.Replace("{sortType}", sortType);
            sqlBuild.Replace("{startNum}", startNum.ToString());
            sqlBuild.Replace("{pageSize}", pageSize.ToString());
            var queryItem = HandleQueryColumParam(queryColumns, "b", sqlBuild);
            sqlBuild = queryItem.Item1;
            var whereItem = HandleWhereParam(whereSql, whereColumns, sqlBuild, dbParams);
            sqlBuild = whereItem.Item1;
            dbParams = whereItem.Item2;
            dbEntity.CommandText = sqlBuild.ToString();
            dbEntity.DbParams = dbParams;
            return dbEntity;
        }

    }
}
