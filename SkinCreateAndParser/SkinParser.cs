using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Threading.Tasks;
using System.Xml;
using System.Windows.Forms;

namespace SkinCreateAndParser
{
    public class SkinParser 
    {


        private string Current = Directory.GetCurrentDirectory();

        public List<ListViewItem> ReadData(string path) 
        {
            List<ListViewItem> lvis = new List<ListViewItem>();
            string[] paths = Directory.GetFiles(path);
            foreach (var item in paths)
            {
                ListViewItem lvi = new ListViewItem(item);
                lvis.Add(lvi);
            }
            return lvis;
        }
        
        private void CreateXml()
        {
            Current = Current.Replace("SkinCreateAndParser", "VIPBrowser");
            Directory.CreateDirectory(Current + "\\skin\\" + this.name);
            using (File.Create(Current + "\\skin\\" + this.name + "\\setting.xml")) { }
            XmlWriter xw = XmlWriter.Create(Current + "\\skin\\" + this.name + "\\setting.xml");
            xw.WriteStartDocument();
            xw.WriteStartElement("setting");
            xw.WriteElementString("SkinName", text);
            xw.WriteElementString("ImagePath", Path.Combine(this.basePath,this.imageName));
            xw.WriteElementString("IsHaveImagePicture", this.isUseImage.ToString());
            xw.WriteEndElement();
            xw.Flush();
            xw.Dispose();
        }
        public void ParseData(string name,string text,string basePath, bool isUseImage, string imageName) 
        {
            this.basePath = basePath;
            this.isUseImage = isUseImage;
            this.imageName = imageName;
            this.name = name;
            this.text = text;

            this.CreateXml();
            this.MoveSkinFile();
            return;
        }
        private void MoveSkinFile() 
        {
            string ski = this.Current + "\\skin\\" + this.name;
            File.Copy(this.basePath + "\\Res.html", ski + "\\Res.html",true);
            File.Copy(this.basePath + "\\Header.html", ski + "\\Header.html",true);
            File.Copy(this.basePath + "\\Footer.html", ski + "\\Footer.html",true);
            if (this.isUseImage)
                File.Copy(this.basePath + "\\" + this.imageName, ski + "\\" + this.imageName);
        }
        private string basePath;
        private string imageName;
        private bool isUseImage;
        private string name;
        private string text;
    }
}
