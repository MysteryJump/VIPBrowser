using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VIPBrowserLibrary.Other.UploadService;

namespace VIPBrowser.ch2Browser
{
    public partial class UploadManagementDialog : Form
    {
        public UploadManagementDialog()
        {
            InitializeComponent();
        }

        private void listViewExtension1_DoubleClick(object sender, EventArgs e)
        {
            if (this.listViewExtension1.SelectedItems.Count < 0)
                return;
            
        }

        private void listViewExtension1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                //var item = this.listViewExtension1.GetItemAt(MousePosition.X,MousePosition.Y);
                //if (item == null)
                //    return;
                //item.Selected = true;
                this.contextMenuStrip1.Show(MousePosition);
            }
        }

        private void UploadManagementDialog_Load(object sender, EventArgs e)
        {
            UploadManagement um = new UploadManagement();
            this.listViewExtension1.Items.AddRange(um.ReadUploadLog().ToArray());
            um = null;
        }

        private void linkCopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.listViewExtension1.SelectedItems.Count < 0)
                return;
            string link = this.listViewExtension1.SelectedItems[0].SubItems[0].Text;
            Clipboard.SetText(link);
            MessageBox.Show("リンクをクリップボードにコピーしました");
            link = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UploadDialog ud = new UploadDialog();
            ud.ShowDialog();
            ud.Dispose();
        }

        private void deleteLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UploadManagement um = new UploadManagement();
        }
    }
}
