using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml;

namespace SkinCreateAndParser
{
    public partial class NewSkinForm : Form
    {
        VIPBrowserLibrary.Setting.GeneralSetting gs = new VIPBrowserLibrary.Setting.GeneralSetting();
        private string ImagePath;
        public string SkinPath { get; set; }
        public new string ShowDialog() 
        {
            DialogResult dr = base.ShowDialog();
            //if (dr == System.Windows.Forms.DialogResult.OK)
                return this.SkinPath;
            
        }
        
        public NewSkinForm()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            var check = sender as CheckBox;
            switch (check.Checked)
            {
                case true:
                    {
                        this.button2.Enabled = true;
                    }
                    break;
                case false: 
                    {
                        this.button2.Enabled = false;
                    }
                    break;
                default:
                    throw new ArgumentNullException();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool isUseImage = false;
            if (this.checkBox1.Checked)
                isUseImage = true;
            string name = this.textBox2.Text;
            string text = this.textBox1.Text;
            string imageName = String.Empty;
            if (isUseImage)
                imageName = new DirectoryInfo(this.ImagePath).Name;
            string Current = gs.CurrentDirectory;
            string skin = gs.SkinFolderPath;
            Current = Current.Replace("SkinCreateAndParser", "VIPBrowser");
            if (!Directory.Exists(skin + "\\" + name))
                Directory.CreateDirectory(skin + "\\" + name);
            else
            {
                MessageBox.Show("このフォルダは既に存在しています");
                return;
            }
            this.SkinPath = skin + "\\" + name;
            using (File.Create(skin + "\\" + name + "\\setting.xml")) { }
            XmlWriter xw = XmlWriter.Create(skin + "\\" + name + "\\setting.xml");
            xw.WriteStartDocument();
            xw.WriteStartElement("setting");
            xw.WriteElementString("SkinName", text);
            xw.WriteElementString("ImagePath", Path.Combine(skin, imageName));
            xw.WriteElementString("IsHaveImagePicture", isUseImage.ToString());
            xw.WriteEndElement();
            xw.Flush();
            xw.Dispose();

            string ski = gs.SkinFolderPath + "\\" + name;
            using (File.Create(ski + "\\Res.html")) { }
            using (File.Create(ski + "\\Header.html")) { }
            using (File.Create(ski + "\\Footer.html")){}
            if (isUseImage)
                File.Copy(this.ImagePath, ski + "\\" + new DirectoryInfo(this.ImagePath).Name);
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.AddExtension = true;
            openFileDialog1.AutoUpgradeEnabled = true;
            openFileDialog1.Multiselect = false;
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.ImagePath = openFileDialog1.FileName;
            }
        }
    }
}
