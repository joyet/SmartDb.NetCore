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

        /// <summary>
        /// 是否获取自动增长值（默认不开启）
        /// </summary>
        public bool IsGetIncrementValue { get; set; }


        public TableAttribute()
        {
            TableName = "";
            Description = "";
            IsGetIncrementValue = false;
        }
    }
}
