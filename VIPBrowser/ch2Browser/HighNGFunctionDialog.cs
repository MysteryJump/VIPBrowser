using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using VIPBrowserLibrary.Chron.ThreadOrResData.Abone;

namespace VIPBrowser.ch2Browser
{
    public partial class HighNGFunctionDialog : Form
    {
        public HighNGFunctionDialog()
        {
            InitializeComponent();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.SaveData();
        }


        private void HighNGFunctionDialog_Load(object sender, EventArgs e)
        {
            this.LoadData();
        }

        private void LoadData()
        {
            AboneManagement am = new AboneManagement();
            am.InstLoad();
            foreach (NGWord n in am.NGCollection)
            {
                string word = String.Empty;
                if (n.IsRegex)
                    word = n.RegexWord.ToString();
                else
                    word = n.Word;
                ListViewItem lvi = new ListViewItem(new string[] 
                {
                    n.AboneTypes.ToString(),
                    word,
                    n.IsRegex.ToString(),
                    n.Url,
                    n.SetTime.ToString(),
                    n.ReleaseTime.ToString()
                });
                lvi.Tag = n;
                listView1.Items.Add(lvi);
            }
        }
        private async void SaveData()
        {
            AboneManagement am = new AboneManagement();
            List<NGWord> ngs = new List<NGWord>();
            foreach (ListViewItem i in listView1.Items)
            {
                NGWord nw;
                bool isReg = false;
                AboneType type = (AboneType)Enum.Parse(typeof(AboneType), i.SubItems[0].Text);
                if (i.SubItems[2].Text == "True")
                    isReg = true;
                if (isReg)
                {
                    nw = new NGWord(new Regex(i.SubItems[1].Text, RegexOptions.Compiled),
                        type, i.SubItems[3].Text,
                        DateTime.Parse(i.SubItems[4].Text),
                        TimeSpan.Parse(i.SubItems[5].Text));
                }
                else
                {
                    nw = new NGWord(i.SubItems[1].Text,
                        type, i.SubItems[3].Text,
                        DateTime.Parse(i.SubItems[4].Text),
                        TimeSpan.Parse(i.SubItems[5].Text));
                }
                ngs.Add(nw);
            }
            am.NGCollection = ngs;
            await am.InstSave();
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count <= 0)
                return;
            int count = this.listView1.SelectedItems[0].Index;
            ListViewItem lvi = this.listView1.SelectedItems[0];
            HighNGCollectionEditDialog hnced = new HighNGCollectionEditDialog();
            hnced.IsRegex = Boolean.Parse(lvi.SubItems[2].Text);
            hnced.Abone = (AboneType)Enum.Parse(typeof(AboneType), lvi.SubItems[0].Text);
            hnced.Word = lvi.SubItems[1].Text;
            hnced.Url = lvi.SubItems[3].Text;
            hnced.Settime = DateTime.Parse(lvi.SubItems[4].Text);
            hnced.ReleaseTime = TimeSpan.Parse(lvi.SubItems[5].Text);
            DialogResult dr = hnced.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                NGWord n = hnced.NGData;
                if (n.IsRegex)
                {
                    lvi = new ListViewItem(new string[] 
                    {
                        n.AboneTypes.ToString(),
                        n.RegexWord.ToString(),
                        n.IsRegex.ToString(),
                        n.Url,
                        n.SetTime.ToString(),
                        n.ReleaseTime.ToString()
                    });
                }
                else
                {
                    lvi = new ListViewItem(new string[]
                    {
                        n.AboneTypes.ToString(),
                        n.Word,
                        n.IsRegex.ToString(),
                        n.Url,
                        n.SetTime.ToString(),
                        n.ReleaseTime.ToString()
                    });
                }
                this.listView1.Items[count] = lvi;
            }
        }

        private void addMemberButton_Click(object sender, EventArgs e)
        {
            HighNGCollectionEditDialog hnced = new HighNGCollectionEditDialog();
            hnced.IsRegex = false;
            hnced.Abone = AboneType.Name;
            hnced.Word = String.Empty;
            hnced.Url = String.Empty;
            hnced.Settime = DateTime.MinValue;
            hnced.ReleaseTime = TimeSpan.MinValue;
            DialogResult dr = hnced.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                NGWord n = hnced.NGData;
                ListViewItem lvi = new ListViewItem(new string[] 
                {
                    n.AboneTypes.ToString(),
                    n.Word,
                    n.IsRegex.ToString(),
                    n.Url,
                    n.SetTime.ToString(),
                    n.ReleaseTime.ToString()
                });
                this.listView1.Items.Add(lvi);
            }
        }

        private void deleteMemberButton_Click(object sender, EventArgs e)
        {
            int count = this.listView1.SelectedItems[0].Index;
            this.listView1.Items[count].Remove();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
