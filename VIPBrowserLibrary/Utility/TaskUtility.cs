using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIPBrowserLibrary.Utility
{
    /// <summary>
    /// 非同期処理などTaskに関連する処理を行います
    /// </summary>
    public static class TaskUtility
    {
        /// <summary>
        /// スレッドを非同期で停止させます
        /// </summary>
        /// <returns></returns>
        public async static Task ThreadStop() 
        {
            await TaskUtility.ThreadStop(1000);
        }
        /// <summary>
        /// 指定した時間の間スレッドを非同期で停止させます
        /// </summary>
        /// <param name="time">指定するミリ秒</param>
        /// <returns></returns>
        public static async Task ThreadStop(int time) 
        {
            await AwaitSet.Awaitable<DBNull>.Run(() => 
            {
                System.Threading.Thread.Sleep(time);
                return DBNull.Value;
            });
        }
        /// <summary>
        /// 返し値のあるメソッドを非同期で実行します
        /// </summary>
        /// <typeparam name="TResult">非同期メソッドの返し値の型</typeparam>
        /// <param name="t">実行するメソッド</param>
        /// <returns>実行結果</returns>
        public static async Task<TResult> AsyncStart<TResult>(Func<TResult> t)
        {
            return await AwaitSet.Awaitable<TResult>.Run(t);
        }
    }
}
