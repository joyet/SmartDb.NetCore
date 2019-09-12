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
        #region private、internal、public  variables

        /// <summary>
        /// 数据库连接对象
        /// </summary>
        private IDbConnection _dbConn = null;

        /// <summary>
        /// 数据库命令对象
        /// </summary>
        private IDbCommand _dbCmd = null;

        /// <summary>
        /// 数据库事务对象
        /// </summary>
        private IDbTransaction _dbTransaction = null;

        #endregion

        #region private、internal、public   Methods

        /// <summary>
        /// 开启事务
        /// </summary>
        public virtual void BeginTransaction()
        {
            if (!IsStartTrans)
            {
                CreateTransaction();
            }
        }

        /// <summary>
        /// 提交事务
        /// </summary>
        public void CommitTransaction()
        {
            if (IsStartTrans)
            {
                if (_dbTransaction != null)
                {
                    _dbTransaction.Commit();
                    CloseTransaction();
                }
            }
        }

        /// <summary>
        /// 回滚事务
        /// </summary>
        public void RollbackTransaction()
        {
            if (IsStartTrans)
            {
                if (_dbTransaction != null)
                {
                    _dbTransaction.Rollback();
                    CloseTransaction();
                }
            }
        }

        /// <summary>
        /// 执行增、删、改操作,返回影响行数
        /// </summary>
        /// <param name="cmdText">SQL语句/存储过程/参数化SQL语句</param>
        /// <param name="dbParams">命令类型:SQL语句/存储过程</param>
        /// <param name="cmdType">IDbDataParameter参数数组</param>
        /// <returns>int</returns>
        private int ExecuteNonQueryWithTrans(string cmdText, List<IDbDataParameter> dbParams = null, CommandType cmdType = CommandType.Text)
        {
            int result = 0;
            if (IsStartTrans)
            {
                PrepareCommand(cmdText, _dbConn, _dbCmd, dbParams, cmdType, _dbTransaction);
                result = _dbCmd.ExecuteNonQuery();
                _dbCmd.Parameters.Clear();
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
        private DataSet ExecuteQueryWithTrans(string cmdText, List<IDbDataParameter> dbParams = null, CommandType cmdType = CommandType.Text)
        {
            DataSet result = null;
            if (IsStartTrans)
            {
                PrepareCommand(cmdText, _dbConn, _dbCmd, dbParams, cmdType, _dbTransaction);
                IDataAdapter da = DbFactory.GetDataAdapter(_dbCmd);
                da.Fill(result);
                _dbCmd.Parameters.Clear();
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
        private IDataReader ExecuteReaderWithTrans(string cmdText, List<IDbDataParameter> dbParams = null, CommandType cmdType = CommandType.Text)
        {
            IDataReader result = null;
            if (IsStartTrans)
            {
                PrepareCommand(cmdText, _dbConn, _dbCmd, dbParams, cmdType, _dbTransaction);
                result = _dbCmd.ExecuteReader(CommandBehavior.CloseConnection);
                _dbCmd.Parameters.Clear();
            }
            return result;
        }

        /// <summary>
        /// 执行查询操作,返回object
        /// </summary>
        /// <param name="cmdText">SQL语句/存储过程/参数化SQL语句</param>
        /// <param name="dbParams">命令类型:SQL语句/存储过程</param>
        /// <param name="cmdType">数据库参数数组</param>
        /// <returns>object</returns>
        private object ExecuteScalarWithTrans(string cmdText, List<IDbDataParameter> dbParams = null, CommandType cmdType = CommandType.Text)
        {
            object result = null;
            if (IsStartTrans)
            {
                PrepareCommand(cmdText, _dbConn, _dbCmd, dbParams, cmdType, _dbTransaction);
                result = _dbCmd.ExecuteScalar();
                _dbCmd.Parameters.Clear();
                if ((Object.Equals(result, null)) || (Object.Equals(result, System.DBNull.Value)))
                {
                    result = null;
                }
            }
            return result;
        }

        /// <summary>
        /// 创建事务相关对象
        /// </summary>
        private void CreateTransaction()
        {
            if (string.IsNullOrEmpty(ConnectionString))
            {
                return;
            }
            if (_dbConn == null)
            {
                _dbConn = DbFactory.GetConnection(ConnectionString);
                _dbConn.Open();
            }
            if (_dbCmd == null)
            {
                _dbCmd = _dbConn.CreateCommand();
            }
            if (_dbTransaction == null)
            {
                _dbTransaction = _dbConn.BeginTransaction();
            }
            IsStartTrans = true;
        }

        /// <summary>
        /// 关闭事务
        /// </summary>
        /// </summary>
        private void CloseTransaction()
        {
            if (IsStartTrans)
            {
                _dbCmd.Dispose();
                _dbTransaction.Dispose();
                _dbConn.Dispose();
                IsStartTrans = false;
            }
        }

        #endregion
    }
}
