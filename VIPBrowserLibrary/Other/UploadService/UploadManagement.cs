using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Threading.Tasks;
using VIPBrowserLibrary.Other.MyExtensions;

namespace VIPBrowserLibrary.Other.UploadService
{
    /// <summary>
    /// アップロード関連の管理を行うクラスです
    /// </summary>
    public class UploadManagement
    {
        Setting.GeneralSetting gs = new Setting.GeneralSetting();
        /// <summary>
        /// アップロードログを読み込みます
        /// </summary>
        /// <returns>アップロードログを格納したList&lt;ListViewItem&gt;</returns>
        public List<ListViewItem> ReadUploadLog()
        {
            List<ListViewItem> lvList = new List<ListViewItem>();
            string path = gs.OtherFolderPath + "\\upload.txt";
            string data = Utility.TextUtility.Read(path);
            if (data.IsNullObject())
            {
                lvList.Add(new ListViewItem());
                return lvList;
            }
            foreach (var item in data.SplitString("\r\n"))
            {
                string[] spt = item.SplitString("<>");
                ListViewItem lvi = new ListViewItem(new string[] { spt[0], spt[1], spt[2], spt[3] });
                lvList.Add(lvi);
            }
            return lvList;
        }
        /// <summary>
        /// データをアップロードします
        /// </summary>
        /// <param name="loader">アップロード先のローダー</param>
        /// <param name="path">アップロードするファイルのパス</param>
        /// <returns>アップロードしたデータに関連付けられたDictionary&lt;string,string&gt;</returns>
        public Dictionary<string, string> UploadData(Uploaders loader, string path)
        {
            return this.UploadData(loader, File.ReadAllBytes(path));
        }
        /// <summary>
        /// データをアップロードします
        /// </summary>
        /// <param name="loader">アップロード先のローダー</param>
        /// <param name="bytes">アップロードするbyte配列</param>
        /// <returns>アップロードしたデータに関連付けられたDictionary&lt;string,string&gt;</returns>
        public Dictionary<string, string> UploadData(Uploaders loader, byte[] bytes)
        {
            switch (loader)
            {
                case Uploaders.Imgur:
                    ImgurService ism = new ImgurService(bytes);
                    return ism.UploadImage();
                case Uploaders.Axfc:
                    throw new NotImplementedException();
                default:
                    break;
            }
            throw new NotSupportedException();
        }
        /// <summary>
        /// データをアップロードします
        /// </summary>
        /// <param name="loader">アップロード先のローダー</param>
        /// <param name="s">アップロードするStream</param>
        /// <returns>アップロードしたデータに関連付けられたDictionary&lt;string,string&gt;</returns>
        public Dictionary<string, string> UploadData(Uploaders loader, Stream s)
        {
            MemoryStream ms = new MemoryStream();
            s.CopyTo(ms);
            var data = this.UploadData(loader, ms.ToArray());
            s.Dispose();
            ms.Dispose();
            return data;
        }
        /// <summary>
        /// アップロードのログを書き込みます
        /// </summary>
        /// <param name="loader">使用したローダー</param>
        /// <param name="dic">書き込むデータ</param>
        public void WriteUploadLog(Uploaders loader, Dictionary<string, string> dic)
        {
            switch (loader)
            {
                case Uploaders.Imgur:
                    dic.Add("Uploader", "Imgur");
                    dic.Add("Downpass", "N/A");
                    dic.Add("Time", DateTime.Now.ToString());
                    break;
                case Uploaders.Axfc:
                    dic.Add("Uploader", "Axfc");
                    dic.Add("Time", DateTime.Now.ToString());
                    break;
                default:
                    throw new ArgumentException();
            }
            string writeData = String.Format("{0}<>{1}<>{2}<>{3}<>{4}\r\n", dic["Link"], dic["Uploader"], dic["Downpass"], dic["Delete"], dic["Time"]);
            File.AppendAllText(gs.OtherFolderPath + "\\upload.txt", writeData);
        }

    }
    /// <summary>
    /// アップロード先を表した列挙体
    /// </summary>
    public enum Uploaders
    {
        /// <summary>
        /// Imgurアップローダー
        /// </summary>
        Imgur,
        /// <summary>
        /// Axfcアップローダー
        /// </summary>
        Axfc
    }

}
//Link<>uploader<>downpass<>delpath/deladdress<>time
