using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Forms;
using VIPBrowserLibrary.Chron.ThreadOrResData;
using mshtml;
using VIPBrowserLibrary.Utility;
using VIPBrowserLibrary.Chron.ThreadOrResData.Abone;

namespace VIPBrowserLibrary.Common
{
    /// <summary>
    /// スレッドビュワーに必要な機能を実装します
    /// </summary>
    public interface IThreadViewer :VIPBrowserLibrary.Other.MyExtensions.GUIExtension.IControl
    {
        /// <summary>
        /// あぼーんの情報を設定または取得します
        /// </summary>
        AboneManagement AboneData { get; set; }
        /// <summary>
        /// 自動更新のタイマーを設定または取得します
        /// </summary>
        Timer RefereshTimer { get; set; }
        /// <summary>
        /// このオブジェクトが自動更新の対象かどうか設定または取得します
        /// </summary>
        bool IsRefresh { get; set; }
        /// <summary>
        /// このオブジェクトに関連付けられているDatのURLを取得または設定します
        /// </summary>
        string DatUrl { get; set; }
        /// <summary>
        /// このオブジェクトの設定データを取得または設定します
        /// </summary>
        VIPBrowserLibrary.Setting.SettingSerial SettingData { get; set; }
        /// <summary>
        /// このオブジェクトに関連付けられているRes配列を取得または設定します
        /// </summary>
        Res[] Reses { get; set; }
        /// <summary>
        /// このオブジェクトにHtmlのデータを書き込みます
        /// </summary>
        /// <param name="data">書き込むHtmlのデータを書き込みます</param>
        void Write(string data);
        /// <summary>
        /// このオブジェクトに関連付けられているHtmlDOMオブジェクトを取得します
        /// </summary>
        HTMLDocument DomDocument { get; }
        /// <summary>
        /// このオブジェクトに関連付けられているHtmlBodyオブジェクトを取得します
        /// </summary>
        HTMLBody HtmlBody { get; }
        /// <summary>
        /// このオブジェクトに関連付けられているドキュメントのスクロール可能位置を取得します
        /// </summary>
        int DocumentScrollHeight { get; }
        /// <summary>
        /// Urlを設定してDatのHtmlを取得します
        /// </summary>
        /// <param name="url">取得先のUrl</param>
        /// <returns>取得したデータ</returns>
        Task<string> SetUrl(string url);
        /// <summary>
        /// このオブジェクトに関連付けられているスレ名を取得または設定します
        /// </summary>
        string ThreadName { get; set; }
        /// <summary>
        /// このオブジェクトに関連付けられているThreadDataを取得します
        /// </summary>
        ThreadData ThreadData { get; set; }
    }
}
