using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Async.NetCore
{
    public static partial class AsyncHelper
    {
        #region Action 相关异步方法

        /// <summary>
        /// Action异步方法
        /// </summary>
        /// <param name="action"></param>
        public static async void ToActionAsycn(this Action action)
        {
            await Task.Run(() => {
                action();
            });
        }

        /// <summary>
        /// Action异步方法
        /// </summary>
        /// <param name="action"></param>
        public static async void ToActionAsycn<T1>(this Action<T1> action, T1 t1)
        {
            await Task.Run(() => {
                action(t1);
            });
        }

        /// <summary>
        /// Action异步方法
        /// </summary>
        /// <param name="action"></param>
        public static async void ToActionAsycn<T1,T2>(this Action<T1, T2> action,T1 t1, T2 t2)
        {
            await Task.Run(() => {
                action(t1, t2);
            });
        }

        /// <summary>
        /// Action异步方法
        /// </summary>
        /// <param name="action"></param>
        public static async void ToActionAsycn<T1, T2,T3>(this Action<T1, T2, T3> action, T1 t1, T2 t2, T3 t3)
        {
            await Task.Run(() => {
                action(t1, t2,t3);
            });
        }

        /// <summary>
        /// Action异步方法
        /// </summary>
        /// <param name="action"></param>
        public static async void ToActionAsycn<T1, T2, T3,T4>(this Action<T1, T2, T3, T4> action, T1 t1, T2 t2, T3 t3, T4 t4)
        {
            await Task.Run(() => {
                action(t1, t2, t3,t4);
            });
        }

        /// <summary>
        /// Action异步方法
        /// </summary>
        /// <param name="action"></param>
        public static async void ToActionAsycn<T1, T2, T3, T4,T5>(this Action<T1, T2, T3, T4, T5> action,
            T1 t1, T2 t2, T3 t3, T4 t4,T5 t5)
        {
            await Task.Run(() => {
                action(t1, t2, t3, t4,t5);
            });
        }

        /// <summary>
        /// Action异步方法
        /// </summary>
        /// <param name="action"></param>
        public static async void ToActionAsycn<T1, T2, T3, T4, T5,T6>(this Action<T1, T2, T3, T4, T5, T6> action, 
            T1 t1, T2 t2, T3 t3, T4 t4, T5 t5,T6 t6)
        {
            await Task.Run(() => {
                action(t1, t2, t3, t4, t5,t6);
            });
        }

        /// <summary>
        /// Action异步方法
        /// </summary>
        /// <param name="action"></param>
        public static async void ToActionAsycn<T1, T2, T3, T4, T5, T6, T7>(this Action<T1, T2, T3, T4, T5, T6, T7> action,
            T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7)
        {
            await Task.Run(() => {
                action(t1, t2, t3, t4, t5, t6, t7);
            });
        }

        /// <summary>
        /// Action异步方法
        /// </summary>
        /// <param name="action"></param>
        public static async void ToActionAsycn<T1, T2, T3, T4, T5, T6, T7, T8>(this Action<T1, T2, T3, T4, T5, T6, T7, T8> action, 
            T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7,T8 t8)
        {
            await Task.Run(() => {
                action(t1, t2, t3, t4, t5, t6, t7, t8);
            });
        }

        /// <summary>
        /// Action异步方法
        /// </summary>
        /// <param name="action"></param>
        public static async void ToActionAsycn<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> action, 
            T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8,T9 t9)
        {
            await Task.Run(() => {
                action(t1, t2, t3, t4, t5, t6, t7, t8,t9);
            });
        }

        /// <summary>
        /// Action异步方法
        /// </summary>
        /// <param name="action"></param>
        public static async void ToActionAsycn<T1, T2, T3, T4, T5, T6, T7, T8, T9,T10>(this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> action, 
            T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9,T10 t10)
        {
            await Task.Run(() => {
                action(t1, t2, t3, t4, t5, t6, t7, t8, t9,t10);
            });
        }

        /// <summary>
        /// Action异步方法
        /// </summary>
        /// <param name="action"></param>
        public static async void ToActionAsycn<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10,T11>(this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> action,
            T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10,T11 t11)
        {
            await Task.Run(() => {
                action(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11);
            });
        }

        /// <summary>
        /// Action异步方法
        /// </summary>
        /// <param name="action"></param>
        public static async void ToActionAsycn<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> action, 
            T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12)
        {
            await Task.Run(() => {
                action(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10,t11, t12);
            });
        }

        /// <summary>
        /// Action异步方法
        /// </summary>
        /// <param name="action"></param>
        public static async void ToActionAsycn<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> action,
            T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13)
        {
            await Task.Run(() => {
                action(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12,t13);
            });
        }

        /// <summary>
        /// Action异步方法
        /// </summary>
        /// <param name="action"></param>
        public static async void ToActionAsycn<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> action, 
            T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13, T14 t14)
        {
            await Task.Run(() => {
                action(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14);
            });
        }

        /// <summary>
        /// Action异步方法
        /// </summary>
        /// <param name="action"></param>
        public static async void ToActionAsycn<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> action,
            T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13, T14 t14, T15 t15)
        {
            await Task.Run(() => {
                action(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15);
            });
        }

        /// <summary>
        /// Action异步方法
        /// </summary>
        /// <param name="action"></param>
        public static async void ToActionAsycn<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> action,
            T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13, T14 t14, T15 t15, T16 t16)
        {
            await Task.Run(() => {
                action(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15, t16);
            });
        }

        #endregion
    }
}
