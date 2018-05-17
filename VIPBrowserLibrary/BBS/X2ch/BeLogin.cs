using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace VIPBrowserLibrary.BBS.X2ch
{
    /// <summary>
    /// Beの機能を提供します
    /// </summary>
    public class BeLogin
    {
        /// <summary>
        /// Beにログインします
        /// </summary>
        /// <param name="mail">メールアドレス</param>
        /// <param name="password">パスワード</param>
        public async Task<bool> Login(string mail,string password)
        {
            string postData = String.Format("m={0}&p={1}&submit=%C5%D0%CF%BF",mail,password);
            CookieCollection cc = BBS.Common.HttpClient.CookieManagement.ReadCookieFromDisk();
            Common.HttpClient hc = new Common.HttpClient("http://be.2ch.net/test/login.php");
            hc.Cookies = new CookieCollection();
            string data = await hc.PostStringAsync(postData);
            if (data.Contains("パスワードかメールアドレスが正しくないようです。"))
                return false;
            int i = 0;
            foreach (Cookie item in hc.Cookies)
            {
                i++;
            }
            if (i == 0)
                return false;
            cc.Add(hc.Cookies);
            Common.HttpClient.CookieManagement.WriteCookieToDisk(cc);
            Common.HttpClient.CookieManagement.RearrangeCookie();
            return true;
        }
        /// <summary>
        /// Beからログアウトします
        /// </summary>
        public void Logout()
        {
            Common.HttpClient.CookieManagement.DeleteCookie("DMDM");
            Common.HttpClient.CookieManagement.DeleteCookie("MDMD");
        }
    }
}
