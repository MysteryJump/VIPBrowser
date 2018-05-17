using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace VIPBrowserLibrary.Utility
{
    /// <summary>
    /// テキストの書き込み・読み取りを行います
    /// </summary>
    public static class TextUtility
    {
        /// <summary>
        /// 指定したパスにテキストを書きこみます
        /// </summary>
        /// <param name="filePath">書き込み先のパス</param>
        /// <param name="writeData">書き込むテキスト</param>
        /// <param name="append">追記する場合はtrue,上書きする場合はfalse</param>
        public static void Write(string filePath, string writeData, bool append)
        {
            WriteCore(filePath, writeData, Encoding.GetEncoding("Shift-JIS"), append);
        }
        /// <summary>
        /// 指定したエンコーディングを使用して指定したパスにテキストを書き込みます
        /// </summary>
        /// <param name="filePath">書き込み先のパス</param>
        /// <param name="writeData">書き込むテキスト</param>
        /// <param name="append">追記する場合はtrue,上書きする場合はfalse</param>
        /// <param name="enc">使用するエンコーディング</param>
        public static void Write(string filePath, string writeData,Encoding enc ,bool append)
        {
            WriteCore(filePath, writeData, enc, append);
        }
        /// <summary>
        /// 非同期で指定したパスにテキストを書き込みます
        /// </summary>
        /// <param name="filePath">書き込み先のパス</param>
        /// <param name="writeData">書き込むテキスト</param>
        /// <param name="append">追記する場合はtrue,上書きする場合はfalse</param>
        public static async Task WriteAsync(string filePath, string writeData,bool append)
        {
            await AwaitSet.Awaitable<string>.Run(() =>
            {
                WriteCore(filePath, writeData, Encoding.GetEncoding("Shift-JIS"), append);
                return "";
            });
        }
        /// <summary>
        /// 非同期で指定したエンコーディングを使用して指定したパスにテキストを書き込みます
        /// </summary>
        /// <param name="filePath">書き込み先のパス</param>
        /// <param name="writeData">書き込むテキスト</param>
        /// <param name="append">追記する場合はtrue,上書きする場合はfalse</param>
        /// <param name="enc">使用するエンコーディング</param>
        public static async Task WriteAsync(string filePath, string writeData, Encoding enc,bool append)
        {
            await AwaitSet.Awaitable<string>.Run(() => 
            {
                WriteCore(filePath, writeData, enc, append);
                return "";
            });
        }
        /// <summary>
        /// 指定されたパスのテキストを読み込みます
        /// </summary>
        /// <param name="filePath">読み込み先のパス</param>
        /// <returns>読み込まれたテキスト</returns>
        public static string Read(string filePath) { return ReadCore(filePath, Encoding.GetEncoding("Shift-JIS")); }
        /// <summary>
        /// 指定されたパスのテキストを指定したエンコーディングを用いて読み込みます
        /// </summary>
        /// <param name="filePath">読み込み先のパス</param>
        /// <param name="enc">使用するエンコーディング</param>
        /// <returns>読み込まれたテキスト</returns>
        public static string Read(string filePath, Encoding enc) { return ReadCore(filePath, enc); }
        /// <summary>
        /// 非同期で指定されたパスのテキストを指定したエンコーディングを用いて読み込みます
        /// </summary>
        /// <param name="filePath">読み込み先のパス</param>
        /// <param name="enc">使用するエンコーディング</param>
        /// <returns>読み込まれたテキスト</returns>
        public static async Task<string> ReadAsync(string filePath, Encoding enc) { return await AwaitSet.Awaitable<string>.Run(() => ReadCore(filePath, enc)); }
        /// <summary>
        /// 非同期で指定されたパスのテキストを読み込みます
        /// </summary>
        /// <param name="filePath">読み込み先のパス</param>
        /// <returns>読み込まれたテキスト</returns>
        public static async Task<string> ReadAsync(string filePath) { return await AwaitSet.Awaitable<string>.Run(() => ReadCore(filePath, Encoding.GetEncoding("Shift-JIS"))); }
        private static void WriteCore(string filePath,string writeData,Encoding enc,bool apppend) 
        {
            using (StreamWriter s = new StreamWriter(filePath,apppend,enc))
            {
                s.Write(writeData);
                
            }
        }

        private static string ReadCore(string filePath,Encoding enc)
        {
            try
            {
                using (StreamReader sr = new StreamReader(filePath, enc))
                {
                    return sr.ReadToEnd();
                }
            }
            catch (FileNotFoundException)
            { return null; }
            catch (DirectoryNotFoundException)
            { return null; }
            catch (ArgumentException) { return null; }
        }
    }
}