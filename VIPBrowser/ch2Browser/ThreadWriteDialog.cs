using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VIPBrowserLibrary.BBS.X2ch;
using VIPBrowserLibrary.Chron.ThreadOrResData;

namespace VIPBrowser.ch2Browser
{
    public partial class ThreadWriteDialog : Form
    {
        private VIPBrowserLibrary.Setting.GeneralSetting gs = new VIPBrowserLibrary.Setting.GeneralSetting();
        public string Url { get; set; }
        public int NowResCount { get; set; }
        public ThreadWriteDialog()
        {
            InitializeComponent();
            this.Load += ThreadWriteDialog_Load;
        }

        void ThreadWriteDialog_Load(object sender, EventArgs e)
        {
            this.Text += ThreadData.ThreadName;
        }

        private async void ThreadWriteButton_Clicked(object sender, EventArgs e)
        {
            if (sentenceTextBox.Text == String.Empty)
                if (DialogResult.No == MessageBox.Show("本文が空ですが書き込みを続けますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                    return;

            string key;
            string folder;


            VIPBrowserLibrary.Common.Type t;
            VIPBrowserLibrary.Common.BBSType bt;
            VIPBrowserLibrary.Common.TypeJudgment.AllJudg(Url, out bt, out t);

            string bbscgi = VIPBrowserLibrary.Common.URLParse.DatToBBScgi(Url, bt, t, out key, out folder);
            Dictionary<string, string> postData = new Dictionary<string, string>();
            if (bt == VIPBrowserLibrary.Common.BBSType._2ch)
            {
                X2chPoster x2p = new X2chPoster(bbscgi);

                postData.Add("bbs", folder);
                postData.Add("key", key);
                postData.Add("FROM", nameTextBox.Text);
                postData.Add("mail", mailTextBox.Text);
                postData.Add("MESSAGE", sentenceTextBox.Text);
                postData.Add("time", VIPBrowserLibrary.Chron.Calture.GetTime(DateTime.Now).ToString());
                bool isTrue = await x2p.Post2ch(postData, false,ThreadData);
                if (isTrue)
                    this.Close();
                else
                    MessageBox.Show("書き込みに失敗しました。");
            }
            else if(bt == VIPBrowserLibrary.Common.BBSType.jbbs)
            {
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
                bool isCan = await jbp.PostJbbs(postData, false,ThreadData);
                if (isCan)
                    this.Close();
                else
                    MessageBox.Show("書き込みに失敗しました。");
            }
        }

        private void CanselButton_Clicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void MainTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            string name = nameTextBox.Text;
            if(name.IndexOf("#") != -1)
            {
                string[] splitName = name.Split('#');
                string trip = VIPBrowserLibrary.Chron.CreateTrip.Create(splitName[1]);
                name = splitName[0] + " ◆" +trip;
            }
            else if (String.IsNullOrEmpty(name))
            {
                VIPBrowserLibrary.Common.BBSType bt =  VIPBrowserLibrary.Common.TypeJudgment.BBSTypeJudg(this.Url);
                string flo = VIPBrowserLibrary.Common.URLParse.DatToFolder(this.Url, bt);
                if (bt != VIPBrowserLibrary.Common.BBSType.machibbs)
                {
                    Dictionary<string, string> data = await VIPBrowserLibrary.Common.GetBoardData.GetBoardDictionary(flo, false);
                    name = data["BBS_NONAME_NAME"];
                }
                else
                {
                    name = "名無しさん";
                }
            }
            string mail = mailTextBox.Text;
            string sentence = sentenceTextBox.Text;
            sentence = VIPBrowserLibrary.Utility.StringUtility.HTMLEncode(sentence);
            string date = DateTime.Now.ToString("yyyy/MM/dd(ddd) HH:mm:ss.ff");
            Res r = new Res(NowResCount + 1, name, mail, sentence, String.Empty, date, String.Empty, true);
            string datas = ResConvert.SimpleConvertCore(r);
            this.webBrowser1.DocumentText = "<html><head></head><body><font face=\"ＭＳ Ｐゴシック\">\n<dl>\n" + datas + "</body></html>";
        }


        public ThreadData ThreadData { get; set; }

    }
}
