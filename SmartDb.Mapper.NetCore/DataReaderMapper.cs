using System;
using System.Data;
using System.Reflection;
using System.Reflection.Emit;

namespace SmartDb.Mapper.NetCore
{
    public class DataReaderMapper<T>
    {
        private static readonly MethodInfo getValueMethod =typeof(IDataRecord).GetMethod("get_Item", new Type[] { typeof(int) });
        private static readonly MethodInfo isDBNullMethod =typeof(IDataRecord).GetMethod("IsDBNull", new Type[] { typeof(int) });
        private delegate T Load(IDataRecord dataRecord);

        private Load handler;

        /// <summary>
        /// 构造函数
        /// </summary>
        private DataReaderMapper() { }

        /// <summary>
        /// 将IDataRecord转为单条实体
        /// </summary>
        /// <param name="dataRecord"></param>
        /// <returns></returns>
        public T Map(IDataRecord dataRecord)
        {
            return handler(dataRecord);
        }

        /// <summary>
        /// 获取DataReaderUtility对象实例
        /// </summary>
        /// <param name="dataRecord"></param>
        /// <returns></returns>
        public static DataReaderMapper<T> GetInstance(IDataRecord dataRecord)
        {
            DataReaderMapper<T> dynamicBuilder = new DataReaderMapper<T>();
            DynamicMethod method = new DynamicMethod("DynamicCreateEntity", typeof(T),
                    new Type[] { typeof(IDataRecord) }, typeof(T), true);
            ILGenerator generator = method.GetILGenerator();
            LocalBuilder result = generator.DeclareLocal(typeof(T));
            generator.Emit(OpCodes.Newobj, typeof(T).GetConstructor(Type.EmptyTypes));
            generator.Emit(OpCodes.Stloc, result);
            for (int i = 0; i < dataRecord.FieldCount; i++)
            {
                PropertyInfo propertyInfo = typeof(T).GetProperty(dataRecord.GetName(i));
                Label endIfLabel = generator.DefineLabel();
                if (propertyInfo != null && propertyInfo.GetSetMethod() != null)
                {
                    generator.Emit(OpCodes.Ldarg_0);
                    generator.Emit(OpCodes.Ldc_I4, i);
                    generator.Emit(OpCodes.Callvirt, isDBNullMethod);
                    generator.Emit(OpCodes.Brtrue, endIfLabel);
                    generator.Emit(OpCodes.Ldloc, result);
                    generator.Emit(OpCodes.Ldarg_0);
                    generator.Emit(OpCodes.Ldc_I4, i);
                    generator.Emit(OpCodes.Callvirt, getValueMethod);
                    generator.Emit(OpCodes.Unbox_Any, dataRecord.GetFieldType(i));
                    generator.Emit(OpCodes.Callvirt, propertyInfo.GetSetMethod());
                    generator.MarkLabel(endIfLabel);
                }
            }
            generator.Emit(OpCodes.Ldloc, result);
            generator.Emit(OpCodes.Ret);
            dynamicBuilder.handler = (Load)method.CreateDelegate(typeof(Load));
            return dynamicBuilder;
        }
    }
}
