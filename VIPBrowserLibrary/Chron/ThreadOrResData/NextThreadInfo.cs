using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;


namespace VIPBrowserLibrary.Chron.ThreadOrResData
{
    /// <summary>
    /// 次スレに関するデータを提供します
    /// </summary>
    public class NextThreadInfo
    {
        /// <summary>
        /// 次スレ検索を行います
        /// </summary>
        /// <param name="searchText">検索するテキスト</param>
        /// <param name="items">検索するアイテム</param>
        /// <returns>検索順にソートされたアイテム</returns>
        public static ListViewItem[] SearchNextThread(string searchText,ListViewItem[] items)
        {
            NextThreadInfo nti = new NextThreadInfo();
            var k = nti.CreateRegexStrings(searchText);
            return nti.SearchThreads(items,k);
        }
		/// <summary>
		/// 次スレのスレタイを作成します
		/// </summary>
		/// <param name="baseTitle">元となるスレタイ</param>
		/// <returns>作成したスレタイ</returns>
		public static string CreateNextThreadTitle(string baseTitle)
		{
			bool isFir = true;
			bool isMatch = false;
			string a = Regex.Replace(baseTitle, "(?<num>\\d+)",
			(d) =>
			{
				isMatch = true;
				if (isFir)
				{
					isFir = false;
					long i = long.Parse(d.Groups["num"].Value);
					return (++i).ToString();
				}
				return d.Groups["num"].Value;
			},
			RegexOptions.Compiled | RegexOptions.RightToLeft);
			if (isMatch)
			{
				return a + "★1";
			}
			return a;
		}

        private ListViewItem[] SearchThreads(ListViewItem[] items,string[] k)
        {
            List<ListViewItem> lvi = new List<ListViewItem>();
            List<ListViewItem> baseList = new List<ListViewItem>(items);
            for (int i = k.Length - 1; i >= 0; i--)
            {
                for (int ii = items.Length - 1; ii >= 0; ii--)
                {
                    string text = items[ii].SubItems["Name"].Text;
                    bool isSuccess = Regex.Match(text, k[i]).Success;
					
                    if (isSuccess)
                    {
                        lvi.Add(items[ii]);
                        baseList.Remove(items[ii]);
                    }
                }
            }
            lvi.AddRange(baseList);
            return lvi.ToArray();
        }

        private string[] CreateRegexStrings(string search)
        {
            bool isFirst = true;
            string str1 = Regex.Replace(search, "(?<d>\\d+)", (d) => 
            {
                if (isFirst)
                {
                    isFirst = false;
                    return "\\d+";
                }
                else
                {
                    return d.Groups["d"].Value;
                }
            }, 
            RegexOptions.RightToLeft | RegexOptions.Compiled);
            string str2 = Regex.Replace(str1, "【.+?】", "【.+?】", RegexOptions.Compiled);
            return new string[] { str2, str1 };
        }
    }
}
