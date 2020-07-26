using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Async.NetCore
{
    public static partial  class AsyncHelper
    {
        #region Func 相关异步方法

        /// <summary>
        /// Func异步方法
        /// </summary>
        /// <param name="Func"></param>
        public static async Task<TResult> ToFuncAsycn<TResult>(this Func<TResult> func)
        {
            var result=await Task.Run(() => {
              return func();
            });
            return result;
        }

        /// <summary>
        /// Func异步方法
        /// </summary>
        /// <param name="Func"></param>
        public static async Task<TResult> ToFuncAsycn<T1, TResult>(this Func<T1, TResult> func, T1 t1)
        {
            var result = await Task.Run(() => {
                return func(t1);
            });
            return result;
        }

        /// <summary>
        /// Func异步方法
        /// </summary>
        /// <param name="Func"></param>
        public static async Task<TResult> ToFuncAsycn<T1, T2, TResult>(this Func<T1, T2, TResult> func, T1 t1, T2 t2)
        {
            var result = await Task.Run(() => {
                return func(t1, t2);
            });
            return result;
        }

        /// <summary>
        /// Func异步方法
        /// </summary>
        /// <param name="Func"></param>
        public static async Task<TResult> ToFuncAsycn<T1, T2, T3, TResult>(this Func<T1, T2, T3, TResult> func, T1 t1, T2 t2, T3 t3)
        {
            var result = await Task.Run(() => {
                return func(t1, t2, t3);
            });
            return result;
        }

        /// <summary>
        /// Func异步方法
        /// </summary>
        /// <param name="Func"></param>
        public static async Task<TResult> ToFuncAsycn<T1, T2, T3, T4, TResult>(this Func<T1, T2, T3, T4, TResult> func,
            T1 t1, T2 t2, T3 t3, T4 t4)
        {
            var result = await Task.Run(() => {
                return func(t1, t2, t3, t4);
            });
            return result;
        }

        /// <summary>
        /// Func异步方法
        /// </summary>
        /// <param name="Func"></param>
        public static async Task<TResult> ToFuncAsycn<T1, T2, T3, T4, T5, TResult>(this Func<T1, T2, T3, T4, T5, TResult> func, 
            T1 t1, T2 t2, T3 t3, T4 t4, T5 t5)
        {
            var result = await Task.Run(() => {
                return func(t1, t2, t3, t4, t5);
            });
            return result;
        }

        /// <summary>
        /// Func异步方法
        /// </summary>
        /// <param name="Func"></param>
        public static async Task<TResult> ToFuncAsycn<T1, T2, T3, T4, T5, T6, TResult>(this Func<T1, T2, T3, T4, T5, T6, TResult> func,
            T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6)
        {
            var result = await Task.Run(() => {
                return func(t1, t2, t3, t4, t5, t6);
            });
            return result;
        }

        /// <summary>
        /// Func异步方法
        /// </summary>
        /// <param name="Func"></param>
        public static async Task<TResult> ToFuncAsycn<T1, T2, T3, T4, T5, T6, T7, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, TResult> func, 
            T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7)
        {
            var result = await Task.Run(() => {
                return func(t1, t2, t3, t4, t5, t6, t7);
            });
            return result;
        }

        /// <summary>
        /// Func异步方法
        /// </summary>
        /// <param name="Func"></param>
        public static async Task<TResult> ToFuncAsycn<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> func, 
            T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8)
        {
            var result = await Task.Run(() => {
                return func(t1, t2, t3, t4, t5, t6, t7, t8);
            });
            return result;
        }

        /// <summary>
        /// Func异步方法
        /// </summary>
        /// <param name="Func"></param>
        public static async Task<TResult> ToFuncAsycn<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> func, 
            T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9)
        {
            var result = await Task.Run(() => {
                return func(t1, t2, t3, t4, t5, t6, t7, t8, t9);
            });
            return result;
        }

        /// <summary>
        /// Func异步方法
        /// </summary>
        /// <param name="Func"></param>
        public static async Task<TResult> ToFuncAsycn<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> func,
            T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10)
        {
            var result = await Task.Run(() => {
                return func(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10);
            });
            return result;
        }

        /// <summary>
        /// Func异步方法
        /// </summary>
        /// <param name="Func"></param>
        public static async Task<TResult> ToFuncAsycn<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> func, 
            T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11)
        {
            var result = await Task.Run(() => {
                return func(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10,t11);
            });
            return result;
        }

        /// <summary>
        /// Func异步方法
        /// </summary>
        /// <param name="Func"></param>
        public static async Task<TResult> ToFuncAsycn<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> func,
            T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12)
        {
            var result = await Task.Run(() => {
                return func(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12);
            });
            return result;
        }

        /// <summary>
        /// Func异步方法
        /// </summary>
        /// <param name="Func"></param>
        public static async Task<TResult> ToFuncAsycn<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> func, 
            T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13)
        {
            var result = await Task.Run(() => {
                return func(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13);
            });
            return result;
        }

        /// <summary>
        /// Func异步方法
        /// </summary>
        /// <param name="Func"></param>
        public static async Task<TResult> ToFuncAsycn<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> func,
            T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13, T14 t14)
        {
            var result = await Task.Run(() => {
                return func(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14);
            });
            return result;
        }

        /// <summary>
        /// Func异步方法
        /// </summary>
        /// <param name="Func"></param>
        public static async Task<TResult> ToFuncAsycn<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> func,
            T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13, T14 t14, T15 t15)
        {
            var result = await Task.Run(() => {
                return func(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15);
            });
            return result;
        }

        #endregion
    }
}
