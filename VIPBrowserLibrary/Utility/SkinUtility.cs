using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using VIPBrowserLibrary.Chron.ThreadOrResData;

namespace VIPBrowserLibrary.Utility
{
    /// <summary>
    /// スキンの管理を行います
    /// </summary>
    public static class SkinUtility
    {
        private static Setting.GeneralSetting gs = new Setting.GeneralSetting();
        /// <summary>
        /// スキンデータを読み込みます
        /// </summary>
        /// <returns>読み込んだSkinData</returns>
        public static SkinData[] ReadSkinData()
        {
            string[] dirs = Directory.GetDirectories(gs.SkinFolderPath);
            // List<SkinData>使えってか、ははっ
            SkinData[] sds = new SkinData[dirs.Length];
            int i = 0;
            foreach (var item in dirs)
            {
                try
                {
                    XmlReader xr = XmlReader.Create(File.Open(item + "\\setting.xml", FileMode.Open, FileAccess.Read));
                    XmlDocument xd = new XmlDocument();
                    xr.Read();
                    xd.Load(xr);
                    SkinData sd = new SkinData();
                    sd.SkinPath = item;
                    var xs = xd.LastChild;
                    xr.Read();
                    sd.SkinName = xs["SkinName"].InnerText;
                    sd.ImagePath = xs["ImagePath"].InnerText;
                    sd.IsHaveImagePicture = Boolean.Parse(xs["IsHaveImagePicture"].InnerText);
                    sds[i] = sd;
                    
                    xd = null;
                    xr.Dispose();
                }
                catch 
                {
                    SkinData sd = new SkinData();
                    sd.SkinPath = item;
                    sd.ImagePath = String.Empty;
                    sd.IsHaveImagePicture = false;
                    sds[i] = sd;
                }
                i++;
            }
            return sds;
        }
    }
}
