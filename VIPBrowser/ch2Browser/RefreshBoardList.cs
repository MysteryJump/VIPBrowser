using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VIPBrowser.ch2Browser
{
    public partial class RefreshBoardList : Form
    {
        public VIPBrowserLibrary.Setting.SettingSerial SettingData { get; set; }

        ch2BrowserControl cc;
        public RefreshBoardList(ch2BrowserControl c)
        {
            cc = c;
            InitializeComponent();
            textBox1.Enabled = false;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            if (!cb.Checked)
                textBox1.Enabled = true;
            else
                textBox1.Enabled = false;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            this.ControlBox = false;
            this.Text = "更新が完了するまで今しばらくお待ちください...";
            this.button1.Enabled = false;
            string url = textBox1.Text;
            if (url == "http://")
                url = SettingData.DefaultBBSMenuAddress;
            cc.treeView1.Nodes.Clear();
            VIPBrowserLibrary.BBS.Common.GetBoardList gbl = new VIPBrowserLibrary.BBS.Common.GetBoardList();


            cc.treeView1.Nodes.Add(await gbl.GetBoardLists(url));
            TreeNode yn = await gbl.GetUserBoardList();
            if (yn != null)
                cc.treeView1.Nodes.Add(yn);
            this.Close();
        }
    }
}
