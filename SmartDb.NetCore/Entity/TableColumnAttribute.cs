using System;

namespace SmartDb.NetCore
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class TableColumnAttribute : Attribute
    {
        /// <summary>
        /// 字段名
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// 字段值
        /// </summary>
        public object ColumnValue { get; set; }

        /// <summary>
        /// 字段数据类型
        /// </summary>
        public object DataType { get; set; }

        /// <summary>
        /// 字段最大长度
        /// </summary>
        public int MaxLength { get; set; }

        /// <summary>
        /// 是否是主键（默认不是）
        /// </summary>
        public bool IsPrimaryKey { get; set; }

        /// <summary>
        /// 此字段是否是自动增长（默认不是）
        /// </summary>
        public bool IsAutoIncrement { get; set; }

        public TableColumnAttribute()
        {
            ColumnName = string.Empty;
            ColumnValue = DBNull.Value;
            DataType = null;
            MaxLength = 0;
            IsPrimaryKey = false;
            IsAutoIncrement = false;
        }
    }
}
