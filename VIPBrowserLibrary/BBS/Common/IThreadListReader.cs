using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIPBrowserLibrary.BBS.Common
{
    /// <summary>
    /// スレッドリストリーダーのインターフェイスを表します
    /// </summary>
    public interface IThreadListReader
    {
        /// <summary>
        /// スレッドリストを読み込みます
        /// </summary>
        /// <returns>読み込んだスレ一覧を格納したListViewItem配列</returns>
        Task<System.Windows.Forms.ListViewItem[]> GetThreadList();
        /// <summary>
        /// スレッドリストをオフラインで読み込みます
        /// </summary>
        /// <returns>オフラインで読み込んだスレ一覧を格納したListViewItem配列</returns>
        Task<System.Windows.Forms.ListViewItem[]> OfflineGetThreadList();
    }
}
