using SmartDb;
using SmartDb.NetCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace SmartDb.SqlServer.NetCore
{
    public class SqlServerFactory: SqlDbFactory
    {
        /// <summary>
        /// 返回数据库连接对象
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public override IDbConnection GetConnection(string connectionString)
        {
            IDbConnection conn = new SqlConnection(connectionString);
            return conn;
        }

        /// <summary>
        ///创建适配器对象
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public override IDataAdapter GetDataAdapter(IDbCommand cmd)
        {
            IDataAdapter dataAdater = new SqlDataAdapter((SqlCommand)cmd);
            return dataAdater;
        }

        /// <summary>
        /// 创建数据库参数对象
        /// </summary>
        /// <param name="dbParamName"></param>
        /// <param name="dbParamValue"></param>
        /// <param name="dbPramDbType"></param>
        /// <param name="dbPramLength"></param>
        /// <returns></returns>
        public override IDbDataParameter GetDbParam(string dbParamName, object dbParamValue, object dbParamDbType = null, int dbPramDbTypeLength = 0)
        {
            IDbDataParameter dbParam = null;
            if (string.IsNullOrEmpty(dbParamName) || dbParamValue == null)
            {
                return dbParam;
            }
            dbParamName = GetDbParamOperator() + dbParamName;
            if (dbParamDbType == null)
            {
                dbParam = new SqlParameter(dbParamName, dbParamValue);
                return dbParam;
            }
            dbParam = new SqlParameter(dbParamName, (SqlDbType)dbParamDbType);
            dbParam.Value = dbParamValue;
            if (dbPramDbTypeLength <= 0)
            {
                return dbParam;
            }
            int dbTypeValue = (int)dbParamDbType;
            switch (dbTypeValue)
            {
                case 1:
                case 3:
                case 5:
                case 10:
                case 12:
                case 21:
                case 22:
                case 32:
                case 33:
                case 34:
                    dbParam.Size = dbPramDbTypeLength;
                    break;
            }
            return dbParam;
        }

        /// <summary>
        /// 获取数据库参数化关键字操作符
        /// </summary>
        /// <returns></returns>
        public override string GetDbParamOperator()
        {
            return "@";
        }

        /// <summary>
        /// 获取数据库自动增长sql
        /// </summary>
        /// <param name="isGetIncrementValue"></param>
        /// <returns></returns>
        public override string GetIncrementSql(bool isGetIncrementValue)
        {
            string sql = string.Empty;
            if (isGetIncrementValue)
            {
                sql = "select @@identity";
            }
            return sql;
        }

    }
}
