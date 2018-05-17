using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Runtime.Serialization.Formatters.Soap;
using System.Xml;

namespace VIPBrowserLibrary.BBS.Common
{
    /// <summary>
    /// HttpWebRequsetを使いWebサイトに要求します
    /// </summary>
    public class HttpClient
    {
        #region プライベートメンバー
        private HttpWebRequest weq = null;
        private HttpWebResponse wep = null;
        private string requestUrl = String.Empty;
        #endregion

        #region パブリックメンバ
        /// <summary>
        /// 要求先のエンコーディングを指定します
        /// </summary>
        public string Encoding { get; set; }
        /// <summary>
        /// 要求時のUser-Agentを指定します
        /// </summary>
        public string UserAgent { get; set; }
        /// <summary>
        /// 要求時のクッキーを指定します
        /// </summary>
        public CookieCollection Cookies { get; set; }
        /// <summary>
        /// 要求時のホストを指定します
        /// </summary>
        public string Host { get; set; }
        /// <summary>
        /// 要求時のリファラーを指定します
        /// </summary>
        public string Referer { get; set; }
        /// <summary>
        /// 要求時のプロキシを指定します
        /// </summary>
        public WebProxy Proxy { get; set; }
        /// <summary>
        /// Hostプロパティを使って標準のCookieから読み取るか指定します
        /// （Cookiesプロパティを設定している場合はそちらが優先されます。）
        /// </summary>
        public bool IsReadCookieFromHost { get; set; }
        /// <summary>
        /// 掲示板以外へのサイトとのアクセスを行う際に指定します
        /// </summary>
        public bool IsOtherSiteRequest { get; set; }
        /// <summary>
        /// 要求を行った際に帰ってきたヘッダーを取得します
        /// </summary>
        public WebHeaderCollection Headers { get; private set; }
        /// <summary>
        /// 要求を行う際の追加のヘッダーを指定します
        /// </summary>
        public WebHeaderCollection ReqHeaders { get; set; }
        /// <summary>
        /// 要求を行う際のRangeヘッダーを設定します
        /// </summary>
        public int Range { get; set; }
        /// <summary>
        /// 要求を行った際に帰ってきたコードを取得します
        /// </summary>
        public HttpStatusCode RequestStatusCode { get; private set; }
        #endregion

        #region コンストラクター
        /// <summary>
        /// 要求先のUrlをSystem.String形式で指定してHttpClientクラスを初期化します
        /// </summary>
        /// <param name="url">要求先のUrlの</param>
        public HttpClient(string url)
        {
            requestUrl = url;
            weq = (HttpWebRequest)HttpWebRequest.Create(url);
            ctor();
        }
        /// <summary>
        /// 要求先のUrlをSystem.Uri形式で指定してHttpClientクラスを初期化します
        /// </summary>
        /// <param name="url">要求先のUrl</param>
        public HttpClient(Uri url)
        {
            requestUrl = url.AbsoluteUri;
            weq = (HttpWebRequest)HttpWebRequest.Create(url);
            ctor();
        }
        #endregion

