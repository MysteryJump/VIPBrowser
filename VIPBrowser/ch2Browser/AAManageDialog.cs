using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VIPBrowserLibrary.Other.AAManagement;

namespace VIPBrowser.ch2Browser
{
    public partial class AAManageDialog : Form
    {
        VIPBrowserLibrary.Setting.GeneralSetting gs = new VIPBrowserLibrary.Setting.GeneralSetting();
        private ReadAAList ReadAA;
        private WriteAAList WriteAA;
        public AAManageDialog()
        {
            InitializeComponent();
            this.ReadAA = new ReadAAList();
            this.WriteAA = new WriteAAList();
        }

        private void AAManageDialog_Load(object sender, EventArgs e)
        {
            var tn = this.ReadAA.ReadIndex();
            if (tn != null)
                this.aaTreeView.Nodes.Add(tn);
            else
                this.aaTreeView.Nodes.Add("", "AA");
        }

        private void addAAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.aaTreeView.SelectedNode == null)
                return;
            TreeNode t = this.aaTreeView.SelectedNode;
            if (t.Level == 0) 
            {
                // AA管理のソフトとの互換性も考えながら対応予定
                MessageBox.Show("第一階層へのファイルの追加はできません");
                return;
            }
            else if (t.Level > 3)
            {
                MessageBox.Show("第二層以上への登録はできません");
            }
            TreeNode tn = new TreeNode();
            tn.Name = gs.AAFolderPath + "\\" + t.Text + "\\" + this.AANameTextBox.Text +".txt";
            tn.Text = AANameTextBox.Text;
            AAData ad = new AAData();
            ad.Content = this.textBox1.Text;
            ad.Favorite = false;
            ad.Tag = new string[0];
            ad.ViewCount = 0;
            tn.Tag = ad;
            t.Nodes.Add(tn);
            this.WriteAA.AddAAFile(tn.Name, ad);
        }

        private void addRootFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string name = null;
            TreeNode tn = new TreeNode();
            Form f = new Form();
            TextBox tb = new TextBox();
            f.Controls.Add(tb);
            f.FormClosed += (_ender ,_e) =>
            {
                name = tb.Text;
            };
            f.ShowDialog(this);
            tn.Text = name;
            tn.Name = gs.AAFolderPath + "\\" + name;
            System.IO.Directory.CreateDirectory(gs.AAFolderPath + "\\" + name);
            this.aaTreeView.Nodes[0].Nodes.Add(tn);
        }

        private void refreshTreeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.aaTreeView.Nodes.Clear();
            var tree = this.ReadAA.ReadAllData();
            if (tree != null)
            {
                this.aaTreeView.Nodes.Add("", "AA");
                this.aaTreeView.Nodes[0].Nodes.AddRange(tree);
            }
        }

        private void AAManageDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.WriteAA.WriteIndex(this.aaTreeView.Nodes[0]);
            this.SelectedText = this.textBox1.Text;
        }

        private void aaTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string path = this.aaTreeView.SelectedNode.Name;
            if (String.IsNullOrEmpty(path) || path.LastIndexOf(".txt") == -1)
                return;
            var aaData = this.ReadAA.ReadAAData(path);
            //this.textBox1.Tag = this.AANameTextBox.Tag = e.Node;
            this.AANameTextBox.Text = new System.IO.DirectoryInfo(path).Name.Replace(".txt","");
            this.textBox1.Text = aaData.Content;
            this.textBox1.Tag = this.AANameTextBox.Tag = new object[] { e.Node, path,this.AANameTextBox.Text };
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            object[] data = (object[])this.AANameTextBox.Tag;
            var node = data[0] as TreeNode;
            var aa = node.Tag as AAData;
            aa.Content = this.textBox1.Text;
            if (this.AANameTextBox.Text == data[2] as string)
                this.WriteAA.AddAAFile((string)data[1], aa);
            else
            {
                string path = node.Name;
                path = path.Replace((data[2] as string) + ".txt", "");
                this.WriteAA.AddAAFile(path  + this.AANameTextBox.Text + ".txt", aa,node.Name);
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var node = this.aaTreeView.SelectedNode;
            if (node == null)
                return;
            var aa = node.Tag as AAData;
            var path = node.Name;
            int len = path.Length - 4;
            if (path.LastIndexOf(".txt") == len)
                System.IO.File.Delete(node.Name);
            else
            {
                if (System.Windows.Forms.DialogResult.Yes == MessageBox.Show("このフォルダのファイルすべて削除されますが続行しますか？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                    System.IO.Directory.Delete(node.Name, true);
                else
                    return;
            }
            //int idx = node.Index;
            this.aaTreeView.Nodes.Remove(node);
            this.WriteAA.WriteIndex(this.aaTreeView.Nodes[0]);
        }

        private void aaTreeView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
               this.aaTreeView.SelectedNode = this.aaTreeView.GetNodeAt(e.X, e.Y);
        }
        public string SelectedText { get; private set; }
    }
}
