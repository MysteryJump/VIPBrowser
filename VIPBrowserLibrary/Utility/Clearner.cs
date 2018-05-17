using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace VIPBrowserLibrary.Utility
{
    /// <summary>
    /// 不必要なものを掃除や整理、修復します
    /// </summary>
    public class Clearner
    {
        private static Setting.GeneralSetting gs = new Setting.GeneralSetting();
        //private Setting.GeneralSetting g = new Setting.GeneralSetting();
        /// <summary>
        /// アプリケーションによって生成されバグの原因になりうるハイフンのみのファイル名のファイルやディレクトリを削除します
        /// </summary>
        public static void DeleteHyphenFiles()
        {
            if (File.Exists(gs.BoardInfoFilePath + "\\-.txt"))
                File.Delete(gs.BoardInfoFilePath + "\\-.txt");
            if (Directory.Exists(gs.DatFilePath + "\\-"))
                Directory.Delete(gs.DatFilePath + "\\-", true);
            return;
        }
    }
}
