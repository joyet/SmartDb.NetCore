using MySql.Data.MySqlClient;
using SmartDb.NetCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SmartDb.MySql.NetCore
{
    public class MySqlFactory:SqlDbFactory
    {
        /// <summary>
        /// 返回数据库连接对象
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public override IDbConnection GetConnection(string connectionString)
        {
            IDbConnection conn = new MySqlConnection(connectionString);
            return conn;
        }

        /// <summary>
        ///创建适配器对象
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public override IDataAdapter GetDataAdapter(IDbCommand cmd)
        {
            IDataAdapter dataAdater = new MySqlDataAdapter((MySqlCommand)cmd);
            return dataAdater;
        }

        /// <summary>
        /// 返回数据库参数对象
        /// </summary>
        /// <param name="columnEntity"></param>
        /// <returns></returns>
        public override IDbDataParameter GetDbParam(string dbParamName, object dbParamValue, object dbPramDbType = null, int dbPramDbTypeLength = 0)
        {
            IDbDataParameter dbParam = null;
            if (string.IsNullOrEmpty(dbParamName) || dbParamValue == null)
            {
                return dbParam;
            }
            dbParamName = GetDbParamOperator() + dbParamName;
            if (dbPramDbType == null)
            {
                dbParam = new MySqlParameter(dbParamName, dbParamValue);
                return dbParam;
            }
            dbParam = new MySqlParameter(dbParamName, (MySqlDbType)dbPramDbType);
            dbParam.Value = dbParamValue;
            if (dbPramDbTypeLength <= 0)
            {
                return dbParam;
            }
            int dbTypeValue = (int)dbPramDbType;
            switch (dbTypeValue)
            {
                case 0:
                case 15:
                case 246:
                case 253:
                case 254:
                case 600:
                case 601:
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
            return "?";
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
