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
        /// 是否是主键（默认不是）
        /// </summary>
        public bool IsPrimaryKey { get; set; }

        /// <summary>
        /// 此字段是否是自动增长（默认不是）
        /// </summary>
        public bool IsAutoIncrement { get; set; }

        /// <summary>
        /// 字段数据类型
        /// </summary>
        public object DataType { get; set; }

        /// <summary>
        /// 字段最大长度
        /// </summary>
        public int MaxLength { get; set; }

        /// <summary>
        /// 是否忽略添加(如果是,则会使用数据库默认值)
        /// </summary>
        public bool IsIgnoreAdd { get; set; }

        /// <summary>
        /// 是否设置默认值(默认不设置)
        /// </summary>
        public bool IsSetDefaultValue { get; set; }

        /// <summary>
        /// 字段默认值
        /// </summary>
        public object DefaultValue { get; set; }

        /// <summary>
        /// 字段值
        /// </summary>
        public object ColumnValue { get; set; }

        public TableColumnAttribute()
        {
            ColumnName = string.Empty;
            DataType = null;
            MaxLength = 0;
            IsPrimaryKey = false;
            IsAutoIncrement = false;
            IsIgnoreAdd = false;
            ColumnValue = DBNull.Value;
            IsSetDefaultValue = false;
            DefaultValue = null;
        }
    }
}
