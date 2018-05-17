using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VIPBrowserLibrary.Other.MyExtensions;

namespace VIPBrowser.Dialogs.Settings
{
    public partial class IDColoringSettingDialog : Form
    {
        public IDColoringSettingDialog()
        {
            InitializeComponent();
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            ListViewItem lvi = this.listView1.SelectedItems[0];
            IDColorSetDialog idcsd = new IDColorSetDialog();
            idcsd.IdColor = lvi.SubItems[3].BackColor;
            idcsd.MinValue = lvi.SubItems[1].Text.Parse();
            idcsd.MaxValue = lvi.SubItems[2].Text.Parse();
            idcsd.ShowDialog();
            lvi.SubItems[3].BackColor = idcsd.IdColor;
            lvi.SubItems[1].Text = idcsd.MinValue.ToString();
            lvi.SubItems[2].Text = idcsd.MaxValue.ToString();
        }

        private void IDColoringSettingDialog_Load(object sender, EventArgs e)
        {
            this.LoadData();
        }

        private void LoadData()
        {
            var colorData = VIPBrowserLibrary.Chron.ThreadOrResData.HtmlSkin.LoadColoringData();
            foreach (var item in colorData)
            {
                ListViewItem.ListViewSubItem lvs = new ListViewItem.ListViewSubItem();
                lvs.Text = ColorTranslator.ToHtml(item.IDColor);
                lvs.Name = "Color";
                ListViewItem.ListViewSubItem lvss = new ListViewItem.ListViewSubItem();
                lvss.Name = "MaxValue";
                lvss.Text = item.Max.ToString();
                ListViewItem.ListViewSubItem lvsss = new ListViewItem.ListViewSubItem();
                lvsss.Text = item.Min.ToString();
                lvsss.Name = item.Min.ToString();
                ListViewItem.ListViewSubItem l = new ListViewItem.ListViewSubItem();
                l.Name = "PrevColor";
                l.Text = "　　";
                l.BackColor = item.IDColor;
                ListViewItem lvi = new ListViewItem(new[] { lvs, lvsss, lvss, l }, 0);
                lvi.UseItemStyleForSubItems = false;
                this.listView1.Items.Add(lvi);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IDColorSetDialog idcsd = new IDColorSetDialog();
            idcsd.IdColor = Color.Black;
            idcsd.MaxValue = 1;
            idcsd.MinValue = 0;
            idcsd.ShowDialog();
            ListViewItem.ListViewSubItem lvs = new ListViewItem.ListViewSubItem();
            lvs.Text = ColorTranslator.ToHtml(idcsd.IdColor);
            lvs.Name = "Color";
            ListViewItem.ListViewSubItem lvss = new ListViewItem.ListViewSubItem();
            lvss.Name = "MaxValue";
            lvss.Text = idcsd.MaxValue.ToString();
            ListViewItem.ListViewSubItem lvsss = new ListViewItem.ListViewSubItem();
            lvsss.Text = idcsd.MinValue.ToString();
            lvsss.Name = idcsd.MinValue.ToString();
            ListViewItem.ListViewSubItem l = new ListViewItem.ListViewSubItem();
            l.Name = "PrevColor";
            l.Text = "　　";
            l.BackColor = idcsd.IdColor;
            ListViewItem lvi = new ListViewItem(new[] { lvs, lvsss, lvss, l }, 0);
            lvi.UseItemStyleForSubItems = false;
            this.listView1.Items.Add(lvi);
        }
 
    }
}
