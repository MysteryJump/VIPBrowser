using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.Management;
using VIPBrowserLibrary.Other.MyExtensions;

namespace VIPBrowserLibrary.Other.AAManagement
{
    /// <summary>
    /// AAリストを読み込むクラスいです
    /// </summary>
    public class ReadAAList
    {
        Setting.GeneralSetting gs = new Setting.GeneralSetting();
        /// <summary>
        /// AAリストのインデックスを読み込みます
        /// </summary>
        /// <returns>読み込んだTreeNode</returns>
        public TreeNode ReadIndex()
        {
            string idxPath = gs.AAFolderPath + "\\aa.idx";
            BinaryFormatter bf = new BinaryFormatter();
            Stream s = File.Open(idxPath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            if (s.Length == 0)
                return null;
            var node = bf.Deserialize(s) as TreeNode;
            if (node == null)
                return null;
            s.Dispose();
            return node;
            
        }
        /// <summary>
        /// AAリストを基準となるパスから読み込みます
        /// </summary>
        /// <returns>読み込んだTeeNode配列</returns>
        public TreeNode[] ReadAllData()
        {
            TreeNode[] tn;
            string path = gs.AAFolderPath;
            string[] dirs = Directory.GetDirectories(path);
            tn = new TreeNode[dirs.Length];
            int i = 0;
            foreach (var d in dirs)
            {
                TreeNode tt = new TreeNode();
                tt.Name = d;
                tt.Text = new DirectoryInfo(d).Name;
                string[] paths = Directory.GetFiles(d, "*.txt", SearchOption.TopDirectoryOnly);
                foreach (var item in paths)
                {
                    TreeNode t = new TreeNode();
                    t.Name = item;
                    t.Tag = this.ReadAAData(item);
                    t.Text = new FileInfo(item).Name;
                    tt.Nodes.Add(t);
                }
                tn[i] = tt;
                i++;
            }
            return tn;
        }
        /// <summary>
        /// AAを読み込みます
        /// </summary>
        /// <param name="path">読み込みのパス</param>
        /// <returns>読み込んだデータ</returns>
        public AAData ReadAAData(string path) 
        {
            bool isEnd = false;
            AAData ad = new AAData();
            string[] lines = File.ReadAllLines(path);
            List<string> aa = new List<string>();
            foreach (var item in lines)
            {
                if (!isEnd)
                {
                    if (item.IndexOf("[TAG]") == 0)
                    {
                        List<string> tagList = new List<string>();
                        string tags = item.Replace("[TAG]","");
                        foreach (var tag in tags.Split(','))
                        {
                            tagList.Add(tag);
                        }
                        ad.Tag = tagList.ToArray();

                    }
                    else if (item.IndexOf("[FAVORITE]") == 0)
                    {
                        ad.Favorite = Boolean.Parse(item.Replace("[FAVORITE]", ""));
                    }
                    else if (item.IndexOf("[VIEWCOUNT]") == 0)
                    {
                        ad.ViewCount = (item.Replace("[VIEWCOUNT]", "")).Parse();
                    }
                    else if (item.IndexOf("[CONTENT]") == 0)
                    {
                        isEnd = true;
                    }
                }
                else
                {
                    aa.Add(item);
                }
            }
            ad.Content = String.Join("\r\n",aa.ToArray());
            return ad;
            
        }
    }
}
