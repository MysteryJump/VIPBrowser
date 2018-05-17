/* 追加プラグイン機能ねえ
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIPBrowserPlugin
{
    /// <summary>
    /// プラグインで実装するインターフェイス
    /// </summary>
    public interface IPlugin
    {
        /// <summary>
        /// プラグイン名
        /// </summary>
        string Name { get; }
        /// <summary>
        /// プラグインのバージョン
        /// </summary>
        string Version { get; }
        /// <summary>
        /// プラグインの説明
        /// </summary>
        string Description { get; }
        /// <summary>
        /// セットアップダイアログを所持しているか
        /// </summary>
        bool HasSetupDialog { get; }
        /// <summary>
        /// プラグインを実行する
        /// </summary>
        void Run();
        /// <summary>
        /// プラグインのインスタンス生成後に呼び出されるメソッド
        /// </summary>
        /// <param name="host">ホストのインターフェイス</param>
        void Initialize(IPluginHost host);
        /// <summary>
        /// セットアップダイアログを表示
        /// </summary>
        void ShowSetupDialog();
    }
    /// <summary>
    /// 専ブラユーザーコントロール内で実装するインターフェイス
    /// </summary>
    public interface IPlugin2
    {
        /// <summary>
        /// プラグインのインスタンス生成後に呼び出されるメソッド
        /// </summary>
        /// <param name="host">ホストのインターフェイス</param>
        void Initialize(IPluginHostCh2Browser host);
        /// <summary>
        /// プラグイン名
        /// </summary>
        string Name { get; }
        /// <summary>
        /// プラグインのバージョン
        /// </summary>
        string Version { get; }
        /// <summary>
        /// プラグインの説明
        /// </summary>
        string Description { get; }
    }

    /// <summary>
    /// プラグインのホストで実装するインターフェイス
    /// </summary>
    public interface IPluginHost
    {
        /// <summary>
        /// ホストのTabコントロール
        /// </summary>
        System.Windows.Forms.TabControl MainTabControl { get; }
    }
    public interface IPluginHostCh2Browser
    {
        void UrlEnter_Click(object sender, EventArgs e);

        string AddressTextBoxText { get; set; }

        string LogText { set; }

        void AddThreadViewToolStripItem(System.Windows.Forms.ToolStripItem tsi);

        void AddThreadListViewToolStripItem(System.Windows.Forms.ToolStripItem tsi);

    }
}
