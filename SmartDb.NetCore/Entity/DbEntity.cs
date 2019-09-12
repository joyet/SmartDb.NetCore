using System;
using System.Collections.Generic;
using System.Data;

namespace SmartDb.NetCore
{
    /// <summary>
    /// 数据库实体
    /// </summary>
    public class DbEntity
    {
        /// <summary>
        ///Sql语句或存储过程 
        /// </summary>
        public string CommandText { get; set; }

        /// <summary>
        ///数据库参数列表
        /// </summary>
        public List<IDbDataParameter> DbParams { get; set; }

        /// <summary>
        ///数据表实体 
        /// </summary>
        public TableAttribute TableEntity { get; set; }

        public DbEntity()
        {
            CommandText = string.Empty;
            DbParams = new List<IDbDataParameter>();
            TableEntity = new TableAttribute();
        }
    }
}
