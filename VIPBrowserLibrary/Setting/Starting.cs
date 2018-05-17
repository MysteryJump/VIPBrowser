using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace VIPBrowserLibrary.Setting
{
    /// <summary>
    /// 起動時の動作を行います
    /// </summary>
    public static class Starting
    {
        /// <summary>
        /// 起動時に必要なファイルの確認と作成を行います
        /// </summary>
        public static void CheckNeedStartFoldersAndFiles()
        {
            VIPBrowserLibrary.Setting.GeneralSetting gs = new VIPBrowserLibrary.Setting.GeneralSetting();
            if (!Directory.Exists(gs.AAFolderPath))
                Directory.CreateDirectory(gs.AAFolderPath);
            if (!Directory.Exists(gs.BoardInfoFilePath))
                Directory.CreateDirectory(gs.BoardInfoFilePath);
            if (!File.Exists(gs.CurrentDirectory + "\\normalboard.bor"))
            {
                using (File.Create(gs.CurrentDirectory + "\\normalboard.bor")) { }
                //isFirstUp = true;
            }
            if (!Directory.Exists(gs.OtherFolderPath))
                Directory.CreateDirectory(gs.OtherFolderPath);
            if (!Directory.Exists(gs.SkinFolderPath))
                Directory.CreateDirectory(gs.SkinFolderPath);

            //using (File.Create(gs.OtherFolderPath + "\\log.txt")) { }

            if (!File.Exists(gs.OtherFolderPath + "\\ng.dat"))
                using (File.Create(gs.OtherFolderPath + "\\ng.dat")) { }

            if (!File.Exists(gs.OtherFolderPath + "\\listcolor.dat"))
                using (File.Create(gs.OtherFolderPath + "\\listcolor.dat")) { }

            if (!Directory.Exists(gs.DatFilePath))
                Directory.CreateDirectory(gs.DatFilePath);

            if (!Directory.Exists(gs.PluginFilePath))
                Directory.CreateDirectory(gs.PluginFilePath);

            if (!File.Exists(gs.OtherFolderPath + "\\coloring.dat"))
                using (var s = File.Create(gs.OtherFolderPath + "\\coloring.dat"))
                {
                    var bytes = System.Text.Encoding.UTF8.GetBytes(@"#000000\1\0
blue\5\2
red\10000\6");
                    s.Write(bytes, 0, bytes.Length);
                    s.Flush();
                }

            if (!Directory.Exists(gs.NotNecessarySettingDataPath))
                Directory.CreateDirectory(gs.NotNecessarySettingDataPath);

            if (!File.Exists(gs.NotNecessarySettingDataPath + "\\thli.dat"))
                using (File.Create(gs.NotNecessarySettingDataPath + "\\thli.dat")) { }

            if (!File.Exists(gs.NotNecessarySettingDataPath + "\\thre.dat"))
                using (File.Create(gs.NotNecessarySettingDataPath + "\\thre.dat")) { }

            if (!File.Exists(gs.OtherFolderPath + "\\favorite.dat"))
                using (File.Create(gs.OtherFolderPath + "\\favorite.dat")) { }

            if (!File.Exists(gs.NotNecessarySettingDataPath + "\\column.dat"))
                using (var s = File.Create(gs.NotNecessarySettingDataPath + "\\column.dat"))
                {
                    var bytes = System.Text.Encoding.UTF8.GetBytes("IsRead.10,Count.25,Name.350,ResCount.50,Time.100,Speed.100,Size.50,OldResCount.50,NewResCount.50");
                    s.Write(bytes, 0, bytes.Length);
                    s.Flush();
                }

			if (!File.Exists(BBS.Common.Samba24.Samba24SettingPath))
				BBS.Common.Samba24.CreateOrUpdateSamba24SettingFile();

			if (!File.Exists(BBS.Common.Samba24.LastWrittenBoardPath))
				using (File.Create(BBS.Common.Samba24.LastWrittenBoardPath)) { }
        }
    }
}