        #region プライベートメソッド
        /// <summary>
        /// GETメソッドのコア
        /// </summary>
        /// <returns>取得したデータ</returns>
        private string GetStringCore()
        {
            try
            {
                weq = (HttpWebRequest)HttpWebRequest.Create(requestUrl);
                bool isHostCookie = false;
                if (this.Range != 0)
                    weq.AddRange(this.Range);
                weq.Headers.Add(HttpRequestHeader.ContentEncoding, Encoding);
                weq.UserAgent = UserAgent;
                //失敬
                weq.Credentials = new NetworkCredential("test", "test");
                if (Host != String.Empty)
                    weq.Host = Host;
                weq.Referer = Referer;
                weq.Headers.Add(ReqHeaders);
                if (Proxy != null)
                {
                    weq.Proxy = Proxy;
                }
                if (Cookies != null)
                {
                    CookieContainer cc = new CookieContainer();
                    foreach (Cookie item in Cookies)
                    {
                        cc.Add(item);
                    }
                    weq.CookieContainer = cc;
                }
                if (IsReadCookieFromHost == true && Host != String.Empty && Cookies == null)
                {
                    isHostCookie = true;
                }
                weq.Method = "GET";
                if (isHostCookie)
                {
                    CookieContainer c = new CookieContainer();
                    CookieCollection cc = CookieManagement.ReadCookieFromDisk();
                    foreach (Cookie item in cc)
                    {
                        c.Add(item);
                    }
                    weq.CookieContainer = c;
                }
                Log.Logger.WriteLog("Start Http GetRequest: "+this.requestUrl);
                wep = (HttpWebResponse)weq.GetResponse();
                if (Cookies != null)
                    Cookies.Add(wep.Cookies);
                if (isHostCookie)
                    CookieManagement.WriteCookieToDisk(wep.Cookies);

                Headers = wep.Headers;
                if (wep.StatusCode == HttpStatusCode.NotModified)
                    return String.Empty;
                this.RequestStatusCode = wep.StatusCode;
                Log.Logger.WriteLog("HTTP/1.1 "+this.RequestStatusCode.ToString());
                return new StreamReader(wep.GetResponseStream(), System.Text.Encoding.GetEncoding(Encoding)).ReadToEnd();
            }
            catch (WebException e)
            {
                this.RequestStatusCode = ((HttpWebResponse)e.Response).StatusCode;
                return null;
                //throw;
            }

            // return new StreamReader(((HttpWebResponse)weq.GetResponse()).GetResponseStream(),System.Text.Encoding.GetEncoding(Encoding)).ReadToEnd();
        }
        /// <summary>
        /// POSTメソッドのコア
        /// </summary>
        /// <param name="postString">送信するテキストデータ</param>
        /// <returns>返送データ</returns>
        private string PostStringCore(string postString)
        {
            bool isHostCookie = false;
            //weq.Credentials = new NetworkCredential("test", "test");
            System.Text.Encoding enc = System.Text.Encoding.GetEncoding(Encoding);
            byte[] postByte = enc.GetBytes(postString);
            int byteCount = postByte.Length;
            weq.Headers.Add(ReqHeaders);
            weq.Headers.Add(HttpRequestHeader.ContentEncoding, Encoding);
            weq.UserAgent = UserAgent;
            weq.Referer = Referer;
            //if (Host != String.Empty)
            //    weq.Host = Host;
            if (Proxy != null)
                weq.Proxy = Proxy;
            if (Cookies != null)
            {
                CookieContainer cc = new CookieContainer();
                foreach (Cookie item in Cookies)
                {
                    cc.Add(item);
                }
                weq.CookieContainer = cc;
            }
            if (IsReadCookieFromHost == true && Host != String.Empty && Cookies == null)
            {
                isHostCookie = true;
            }
            if (isHostCookie)
            {
                CookieContainer cc = new CookieContainer();
                foreach (Cookie item in CookieManagement.ReadCookieFromDisk())
                {
                    cc.Add(new Cookie(item.Name, item.Value, "/", item.Domain));
                }
                weq.CookieContainer = cc;
            }
            weq.Method = "POST";
            weq.ContentLength = byteCount;
            weq.ContentType = "application/x-www-form-urlencoded";
            Log.Logger.WriteLog("Start Http PostRequest: " + this.requestUrl);
            Stream s = weq.GetRequestStream();
            s.Write(postByte, 0, byteCount);
            System.Threading.Thread.Sleep(50);
            s.Dispose();
            wep = (HttpWebResponse)weq.GetResponse();

            if (Cookies != null)
                Cookies.Add(wep.Cookies);
            if (isHostCookie)
                CookieManagement.WriteCookieToDisk(wep.Cookies);
            Headers = wep.Headers;
            Stream ss = wep.GetResponseStream();
            Log.Logger.WriteLog("HTTP/1.1 " + wep.StatusCode.ToString());
            //weq.Abort();
            return new StreamReader(ss, System.Text.Encoding.GetEncoding(Encoding)).ReadToEnd();
        }
        /// <summary>
        /// データをダウンロードするGETメソッドのコア
        /// </summary>
        /// <returns>取得したデータ</returns>
        private Stream GetBinaryDataCore()
        {
            try
            {
                                weq = (HttpWebRequest)HttpWebRequest.Create(requestUrl);
                bool isHostCookie = false;
                if (this.Range != 0)
                    weq.AddRange(this.Range);
                weq.Headers.Add(HttpRequestHeader.ContentEncoding, Encoding);
                weq.UserAgent = UserAgent;
                if (Host != String.Empty)
                    weq.Host = Host;
                weq.Referer = Referer;
                weq.Headers.Add(ReqHeaders);
                if (Proxy != null)
                {
                    weq.Proxy = Proxy;
                }
                if (Cookies != null)
                {
                    CookieContainer cc = new CookieContainer();
                    foreach (Cookie item in Cookies)
                    {
                        cc.Add(item);
                    }
                    weq.CookieContainer = cc;
                }
                if (IsReadCookieFromHost == true && Host != String.Empty && Cookies == null)
                {
                    isHostCookie = true;
                }
                weq.Method = "GET";
                if (isHostCookie)
                {
                    CookieContainer c = new CookieContainer();
                    CookieCollection cc = CookieManagement.ReadCookieFromDisk();
                    foreach (Cookie item in cc)
                    {
                        c.Add(item);
                    }
                    weq.CookieContainer = c;
                }
                wep = (HttpWebResponse)weq.GetResponse();
                if (Cookies != null)
                    Cookies.Add(wep.Cookies);
                if (isHostCookie)
                    CookieManagement.WriteCookieToDisk(wep.Cookies);

                Headers = wep.Headers;
                if (wep.StatusCode == HttpStatusCode.NotModified)
                    throw new WebException();
                this.RequestStatusCode = wep.StatusCode;
                return wep.GetResponseStream();
            }
            catch (Exception)
            {
                
                throw;
            }
        }



