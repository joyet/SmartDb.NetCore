using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SmartDb.NetCore
{
   public class DbAopEntity
    {
        
        /// <summary>
        ///数据库连接
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        ///Sql语句或存储过程 
        /// </summary>
        public string CommandText { get; set; }

        /// <summary>
        ///数据库参数列表
        /// </summary>
        public List<IDbDataParameter> DbParams { get; set; }

        /// <summary>
        ///命令类型
        /// </summary>
        public CommandType CmdType { get; set; }

        /// <summary>
        /// 执行开始时间
        /// </summary>
        public DateTime StartDateTime { get; set; }

        /// <summary>
        /// /// <summary>
        /// 执行结束时间
        /// </summary>
        /// </summary>
        public DateTime EndDateTime { get; set; }

        /// <summary>
        /// /// <summary>
        ///总共用时
        /// </summary>
        /// </summary>
        public TimeSpan Elapsed { get; set; }
    }
}
