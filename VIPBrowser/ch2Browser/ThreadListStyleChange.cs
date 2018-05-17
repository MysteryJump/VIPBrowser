using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VIPBrowserLibrary.Chron.ThreadOrResData;
using System.Collections.Specialized;
using VIPBrowserLibrary.Other.MyExtensions;
using VIPBrowserLibrary.Chron;


namespace VIPBrowser.ch2Browser
{
    public partial class ThreadListStyleChange : Form
    {

        VIPBrowserLibrary.Setting.GeneralSetting gs = new VIPBrowserLibrary.Setting.GeneralSetting();
        public ThreadListStyleChange()
        {
            InitializeComponent();
            this.LoadData();
        }

        private bool IsOK = false;
        private void LoadData()
        {
            ThreadListItemColorRing tlicr = new ThreadListItemColorRing();
            string data = tlicr.Read();
            NameValueCollection nvc = tlicr.ConvertValueCollectionFromText(data);
            int i = 1;
            foreach (string item in nvc)
            {
                string[] sdata = nvc.GetValues(item);
                foreach (string m in sdata)
                {
                    if (String.IsNullOrEmpty(m))
                        break;
                    string[] msp = ((i++).ToString() + "&" + item + "&" + m).Split('&');

                    ListViewItem lvi = new ListViewItem(msp);
                    listView1.Items.Add(lvi);
                }
            }

            ThreadColumn tc = new ThreadColumn();
            tc.ReadColumnData(ThreadColumn.ColumnDataPath);
            var columnData = tc.ConvertToSettingData();
            foreach (var item in columnData[0])
            {
                this.displayColumnListBox.Items.Add(ThreadColumn.ExchangeKeyOrDisplayValue(item.Key,true));
            }
            this.displayColumnListBox.Tag = columnData[0];
            foreach (var item in columnData[1])
            {
                this.notDisplayColumnListBox.Items.Add(ThreadColumn.ExchangeKeyOrDisplayValue(item.Key, true));
            }
            this.notDisplayColumnListBox.Tag = columnData[1];

        }

        private void ThreadListStyleChange_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!IsOK)
            {
                DialogResult dr = MessageBox.Show("変更を保存しないで終了しますか？", "警告", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (dr == DialogResult.Yes)
                    e.Cancel = false;
                else if (dr == DialogResult.No)
                {
                    this.SaveData();
                    e.Cancel = false;
                }
                else
                {
                    e.Cancel = true;
                    return;
                }
            }
        }


        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Color c;
            VIPBrowserLibrary.Chron.ThreadOrResData.ChangeColorTypeConditions nowType;
            string condition;

