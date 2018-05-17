using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIPBrowserLibrary.BBS.Common
{
    /// <summary>
    /// 書き込み履歴を保管するクラスです
    /// </summary>
    public class WriteRecord
    {

        Setting.GeneralSetting gs = new Setting.GeneralSetting();
        

        /// <summary>
        /// 書き込み履歴を書き込みます
        /// </summary>
        /// <param name="r">書き込むレス</param>
        /// <param name="td">書き込むスレッドの情報</param>
		/// <param name="ss">設定データを表します</param>
        public void Write(Chron.ThreadOrResData.Res r,Chron.ThreadOrResData.ThreadData td,Setting.SettingSerial ss) 
        {
            if (!ss.IsSaveWriteRecord)
                return;
            VIPBrowserLibrary.Common.BBSType bt;
            VIPBrowserLibrary.Common.Type t;
            VIPBrowserLibrary.Common.TypeJudgment.AllJudg(td.ThreadAddress,out bt,out t);
            string readUrl = String.Empty;
            if (t == VIPBrowserLibrary.Common.Type.thread)
                readUrl = VIPBrowserLibrary.Common.URLParse.DatToReadcgi(td.ThreadAddress, bt);
            else
                readUrl = td.ThreadAddress;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("--------------------------------------------");
            sb.Append("Date\t:").AppendLine(DateTime.Now.ToString("yy/MM/dd HH:mm:ss"));
            sb.Append("Subject\t:").AppendLine(td.ThreadName);
            sb.Append("URL\t:").AppendLine(readUrl);
            sb.Append("FROM\t:").AppendLine(r.Name);
            sb.Append("MAIL\t:").AppendLine(r.Mail);
            sb.AppendLine();
            sb.AppendLine(r.Sentence).AppendLine().AppendLine();


            Utility.TextUtility.Write(gs.CurrentDirectory + "\\kakikomi.txt", sb.ToString(), true);
        }
    }
}
