using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace VIPBrowserLibrary.BBS.MachiBBS
{
    /// <summary>
    /// まちBBSのスレを取得します
    /// </summary>
    public class MachiBBSThreadReader : Common.CommonThreadReader,Common.IThreadReader
    {
        private Setting.GeneralSetting gs = new Setting.GeneralSetting();
        private string datKey;
        private string datFolder;
        private string readCgiUrl;
		/// <summary>
		/// Htmlを出力するか設定します
		/// </summary>
		public bool IsOutHtml { get; set; }

        /// <summary>
        /// 取得先のURL
        /// </summary>
        public string GetUrl
        {
            get
            {
                return getUrl;
            }
            set
            {
                getUrl = value;
                Match m = Regex.Match(value, @"http://(?<host>\w+)[.]machi[.]to/bbs/read.cgi/(?<folder>\w+)/(?<key>\d{9,10})/?", RegexOptions.Compiled);
                datFolder = m.Groups["host"].Value + ".machi.to" + "-" + m.Groups["folder"].Value;
                datKey = m.Groups["key"].Value;
                readCgiUrl = "http://" + m.Groups["host"].Value + ".machi.to/bbs/read.cgi/" + m.Groups["folder"].Value + "/";
            }
        }
        /// <summary>
        /// 取得先のURLを指定してこのクラスのインスタンスを生成します
        /// </summary>
        /// <param name="url">取得先のURL</param>
        public MachiBBSThreadReader(string url)
        {
            this.GetUrl = url;
			this.IsOutHtml = true;
        }
        /// <summary>
        /// レスを取得します
        /// </summary>
        /// <returns>取得し変換したHTML</returns>
        public async Task<string> GetResponse()
        {
            Common.HttpClient hc = new Common.HttpClient(GetUrl.Replace("read.cgi","offlaw.cgi"));
            hc.Encoding = "Shift-JIS";
            string data = await hc.GetStringAsync();

            string fileDirectory = gs.DatFilePath + "\\" + datFolder;
            if (!Directory.Exists(fileDirectory))
                Directory.CreateDirectory(fileDirectory);

            string filePath = fileDirectory + "\\" + datKey;

            Utility.TextUtility.Write(filePath + ".cgi", data, false);
            //FileInfo fi = new FileInfo(filePath + ".cgi");

            long size = Encoding.GetEncoding("Shift-JIS").GetByteCount(data);
            string byteCount = Chron.Calture.DatSizeFormat(size, 1);
            string result = await AwaitSet.Awaitable<string>.Run(() =>
            {
                return base.GetResponse(data, new Regex(@"\d+<>(?<name>.*)<>(?<mail>.*)<>(?<date>.*?)((?<ID>ID:.*?|発信元:.*?|))<>(?<sentence>.*)<>(?<threadName>.*)\n", RegexOptions.Compiled),readCgiUrl, this.IsOutHtml);
            });
            this.resSets = base.ResSets;
            Chron.ThreadOrResData.ThreadData td = new Chron.ThreadOrResData.ThreadData();
            td.ThisBBS = VIPBrowserLibrary.Common.BBSType.machibbs;
            if (File.Exists(filePath + ".xml"))
            {
                td = Chron.ThreadOrResData.ThreadDataWriterAndReader.Read(filePath + ".xml");
                td.ThreadKey = datKey;
                td.ThreadAddress = GetUrl;
                td.DatSize = byteCount;
                td.ThreadName = threadName = base.ThreadName;
                td.GetRescount = base.ResCount;
                td.ThisFilePath = filePath;
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
            }
            td.LastModified = hc.Headers["Last-Modified"];
            Chron.ThreadOrResData.ThreadDataWriterAndReader.Write(td, filePath + ".xml");


            return result;
            //throw new NotImplementedException();
        }
        /// <summary>
        /// 取得先のスレッドタイトル
        /// </summary>
        public new string ThreadName
        {
            get { return threadName; }
        }
        /// <summary>
        /// 取得したRes配列
        /// </summary>
        public new Chron.ThreadOrResData.Res[] ResSets
        {
            get { return resSets; }
        }
        /// <summary>
        /// 取得先のスレのデータ
        /// </summary>
        public Chron.ThreadOrResData.ThreadData ThreadInfo
        {
            get { return threadInfo; }
        }

        private string threadName = String.Empty;
        private Chron.ThreadOrResData.Res[] resSets = null;
        private string getUrl = String.Empty;
        private Chron.ThreadOrResData.ThreadData threadInfo = null;

        /// <summary>
        /// オフラインでスレを取得します
        /// </summary>
        /// <returns>取得したデータ</returns>
        public string OfflineGetResponse()
        {
            string fileDirectory = gs.DatFilePath + "\\" + datFolder;
            if (!Directory.Exists(fileDirectory))
                Directory.CreateDirectory(fileDirectory);

            string filePath = fileDirectory + "\\" + datKey;

            string data = Utility.TextUtility.Read(filePath + ".cgi",Encoding.GetEncoding("Shift-JIS"));
            //FileInfo fi = new FileInfo(filePath + ".cgi");

            long size = Encoding.GetEncoding("Shift-JIS").GetByteCount(data);
            string byteCount = Chron.Calture.DatSizeFormat(size, 1);
            string result =  base.GetResponse(data, new Regex(@"\d+<>(?<name>.*)<>(?<mail>.*)<>(?<date>.*?)((?<ID>ID:.*?|発信元:.*?|))<>(?<sentence>.*)<>(?<threadName>.*)\n", RegexOptions.Compiled), readCgiUrl);
            
            this.resSets = base.ResSets;
            Chron.ThreadOrResData.ThreadData td = new Chron.ThreadOrResData.ThreadData();
            td.ThisBBS = VIPBrowserLibrary.Common.BBSType.machibbs;
            if (File.Exists(filePath + ".xml"))
            {
                td = Chron.ThreadOrResData.ThreadDataWriterAndReader.Read(filePath + ".xml");
                td.ThreadKey = datKey;
                td.ThreadAddress = GetUrl;
                td.DatSize = byteCount;
                td.ThreadName = threadName = base.ThreadName;
                td.GetRescount = base.ResCount;
                td.ThisFilePath = filePath;
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
            }
            return result;
        }
    }
}
