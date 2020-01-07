using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartDb.NetCore
{
   public abstract  class SqlDbFactory
    { 
        /// <summary>
        /// 创建数据库连接对象
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public abstract IDbConnection GetConnection(string connectionString);

        /// <summary>
        /// 创建适配器对象
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public abstract IDataAdapter GetDataAdapter(IDbCommand cmd);

        /// <summary>
        /// 创建数据库参数对象
        /// </summary>
        /// <param name="tableColumn"></param>
        /// <returns></returns>
        public virtual IDbDataParameter GetDbParam(TableColumnAttribute tableColumn)
        {
            IDbDataParameter dbParam = null;
            if (tableColumn == null)
            {
                return dbParam;
            }
            dbParam = GetDbParam(tableColumn.ColumnName, tableColumn.ColumnValue, tableColumn.DataType, tableColumn.MaxLength);
            return dbParam;
        }

        /// <summary>
        /// 创建数据库参数对象
        /// </summary>
        /// <param name="dbParamName"></param>
        /// <param name="dbParamValue"></param>
        /// <param name="dbPramDbType"></param>
        /// <param name="dbPramDbTypeLength"></param>
        /// <returns></returns>
        public abstract IDbDataParameter GetDbParam(string dbParamName,object dbParamValue,object dbPramDbType=null,int dbPramDbTypeLength = 0);

        /// <summary>
        /// 返回数据库参数对象列表
        /// </summary>
        /// <param name="objParam"></param>
        /// <returns></returns>
        public virtual List<IDbDataParameter> GetDbParamList(object objParam)
        {
            List<IDbDataParameter> dbParams = null;
            if (objParam == null)
            {
                return dbParams;
            }
            var tableColumns = new AttributeBuilder().GetColumnInfos(objParam);
            if (tableColumns == null)
            {
                return dbParams;
            }
            if (tableColumns.Count == 0)
            {
                return dbParams;
            }
            dbParams = new List<IDbDataParameter>();
            foreach (TableColumnAttribute tableColumn in tableColumns)
            {
                var dbParam = GetDbParam(tableColumn);
                dbParams.Add(dbParam);
            }
            return dbParams;
        }

        /// <summary>
        /// 返回数据库事务对象
        /// </summary>
        /// <param name="conn"></param>
        /// <returns></returns>
        public virtual IDbTransaction GetDbTransaction(IDbConnection conn)
        {
            IDbTransaction dbTransaction = null;
            if (conn != null && conn.State == ConnectionState.Open)
            {
                dbTransaction = conn.BeginTransaction();
            }
            return dbTransaction;
        }

        /// <summary>
        /// 创建数据库参数化关键字操作符
        /// </summary>
        /// <returns></returns>
        public virtual string GetDbParamOperator()
        {
            return "";
        }
    }
}
