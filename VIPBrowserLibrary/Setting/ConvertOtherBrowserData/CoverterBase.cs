using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace VIPBrowserLibrary.Setting.ConvertOtherBrowserData
{
    /// <summary>
    /// 各他専ブラからのデータの変換を行う基底クラスです
    /// </summary>
    public abstract class ConverterBase : IOtherDataConverter
    {
        /// <summary>
        /// 指定したパスをもとにこのクラスの新しいインスタンスを初期化します
        /// </summary>
        /// <param name="path">指定されるパス</param>
        public ConverterBase(string path) 
        {
            this.ConvertBasePath = path;
        }
        /// <summary>
        /// このクラスの元の変換先のパスを取得または設定します
        /// </summary>
        public string ConvertBasePath { get; set; }
        /// <summary>
        /// NGリストを変換します
        /// </summary>
        public abstract void ConvertNGList();
        /// <summary>
        /// 過去ログファイルを変換します
        /// </summary>
        public abstract void ConvertLog();
        /// <summary>
        /// 書き込み履歴を変換します
        /// </summary>
        public void ConvertKakikomi()
        {
			var gs = new GeneralSetting();
			File.Copy(this.ConvertBasePath + "\\kakikomi.txt", gs.CurrentDirectory + "\\kakikomi.txt");
        }
        /// <summary>
        /// AAリストを変換します
        /// </summary>
        public abstract void ConvertAAList();
        /// <summary>
        /// お気に入り一覧を変換します
        /// </summary>
        public abstract void ConvertFavorite();
        /// <summary>
        /// Cookieを変換します
        /// </summary>
        public abstract void ConvertCookie();
    }
}
