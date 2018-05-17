using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace VIPBrowserLibrary.BBS.Common
{
    /// <summary>
    /// ローカルに保存してある過去ログを読み込みます
    /// </summary>
    public class ReadDotNetFile
    {
        private Setting.GeneralSetting gs = new Setting.GeneralSetting();
        /// <summary>
        /// 過去ログのdatのパス一覧を取得します
        /// </summary>
        /// <returns>取得したパス</returns>
        public string[] ReadFileNode()
        {
            List<string> fileList = new List<string>();
            string[] nodes = Directory.GetDirectories(gs.DatFilePath);
            foreach (var dir in nodes)
            {
                string[] files = Directory.GetFiles(dir);
                foreach (var file in files)
                {
                    if (file.Contains(".dat"))
                        fileList.Add(file);
                }
            }
            return fileList.ToArray();
        }
        /// <summary>
        /// datを関連付けたListViewItem[]形式のデータを返します
        /// </summary>
        /// <returns>過去ログファイル一覧を収めたListViewItem配列</returns>
        public async Task<ListViewItem[]> GetDotNetFiles()
        {
            return await AwaitSet.Awaitable<ListViewItem[]>.Run(() =>
            {
                Utility.Clearner.DeleteHyphenFiles();
                int i = 1;
                //int count = 0;
                string[] files = this.ReadFileNode();
                List<ListViewItem> lvs = new List<ListViewItem>();
                foreach (var file in files)
                {
                    string dataFile = file.Replace(".dat", ".xml");
                    Chron.ThreadOrResData.ThreadData td = Chron.ThreadOrResData.ThreadDataWriterAndReader.Read(dataFile);
                    if (td == null)
                        continue;
                    DateTime dt = Chron.Calture.UnixTimeToDateTime(td.ThreadKey);
                    ulong speed = Chron.Calture.ThreadAuthority(dt, td.GetRescount.ToString());
                    ListViewItem lvi = new ListViewItem(new string[]
                {
                    i.ToString(),
                    td.ThreadName,
                    dt.ToString(),

                });
                    lvi.Name = td.ThisFilePath + ".dat";
                    lvs.Add(lvi);
                    i++;
                    //count++;
                }
                return lvs.ToArray();
            });
        }

		//public async Task<ListViewItem[]> UpdateDotNetFilesList()
		//{
		//	return await AwaitSet.Awaitable<ListViewItem[]>.Run(() =>
		//	{
		//		File.Delete(gs.NotNecessarySettingDataPath + "\\pastlogs.dat");
		//		Utility.Clearner.DeleteHyphenFiles();
		//		int i = 1;
		//		//int count = 0;
		//		string[] files = this.ReadFileNode();
		//		List<ListViewItem> lvs = new List<ListViewItem>();
		//		foreach (var file in files)
		//		{
		//			string dataFile = file.Replace(".dat", ".xml");
		//			Chron.ThreadOrResData.ThreadData td = Chron.ThreadOrResData.ThreadDataWriterAndReader.Read(dataFile);
		//			if (td == null)
		//				continue;
					
		//			DateTime dt = Chron.Calture.UnixTimeToDateTime(td.ThreadKey);
		//			ulong speed = Chron.Calture.ThreadAuthority(dt, td.GetRescount.ToString());
		//			ListViewItem lvi = new ListViewItem(new string[]
		//		{
		//			i.ToString(),
		//			td.ThreadName,
		//			dt.ToString(),

		//		});
		//			lvi.Name = td.ThisFilePath + ".dat";
		//			lvs.Add(lvi);
		//			File.AppendAllText(gs.NotNecessarySettingDataPath + "\\pastlogs.dat", td.ThreadAddress + "\r\n");
		//			i++;
		//			//count++;
		//		}
		//		return lvs.ToArray();
		//	});
		//}
    }
}
