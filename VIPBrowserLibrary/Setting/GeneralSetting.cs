using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace VIPBrowserLibrary.Setting
{
    /// <summary>
    /// 環境変数のクラスです
    /// </summary>
    public class GeneralSetting
    {
        /// <summary>
        /// 板のSETTING.TXTを格納するフォルダを指定します
        /// </summary>
        public string BoardInfoFilePath { get { return currentDirectory + "\\board"; } }
        /// <summary>
        /// プログラムのカレントディレクトリを表します
        /// </summary>
        public string CurrentDirectory { get { return currentDirectory; } }
        /// <summary>
        /// datファイルを保存するフォルダーを指定します
        /// </summary>
        public string DatFilePath { get { return currentDirectory + "\\dat"; } }
        /// <summary>
        /// プログラムのプラグインを格納するファイルを指定します
        /// </summary>
        public string PluginFilePath { get { return currentDirectory + "\\plugin"; } }
        /// <summary>
        /// レスを表示するスキンの場所を表します
        /// </summary>
        public string SkinFolderPath { get { return currentDirectory + "\\skin"; } }
        /// <summary>
        /// その他のファイルの設定場所を表します
        /// </summary>
        public string OtherFolderPath { get { return currentDirectory + "\\other"; } }
        /// <summary>
        /// 起動時になくても起動できる設定データのパスを表します
        /// </summary>
        public string NotNecessarySettingDataPath { get { return this.OtherFolderPath + "\\setting"; } }
        /// <summary>
        /// AAの保存場所をあらわします
        /// </summary>
        public string AAFolderPath { get { return currentDirectory + "\\aa"; } }
        /// <summary>
        /// バッキングフィールド
        /// </summary>
        private string currentDirectory = System.IO.Directory.GetCurrentDirectory();

    }
}
