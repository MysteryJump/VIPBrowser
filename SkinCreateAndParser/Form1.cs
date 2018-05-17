using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using f = System.IO.File;

namespace SkinCreateAndParser
{
    public partial class Form1 : Form
    {
        SkinParser sp = new SkinParser();
        public Form1()
        {
            InitializeComponent();
            this.tabControl2.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.folderBrowserDialog1.SelectedPath = System.IO.Directory.GetCurrentDirectory();
            if (DialogResult.OK == this.folderBrowserDialog1.ShowDialog(this)) 
            {
                string path = this.folderBrowserDialog1.SelectedPath;
                this.listView1.Items.AddRange(sp.ReadData(path).ToArray());
                this.button1.Enabled = true;
                this.textBox2.Text = path;
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count < 0)
            {
                return;
            }

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            var check = sender as CheckBox;
            if (check.Checked)
            {
                this.listView1.Enabled = true;
            }
            else
            {
                this.listView1.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string imageString = String.Empty;
            bool isUseImage = false;
            if (checkBox1.Checked)
                isUseImage = true;
            if (isUseImage)
            {
                if (this.listView1.SelectedItems.Count < 0) 
                {
                    MessageBox.Show("プレビュー画像にするイメージを選択してください");
                    return;
                }
                imageString = new System.IO.DirectoryInfo(this.listView1.SelectedItems[0].Text).Name;
            }
            sp.ParseData(new System.IO.DirectoryInfo(this.textBox2.Text).Name, this.textBox1.Text, this.textBox2.Text, isUseImage, imageString);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string res = this.resSkin.NowString;
            string header = this.headerSkin.NowString;
            string footer = this.footerSkin.NowString;
            string path = (string)this.tabControl2.Tag;
            f.WriteAllText(path + "\\Res.html", res);
            f.WriteAllText(path + "\\Header.html", header);
            f.WriteAllText(path + "\\Footer.html", footer);
        }

        private void CreateNewSkinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewSkinForm nsf = new NewSkinForm();
            string daa = nsf.ShowDialog();
            if (daa != null)
            {
                this.tabControl2.Enabled = true;
                this.tabControl2.Tag = daa;
            }
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.button3_Click(null, null);
        }

        private void closeskinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.tabControl2.Tag = null;
            for (int i = 0; i < 3; i++)
            {
                BaseCreateSkin bcs = this.tabControl2.TabPages[i].Controls[0] as BaseCreateSkin;
                bcs.textBox3.Text = String.Empty;
                bcs.webBrowser1.DocumentText = String.Empty;
            }
            this.tabControl2.Enabled = false;
        }
    }
}
