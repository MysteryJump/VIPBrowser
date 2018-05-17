using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace VIPBrowserLibrary.BBS.MachiBBS
{
    /// <summary>
    /// まちBBSのスレッドリストを取得します
    /// </summary>
    public class MachiBBSThreadListReader : Common.CommonThreadListReader,Common.IThreadListReader
    {

        private Common.HttpClient hc = null;
        private string requestAddress = String.Empty;
        /// <summary>
        /// まちBBSの板のアドレスを使用してMachiBBSThreadListReaderクラスの新しいインスタンスを初期化します
        /// </summary>
        /// <param name="boardAddress">取得する板のアドレス</param>
        public MachiBBSThreadListReader(string boardAddress)
        {
            Match m = Regex.Match(boardAddress, @"http://(?<host>\w+)[.]machi[.]to/(?<folder>\w+)/?", RegexOptions.Compiled);
            requestAddress = boardAddress;
            hc = new Common.HttpClient("http://" + m.Groups["host"].Value + ".machi.to" + "/bbs/offlaw.cgi/" + m.Groups["folder"].Value + "/");
        }
        /// <summary>
        /// 指定されたまちBBSのスレッドリストを取得します
        /// </summary>
        /// <returns>取得したLitsViewItem[]</returns>
        public async Task<System.Windows.Forms.ListViewItem[]> GetThreadList()
        {
            Regex re = new Regex(@"\d+<>(?<datName>\d{9,10})<>(?<threadTitle>.+)[(](?<resCount>\d{1,4})[)]", RegexOptions.Compiled);
            return await base.ParseThreadList(re, requestAddress, hc);
        }

        /// <summary>
        /// オフラインでスレ一覧を取得します
        /// </summary>
        /// <returns>取得したLitsViewItem[]</returns>
        public async Task<System.Windows.Forms.ListViewItem[]> OfflineGetThreadList()
        {
            Regex re = new Regex(@"\d+<>(?<datName>\d{9,10})<>(?<threadTitle>.+)[(](?<resCount>\d{1,4})[)]", RegexOptions.Compiled);
            return await base.ParseThreadList(re, requestAddress);
        }
    }
}
