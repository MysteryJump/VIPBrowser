using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace VIPBrowserLibrary.Other.AAManagement
{
    /// <summary>
    /// AAリストの書き込みを行います
    /// </summary>
    public class WriteAAList
    {
        Setting.GeneralSetting gs = new Setting.GeneralSetting();
        /// <summary>
        /// インデックスを書き込みます
        /// </summary>
        /// <param name="tn">書き込むインデックス</param>
        public void WriteIndex(TreeNode tn) 
        {
            string idxPath = gs.AAFolderPath + "\\aa.idx";
            BinaryFormatter bf = new BinaryFormatter();
            Stream s = File.Open(idxPath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            bf.Serialize(s,tn);
            s.Dispose();            
        }
        /// <summary>
        /// AAファイルを書き込みます
        /// </summary>
        /// <param name="path">書き込み先のパス</param>
        /// <param name="ad">書き込むAAData</param>
        public void AddAAFile(string path,AAData ad) 
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[TAG]");
            sb.AppendLine(String.Join(",",ad.Tag));
            sb.Append("[FAVORITE]");
            sb.AppendLine(ad.Favorite.ToString());
            sb.Append("[VIEWCOUNT]");
            sb.AppendLine(ad.ViewCount.ToString());
            sb.AppendLine("[CONTENT]");
            sb.Append(ad.Content);
            if (File.Exists(path))
                File.Delete(path);
            File.AppendAllText(path, sb.ToString());
        }
        /// <summary>
        /// AAファイルを書き込みます
        /// </summary>
        /// <param name="path">書き込み先のパス</param>
        /// <param name="ad">書き込むAAData</param>
        /// <param name="basePath">元のパス</param>
        public void AddAAFile(string path, AAData ad, string basePath) 
        {
            if (File.Exists(path))
                File.Delete(path);
            this.AddAAFile(path, ad);
        }
    }
}
