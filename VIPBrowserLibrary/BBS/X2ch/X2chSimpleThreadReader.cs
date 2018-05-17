using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace VIPBrowserLibrary.BBS.X2ch
{
    /// <summary>
    /// このクラスは使用しないでください
    /// </summary>
    [Obsolete("X2chThreadReaderクラスを使用してください")]
    public class X2chSimpleThreadReader : Common.CommonSimpleThreadReader
    {
        VIPBrowserLibrary.Setting.GeneralSetting gs = new Setting.GeneralSetting();

        /// <summary>
        /// 指定したURLを使用してX2chSimpleThreadReaderクラスの新しいインスタンスを初期化します
        /// </summary>
        /// <param name="getUrl">要求先のURL</param>
        public X2chSimpleThreadReader(string getUrl) 
        {
            
            GetUrl = getUrl;
            Match m = new Regex(@"http://(?<host>.+)/(?<folder>.+)/dat/(?<threadKey>\d{9,10})[.]dat",RegexOptions.Compiled).Match(getUrl);
            readCgiUrl = String.Format("http://{0}/test/read.cgi/{1}/{2}/",m.Groups["host"].Value,m.Groups["folder"].Value,m.Groups["threadKey"].Value);
            datFolder = m.Groups["host"].Value +"-"+ m.Groups["folder"].Value;
            datKey = m.Groups["threadKey"].Value;
        }


        private new string  datFolder = String.Empty;
        private string datKey = String.Empty;
        /// <summary>
        /// read.cgiのアドレス
        /// </summary>
        private string readCgiUrl;
        /// <summary>
        /// 手に入れる先のデータ
        /// </summary>
        private string GetUrl { get; set; }
        /// <summary>
        /// レス数の変数定義
        /// </summary>
        private int resCount { get; set; }
        /// <summary>
        /// スレッドの名前の変数定義
        /// </summary>
        private string threadName;
        /// <summary>
        /// StringBuilderの変数定義
        /// </summary>
        StringBuilder strb = new StringBuilder("<html>\n    <head><meta http-equiv='Content-Type' content=\"text/html;charset=UTF-8\"><thisdata Url=");
        /// <summary>
        /// idを格納するハッシュテーブルの定義
        /// </summary>
        Hashtable id = new Hashtable();

        

        private string GetResponses()
        {
            //int time = 0;
            System.Diagnostics.Stopwatch t = new System.Diagnostics.Stopwatch();
            
            //Console.WriteLine("Start Response Analysis");
            strb.Append("\"");
            strb.Append(readCgiUrl);
            strb.Append("\">\n</head>");
            if (GetUrl == null)
            {
                return null;
            }
            //strb.Clear();
            id.Clear();
            int IDdebugCount = 0;
            int DatadebugCount = 0;
            VIPBrowserLibrary.BBS.Common.HttpClient hc = new Common.HttpClient(GetUrl);

            string getData = hc.GetStringSync();
            if(!System.IO.Directory.Exists(gs.DatFilePath + "\\" +datFolder))
                System.IO.Directory.CreateDirectory(gs.DatFilePath + "\\" + datFolder);
            Utility.TextUtility.Write(gs.DatFilePath + "\\" + datFolder + "\\" + datKey + ".dat", getData, false);
            //Utility.TextUtility.Write(gs.DatFilePath + "\\" + datFolder + "\\" + datKey + ".idx", getData, false);
            t.Start();
            MatchCollection threadDataCollection = new Regex(@"(?<name>.*)<>(?<mail>.*)<>(?<date>.*?)((?<ID>ID:.*?|発信元:.*?|))(?<BE>BE:.*?)?<>(?<sentence>.*)<>(?<threadName>.*)\n",
                RegexOptions.Compiled).Matches(getData);

            foreach (Match ids in threadDataCollection)
            {
                IDdebugCount++;
                if (id[ids.Groups["ID"].Value] == null)
                {
                    id.Add(ids.Groups["ID"].Value, new int[] { 0, 1 });
                }
                else
                {
                    ((int[])id[ids.Groups["ID"].Value])[1] = ((int[])id[ids.Groups["ID"].Value])[1] + 1;
                }
            }

            foreach (Match item in threadDataCollection)
            {
                DatadebugCount++;
                resCount++;
                if (!String.IsNullOrEmpty(item.Groups["threadName"].Value))
                {
                    threadName = item.Groups["threadName"].Value;
                }

                ((int[])id[item.Groups["ID"].Value])[0] = ((int[])id[item.Groups["ID"].Value])[0] + 1;
                string idString;
                if (((int[])id[item.Groups["ID"].Value])[1] > 1)
                {
                    idString = "<span style='color:#00F; text-decoration:underline;'>" + item.Groups["ID"].Value + "</span>" + " [" + ((int[])id[item.Groups["ID"].Value])[0] + "/" + ((int[])id[item.Groups["ID"].Value])[1] + "]";
                }
                else if (((int[])id[item.Groups["ID"].Value])[1] > 3)
                {
                    idString = "<span style='color:#F00; text-decoration:underline;'>" + item.Groups["ID"].Value + "</span>" + " [" + ((int[])id[item.Groups["ID"].Value])[0] + "/" + ((int[])id[item.Groups["ID"].Value])[1] + "]";
                }
                else if (((int[])id[item.Groups["ID"].Value])[1] == 1)
                {
                    idString = "<span style='color:#000; text-decoration:underline;'>" + item.Groups["ID"].Value + "</span>" + " [" + ((int[])id[item.Groups["ID"].Value])[0] + "/" + ((int[])id[item.Groups["ID"].Value])[1] + "]";
                }
                else
                {
                    idString = "<span style='color:#000; text-decoration:underline;'>" + item.Groups["ID"].Value + "</span>";
                }
                if (item.Groups["ID"].Value.Length <= 8)
                {
                    idString = "<span style='color:#000; text-decoration:underline;'>" + item.Groups["ID"].Value + "</span>";
                }

                strb.Append("  <body bgcolor='#efefef'><font face='ＭＳ Ｐゴシック'>\n");
                strb.Append("    <div style='margin-left:10px;'><span style='color:#00F; text-decoration:underline'>");
                strb.Append(resCount.ToString());
                strb.Append("</span>");
                strb.Append(" ：<font color=#008800><b>");
                strb.Append(item.Groups["name"].Value);
                strb.Append("</b></font>[");
                string mail;
                Match mmm = new Regex(@"(?<date>.*)",RegexOptions.Compiled).Match(item.Groups["mail"].Value);
                if (mmm.Success)
                {
                    mail = mmm.Groups["date"].Value;
                    mail = mail.Replace("sage", "↓");
                }
                else
                {
                    mail = item.Groups["mail"].Value;
                }
                strb.Append((mail == "↓" ? "<span style='color:#F00;'>" + mail + "</span>" : mail));
                strb.Append("]");
                strb.Append(item.Groups["date"].Value);
                strb.Append(" ");
                //strb.Append("<span style='text-decoration:underline;'>ID:</span>");
                strb.Append(idString);
                strb.Append("</div><div style='margin-left:25px;'>");
                strb.Append(item.Groups["BE"].Value);
                strb.Append("<br>");
                strb.Append(item.Groups["sentence"].Value);
                strb.Append("<br><br></div>\n");
                //Console.WriteLine("ResCount : "+resCount.ToString());
            }
            strb.Append("</font></body></html>");
            t.Stop();
            Console.WriteLine(t.ElapsedMilliseconds);
            resCount = 0;
            string datFile = strb.ToString();
            strb.Clear();
            Console.WriteLine("End the Res Analysis");
            return datFile;
        }
        /// <summary>
        /// 手に入れたhtml形式のスレッドデータ
        /// </summary>
        /// <returns>html形式のデータ</returns>
        public async Task<string> GetResponseMethod()
        {
            this.Url = GetUrl;
            this.DatFolder = datFolder;
            this.DatKey = datKey;
            return await AwaitSet.Awaitable<string>.Run(() =>
            {
                return base.GetResponseMethods(new Regex(@"(?<name>.*)<>(?<mail>.*)<>(?<date>.*?)((?<ID>ID:.*?|発信元:.*?|))(?<BE>BE:.*?)?<>(?<sentence>.*)<>(?<threadName>.*)\n",RegexOptions.Compiled));
            });
        }
        /// <summary>
        /// スレッド名を取得します
        /// </summary>
        public string GetThreadName
        {
            get { return this.ThreadName; }
        }
        /// <summary>
        /// スレッドに関連付けられたResCollectionを取得します
        /// </summary>
        public Chron.ThreadOrResData.ResCollection GetResCollection
        {
            get { return this.GetResCollectionData; }
        }
    }
}
