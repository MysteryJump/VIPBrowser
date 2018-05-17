using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace VIPBrowserLibrary.Other
{
//    /// <summary>
//    /// madakana.cgiからこのホストが規制されている確認します
//    /// </summary>
//    public class HostDenyChecker
//    {
//        private string madakanaUrl;
//        private bool is2ch;
//        private string myHost;
//        public string ControlBoardName { get { return boardName; } }
//        private string boardName;
//        /// <summary>
//        /// 指定したmadakana.cgiのURLを使用してこのインスタンスを初期化します
//        /// </summary>
//        /// <param name="url">madakana.cgiのURL</param>
//        public HostDenyChecker(string url)
//        {
//            if (!url.Contains("madakana.cgi"))
//                throw new ArgumentException("このURLの形式には対応していません");
//            this.madakanaUrl = url;
//        }

//        public async Task<bool> IsDeny()
//        {
//            BBS.Common.HttpClient hc = new BBS.Common.HttpClient("http://uravip.tonkotsu.jp/host.pl");
//            this.myHost = await hc.GetStringAsync();
//            if (this.is2ch)
//            {
//                this.Analysis2chDenyListData();
//                return true;
//            }
//            else
//            {
//                object[] data = (object[])await this.AnalysisOtherDenyListData();
//                this.boardName = (string)data[1];
//                return (bool)data[0];
//            }
//        }

//        private async Task<object> AnalysisOtherDenyListData()
//        {
//            string denyListData = await this.GetDenyHostList();
//            var mc = Regex.Matches(denyListData, @"<p>(?<boards>.+?)</p>", RegexOptions.IgnoreCase | RegexOptions.Compiled);
//            for (int i = 1; i < mc.Count; i++)
//            {
//                List<string> denyList = new List<string>();
//                string data = mc[i].Groups["boards"].Value;
//                string patern = 
//@"#-----------------------------------------------------------------------------<br>
//# <a href="".+?"">(?<name>.+?)</a>.+?<br>
//#-----------------------------------------------------------------------------<br>";
//                var m = Regex.Match(data, patern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
//                var baseString = Regex.Replace(data, patern, "");
//                foreach (var item in baseString.Split(new string[]{"<br>"} , StringSplitOptions.RemoveEmptyEntries))
//                {
//                    if (item.TrimStart(' ').TrimStart('　').IndexOf('#') == 0)
//                        denyList.Add(item);
//                }
//                foreach (var item in denyList)
//                {
//                    if (Regex.IsMatch(this.myHost, item))
//                    {
//                        return new object[]{ false,m.Groups["name"].Value};
//                    }
//                }
//            }
//            return new object[] { true ,String.Empty };
//        }
//        private async void Analysis2chDenyListData()
//        {
//            string denyListData = await this.GetDenyHostList();
            
//        }

//        private async Task<string> GetDenyHostList() 
//        {
//            BBS.Common.HttpClient hc = new BBS.Common.HttpClient(this.madakanaUrl);
//            if (this.is2ch)
//            {
//                hc.Encoding = "Shift-JIS";
//                return await hc.PostStringAsync("HOST=" + this.myHost + "&submit=検索");
//            }
//            else
//            {
//                hc.Encoding = "Shift-JIS";
//                return await hc.GetStringAsync();
//            }
//        }
//    }

    /// <summary>
    /// 2chの規制状況を確認します
    /// </summary>
    public static class SimpleDenyChecker
    {
        /// <summary>
        /// 規制されているか確認します
        /// </summary>
        /// <returns>規制状況</returns>
        /// <param name="specific">板別規制の際の板名</param>
        public static DenyType Check(ref string[] specific)
        {
            BBS.Common.HttpClient hc = new BBS.Common.HttpClient("http://uravip.tonkotsu.jp/host.pl");
            var host = hc.GetStringSync();
            BBS.Common.HttpClient hcc = new BBS.Common.HttpClient("http://qb7.2ch.net/_403/madakana.cgi");
            string dat = hcc.GetStringSync();
            var data = dat.Split(new string[] { "<hr>" }, StringSplitOptions.RemoveEmptyEntries);
            var baseString = data[1];
            string[] lines = baseString.Split(new string[] { "<br>", "<br/>" }, StringSplitOptions.RemoveEmptyEntries);
            var l = new List<string>();
            foreach (var item in lines)
            {
                string li = item.Replace("\n","");
                if (li.IndexOf("#") != 0 && !String.IsNullOrWhiteSpace(li))
                {
                    li = VIPBrowserLibrary.Utility.StringUtility.RemoveTag(li, "b");
                    li = Regex.Replace(li, "<small.+?>.+?</small>", "", RegexOptions.Compiled).Replace(" ","");
                    l.Add(li);
                }
            }
            List<string> speList = new List<string>();
            bool isC = false;
            DenyType dt = 0;
            foreach (var item in l)
            {
                string hosts = Regex.Replace(item, @"_[\w_]+_", "", RegexOptions.Compiled);
                if (Regex.IsMatch(host, hosts, RegexOptions.Compiled))
                {
                    if (Regex.IsMatch(item, @"_\d{10}_", RegexOptions.Compiled))
                        continue;
                    isC = true;
                    if (item.Contains("_HANA_"))
                    {
                        dt = DenyType.Hana | dt;
                    }
                    else if (item.Contains("_BBS_"))
                    {
                        dt = DenyType.SpecificBoard | dt;
                        string boardName = Regex.Match(item, "_BBS_(?<name>.+?)_", RegexOptions.Compiled).Groups["name"].Value;
                        speList.Add(boardName);
                    }
                    else if (item.Contains("_2CH_"))
                    {
                        dt = DenyType.Twoch | dt;
                    }
                    else if (item.Contains("_PINK_"))
                    {
                        dt = DenyType.Pink | dt;
                    }
                    else
                    {
                        dt = DenyType.AllBoard | dt;
                    }
                }
            }
            specific = speList.ToArray();
            if (isC)
                return dt;
            else
                return DenyType.None;
        }
    }
    /// <summary>
    /// 規制状況について設定します
    /// </summary>
    [Flags]
    public enum DenyType
    {
        /// <summary>
        /// 全板規制
        /// </summary>
        AllBoard = 1,
        /// <summary>
        /// 花園規制
        /// </summary>
        Hana = 2,
        /// <summary>
        /// 2ch規制
        /// </summary>
        Twoch = 4,
        /// <summary>
        /// PINK規制
        /// </summary>
        Pink = 8,
        /// <summary>
        /// 特定板規制
        /// </summary>
        SpecificBoard = 16,
        /// <summary>
        /// 規制なし
        /// </summary>
        None = 32
    }
}
