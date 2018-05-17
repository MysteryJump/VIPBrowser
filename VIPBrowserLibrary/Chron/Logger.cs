using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Log
{
    /// <summary>
    /// ログを書き込むクラスです
    /// </summary>
    public static class Logger
    {
        static VIPBrowserLibrary.Setting.GeneralSetting gs = new VIPBrowserLibrary.Setting.GeneralSetting();
        /// <summary>
        /// 指定したエラーメッセージを書き込みます
        /// </summary>
        /// <param name="errmsg">書き込むエラーメッセージ</param>
        public static void WriteLog(string errmsg)
        {
            string msg = errmsg + "  " + DateTime.Now.ToString() + "\r\n";
            if (msg.Contains("Exit Application"))
                msg += "\r\n";
            VIPBrowserLibrary.Utility.TextUtility.Write(gs.CurrentDirectory + "\\log.txt", msg, true);
            
        }
        /// <summary>
        /// 指定した例外を書き込みます
        /// </summary>
        /// <param name="errex">書き込む例外</param>
        public static void WriteLog(Exception errex)
        {
            VIPBrowserLibrary.Utility.TextUtility.Write(gs.CurrentDirectory + "\\exlog.txt", errex.ToString() + DateTime.Now.ToString() + "\r\n", true);
            VIPBrowserLibrary.Setting.PushErrorData.DataPush(VIPBrowserLibrary.Utility.StringUtility.URLEncode(errex.ToString()));
        }
    }
}
