using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIPBrowserLibrary.Setting
{
    /// <summary>
    /// エラーデータを送信するクラスです
    /// </summary>
    public static class PushErrorData
    {
        /// <summary>
        /// 指定した文字列を送信します
        /// </summary>
        /// <param name="data">送信するデータ</param>
        public static async void DataPush(string data)
        {
            Setting.GeneralSetting gs = new GeneralSetting();
            string path = gs.CurrentDirectory + "\\exlog.txt";
            BBS.Common.HttpClient hc = new BBS.Common.HttpClient("http://uravip.tonkotsu.jp/test/bbs.cgi");
            var coo = new System.Net.CookieCollection();
            coo.Add(new System.Net.Cookie("NAME", "","/","uravip.tonkotsu.jp"));
            coo.Add(new System.Net.Cookie("MAIL", "", "/", "uravip.tonkotsu.jp"));
            hc.Cookies = coo;
            await hc.PostStringAsync(String.Format("bbs=labo&key=1385391757&time={0}&FROM=&mail=&MESSAGE={1}&submit=書き込む",Chron.Calture.GetTime(DateTime.Now),data));

        }
    }
}
