using System;

namespace SmartDb.NetCore
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class TableAttribute : Attribute
    {
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 表描述
        /// </summary>
        public string Description { get; set; }

        public TableAttribute()
        {
            TableName = "";
            Description = "";
        }
    }
}
