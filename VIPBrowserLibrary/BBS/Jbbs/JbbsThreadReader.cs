using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using VIPBrowserLibrary.Other.MyExtensions;

namespace VIPBrowserLibrary.BBS.Jbbs
{
    /// <summary>
    /// したらばへのスレッドの取得を行います
    /// </summary>
    public class JbbsThreadReader: Common.CommonThreadReader,Common.IThreadReader
    {
        private Setting.GeneralSetting gs = new Setting.GeneralSetting();
        private string datKey;
        private string datFolder;
        private string readCgiUrl;
		/// <summary>
		/// HTmlを出力するか設定します
		/// </summary>
		public bool IsOutHtml { get; set; }

        /// <summary>
        /// 取得先のURLを設定または取得します
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
                var m = Regex.Match(value, @"http://jbbs.(livedoor.jp|shitaraba.net)/bbs/rawmode.cgi/(?<category>\w+)/(?<folder>\d+)/(?<key>\d{9,10})/?");
                datFolder = "jbbs-"+ m.Groups["category"].Value + "-" + m.Groups["folder"].Value;
                readCgiUrl = String.Format("http://jbbs.shitaraba.jp/bbs/read.cgi/{0}/{1}/{2}/", m.Groups["category"].Value, m.Groups["folder"].Value, m.Groups["key"].Value);
                datKey = m.Groups["key"].Value;
            }
        }
        /// <summary>
        /// 指定したURLを用いてJbbsThreadReaderのインスタンスを初期化します
        /// </summary>
        /// <param name="url">取得先のurl</param>
        public JbbsThreadReader(string url)
        {
            this.GetUrl = url;
			this.IsOutHtml = true;
        }
        /// <summary>
        /// スレッドのデータをダウンロードしてをHTML形式に変換します
        /// </summary>
        /// <returns>変換されたスレッドのHTML</returns>
        public async Task<string> GetResponse()
        {
            string fileDirectory = gs.DatFilePath + "\\" + datFolder;
            if (!System.IO.Directory.Exists(fileDirectory))
                System.IO.Directory.CreateDirectory(fileDirectory);
            string filePath = fileDirectory + "\\" + datKey;
            string data = String.Empty;
            if (!GetUrl.Contains(gs.DatFilePath))
            {
                Common.HttpClient hc = new Common.HttpClient(GetUrl);
                hc.Encoding = "EUC-JP";
                data = await hc.GetStringAsync();

                Utility.TextUtility.Write(filePath + ".cgi", data, false);
                //System.IO.FileInfo fi = new System.IO.FileInfo(filePath + ".cgi");
            }
            else
            {
                data = System.IO.File.ReadAllText(GetUrl, Encoding.GetEncoding("Shift-JIS"));
            }
            long size = Encoding.GetEncoding("EUC-JP").GetByteCount(data);
            string byteCount = Chron.Calture.DatSizeFormat(size, 1);
            var result = await AwaitSet.Awaitable<string>.Run(() =>
            {
                return base.GetResponse(data, new Regex(@"\d{1,6}<>(?<name>.*)<>(?<mail>.*)<>(?<date>.*)<>(?<sentence>.+)<>(?<threadName>.*)<>(?<ID>.*)\n", RegexOptions.Compiled),readCgiUrl,this.IsOutHtml);
            });
            this.resSets = base.ResSets;
            Chron.ThreadOrResData.ThreadData td = new Chron.ThreadOrResData.ThreadData();
            td.ThisBBS = VIPBrowserLibrary.Common.BBSType.jbbs;
            if(System.IO.File.Exists(filePath + ".xml"))
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



            Chron.ThreadOrResData.ThreadDataWriterAndReader.Write(td, filePath + ".xml");
            return result;
        }
        /// <summary>
        /// スレッド名を取得します
        /// </summary>
        public new string ThreadName
        {
            get { return threadName; }
        }
        /// <summary>
        /// スレッドに関連づけられたRes配列を取得します
        /// </summary>
        public new Chron.ThreadOrResData.Res[] ResSets
        {
            get { return resSets; }
        }

        private string threadName = String.Empty;
        private Chron.ThreadOrResData.Res[] resSets = null;
        private string getUrl = String.Empty;

        private Chron.ThreadOrResData.ThreadData threadInfo = null;
        /// <summary>
        /// スレッドに関連付けられたスレッドの情報を取得します
        /// </summary>
        public Chron.ThreadOrResData.ThreadData ThreadInfo { get { return threadInfo; } }

        /// <summary>
        /// オフラインでスレを取得します
        /// </summary>
        /// <returns>取得したデータ</returns>
        public string OfflineGetResponse()
        {
            string fileDirectory = gs.DatFilePath + "\\" + datFolder;
            if (!System.IO.Directory.Exists(fileDirectory))
                System.IO.Directory.CreateDirectory(fileDirectory);
            string filePath = fileDirectory + "\\" + datKey;
            string data = Utility.TextUtility.Read(filePath + ".cgi");
            long size = Encoding.GetEncoding("EUC-JP").GetByteCount(data);
            string byteCount = Chron.Calture.DatSizeFormat(size, 1);
            var result = base.GetResponse(data, new Regex(@"\d{1,6}<>(?<name>.*)<>(?<mail>.*)<>(?<date>.*)<>(?<sentence>.+)<>(?<threadName>.*)<>(?<ID>.*)\n", RegexOptions.Compiled), readCgiUrl);
            
            this.resSets = base.ResSets;
            Chron.ThreadOrResData.ThreadData td = new Chron.ThreadOrResData.ThreadData();
            td.ThisBBS = VIPBrowserLibrary.Common.BBSType.jbbs;
            if (System.IO.File.Exists(filePath + ".xml"))
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
