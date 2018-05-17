using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIPBrowserLibrary.Chron
{
    /// <summary>
    /// ログを書き込むインターフェイスです
    /// </summary>
    public interface ILogInterface
    {
        /// <summary>
        /// 文字列を使用してログを書き込みます
        /// </summary>
        /// <param name="errmsg">書き込むログのテキスト</param>
        void WriteLog(string errmsg);
        /// <summary>
        /// 対象の例外を指定してログを書き込みます
        /// </summary>
        /// <param name="errex">書き込む例外</param>
        void WriteLog(Exception errex);
    }
}