        /// <summary>
        /// コンストラクターロジックの簡略化用
        /// </summary>
        private void ctor()
        {
            this.Encoding = "Shift-JIS";
            this.Cookies = null;
            this.Host = String.Empty;
            this.Proxy = null;
            this.Referer = String.Empty;
            this.UserAgent = "Monazilla/1.00(VIPBrowser ver 0.01 dev)";
            this.IsOtherSiteRequest = false;
            this.ReqHeaders = new WebHeaderCollection();
        }
        #endregion

        #region パブリックメソッド
        /// <summary>
        /// 同期操作で文字列を取得します
        /// </summary>
        /// <returns>取得したデータ</returns>
        public string GetStringSync()
        {
            return GetStringCore();
        }
        /// <summary>
        /// 非同期操作で文字列を取得します
        /// </summary>
        /// <returns>取得したデータ</returns>
        public async Task<string> GetStringAsync()
        {
            return await AwaitSet.Awaitable<string>.Run(() => GetStringCore());
        }
        /// <summary>
        /// 同期操作で文字列を送信します
        /// </summary>
        /// <param name="postData">送信するSystem.String形式のデータ</param>
        /// <returns>返送データ</returns>
        public string PostStringSync(string postData)
        {
            return PostStringCore(postData);
        }
        /// <summary>
        /// 非同期操作で文字列を送信します
        /// </summary>
        /// <param name="postData">送信するSystem.String形式のデータ</param>
        /// <returns>返送データ</returns>
        public async Task<string> PostStringAsync(string postData)
        {
            return await AwaitSet.Awaitable<string>.Run(() => PostStringCore(postData));
        }
        /// <summary>
        /// 非同期操作でデータを取得します
        /// </summary>
        /// <returns>取得したデータ</returns>
        public async Task<Stream> GetDataAsync()
        {
            return await AwaitSet.Awaitable<Stream>.Run(() => GetBinaryDataCore());
        }
        #endregion

