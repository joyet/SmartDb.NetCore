using System;
using System.Collections.Generic;
using System.Reflection;

namespace SmartDb.NetCore
{
    public class AttributeBuilder
    {
        /// <summary>
        /// 获取表信息
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public TableAttribute GetTableInfo(Type type)
        {
            var tableEntity = new TableAttribute();
            tableEntity.TableName = type.Name;
            object[] customAttributes = type.GetCustomAttributes(typeof(TableAttribute), true);
            if (customAttributes == null|| customAttributes.Length <= 0)
            {
                return tableEntity;
            }
            tableEntity = customAttributes[0] as TableAttribute;
            return tableEntity;
        }

        /// <summary>
        /// 根据实体类型、实体对象实例获取表字段列表
        /// </summary>
        /// <param name="type"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<TableColumnAttribute> GetColumnInfos(Type type,object entity)
        {
            var columnList=new List<TableColumnAttribute>();
            PropertyInfo[] propertys = type.GetProperties();
            if (propertys == null|| propertys.Length <= 0)
            {
                return columnList;
            }
            foreach (PropertyInfo proInfo in propertys)
            {
                TableColumnAttribute systemColumn = GetSystemProperInfo(proInfo, entity);
                TableColumnAttribute selftDefineColumn =GetCustomAttributeInfo(proInfo, entity);
                if (selftDefineColumn == null)
                {
                    columnList.Add(systemColumn);
                    continue;
                }
                columnList.Add(selftDefineColumn);
            }
            return columnList;
       }

        /// <summary>
        /// 根据参数获取表字段列表
        /// </summary>
        /// <param name="param">参数对象,例:new {Uid=1,Uname="joyet"}</param>
        /// <returns></returns>
        public List<TableColumnAttribute> GetColumnInfos(object param)
        {
            var columnList = new List<TableColumnAttribute>();
            if (param == null)
            {
                return columnList;
            }
            Type type = param.GetType();
            PropertyInfo[] props = type.GetProperties();
            if (props == null || props.Length <= 0)
            {
                return columnList;
            }
            foreach (PropertyInfo proInfo in props)
            {
                TableColumnAttribute systemColumn = GetSystemProperInfo(proInfo, param);
                columnList.Add(systemColumn);
            }
            return columnList;
        }

        /// <summary>
        /// 获取表主键字段信息
        /// </summary>
        /// <returns></returns>
        public TableColumnAttribute GetPkColumnInfo(Type type)
        {
            TableColumnAttribute pkColumn = null;
            PropertyInfo[] props = type.GetProperties();
            if (props == null || props.Length <= 0)
            {
                return pkColumn;
            }
            foreach (PropertyInfo proInfo in props)
            {
                TableColumnAttribute systemColumn = GetSystemProperInfo(proInfo, null);
                TableColumnAttribute selftDefineColumn = GetCustomAttributeInfo(proInfo, null);
                if (selftDefineColumn != null && selftDefineColumn.IsPrimaryKey)
                {
                    pkColumn = selftDefineColumn;
                    break;
                }
            }
            return pkColumn;
        }

        /// <summary>
        /// 获取系统单个属性信息
        /// </summary>
        /// <param name="proInfo"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        private TableColumnAttribute GetSystemProperInfo(PropertyInfo proInfo, object entity)
       {
            TableColumnAttribute column = null;
            if (proInfo == null)
            {
                return column;
            }
            column = new TableColumnAttribute();
            column.ColumnName = proInfo.Name;
            if (entity != null)
            {
                object attrValue = proInfo.GetValue(entity, null);
                if (attrValue != null)
                {
                    column.ColumnValue = attrValue;
                }
            }
            return column;
       }

        /// <summary>
        /// 获取单个属性的自定义特性信息
        /// </summary>
        /// <param name="proInfo"></param>
        /// <param name="param"></param>
        /// <returns></returns>
       private  TableColumnAttribute GetCustomAttributeInfo(PropertyInfo proInfo, object param)
       {
           TableColumnAttribute columnEntity = null;
           if (proInfo == null)
           {
                return columnEntity;
            }
            object[] customAttributes = proInfo.GetCustomAttributes(typeof(TableColumnAttribute), true);
            if (customAttributes == null)
            {
                return columnEntity;
            }
            if (customAttributes.Length<=0)
            {
                return columnEntity;
            }
            columnEntity = (TableColumnAttribute)customAttributes[0];
            if (columnEntity == null)
            {
                return columnEntity;
            }
            columnEntity.ColumnName = string.IsNullOrEmpty(columnEntity.ColumnName) ? proInfo.Name : columnEntity.ColumnName;
            if (param != null)
            {
                object attrValue = proInfo.GetValue(param, null);
                if (attrValue != null)
                {
                    columnEntity.ColumnValue = attrValue;
                }
            }
            return columnEntity;
       }

    }
}
