using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIPBrowserLibrary.BBS.Common
{
    /// <summary>
    /// スレッドを読み込むクラスのインターフェイスです
    /// </summary>
    public interface IThreadReader
    {
        /// <summary>
        /// 読み込む対象のURL
        /// </summary>
        string GetUrl { get; set; }
        /// <summary>
        /// スレッドを取得します
        /// </summary>
        /// <returns>取得したデータ</returns>
        Task<string> GetResponse();
        /// <summary>
        /// スレッドの名前を取得します
        /// </summary>
        string ThreadName { get; }
        /// <summary>
        /// スレッドに関連付けられたレス配列を取得します
        /// </summary>
        Chron.ThreadOrResData.Res[] ResSets { get; }
        /// <summary>
        /// スレッドの情報を取得します
        /// </summary>
        Chron.ThreadOrResData.ThreadData ThreadInfo { get; }
        /// <summary>
        /// オフラインでスレッドを取得します
        /// </summary>
        /// <returns>取得したデータ</returns>
        string OfflineGetResponse();
		/// <summary>
		/// Htmlを出力するか設定します
		/// </summary>
		bool IsOutHtml { get; set; }
    }
}
