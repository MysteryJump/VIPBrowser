using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using VIPBrowserLibrary.Other.MyExtensions;

namespace VIPBrowserLibrary.BBS.X2ch
{
    /// <summary>
    /// 2chまたはそれに準ずる外部掲示板からスレッドを取得します
    /// </summary>
    public class X2chThreadReader : Common.CommonThreadReader,Common.IThreadReader
    {
        private string getUrl;
        private Setting.GeneralSetting gs = new Setting.GeneralSetting();
        private string datKey;
        private string datFolder;
        private string readCgiUrl;
        /// <summary>
        /// スレッドのUrlを設定または取得します
        /// </summary>
        public string GetUrl
        {
            get { return getUrl; }
            set 
            {
                getUrl = value;
                Match m = Regex.Match(value, @"http://(?<host>.+)/(?<folder>.+)/dat/(?<threadKey>\d{9,10})[.]dat");
                datFolder = m.Groups["host"].Value + "-" + m.Groups["folder"].Value;
                readCgiUrl = String.Format("http://{0}/test/read.cgi/{1}/{2}/", m.Groups["host"].Value, m.Groups["folder"].Value, m.Groups["threadKey"].Value);
                datKey = m.Groups["threadKey"].Value;
            }
        }

        

        /// <summary>
        /// 指定したスレッドのUrlを指定してこのクラスの新しいインスタンスを初期化します
        /// </summary>
        /// <param name="getUrl">使用するUrl</param>
        public X2chThreadReader(string getUrl)
        {
            GetUrl = getUrl;
			this.IsOutHtml = true;
        }
        /// <summary>
        /// このスレッドを取得して変換します
        /// </summary>
        /// <returns>変換したスレッドのUrl</returns>
        public async Task<string> GetResponse()
        {
            int range = 0;
            string fileDirectory = gs.DatFilePath + "\\" + datFolder;
            if (!System.IO.Directory.Exists(fileDirectory))
                System.IO.Directory.CreateDirectory(fileDirectory);
            string filePath = fileDirectory + "\\" + datKey;
            bool isRefresh = System.IO.File.Exists(filePath + ".dat");
            if (isRefresh)
            {
                Chron.ThreadOrResData.ThreadData tdd = Chron.ThreadOrResData.ThreadDataWriterAndReader.Read(filePath + ".xml");
				if (!tdd.IsNullObject())
				{
					range = tdd.DatByte;
				} 
                
                
            }
            Common.HttpClient hc = null;
            long size = 0;
            string data = String.Empty;
            string etag = String.Empty;
            if (!GetUrl.Contains(gs.DatFilePath))
            {
                hc = new Common.HttpClient(GetUrl);
                hc.UserAgent = "Monazilla/1.00(VIPBrowser ver 0.0.0.3)";
                if (!isRefresh)
                    data = await hc.GetStringAsync();
                else
                {
                    data = Utility.TextUtility.Read(filePath + ".dat", Encoding.GetEncoding("Shift-JIS"));
                    hc.Range = range - 1;
                    size = range;
                    string get = await hc.GetStringAsync();
                    System.Net.HttpStatusCode hsc = hc.RequestStatusCode;
                    switch (hsc)
                    {
                        case System.Net.HttpStatusCode.PartialContent:
                            data += get;
                            isRefresh = false;
                            break;
                        case System.Net.HttpStatusCode.NotModified:
                            //size += 10;
                            break;
                        case System.Net.HttpStatusCode.RequestedRangeNotSatisfiable:
                            hc.Range = 0;
                            size = 0;
                            data = await hc.GetStringAsync();
                            isRefresh = false;
                            break;
                        default:
                            break;
                    }
                    etag = hc.Headers["ETag"];
                }
            }
            else
            {
                data = System.IO.File.ReadAllText(GetUrl, Encoding.GetEncoding("Shift-JIS"));
            }

            Regex re = new Regex(@"(?<name>.*)<>(?<mail>.*)<>(?<date>.*?)((?<ID>ID:.*?|発信元:.*?|HOST:.*?|))(?<BE>BE:.*?)?<>(?<sentence>.*)<>(?<threadName>.*)\n", RegexOptions.Compiled);

            Utility.TextUtility.Write(filePath+ ".dat", data, false);
            
            //System.IO.FileInfo fi = new System.IO.FileInfo(filePath + ".dat");
            if (!isRefresh)
                size = Encoding.GetEncoding("Shift-JIS").GetByteCount(data);
            string byteCount = Chron.Calture.DatSizeFormat(size, 1);
            var result = await AwaitSet.Awaitable<string>.Run(() =>
            {
                return base.GetResponse(data, re,readCgiUrl, this.IsOutHtml);
            });
            if (result == null && (GetUrl.Contains(".2ch.net") || GetUrl.Contains(".bbspink.com")) && !GetUrl.Contains(gs.DatFilePath))
            {
                //offlaw2.soによる過去ログスレ取得予定
                hc = new Common.HttpClient(VIPBrowserLibrary.Common.URLParse.ReadcgiToOfflaw2(readCgiUrl));
                hc.Referer = readCgiUrl;
                data = await hc.GetStringAsync();
                result = await AwaitSet.Awaitable<string>.Run(() =>
                {
                    return base.GetResponse(data, re,readCgiUrl);
                });
                result += "<br><center>---このスレは過去ログ化されています---</center>";
            }
            this.resSets = base.ResSets;
            Chron.ThreadOrResData.ThreadData td = new Chron.ThreadOrResData.ThreadData();
            td.ThisBBS = VIPBrowserLibrary.Common.BBSType._2ch;
            if (System.IO.File.Exists(filePath + ".xml"))
            {
                td = Chron.ThreadOrResData.ThreadDataWriterAndReader.Read(filePath + ".xml");
                td.ThreadKey = datKey;
                td.ThreadAddress = GetUrl;
                td.DatSize = byteCount;
                td.ThreadName = threadName = base.ThreadName;
                td.GetRescount = base.ResCount;
                td.ThisFilePath = filePath;
                td.DatByte = (int)size;
                this.threadInfo = td;
            }
            else
            {
                td.ThreadKey = datKey;
                td.ThreadAddress = GetUrl;
                td.DatSize = byteCount;
                td.ThreadName = threadName = base.ThreadName;
                td.GetRescount = base.ResCount;
                td.ThisFilePath = filePath;
                this.threadInfo = td;
                td.DatByte = (int)size;
            }
            td.ETag = etag;
            Chron.ThreadOrResData.ThreadDataWriterAndReader.Write(td, filePath + ".xml");


            return result;
        }
        /// <summary>
        /// スレッド名
        /// </summary>
        public new string ThreadName { get { return threadName; } }
        private string threadName = null;
        /// <summary>
        /// スレッドに関連付けられたRes配列
        /// </summary>
        public new Chron.ThreadOrResData.Res[] ResSets { get { return resSets; } }
        private Chron.ThreadOrResData.Res[] resSets = null;
        /// <summary>
        /// スレッドに関連付けられたスレッドデータ
        /// </summary>
        public Chron.ThreadOrResData.ThreadData ThreadInfo { get { return threadInfo; } }
        private Chron.ThreadOrResData.ThreadData threadInfo = null;


