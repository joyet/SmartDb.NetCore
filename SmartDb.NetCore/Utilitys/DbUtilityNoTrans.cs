using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartDb.NetCore
{
    public partial class DbUtility
    {
        #region private Methods

        /// <summary>
        /// 执行增、删、改操作,返回影响行数
        /// </summary>
        /// <param name="cmdText">SQL语句/存储过程/参数化SQL语句</param>
        /// <param name="dbParams">命令类型:SQL语句/存储过程</param>
        /// <param name="cmdType">IDbDataParameter参数数组</param>
        /// <returns>int</returns>
        private int ExecuteNonQueryNoTrans(string cmdText, List<IDbDataParameter> dbParams = null, CommandType cmdType = CommandType.Text)
        {
            int result = 0;
            using (IDbConnection conn = DbFactory.GetConnection(ConnectionString))
            {
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    PrepareCommand(cmdText, conn, cmd, dbParams, cmdType);
                    result = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
            }
            return result;
        }

        /// <summary>
        /// 执行查询操作,返回DataSet
        /// </summary>
        /// <param name="cmdText">SQL语句/存储过程/参数化SQL语句</param>
        /// <param name="dbParams">命令类型:SQL语句/存储过程</param>
        /// <param name="cmdType">数据库参数数组</param>
        /// <returns>DataSet</returns>
        private DataSet ExecuteQueryNoTrans(string cmdText, List<IDbDataParameter> dbParams = null, CommandType cmdType = CommandType.Text)
        {
            DataSet result = null;
            using (IDbConnection conn = DbFactory.GetConnection(ConnectionString))
            {
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    result = new DataSet();
                    PrepareCommand(cmdText, conn, cmd, dbParams, cmdType);
                    IDataAdapter da = DbFactory.GetDataAdapter(cmd);
                    da.Fill(result);
                    cmd.Parameters.Clear();
                }
            }
            return result;
        }

        /// <summary>
        /// 执行查询操作,返回IDataReader
        /// </summary>
        /// <param name="cmdText">SQL语句/存储过程/参数化SQL语句</param>
        /// <param name="dbParams">命令类型:SQL语句/存储过程</param>
        /// <param name="cmdType">数据库参数数组</param>
        /// <returns>IDataReader</returns>
        private IDataReader ExecuteReaderNoTrans(string cmdText, List<IDbDataParameter> dbParams = null, CommandType cmdType = CommandType.Text)
        {
            IDataReader result = null;
            IDbConnection conn = DbFactory.GetConnection(ConnectionString);
            IDbCommand cmd = conn.CreateCommand();
            PrepareCommand(cmdText, conn, cmd, dbParams, cmdType);
            result = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            cmd.Parameters.Clear();
            return result;
        }

        /// <summary>
        /// 执行查询操作,返回object
        /// </summary>
        /// <param name="cmdText">SQL语句/存储过程/参数化SQL语句</param>
        /// <param name="dbParams">命令类型:SQL语句/存储过程</param>
        /// <param name="cmdType">数据库参数数组</param>
        /// <returns>object</returns>
        private object ExecuteScalarNoTrans(string cmdText, List<IDbDataParameter> dbParams = null, CommandType cmdType = CommandType.Text)
        {
            object result = null;
            using (IDbConnection conn = DbFactory.GetConnection(ConnectionString))
            {
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    PrepareCommand(cmdText, conn, cmd, dbParams, cmdType);
                    result = cmd.ExecuteScalar();
                    if ((Object.Equals(result, null)) || (Object.Equals(result, System.DBNull.Value)))
                    {
                        result = null;
                    }
                    cmd.Parameters.Clear();
                }
            }
            return result;
        }

        #endregion
    }
}
