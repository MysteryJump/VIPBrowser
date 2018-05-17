using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Re = System.Text.RegularExpressions.Regex;
using Mc = System.Text.RegularExpressions.MatchCollection;

namespace VIPBrowserLibrary.BBS.X2ch
{
    /// <summary>
    /// 2chおよびピンクちゃんねるから指定したスレッドを検索します
    /// </summary>
    public class AllBoardSercher
    {
        private string SearchText { get; set; }
        /// <summary>
        /// AllBoardSercherのインスタンスを初期化します
        /// </summary>
        public AllBoardSercher(string searchText)
        {
            this.SearchText = Utility.StringUtility.URLEncode(searchText,Encoding.GetEncoding("UTF-8"));
        }
        /// <summary>
        /// 全板横断検索を行います
        /// </summary>
        /// <returns>検索した結果のListViewItem</returns>
        public async Task<ListViewItem[]> SearchThread() 
        {
			string url = String.Format("http://ff2ch.syoboi.jp/?q={0}", SearchText);
            return await SearchCore(url);
        }
        /// <summary>
        /// 2chの全板横断検索を行います
        /// </summary>
        /// <returns>検索した結果のListViewItem</returns>
        [Obsolete("SearchThreadメソッドを使用してください")]
		public async Task<ListViewItem[]> Search2ch() 
        {
            string url = String.Format("http://find.2ch.net/?STR={0}&COUNT=50&BBS=2ch&TYPE=TITLE", SearchText);
            return await SearchCore(url);
        }
        /// <summary>
        /// BBSPinkの全板横断検索を行います
        /// </summary>
        /// <returns>検索した結果のListViewItem</returns>
		[Obsolete("SearchThreadメソッドを使用してください")]
        public async Task<ListViewItem[]> SearchBBSPink() 
        {
            string url = String.Format("http://find.2ch.net/?STR={0}&COUNT=50&BBS=bbspink&TYPE=TITLE", SearchText);
            return await SearchCore(url);
        }
        /// <summary>
        /// 検索メソッドコア
        /// </summary>
        /// <returns>結果を列挙したListViewItem[]</returns>
        private async Task<ListViewItem[]> SearchCore(string searchAddress) 
        {
            return await AwaitSet.Awaitable<ListViewItem[]>.Run(() =>
            {
                Common.HttpClient hc = new Common.HttpClient(searchAddress);
				hc.Encoding = "UTF-8";
				hc.IsOtherSiteRequest = true;
                ListViewItem[] liCollection = null;

                int i = 0;
				HtmlAgilityPack.HtmlDocument hdc = new HtmlAgilityPack.HtmlDocument();
                
                string data = hc.GetStringSync();
				hdc.LoadHtml(data);
				HtmlAgilityPack.HtmlNodeCollection nodes = hdc.DocumentNode.SelectNodes("/html[1]/body[1]/ul[1]/li");
				liCollection = new ListViewItem[nodes.Count];
				foreach (var item in nodes)
				{
					HtmlAgilityPack.HtmlDocument hd = new HtmlAgilityPack.HtmlDocument();
					hd.LoadHtml(item.InnerHtml);

		
					var title = hd.DocumentNode.SelectSingleNode("/a[1]").InnerText;
					var resCount = Re.Replace(hd.DocumentNode.SelectSingleNode("/span[1]").InnerText,"\\D","");
					var readUrl = hd.DocumentNode.SelectSingleNode("/a[1]").Attributes["href"].Value;
					var url = VIPBrowserLibrary.Common.URLParse.ReadcgiToDat(readUrl, VIPBrowserLibrary.Common.TypeJudgment.BBSTypeJudg(readUrl));
					var ma = Re.Match(url, @"(http://\w+[.]\w+[.]\w+/\w+/dat/(?<num>\d{9,10}).dat|http://.+?/\w+?/.+?/\w+?/(?<num>\d{9,10})/)", System.Text.RegularExpressions.RegexOptions.Compiled).Groups["num"].Value;
					var standtime = Chron.Calture.UnixTimeToDateTime(ma);
					ulong speed = Chron.Calture.ThreadAuthority(standtime, resCount);
					var lvi = new ListViewItem(new string[] { (i + 1).ToString(),title, resCount, standtime.ToString(), speed.ToString() });
					lvi.ImageKey = url;
					liCollection[i] = lvi;
					i++;
				}
				
				//Mc mc = Re.Matches(data, @"<dt><a\shref=""(?<readurl>http://\w+[.]\w+[.]\w+/test/read.cgi/\w+/\d{9,10}/)\d{1,3}-\d{1,3}"">(?<threadName>.{1,50})</a>\s[(](?<resCount>\d+)[)]\s-\s<font\ssize=-1><a\shref=http://\w+.\w+.\w+/\w+/>.+</a>.+</font></dt>", System.Text.RegularExpressions.RegexOptions.Compiled);
				
				//foreach (System.Text.RegularExpressions.Match m in mc)
				//{
				//	//string ma = Re.Match(m.Groups["readurl"].Value, @"http://\w+[.]\w+[.]\w+/test/read.cgi/\w+/(?<num>\d{9,10})/", System.Text.RegularExpressions.RegexOptions.Compiled).Groups["num"].Value;
				//	//DateTime standtime = Chron.Calture.UnixTimeToDateTime(ma);
				//	//ulong speed = Chron.Calture.ThreadAuthority(standtime,m.Groups["resCount"].Value);
				//	//ListViewItem lvi = new ListViewItem(new string[] { (i + 1).ToString(),m.Groups["threadName"].Value,m.Groups["resCount"].Value ,standtime.ToString(),speed.ToString()});
				//	//lvi.ImageKey = VIPBrowserLibrary.Common.URLParse.ReadcgiToDat(m.Groups["readurl"].Value, VIPBrowserLibrary.Common.BBSType._2ch);
				//	//liCollection[i] = lvi;
				//	i++;
				//}
                
                return liCollection;
            });
        }

    }
}