        #region Cookie管理ロジック
        /// <summary>
        /// Cookieの管理をするクラスです。このクラスは継承できません。
        /// </summary>
        public static class CookieManagement
        {
            static VIPBrowserLibrary.Setting.GeneralSetting gs = new Setting.GeneralSetting();
            /// <summary>
            /// Cookieを読み込みます
            /// </summary>
            /// <returns>CookieCollection形式のCookie</returns>
            static public CookieCollection ReadCookieFromDisk()
            {
                using (FileStream fs = new FileStream(gs.CurrentDirectory + "\\cookie.soap", FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    try
                    {
                        CookieCollection cc = new CookieCollection();
                        //SoapFormatter sf = new SoapFormatter();
                        //return (CookieCollection)sf.Deserialize(fs);
                        string cookieData = Utility.TextUtility.Read(gs.CurrentDirectory + "\\cookie.ini");
                        foreach (string item in cookieData.Split(new string[] { "\r\n" }, StringSplitOptions.None))
                        {
                            if (item == String.Empty)
                                break;
                            string[] items = item.Split(';');
                            cc.Add(new Cookie(items[0], items[1], "/", items[2]));
                        }
                        return cc;
                    }
                    catch
                    {
                        return new CookieCollection();
                    }
                }
            }
            /// <summary>
            /// 指定した名前のCookieを削除します
            /// </summary>
            /// <param name="name">指定する名前</param>
            static public void DeleteCookie(string name)
            {
                try
                {
                    string cookieData = Utility.TextUtility.Read(gs.CurrentDirectory + "\\cookie.ini");
                    string[] cookieLine = cookieData.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                    List<string> cookieList = cookieLine.ToList<string>();
                    List<string> writeList = new List<string>();
                    foreach (string item in cookieList)
                    {
                        if (item.IndexOf(name + ";") != 0)
                        {
                            writeList.Add(item);
                        }
                    }
                    string writeData = String.Join("\r\n",writeList.ToArray());
                    Utility.TextUtility.Write(gs.CurrentDirectory + "\\cookie.ini",writeData,false);
                    return;
                }
                catch (Exception)
                {

                    throw;
                }
            }
            /// <summary>
            /// Cookieを書き込みます
            /// </summary>
            /// <param name="cc">書き込むCookieCollection</param>
            static public void WriteCookieToDisk(CookieCollection cc)
            {
                //using (FileStream fs = new FileStream(gs.CurrentDirectory + "\\cookie.soap",FileMode.Open,FileAccess.ReadWrite))
                //{
                try
                {


                    //SoapFormatter sf = new SoapFormatter();
                    //CookieCollection ccc = sf.Deserialize(fs) as CookieCollection;
                    //cc.Add(ccc);
                    //sf.Serialize(fs, cc);
                    foreach (Cookie item in cc)
                    {
                        if (item.Domain == String.Empty)
                            item.Domain = "2ch.net";
                        Utility.TextUtility.Write(gs.CurrentDirectory + "\\cookie.ini", item.Name + ";" + item.Value + ";" + item.Domain + "\r\n", true);
                    }
                }
                catch
                {
                    throw;
                }
                //}
            }
            /// <summary>
            /// 不必要なCookieを削除し書き込みのスピードを高速化させます
            /// </summary>
            static public void RearrangeCookie()
            {
                CookieCollection cc = ReadCookieFromDisk();
                try
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (Cookie item in cc)
                    {
                        sb.Append(item.Name + ";" + item.Value + ";" + item.Domain + "\r\n");
                    }
                    Utility.TextUtility.Write(gs.CurrentDirectory + "\\cookie.ini", sb.ToString(), false);
                }
                catch
                {
                    throw;
                }
                return;
            }


        }




        #endregion
    }
}

