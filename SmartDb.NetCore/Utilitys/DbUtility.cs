﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartDb.NetCore
{
    public partial class DbUtility
    {
        #region private、internal、public  variables

        private string _connectionString = string.Empty;

        /// <summary>
        /// 是否开启事务
        /// </summary>
        internal bool IsStartTrans { get; set; }

        /// <summary>
        /// 数据库库连接串
        /// </summary>
        public string ConnectionString
        {
            get
            {
                return _connectionString;
            }
            set
            {
                if (!IsStartTrans)
                {
                    _connectionString = value;
                }
            }
        }

        /// <summary>
        /// 数据库工厂
        /// </summary>
        public  SqlDbFactory DbFactory { get; set; }

        /// <summary>
        /// 数据库执行AOP委托
        /// </summary>
        public Action<DbAopEntity> AopAction { get; set; }

        #endregion

        #region private、internal、public   Methods

        public DbUtility()
        {
            ConnectionString = string.Empty;
            DbFactory = null;
            IsStartTrans = false;
        }

        /// <summary>
        /// 执行增、删、改操作,返回影响行数
        /// </summary>
        /// <param name="cmdText">SQL语句/存储过程/参数化SQL语句</param>
        /// <param name="dbParams">命令类型:SQL语句/存储过程</param>
        /// <param name="cmdType">IDbDataParameter参数数组</param>
        /// <returns>int</returns>
        public int ExecuteNonQuery(string cmdText, List<IDbDataParameter> dbParams = null, CommandType cmdType = CommandType.Text)
        {
            var result = 0;
            if (!IsStartTrans)
            {
                ExecuteAopAction(cmdText, dbParams, cmdType, (cmdText0, dbParams0, cmdType0) =>
                {
                    result = ExecuteNonQueryNoTrans(cmdText, dbParams, cmdType);
                });
                return result;
            }
            ExecuteAopAction(cmdText, dbParams, cmdType, (cmdText0, dbParams0, cmdType0) =>
            {
                result = ExecuteNonQueryWithTrans(cmdText, dbParams, cmdType);
            });
            return result;
        }

        /// <summary>
        /// 执行查询操作,返回DataSet
        /// </summary>
        /// <param name="cmdText">SQL语句/存储过程/参数化SQL语句</param>
        /// <param name="dbParams">命令类型:SQL语句/存储过程</param>
        /// <param name="cmdType">数据库参数数组</param>
        /// <returns>DataSet</returns>
        public DataSet ExecuteQuery(string cmdText, List<IDbDataParameter> dbParams = null, CommandType cmdType = CommandType.Text)
        {
            DataSet result = null;
            if (!IsStartTrans)
            {
                ExecuteAopAction(cmdText, dbParams, cmdType, (cmdText0, dbParams0, cmdType0) =>
                {
                    result = ExecuteQueryNoTrans(cmdText, dbParams, cmdType);
                });
                return result;
            }
            ExecuteAopAction(cmdText, dbParams, cmdType, (cmdText0, dbParams0, cmdType0) =>
            {
                result = ExecuteQueryWithTrans(cmdText, dbParams, cmdType);
            });
            return result;
        }

        /// <summary>
        /// 执行查询操作,返回IDataReader
        /// </summary>
        /// <param name="cmdText">SQL语句/存储过程/参数化SQL语句</param>
        /// <param name="dbParams">命令类型:SQL语句/存储过程</param>
        /// <param name="cmdType">数据库参数数组</param>
        /// <returns>IDataReader</returns>
        public IDataReader ExecuteReader(string cmdText, List<IDbDataParameter> dbParams = null, CommandType cmdType = CommandType.Text)
        {
            IDataReader result = null;
            if (!IsStartTrans)
            {
                ExecuteAopAction(cmdText, dbParams, cmdType, (cmdText0, dbParams0, cmdType0) =>
                {
                    result = ExecuteReaderNoTrans(cmdText, dbParams, cmdType);
                });
                return result;
            }
            ExecuteAopAction(cmdText, dbParams, cmdType, (cmdText0, dbParams0, cmdType0) =>
            {
                result = ExecuteReaderWithTrans(cmdText, dbParams, cmdType);
            });
          
            return result;
        }

        /// <summary>
        /// 执行查询操作,返回object
        /// </summary>
        /// <param name="cmdText">SQL语句/存储过程/参数化SQL语句</param>
        /// <param name="dbParams">命令类型:SQL语句/存储过程</param>
        /// <param name="cmdType">数据库参数数组</param>
        /// <returns>object</returns>
        public object ExecuteScalar(string cmdText, List<IDbDataParameter> dbParams = null,CommandType cmdType = CommandType.Text)
        {
            object result = null;
            if (!IsStartTrans)
            {
                ExecuteAopAction(cmdText, dbParams, cmdType, (cmdText0, dbParams0, cmdType0) =>
                {
                    result = ExecuteScalarNoTrans(cmdText, dbParams, cmdType);
                });
                return result;
            }
            ExecuteAopAction(cmdText, dbParams, cmdType, (cmdText0, dbParams0, cmdType0) =>
            {
                result = ExecuteScalarWithTrans(cmdText, dbParams, cmdType);
            });
            return result;
        }

        /// <summary>
        /// 参数据底层公共操作
        /// </summary>
        /// <param name="cmdText">SQL语句/存储过程/参数化SQL语句</param>
        /// <param name="conn">数据为连接对象</param>
        /// <param name="cmd">执行SQL命令对象</param>
        /// <param name="dbParms">数据库参数数组</param>
        /// <param name="cmdType">命令类型:SQL语句/存储过程</param>
        /// <param name="dbTransaction">数据库事务对象</param>
        private void PrepareCommand(string cmdText, IDbConnection conn, IDbCommand cmd, List<IDbDataParameter> dbParms, CommandType cmdType, IDbTransaction dbTransaction = null)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (dbTransaction != null)
            {
                cmd.Transaction = dbTransaction;
            }
            cmd.CommandType = cmdType;
            if (dbParms != null)
            {
                foreach (IDbDataParameter parameter in dbParms)
                {
                    if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) && (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    cmd.Parameters.Add(parameter);
                }
            }
        }

        /// <summary>
        /// 执行公共函数
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="cmdText"></param>
        /// <param name="dbParams"></param>
        /// <param name="cmdType"></param>
        /// <param name="exeuteAction"></param>
        /// <returns></returns>
        private void ExecuteAopAction(string cmdText, List<IDbDataParameter> dbParams, CommandType cmdType, Action<string, List<IDbDataParameter>, CommandType> exeuteAction)
        {
            DbAopEntity dbAopEntity = new DbAopEntity()
            {
                ConnectionString = _connectionString,
                CommandText = cmdText,
                DbParams = dbParams,
                CmdType = cmdType,
                StartDateTime = DateTime.Now
            };

            //开启执行时间计算
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            //开始执行委托中方法
            exeuteAction(cmdText, dbParams, cmdType);

            //结束执行时间计算
            stopwatch.Stop();
            dbAopEntity.EndDateTime = DateTime.Now;
            dbAopEntity.Elapsed = stopwatch.Elapsed;

            //调用委托中方法
            AopAction?.Invoke(dbAopEntity);
        }

        /// <summary>
        /// 执行公共函数
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="cmdText"></param>
        /// <param name="dbParams"></param>
        /// <param name="cmdType"></param>
        /// <param name="exeuteFunc"></param>
        /// <returns></returns>
        private  TResult ExecuteAopFunc<TResult>(string cmdText, List<IDbDataParameter> dbParams, CommandType cmdType, Func<string, List<IDbDataParameter>, CommandType, TResult> exeuteFunc)
        {
            DbAopEntity dbAopEntity = new DbAopEntity()
            {
                ConnectionString = _connectionString,
                CommandText = cmdText,
                DbParams = dbParams,
                CmdType = cmdType,
                StartDateTime = DateTime.Now
            };

            //开启执行时间计算
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            //开始执行委托中方法
            TResult result = exeuteFunc(cmdText, dbParams, cmdType);

            //结束执行时间计算
            stopwatch.Stop();
            dbAopEntity.EndDateTime = DateTime.Now;
            dbAopEntity.Elapsed = stopwatch.Elapsed;

            //调用委托中方法
            AopAction?.Invoke(dbAopEntity);

            return result;
        }

        #endregion
    }
}
