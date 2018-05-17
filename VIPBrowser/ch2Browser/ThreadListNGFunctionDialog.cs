using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VIPBrowserLibrary.Chron.ThreadOrResData.Abone;

namespace VIPBrowser.ch2Browser
{
    public partial class ThreadListNGFunctionDialog : Form
    {
        public ThreadListNGFunctionDialog()
        {
            InitializeComponent();
        }

        private void ThreadListNGFunctionDialog_Load(object sender, EventArgs e)
        {
            this.LoadData();
        }

        private void LoadData()
        {
            ThreadListAbone tla = new ThreadListAbone();
            tla.InstLoad();
            List<string> li = tla.NGCollection;
            foreach (string m in li)
            {
                this.listView1.Items.Add(m);
            }
            return;
        }

        private async void okButton_Click(object sender, EventArgs e)
        {
            await this.SaveData();
            this.Close();
        }

        private async Task SaveData()
        {
            ThreadListAbone tla = new ThreadListAbone();
            foreach (ListViewItem i in listView1.Items)
            {
                tla.NGCollection.Add(i.Text);
            }
            await tla.InstSave();
        }

        private void addNGMemberButton_Click(object sender, EventArgs e)
        {
            ThreadListNGCollectionEditDialog tlngced = new ThreadListNGCollectionEditDialog();
            tlngced.NGName = "";
            tlngced.ShowDialog(this);
            this.listView1.Items.Add(tlngced.NGName);
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            this.listView1.SelectedItems[0].Remove();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count <= 0)
                return;
            ThreadListNGCollectionEditDialog tlngced = new ThreadListNGCollectionEditDialog();
            tlngced.NGName = this.listView1.SelectedItems[0].Text;
            tlngced.ShowDialog(this);
            this.listView1.SelectedItems[0].Text = tlngced.NGName;
        }

    }
}
