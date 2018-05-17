using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIPBrowserLibrary.Setting.ConvertOtherBrowserData
{
    /// <summary>
    /// 他のブラウザからのデータ移行可能なクラスを表します。
    /// </summary>
    public interface IOtherDataConverter
    {
        /// <summary>
        /// NGリストのコンバートを行うメソッド
        /// </summary>
        void ConvertNGList();
        /// <summary>
        /// 過去ログのコンバートを行うメソッド
        /// </summary>
        void ConvertLog();
        /// <summary>
        /// AA一覧のコンバートを行うメソッド
        /// </summary>
        void ConvertAAList();
        /// <summary>
        /// お気に入りのコンバートを行うメソッド
        /// </summary>
        void ConvertFavorite();
        /// <summary>
        /// クッキーのコンバートを行うメソッド
        /// </summary>
        void ConvertCookie();
		/// <summary>
		/// コンバートの基本パス
		/// </summary>
		string ConvertBasePath { get; set; }
    }
}
