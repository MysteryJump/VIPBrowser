using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VIPBrowser.Dialogs
{
    public partial class PluginSetting : Form
    {
        public PluginSetting()
        {
            InitializeComponent();
        }

        private void PluginSetting_Load(object sender, EventArgs e)
        {
            foreach (VIPBrowserPlugin.IPlugin i in ((Form1)this.Owner).Plugin)
            {
                ListViewItem lvi = new ListViewItem(new string[] { i.Name, i.Version, i.Description });
                lvi.Tag = i;
                this.listView1.Items.Add(lvi);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count < 0)
                return;
            VIPBrowserPlugin.IPlugin ip = this.listView1.SelectedItems[0].Tag as VIPBrowserPlugin.IPlugin;
            if (!ip.HasSetupDialog)
                return;
            ip.ShowSetupDialog();
        }
    }
}
