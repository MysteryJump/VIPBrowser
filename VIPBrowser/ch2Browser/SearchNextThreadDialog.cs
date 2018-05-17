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
using VIPBrowserLibrary.Chron;

namespace VIPBrowser.ch2Browser
{
	public partial class SearchNextThreadDialog : Form
	{
		public SearchNextThreadDialog()
		{
			InitializeComponent();
		}
		public string SearchThreadName { get; set; }
		public ListViewItem[] SearchItems { get; set; }
		private void SearchNextThreadDialog_Load(object sender, EventArgs e)
		{
			var data = NextThreadInfo.SearchNextThread(this.SearchThreadName, this.SearchItems);
			this.label1.Text = "スレタイ:" + this.SearchThreadName;
			this.Text = "次スレ検索:" + this.SearchThreadName;
			try
			{
				this.listView1.Items.Clear();
				
				foreach (var item in data)
				{
					var i = item.Clone() as ListViewItem;
					i.SubItems.Clear();
					foreach (ListViewItem.ListViewSubItem list in item.SubItems)
					{
						ListViewItem.ListViewSubItem sub = new ListViewItem.ListViewSubItem();
						sub.BackColor = list.BackColor;
						sub.Font = list.Font;
						sub.ForeColor = list.ForeColor;
						sub.Name = list.Name;
						sub.Tag = list.Tag;
						sub.Text = list.Text;
						i.SubItems.Add(sub);
					}
					
					i.SubItems.RemoveByKey("IsRead");
					i.SubItems.RemoveByKey("OldResCount");
					i.SubItems.RemoveByKey("NewResCount");
					i.SubItems.RemoveByKey("Size");

					i.SubItems.RemoveAt(0);
					this.listView1.Items.Add(i);
				}
			}
			catch(Exception)
			{

			}
		}

		private void listView1_SelectedIndexChanged(object sender, EventArgs e)
		{
			string dat = (sender as ListView).SelectedItems[0].ImageKey;
			string read = VIPBrowserLibrary.Common.URLParse.DatToReadcgi(dat, VIPBrowserLibrary.Common.TypeJudgment.BBSTypeJudg(dat));
			this.MainForm.AddressTextBox.Text = read;
			this.MainForm.UrlEnter_Click(null, EventArgs.Empty);
			this.Close();
		}

		public ch2BrowserControl MainForm { get; set; }
	}
}
