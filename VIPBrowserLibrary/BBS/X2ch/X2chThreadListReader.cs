using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace VIPBrowserLibrary.BBS.X2ch
{
    /// <summary>
    /// 2chおよびそれに準ずるスレッドの読み込みを行います
    /// </summary>
    public class X2chThreadListReader : VIPBrowserLibrary.BBS.Common.CommonThreadListReader,Common.IThreadListReader
    {
        // Setting.GeneralSetting gs = new Setting.GeneralSetting();
        private Common.HttpClient hc = null;
        private string requestAddress = String.Empty;
        /// <summary>
        /// スレッドリストを取得する板のURLを指定してこのクラスの新しいインスタンスを初期化します
        /// </summary>
        /// <param name="boardAddress">板のアドレス</param>
        public X2chThreadListReader(string boardAddress)
        {
            hc = new Common.HttpClient(new Uri(boardAddress + "subject.txt"));
            requestAddress = boardAddress;
        }
        /// <summary>
        /// スレッド一覧を取得します
        /// </summary>
        /// <returns>取得したスレッドリストのListViewItem配列</returns>
        public async Task<ListViewItem[]> GetThreadList()
        {
            Regex re = new Regex(@"(?<datName>\d{10,10})[.]dat[<][>](?<threadTitle>.+)\s[(](?<resCount>\d{1,4})[)]", RegexOptions.Compiled);
            //ListViewItem[] liCollection = null;
            try
            {
                return await base.ParseThreadList(re, requestAddress, hc);
                // return liCollection;
                
            }
            catch (ArgumentNullException)
            {
                return null;
            }
        }
        /// <summary>
        /// オフラインでスレッドリストを取得します
        /// </summary>
        /// <returns>取得したスレッドリストのListViewItem配列</returns>
        public async Task<ListViewItem[]> OfflineGetThreadList()
        {
            Regex re = new Regex(@"(?<datName>\d{10,10})[.]dat[<][>](?<threadTitle>.+)\s[(](?<resCount>\d{1,4})[)]", RegexOptions.Compiled);
            //ListViewItem[] liCollection = null;
            try
            {
                return await base.ParseThreadList(re, requestAddress);
            }
            catch (ArgumentNullException)
            {
                return null;
            }
        }
    }
}
