using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Windows.Forms;

namespace VIPBrowserLibrary.Setting
{
    /// <summary>
    /// 設定ファイルの読み込みおよび書き込みを行うクラスです
    /// </summary>
    public class Serializer
    {
        private GeneralSetting gs = new GeneralSetting();
        /// <summary>
        /// 設定ファイルを読み込みます
        /// </summary>
        /// <returns>読み込んだ設定データ</returns>
        public SettingSerial Deserilize()
        {
            Log.Logger.WriteLog("Start Read SettingData");

            SettingSerial sss = new SettingSerial();
            
            XmlSerializer xs = new XmlSerializer(typeof(SettingSerial));

            using (System.IO.FileStream fs = new System.IO.FileStream(gs.CurrentDirectory+"\\Setting.xml",System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite))
            {
                try
                {
                    sss = (SettingSerial)xs.Deserialize(fs);
                }
                catch 
                {
                    Log.Logger.WriteLog("Failed Read SettingData");
                    return new SettingSerial();
                }
            }
            Log.Logger.WriteLog("Success Read Setting Data");
            return sss;
        }
        /// <summary>
        /// 設定データを書き込みます
        /// </summary>
        /// <param name="ss">書き込む設定データ</param>
        public void Serialize(SettingSerial ss)
        {
            XmlSerializer xs = new XmlSerializer(typeof(SettingSerial));
            using (System.IO.FileStream fs = new System.IO.FileStream(gs.CurrentDirectory + "\\Setting.xml", System.IO.FileMode.Create, System.IO.FileAccess.Write))
            {
                Log.Logger.WriteLog("Write Setting Data");
                xs.Serialize(fs, ss);
            }
        }
    }
    /// <summary>
    /// 設定のデータを表します
    /// </summary>
	[Serializable]
	public class SettingSerial
	{

		/// <summary>
		/// スレッド一覧を見るのにシンプルスレッドビュワーを使用するか表します
		/// </summary>
		public bool IsSimpleThreadViewer = true;
		/// <summary>
		/// スレッドを見るタブをマルチラインで表示するか
		/// </summary>
		public bool IsThreadViewMultiLine = false;
		/// <summary>
		/// スレッドリストを見るタブをマルチラインで表示するかどうか
		/// </summary>
		public bool IsThreadListViewMultiLine = false;
		/// <summary>
		/// デフォルトのBBSMENUのURLを指定します
		/// </summary>
		public string DefaultBBSMenuAddress = "http://menu.2ch.net/bbsmenu.html";
		/// <summary>
		/// VisualStyleを用いて描画するか指定します
		/// </summary>
		public bool IsUseVisualStyle = true;
		/// <summary>
		/// アプリケーションの終了時に警告を出すか設定します
		/// </summary>
		public bool IsFormClosingWarning = false;
		/// <summary>
		/// フォームの初期のHeightを設定します
		/// </summary>
		public int FormHeight = 450;
		/// <summary>
		/// フォームの初期のWidthを設定します
		/// </summary>
		public int FormWidth = 830;
		/// <summary>
		/// フォームの初期のX座標を設定します
		/// </summary>
		public int FormX = 15;
		/// <summary>
		/// フォームの初期のY座標を設定します
		/// </summary>
		public int FormY = 15;
		/// <summary>
		/// フォームの初期位置を保存するか設定します
		/// </summary>
		public bool IsSaveFormLocation = true;
		/// <summary>
		/// 使用する検索APIを設定します
		/// </summary>
		public DefaultSearchEngine DefalutSearcher = DefaultSearchEngine.Google;
		/// <summary>
		/// 書き込み履歴を保存するか設定します
		/// </summary>
		public bool IsSaveWriteRecord = false;
		/// <summary>
		/// 定期的にガベージコレクトを行うか設定します
		/// </summary>
		public bool IsPeriodicallyGC = true;
		/// <summary>
		/// Beにログインしているか表します
		/// </summary>
		public bool IsBeLogin = false;
		/// <summary>
		/// Rokkaにログインしているかあらわします
		/// </summary>
		public bool IsRokkaLogin = false;
		/// <summary>
		/// P2にログインしているか表します
		/// </summary>
		public bool IsP2Login = false;
		/// <summary>
		/// スキンを使用中か表します
		/// </summary>
		public bool IsUsingSkin = false;
		/// <summary>
		/// 使用中のスキンのパスを表します
		/// </summary>
		public string UsingSkinPath = String.Empty;
		/// <summary>
		/// フォームが最大化されているか表します
		/// </summary>
		public bool IsMaximized = false;
		/// <summary>
		/// フォームのポジションを表します
		/// </summary>
		public FormStartPosition StartPosition = FormStartPosition.Manual;
		/// <summary>
		/// スレッドリストを終了時に保存するか設定します
		/// </summary>
		public bool IsSaveThreadListView = false;
		/// <summary>
		/// スレッドビューを終了時に保存するか設定します
		/// </summary>
		public bool IsSaveThreadView = false;
		/// <summary>
		/// MainForm(MainControl)のアドレスバーに起動時に表示されているものを表します
		/// </summary>
		public string DefaultAddressBarText = "";
		/// <summary>
		/// 浪人にログインしているか表します
		/// </summary>
		public bool IsRoninLogined = false;
		/// <summary>
		/// スタートページを表示するか表します
		/// </summary>
		public bool IsShowStartPage = true;
		/// <summary>
		/// マルチスレッド処理を行うか表します
		/// </summary>
		public bool IsMultiThreading = true;
	}
    /// <summary>
    /// デフォルトの検索エンジンを表します
    /// </summary>
    [Serializable]
    [Flags]
    public enum DefaultSearchEngine 
    {
        /// <summary>
        /// Google検索
        /// </summary>
        Google = 1,
        /// <summary>
        /// Yahoo検索
        /// </summary>
        Yahoo = 2,
        /// <summary>
        /// Bing検索
        /// </summary>
        Bing = 4,
        /// <summary>
        /// Amazon検索
        /// </summary>
        Amazon = 8
    }
}
