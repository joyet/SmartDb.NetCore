using SmartDb;
using SmartDb.NetCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Text;

namespace SmartDb.SQLite.NetCore
{
    public class SQLiteDbFactory: SqlDbFactory
    {
        /// <summary>
        /// 返回数据库连接对象
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public override IDbConnection GetConnection(string connectionString)
        {
            IDbConnection conn = new SQLiteConnection(connectionString);
            //conn.Open();
            return conn;
        }

        /// <summary>
        ///创建适配器对象
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public override IDataAdapter GetDataAdapter(IDbCommand cmd)
        {
            IDataAdapter dataAdater = new SQLiteDataAdapter((SQLiteCommand)cmd);
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
                dbParam = new SQLiteParameter(dbParamName, dbParamValue);
                return dbParam;
            }
            dbParam = new SQLiteParameter(dbParamName, (DbType)dbParamDbType);
            dbParam.Value = dbParamValue;
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

    }
}
