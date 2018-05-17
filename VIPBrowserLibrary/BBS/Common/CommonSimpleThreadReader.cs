using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace VIPBrowserLibrary.BBS.Common
{
    /// <summary>
    /// このクラスは使用しないでください
    /// </summary>
    public abstract class CommonSimpleThreadReader
    {
        VIPBrowserLibrary.Setting.GeneralSetting gs = new Setting.GeneralSetting();
        /// <summary>
        /// 要求先のURL
        /// </summary>
        protected string Url { get; set; }
        /// <summary>
        /// Datの保存フォルダー
        /// </summary>
        protected string DatFolder { get; set; }

        // private string ___url;

        //protected CommonSimpleThreadReader(string getUrl) 
        //{
        //    GetUrl = getUrl;
        //    Match m = new Regex(@"http://(?<host>.+)/(?<folder>.+)/dat/(?<threadKey>\d{9,10})[.]dat",RegexOptions.Compiled).Match(getUrl);
        //    readCgiUrl = String.Format("http://{0}/test/read.cgi/{1}/{2}/",m.Groups["host"].Value,m.Groups["folder"].Value,m.Groups["threadKey"].Value);
        //    datFolder = m.Groups["host"].Value +"-"+ m.Groups["folder"].Value;
        //    datKey = m.Groups["threadKey"].Value;
        //}
        private Chron.ThreadOrResData.Res[] r = null;
        /// <summary>
        /// 読み込むスレッドのキー
        /// </summary>
        protected string DatKey = String.Empty;
        /// <summary>
        /// 読み込むスレッドの保存先のフォルダー
        /// </summary>
        public string datFolder = String.Empty;

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
        //StringBuilder strb = new StringBuilder("<html>\n    <head><meta http-equiv='Content-Type' content=\"text/html;charset=UTF-8\"><thisdata Url=");
        /// <summary>
        /// idを格納するハッシュテーブルの定義
        /// </summary>
        Hashtable id = new Hashtable();
        private string GetData = String.Empty;
        

        private string GetResponses(Regex re)
        {
            resCount++;
            Console.WriteLine("Start Response Analysis");
            if (Url == null)
                throw new ArgumentNullException("取得先のURLが指定されていません");
            if (Url == String.Empty)
                throw new ArgumentException("取得先のURLは無効です");
            
            //strb.Clear();
            id.Clear();
            VIPBrowserLibrary.BBS.Common.HttpClient hc = new Common.HttpClient(Url);

            string getData = GetData = hc.GetStringSync();
            if(!System.IO.Directory.Exists(gs.DatFilePath + "\\" +DatFolder))
                System.IO.Directory.CreateDirectory(gs.DatFilePath + "\\" + DatFolder);
            Utility.TextUtility.Write(gs.DatFilePath + "\\" + DatFolder + "\\" + DatKey + ".dat", getData, false);
            //Utility.TextUtility.Write(gs.DatFilePath + "\\" + datFolder + "\\" + datKey + ".idx", getData, false);

            getData = Regex.Replace(getData,@"[<]a href.{1,100}\starget=""_blank""[>]","",RegexOptions.Compiled).Replace("</a>","");
            

            MatchCollection mc = re.Matches(getData);
            StringBuilder sb = new StringBuilder("<html><head></head><body><font face=\"ＭＳ Ｐゴシック\">\n<dl>\n");

            
            
            foreach (Match ids in mc)
            {
                string idData = ids.Groups["ID"].Value;
                if (id[idData] == null)
                {
                    id.Add(idData, new int[] { 0, 1 });
                }
                else
                {
                    ((int[])id[idData])[1] = ((int[])id[idData])[1] + 1;
                }
            }
            threadName = mc[0].Groups["threadName"].Value;
            r = new Chron.ThreadOrResData.Res[mc.Count];
            foreach (Match m in mc)
            {
                string idData = m.Groups["ID"].Value;

                

                ((int[])id[idData])[0] = ((int[])id[idData])[0] + 1;
                string idString;
                string data = ((int[])id[idData])[0] + "/" + ((int[])id[idData])[1];
                if (((int[])id[idData])[1] > 1)
                {
                    //idString = "<span style='color:#00F; text-decoration:underline;'>" + idData + "</span>" + " [" + ((int[])id[idData])[0] + "/" + ((int[])id[idData])[1] + "]";
                    idString = String.Format(@"<a href=""method:Extract($3,{0})"" style=""color:black;"" target=""_blank"">{0}</a>({1})", idData, data);
                }
                else if (((int[])id[idData])[1] > 3)
                {
                    // idString = "<span style='color:#F00; text-decoration:underline;'>" + idData + "</span>" + " [" + ((int[])id[idData])[0] + "/" + ((int[])id[idData])[1] + "]";
                    idString = String.Format(@"<a href=""method:Extract($3,{0})"" style=""color:black;"" target=""_blank"">{0}</a>({1})", idData, data);
                }
                else if (((int[])id[idData])[1] == 1)
                {
                    //   idString = "<span style='color:#000; text-decoration:underline;'>" + idData + "</span>" + " [" + ((int[])id[idData])[0] + "/" + ((int[])id[idData])[1] + "]";
                    idString = String.Format(@"<a href=""method:Extract($3,{0})"" style=""color:black;"" target=""_blank"">{0}</a>({1})", idData, data);
                }
                else
                {
                    idString = "<span style='color:#000; text-decoration:underline;'>" + idData + "</span>";
                }
                //if (idData.Length <= 8)
                //{
                //    idString = "<span style='color:#000; text-decoration:underline;'>" + idData + "</span>";
                //}
                string mail;
                Match mmm = new Regex(@"(?<date>.*)", RegexOptions.Compiled).Match(m.Groups["mail"].Value);
                if (mmm.Success)
                {
                    mail = mmm.Groups["date"].Value;
                    mail = mail.Replace("sage", "↓");
                }
                else
                {
                    mail = m.Groups["mail"].Value;
                }

                string sentenceData = m.Groups["sentence"].Value;

                sentenceData = Regex.Replace(sentenceData, @"&gt;&gt;(?<res>\d+)",
                        (d) =>
                        {
                            return "<a href=\"#\">&gt;&gt;" + d.Groups["res"].Value + "</a>";
                        });
                r[resCount - 1] = new Chron.ThreadOrResData.Res(resCount, m.Groups["name"].Value, m.Groups["mail"].Value, m.Groups["sentence"].Value, m.Groups["ID"].Value.Replace("ID:", "").Replace("発信元", ""), m.Groups["date"].Value, String.Empty, true);


                sb.Append("<dt id=\"s").Append(resCount).Append("\" class=\"\">");
                sb.Append("<indices id=\"").Append(resCount).Append("\"></indices>");
                sb.AppendFormat(@"<b><a href=""menu:{0}"" name=""{0}"" target=""_blank"">{0}</a></b>", resCount);
                sb.AppendFormat(@" 名前：<font color=""green""><b>{0}</b></font>", m.Groups["name"].Value);
                sb.AppendFormat(@" [{0}] ", mail);
                sb.AppendFormat(@"投稿日：{0} ", m.Groups["date"].Value);
                sb.Append(/*@"<a href=""method:Extract($3,{0})"" style=""color:black;"" target=""_blank"">{1}</a>", idData, */idString);
                sb.AppendFormat(@"  <a href=""method:Extract($5,)"" style=""color:black;"" target=""_blank""></a> </dt><dd>{0}<br><br></dd>", sentenceData);
                sb.AppendLine();
                resCount++;
            }
            sb.Append("</html>");

            resCount = 0;
            string datFile = sb.ToString();
            sb.Clear();
            Console.WriteLine("End the Res Analysis");
            return datFile;
        }
        /// <summary>
        /// 手に入れたhtml形式のスレッドデータ
        /// </summary>
        /// <returns>html形式のデータ</returns>
        protected string GetResponseMethods(Regex re)
        {
                return GetResponses(re);   
        }
        /// <summary>
        /// 指定した正規表現を使用してスレッドを読み込みます
        /// </summary>
        /// <param name="re">使用する正規表現</param>
        /// <param name="datData">元のdatデータ</param>
        /// <returns>読み込まれたhtml形式のデータ</returns>
        protected string GetResponseMethods(Regex re, ref string datData)
        {
            datData = GetData;
            return GetResponses(re);
        }
        /// <summary>
        /// スレ名を取得します
        /// </summary>
        protected string ThreadName
        {
            get { return threadName; }
        }
        /// <summary>
        /// Res構造体を取得します
        /// </summary>
        protected Chron.ThreadOrResData.Res[] GetResData { get { return r; } }
        /// <summary>
        /// Resのコレクションクラスを取得します
        /// </summary>
        protected Chron.ThreadOrResData.ResCollection GetResCollectionData
        {
            get
            {
                Chron.ThreadOrResData.ResCollection rc = new Chron.ThreadOrResData.ResCollection();
                rc.AddRange(r);
                return rc;
            }
        }
    }
}
