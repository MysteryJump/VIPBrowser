using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIPBrowserLibrary.BBS.Jbbs
{
    /// <summary>
    /// したらばにレスをポストするクラスです
    /// </summary>
    public class JbbsPoster : Common.PostBase
    {
        private Common.HttpClient hc = null;
        private const string hostAddress = "http://jbbs.shitaraba.net/bbs/write.cgi";

        /// <summary>
        /// JbbsPosterクラスの新しいインスタンスを初期化します
        /// </summary>
        public JbbsPoster()
        {
            // TODO: Complete member initialization
        }
        /// <summary>
        /// レスをしたらばに送信します
        /// </summary>
        /// <param name="postData">送信するデータを収めたDictionary&lt;string,string&gt;</param>
        /// <param name="isThread">スレッドである場合はtrue,レスである場合はfalse</param>
        /// <param name="td">スレッドの情報</param>
        /// <returns>成功の可否</returns>
        public async Task<bool> PostJbbs(Dictionary<string, string> postData, bool isThread,Chron.ThreadOrResData.ThreadData td)
        {
            hc = new Common.HttpClient(hostAddress);
            bool isCan = false;
            hc.Encoding = "EUC-JP";
            hc.Host = "jbbs.livedoor.jp";
            hc.IsReadCookieFromHost = true;
            if (!isThread)
                hc.Referer = "http://jbbs.shitaraba.net/bbs/read.cgi/" + postData["DIR"] + "/" + postData["BBS"] + "/" + postData["KEY"] + "/";
            else 
                hc.Referer = "http://jbbs.shitaraba.net/" + postData["DIR"] + "/" + postData["BBS"] + "/";
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<string, string> item in postData)
            {
                sb.Append(item.Key);
                sb.Append("=");
                sb.Append(item.Value);
                sb.Append("&");
            }
            if (isThread)
                sb.Append("submit=新規スレッド作成");
            else
                sb.Append("submit=書き込む");
            string postString = sb.ToString().TrimEnd('&');
            string data = await hc.PostStringAsync(postString);
            if (data.Contains("書きこみました。"))
            {
                base.WriteRecords(postData, td);
                return isCan = true;
            }
            
            return isCan;
        }
    }
}
