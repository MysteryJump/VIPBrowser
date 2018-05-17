using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Specialized;
using VIPBrowserLibrary.Chron.ThreadOrResData.Abone;

namespace VIPBrowser.ch2Browser
{
    public partial class NGFunctionDialog : Form
    {
        public NGFunctionDialog()
        {
            InitializeComponent();
            this.listView1.FullRowSelect = true;
        }

        private async void SaveData()
        {
            SimpleAbone sa = new SimpleAbone();
            foreach (ListViewItem m in listView1.Items)
            {
                sa.NGCollection.Add(m.SubItems[0].Text, m.SubItems[1].Text);
            }
            await sa.InstSave();
        }

        private void LoadData()
        {
            SimpleAbone sa = new SimpleAbone();
            sa.InstLoad();
            NameValueCollection ngs = sa.NGCollection;
            foreach (string item in ngs)
            {
                string[] data = ngs.GetValues(item);
                foreach (string im in data)
                {
                    ListViewItem lvi = new ListViewItem(new string[] { item, im });
                    listView1.Items.Add(lvi);
                }
            }
        }

        private void NGFunctionDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            //this.LoadData();
        }

        private void NGFunctionDialog_Load(object sender, EventArgs e)
        {
            this.LoadData();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.SaveData();
            this.Close();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count <= 0)
                return;
            ListViewItem lvi = this.listView1.SelectedItems[0];
            NGCollectionEditDialog ngced = new NGCollectionEditDialog();
            ngced.ComboBoxSet = lvi.SubItems[0].Text;
            ngced.Conditions = lvi.SubItems[1].Text;
            ngced.Owner = this;
            ngced.ShowDialog(this);
            listView1.SelectedItems[0].SubItems[0].Text = ngced.Type.ToString();
            listView1.SelectedItems[0].SubItems[1].Text = ngced.Conditions;
            return;
        }

        private void addNGMemberButton_Click(object sender, EventArgs e)
        {
            ListViewItem lvi = new ListViewItem(new string[] { "", "" });
            int count = listView1.Items.Count;
            NGCollectionEditDialog ngced = new NGCollectionEditDialog();
            ngced.ComboBoxSet = lvi.SubItems[0].Text;
            ngced.Conditions = lvi.SubItems[1].Text;
            ngced.Owner = this;
            ngced.ShowDialog(this);
            lvi.SubItems[0].Text = ngced.Type.ToString();
            lvi.SubItems[1].Text = ngced.Conditions;
            listView1.Items.Add(lvi);
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            this.listView1.SelectedItems[0].Remove();
        }
    }
}
