using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace Smart.Foundation.NetCore
{
    public static class EnumHelper
    {
        /// <summary>
        /// 获取枚举描述
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum enumValue)
        {
            string enumDescription = string.Empty;
            Type type = enumValue.GetType();
            string typeName = Enum.GetName(type, enumValue);
            if (typeName == null)
            {
                return enumDescription;
            }
            FieldInfo field = type.GetField(typeName);
            DescriptionAttribute attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
            if (attribute == null)
            {
                return enumDescription;
            }
            return attribute.Description;
        }
      
        /// <summary>
        /// 根据枚举值获取枚举
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T ObjectToEnum<T>(this object value)
        {
            T t = default(T);
            t = (T)Enum.ToObject(typeof(T), value);
            return t;
        }

    }
}