        /// <summary>
        /// オフラインでスレッドを取得します
        /// </summary>
        /// <returns>取得したデータ</returns>
        public string OfflineGetResponse()
        {
            string fileDirectory = gs.DatFilePath + "\\" + datFolder;
            if (!System.IO.Directory.Exists(fileDirectory))
                System.IO.Directory.CreateDirectory(fileDirectory);
            string filePath = fileDirectory + "\\" + datKey;
            Regex re = new Regex(@"(?<name>.*)<>(?<mail>.*)<>(?<date>.*?)((?<ID>ID:.*?|発信元:.*?|HOST:.*?|))(?<BE>BE:.*?)?<>(?<sentence>.*)<>(?<threadName>.*)\n", RegexOptions.Compiled);

           string data = Utility.TextUtility.Read(filePath + ".dat");

            //System.IO.FileInfo fi = new System.IO.FileInfo(filePath + ".dat");
              long  size = Encoding.GetEncoding("Shift-JIS").GetByteCount(data);
            string byteCount = Chron.Calture.DatSizeFormat(size, 1);
            var result =  base.GetResponse(data, re, readCgiUrl);
            this.resSets = base.ResSets;
            Chron.ThreadOrResData.ThreadData td = new Chron.ThreadOrResData.ThreadData();
            td.ThisBBS = VIPBrowserLibrary.Common.BBSType._2ch;
            if (System.IO.File.Exists(filePath + ".xml"))
            {
                td = Chron.ThreadOrResData.ThreadDataWriterAndReader.Read(filePath + ".xml");
                td.ThreadKey = datKey;
                td.ThreadAddress = GetUrl;
                td.DatSize = byteCount;
                td.ThreadName = threadName = base.ThreadName;
                td.GetRescount = base.ResCount;
                td.ThisFilePath = filePath;
                td.DatByte = (int)size;
                this.threadInfo = td;
            }
            else
            {
                td.ThreadKey = datKey;
                td.ThreadAddress = GetUrl;
                td.DatSize = byteCount;
                td.ThreadName = threadName = base.ThreadName;
                td.GetRescount = base.ResCount;
                td.ThisFilePath = filePath;
                this.threadInfo = td;
                td.DatByte = (int)size;
            }
            return result;
        }
		/// <summary>
		/// Htmlを出力するか設定します
		/// </summary>
		public bool IsOutHtml { get; set; }
    }
}
