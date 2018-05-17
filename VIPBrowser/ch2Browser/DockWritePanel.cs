using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace VIPBrowser.ch2Browser
{
    public partial class DockWritePanel : UserControl
    {
        private bool isThreadPost = false;

        //private string postUrl;

        private bool isCan;

        public ch2BrowserControl cbc;

        public DockWritePanel()
        {
            InitializeComponent();
        }

        private void IsThreadCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!IsThreadCheckBox.Checked)
            {
                threadTitleTextBox.Enabled = false;
                isThreadPost = false;
            }
            else
            {
                threadTitleTextBox.Enabled = true;
                isThreadPost = true;
            }
        }

        private async void acceptButton_Click(object sender, EventArgs e)
        {
            if (sentenceTextBox.Text == String.Empty)
                if (DialogResult.No == MessageBox.Show("本文が空ですが書き込みを続けますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                    return;
            this.acceptButton.Enabled = false;
            string boardUrl = String.Empty;
            #region スレ立て時の動作
            if (isThreadPost)
            {
                if (cbc.threadListViewTabControl.SelectedTab != null)
                {
                    string key;
                    string folder;
                    string url = boardUrl = cbc.threadListViewTabControl.SelectedTab.Name;

                    VIPBrowserLibrary.Common.BBSType bt;
                    VIPBrowserLibrary.Common.Type t;
                    VIPBrowserLibrary.Common.TypeJudgment.AllJudg(url, out bt, out t);
                    VIPBrowserLibrary.Chron.ThreadOrResData.ThreadData td = new VIPBrowserLibrary.Chron.ThreadOrResData.ThreadData();
                    td.ThreadAddress = url;
                    td.ThreadName = threadTitleTextBox.Text;
                    
                    string bbscgi = VIPBrowserLibrary.Common.URLParse.DatToBBScgi(url, bt, t ,out key, out folder);

                    Dictionary<string, string> postData = new Dictionary<string, string>();
                    if (bt == VIPBrowserLibrary.Common.BBSType._2ch)
                    {
                        postData.Add("bbs", folder);
                        postData.Add("subject", threadTitleTextBox.Text);
                        postData.Add("FROM", nameTextBox.Text);
                        postData.Add("mail", mailTextBox.Text);
                        postData.Add("MESSAGE", sentenceTextBox.Text);
                        postData.Add("time", VIPBrowserLibrary.Chron.Calture.GetTime(DateTime.Now).ToString());
                        VIPBrowserLibrary.BBS.X2ch.X2chPoster x2p = new VIPBrowserLibrary.BBS.X2ch.X2chPoster(bbscgi);
                        isCan = await x2p.Post2ch(postData, true,td);
                    }
                    else if(bt == VIPBrowserLibrary.Common.BBSType.jbbs)
                    {
                        //DIR=[板ジャンル]&BBS=[板番号]&TIME=[投稿時間]&NAME=[名前]&MAIL=[メールアドレス]&MESSAGE=[本文]&SUBJECT=[タイトル]&submit=新規スレッド作成
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
                        isCan = await jbp.PostJbbs(postData, true,td);
                    }
                }
            }
            #endregion
            #region レス時の動作
            else
            {
                if (cbc.threadViewTabControl.SelectedTab != null)
                {
                    string key;
                    string folder;
                    string url = cbc.threadViewTabControl.SelectedTab.Name;
                    VIPBrowserLibrary.Chron.ThreadOrResData.ThreadData td = (cbc.threadViewTabControl.SelectedTab.Controls[0] as ch2Browser.IEComponentThreadViewer).ThreadData;
                    VIPBrowserLibrary.Common.BBSType bt;
                    VIPBrowserLibrary.Common.Type t;
                    VIPBrowserLibrary.Common.TypeJudgment.AllJudg(url, out bt, out t);

                    string bbscgi = VIPBrowserLibrary.Common.URLParse.DatToBBScgi(url, bt,t, out key, out folder);

                    Dictionary<string, string> postData = new Dictionary<string, string>();

                    if (bt == VIPBrowserLibrary.Common.BBSType._2ch)
                    {
                        postData.Add("subject", "");
                        postData.Add("bbs", folder);
                        postData.Add("key", key);
                        postData.Add("FROM", nameTextBox.Text);
                        postData.Add("mail", mailTextBox.Text);
                        postData.Add("MESSAGE", sentenceTextBox.Text);
                        postData.Add("time", VIPBrowserLibrary.Chron.Calture.GetTime(DateTime.Now).ToString());
                        VIPBrowserLibrary.BBS.X2ch.X2chPoster x2p = new VIPBrowserLibrary.BBS.X2ch.X2chPoster(bbscgi);
                        isCan = await x2p.Post2ch(postData, false,td);
                    }
                    else if (bt == VIPBrowserLibrary.Common.BBSType.jbbs)
                    {
                        //DIR=[板ジャンル]&BBS=[板番号]&TIME=[投稿時間]&NAME=[名前]&MAIL=[メールアドレス]&MESSAGE=[本文]&KEY=[スレッド番号]&submit=書き込む
                        string[] fcdata = folder.Split('/'); 
                        //throw new NotSupportedException();
                        postData.Add("DIR", fcdata[0]);
                        postData.Add("BBS", fcdata[1]);
                        postData.Add("TIME", VIPBrowserLibrary.Chron.Calture.GetTime(DateTime.Now).ToString());
                        postData.Add("NAME", nameTextBox.Text);
                        postData.Add("MAIL", mailTextBox.Text);
                        postData.Add("MESSAGE", sentenceTextBox.Text);
                        postData.Add("KEY", key);
                        //postData.Add("submit", "書き込み");
                        VIPBrowserLibrary.BBS.Jbbs.JbbsPoster jbp = new VIPBrowserLibrary.BBS.Jbbs.JbbsPoster();
                        isCan = await jbp.PostJbbs(postData, false,td);
                    }
                }
            
            }
            #endregion

            if (!isCan)
                MessageBox.Show("書き込みに失敗しました\n");
            else
                sentenceTextBox.Text = String.Empty;
            this.acceptButton.Enabled = true;
            if (isThreadPost && isCan)
            {
                await this.cbc.MakedNewThread(threadTitleTextBox.Text, boardUrl);
            }
        }

        private void uploaderButton_Click(object sender, EventArgs e)
        {
            UploadDialog ud = new UploadDialog();
            ud.ShowDialog();
        }

        private void AAButton_Click(object sender, EventArgs e)
        {
            AAManageDialog amd = new AAManageDialog();
            amd.ShowDialog();
            this.sentenceTextBox.Text += amd.SelectedText;
        }
    }
}
