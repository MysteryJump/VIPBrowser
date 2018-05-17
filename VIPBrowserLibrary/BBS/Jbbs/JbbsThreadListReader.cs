using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace VIPBrowserLibrary.BBS.Jbbs
{
    /// <summary>
    /// したらばのスレッド一覧を取得します
    /// </summary>
    public class JbbsThreadListReader : Common.CommonThreadListReader,Common.IThreadListReader
    {
        Common.HttpClient hc = null;
        private string Url = String.Empty;
        /// <summary>
        /// 指定したURLを使用してJbbsThreadListReaderクラスの新しいインスタンスを初期化します
        /// </summary>
        /// <param name="url">初期化に使用する板名のURL</param>
        public JbbsThreadListReader(string url) 
        {
            hc = new Common.HttpClient(url + "subject.txt");
            Url = url;
        }
        /// <summary>
        /// スレッド一覧を取得します
        /// </summary>
        /// <returns>取得したスレッド一覧のListViewItem配列</returns>
        public async Task<System.Windows.Forms.ListViewItem[]> GetThreadList()
        {
            hc.Encoding = "EUC-JP";
            Regex re = new Regex(@"(?<datName>\d{9,10})[.]cgi,(?<threadTitle>.+)[(](?<resCount>\d{1,5})[)]", RegexOptions.Compiled);
            return await this.ParseThreadList(re, Url, hc);
        }

        /// <summary>
        /// オフラインでスレッド一覧を取得します
        /// </summary>
        /// <returns>取得したスレッド一覧のListViewItem配列</returns>
        public async Task<System.Windows.Forms.ListViewItem[]> OfflineGetThreadList()
        {
            Regex re = new Regex(@"(?<datName>\d{9,10})[.]cgi,(?<threadTitle>.+)[(](?<resCount>\d{1,5})[)]", RegexOptions.Compiled);
            return await this.ParseThreadList(re, Url);
        }
    }
}
