using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VIPBrowserLibrary.Chron.ThreadOrResData;
using System.Windows.Forms;
using VIPBrowserLibrary.BBS.X2ch;
using VIPBrowserLibrary.Common;

namespace VIPBrowser.ch2Browser
{
	public partial class NewThreadWindow : Form
	{
		public NewThreadWindow()
		{
			InitializeComponent();
		}
		public bool IsNextThread { get; set; }
		public string BoardName 
		{
			get { return this.Text.Replace("新規スレを立てる：",""); }
			set { this.Text = "新規スレを立てる：" + value; }
		}
		public ch2BrowserControl OwnerControlForm { get; set; }
		public string BoardAddress 
		{
			get { return this.boardAddress; }
			set 
			{ 
				this.boardAddress = value;
				this.bbsType = VIPBrowserLibrary.Common.TypeJudgment.BBSTypeJudg(value);
			}
		}
		private string boardAddress;
		private VIPBrowserLibrary.Common.BBSType bbsType;
		public string ThreadName 
		{
			get { return this.threadName; }
			set
			{
				this.threadName = value;
				if (this.IsNextThread)
				{
					this.threadName = NextThreadInfo.CreateNextThreadTitle(value);
				}
				this.threadTitleTextBox.Text = this.threadName;
				
			}
		}
		private string threadName;
		private async void threadWriteButton_Click(object sender, EventArgs e)
		{
			Dictionary<string, string> postData = new Dictionary<string, string>();
			ThreadData td = new ThreadData();
			string key,folder;
			string bbscgi = URLParse.DatToBBScgi(this.boardAddress, this.bbsType, VIPBrowserLibrary.Common.Type.threadlist, out key, out folder);
			bool isCan = false;
			switch (this.bbsType)
			{
				case VIPBrowserLibrary.Common.BBSType._2ch:
					{
						postData.Add("bbs", folder);
						postData.Add("subject", threadTitleTextBox.Text);
						postData.Add("FROM", nameTextBox.Text);
						postData.Add("mail", mailTextBox.Text);
						postData.Add("MESSAGE", sentenceTextBox.Text);
						postData.Add("time", VIPBrowserLibrary.Chron.Calture.GetTime(DateTime.Now).ToString());
						VIPBrowserLibrary.BBS.X2ch.X2chPoster x2p = new VIPBrowserLibrary.BBS.X2ch.X2chPoster(bbscgi);
						isCan = await x2p.Post2ch(postData, true, td);
					}
					break;
				case VIPBrowserLibrary.Common.BBSType.jbbs:
					{
						string[] fcData = folder.Split('/');
						postData.Add("DIR", fcData[0]);
						postData.Add("BBS", fcData[1]);
						postData.Add("TIME", VIPBrowserLibrary.Chron.Calture.GetTime(DateTime.Now).ToString());
						postData.Add("NAME", nameTextBox.Text);
						postData.Add("MAIL", mailTextBox.Text);
						postData.Add("MESSAGE", sentenceTextBox.Text);
						postData.Add("SUBJECT", threadTitleTextBox.Text);
						//postData.Add("submit", "新規スレッド作成");
						VIPBrowserLibrary.BBS.Jbbs.JbbsPoster jbp = new VIPBrowserLibrary.BBS.Jbbs.JbbsPoster();
						isCan = await jbp.PostJbbs(postData, true, td);
					}
					break;
				case VIPBrowserLibrary.Common.BBSType.machibbs:
					throw new NotImplementedException();

			}
			if (!isCan)
				MessageBox.Show("書き込みに失敗しました\n");
			else
				sentenceTextBox.Text = String.Empty;
			
			if (isCan)
			{
				await this.OwnerControlForm.MakedNewThread(threadTitleTextBox.Text, this.boardAddress);
			}
			this.Close();
		}

		private void canselButton_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
