using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Collections.Specialized;
using System.IO;
using System.Text.RegularExpressions;

namespace VIPBrowserLibrary.Other.UploadService
{
    /// <summary>
    /// 斧へのアップロードを提供します
    /// </summary>
    public class AxfcService
    {
        /// <summary>
        /// アップロード先のUri
        /// </summary>
        public Uri Url
        {
            get { return new Uri(this.url); }
            set { this.url = value.ToString(); }
        }
        private string url;
        private byte[] PostBytes;
        /// <summary>
        /// 指定したストリームを使用してこのクラスの新しいインスタンスを初期化します
        /// </summary>
        /// <param name="s">アップーロードするストリーム</param>
        public AxfcService(Stream s)
        {
            MemoryStream ms = new MemoryStream();
            s.CopyTo(ms);
            this.PostBytes = ms.ToArray(); 
        }
        /// <summary>
        /// 指定したパスからデータを読み込んでこのクラスの新しいインスタンスを初期化します
        /// </summary>
        /// <param name="path">読み込み先のパス</param>
        public AxfcService(string path)
        {
            this.PostBytes = File.ReadAllBytes(path);
        }
        /// <summary>
        /// ファイルに関連付けるオプションをアップロードします
        /// </summary>
        /// <param name="nvc">アップロードするNameValueCollection</param>
        /// <returns>アップロード後の応答データ</returns>
        public string UploadRequestData(NameValueCollection nvc)
        {
            WebClient wec = new WebClient();
            return Encoding.UTF8.GetString(wec.UploadValues("http://www1.axfc.net/uploader/post.pl", "POST", nvc));
        }
        /// <summary>
        /// Axfc側から送られてきたアップロードデータを解析します
        /// </summary>
        /// <param name="htmls">解析対象のHtml</param>
        /// <returns>解析したUrl</returns>
        public string ParseDataRequestAddress(string htmls)
        {
            var m = Regex.Match(htmls,@"<FORM\sMETHOD=""POST""\sENCTYPE=""multipart/form-data""\sACTION=""(?<url>.+?)"">", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            string url = this.url = m.Groups["url"].Value;
            return url;
        }
        /// <summary>
        /// バイナリデータをファイル名を指定して送信します
        /// </summary>
        /// <param name="fileName">関連づけるファイル名</param>
        /// <returns>Axfc側からの応答データ</returns>
        public string UploadByteData(string fileName)
        {
            //文字コード
            Encoding enc = Encoding.UTF8;
            //区切り文字列
            string boundary = System.Environment.TickCount.ToString();

            //WebRequestの作成
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(this.Url);
            //メソッドにPOSTを指定
            req.Method = "POST";
            //ContentTypeを設定
            req.ContentType = "multipart/form-data; boundary=" + "--" + boundary;

            //POST送信するデータを作成
            string postData = "";
            postData = 
                "--" + boundary + "\r\n" +
                "Content-Disposition: form-data; name=\"filedata\"; filename=\"" +
                    fileName + "\"\r\n" +
                "Content-Type: application/octet-stream\r\n" +
                "Content-Transfer-Encoding: binary\r\n\r\n";
            //バイト型配列に変換
            byte[] startData = enc.GetBytes(postData);
            postData = "\r\n--" + boundary + "--\r\n";
            byte[] endData = enc.GetBytes(postData);
            System.Timers.Timer t = new System.Timers.Timer();
            t.Elapsed += (_sender, _e) =>
            {
                t.Stop();
                WebClient wecc = new WebClient();
                wecc.Encoding = Encoding.UTF8;
                string url = this.url.Replace("upload.cgi?s=", "progress.pl?");
                string x = wecc.DownloadString(url);
                Match dataMatch = new Regex("</h4>(?<speed>.+)<br>(?<time>.+)<br>(?<byteCount>.+)<br>(?<percent>.+)(％|%)完了<BR>", RegexOptions.IgnoreCase).Match(x);
                wecc.Dispose();
                Console.WriteLine(dataMatch.Value);
                t.Start();

            };
            t.Interval = 5000;
            MemoryStream ms = new MemoryStream(this.PostBytes);
            //POST送信するデータの長さを指定
            req.ContentLength = startData.Length + endData.Length + ms.Length;

            //データをPOST送信するためのStreamを取得
            Stream reqStream = req.GetRequestStream();
            //送信するデータを書き込む
            reqStream.Write(startData, 0, startData.Length);
            //ファイルの内容を送信
            ulong sendByte = 0;
            byte[] readData = new byte[0x1000];
            int readSize = 0;
            t.Start();
            while (true)
            {
                readSize = ms.Read(readData, 0, readData.Length);
                sendByte += (ulong)readSize;
                if (readSize == 0)
                    break;
                reqStream.Write(readData, 0, readSize);
            }
            ms.Close();
            reqStream.Write(endData, 0, endData.Length);
            reqStream.Close();
            t.Close();
            //サーバーからの応答を受信するためのWebResponseを取得
            HttpWebResponse res = (HttpWebResponse)req.GetResponse();
            
            Stream resStream = res.GetResponseStream();
            //受信して表示
            StreamReader sr = new StreamReader(resStream, enc);
            string dat = sr.ReadToEnd();
            //閉じる
            sr.Close();
            return dat;
        }
    }
}