            if (listView1.SelectedItems.Count <= 0)
                return;
            ListViewItem lvi = this.listView1.SelectedItems[0];
            ThreadListStyleColorChangeForm tlsccf = new ThreadListStyleColorChangeForm();
            tlsccf.SelectColor = lvi.SubItems[2].Text.ParseColor();
            tlsccf.NowType = (VIPBrowserLibrary.Chron.ThreadOrResData.ChangeColorTypeConditions)Enum.Parse(typeof(VIPBrowserLibrary.Chron.ThreadOrResData.ChangeColorTypeConditions), lvi.SubItems[1].Text);
            tlsccf.Conditions = lvi.SubItems[3].Text;
            tlsccf.FormClosing += (_sender, _e) =>
            {
                c = tlsccf.SelectColor;
                condition = tlsccf.Conditions;
                nowType = tlsccf.NowType;
                listView1.SelectedItems[0].SubItems[1].Text = nowType.ToString();
                listView1.SelectedItems[0].SubItems[2].Text = c.ToKnownColor().ToString();
                listView1.SelectedItems[0].SubItems[3].Text = condition;
            };
            tlsccf.ShowDialog();
        }

        private void NewCreateColorRingButton_Clicked(object sender, EventArgs e)
        {
            //また俺クローンコード増やしちゃうの？
            int count = listView1.Items.Count;
            listView1.Items.Add(new ListViewItem(new string[] { "", "", "", "" }));
            Color c;
            VIPBrowserLibrary.Chron.ThreadOrResData.ChangeColorTypeConditions nowType;
            string condition;

            ListViewItem lvi = this.listView1.Items[count];
            ThreadListStyleColorChangeForm tlsccf = new ThreadListStyleColorChangeForm();
            tlsccf.SelectColor = Color.Empty;
            tlsccf.NowType = ChangeColorTypeConditions.Speed;
            tlsccf.Conditions = String.Empty;
            tlsccf.FormClosing += (_sender, _e) =>
            {
                c = tlsccf.SelectColor;
                condition = tlsccf.Conditions;
                nowType = tlsccf.NowType;
                listView1.Items[count].SubItems[1].Text = nowType.ToString();
                listView1.Items[count].SubItems[2].Text = c.ToKnownColor().ToString();
                listView1.Items[count].SubItems[3].Text = condition;
            };
            tlsccf.ShowDialog();
        }

        private void deleteItemButton_Click(object sender, EventArgs e)
        {
            this.listView1.SelectedItems[0].Remove();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.SaveData();
            IsOK = true;
            this.Close();
        }

        private void SaveData()
        {
            StringBuilder sb = new StringBuilder();
            ThreadListItemColorRing tlicr = new ThreadListItemColorRing();
            foreach (ListViewItem item in listView1.Items)
            {
                string[] data = { item.SubItems[1].Text, item.SubItems[2].Text, item.SubItems[3].Text };
                sb.Append(data[0]);
                sb.Append(":");
                sb.Append(data[1]);
                sb.Append("&");
                sb.Append(data[2] + "\r\n");
            }
            tlicr.Write(sb.ToString());

            ThreadColumn tc = new ThreadColumn();
            int i = 0;

            List<KeyValuePair<string, int>> disList = new List<KeyValuePair<string, int>>();
            List<KeyValuePair<string, int>> nonDisList = new List<KeyValuePair<string, int>>();

            foreach (var item in this.displayColumnListBox.Items)
            {
                disList.Add(new KeyValuePair<string, int>(item.ToString(), (((List<KeyValuePair<string, int>>)(this.displayColumnListBox.Tag))[i].Value)));
            }
            foreach (var item in this.notDisplayColumnListBox.Items)
            {
               nonDisList.Add(new KeyValuePair<string, int>(item.ToString(), (((List<KeyValuePair<string, int>>)(this.displayColumnListBox.Tag))[i].Value)));

            }
            tc.SaveSettingData(new List<KeyValuePair<string, int>>[] { disList, nonDisList });
        }

        private void moveRightColumnButton_Click(object sender, EventArgs e)
        {
            int idx = this.notDisplayColumnListBox.SelectedIndex;

            if (idx == -1)
                return;

            var tc = this.notDisplayColumnListBox.Tag as List<KeyValuePair<string, int>>;
            var pair = tc[idx];
            this.notDisplayColumnListBox.Items.RemoveAt(idx);
            
            tc.RemoveAt(idx);
            this.notDisplayColumnListBox.Tag = tc;

            this.displayColumnListBox.Items.Add(pair.Key);
            var newcl = this.displayColumnListBox.Tag as List<KeyValuePair<string, int>>;
            newcl.Add(pair);
            this.displayColumnListBox.Tag = newcl;
        }

        private void moveLeftColumnButton_Click(object sender, EventArgs e)
        {
            int idx = this.displayColumnListBox.SelectedIndex;
            if (idx == -1)
                return;
            var tc = this.displayColumnListBox.Tag as List<KeyValuePair<string, int>>;
            var pair = tc[idx];
            this.displayColumnListBox.Items.RemoveAt(idx);

            tc.RemoveAt(idx);
            this.displayColumnListBox.Tag = tc;

            this.notDisplayColumnListBox.Items.Add(pair.Key);
            var newcl = this.notDisplayColumnListBox.Tag as List<KeyValuePair<string, int>>;
            newcl.Add(pair);
            this.notDisplayColumnListBox.Tag = newcl;
        }
    }
}
