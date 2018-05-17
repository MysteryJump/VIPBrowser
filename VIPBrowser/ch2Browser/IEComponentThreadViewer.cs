using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VIPBrowserLibrary.Chron.ThreadOrResData;
using mshtml;
using VIPBrowserLibrary.Utility;
using VIPBrowserLibrary.Chron.ThreadOrResData.Abone;


namespace VIPBrowser.ch2Browser
{
    public class IEComponentThreadViewer : WebBrowser, VIPBrowserLibrary.Common.IThreadViewer
    {
        //public SimpleAbone SimpleAbone { get; set; }

        public AboneManagement AboneData { get; set; }

        public Timer RefereshTimer { get; set; }

        public bool IsRefresh 
        {
            get { return isRefresh; }
            set
            {
                isRefresh = value;
                if (value)
                {
                    RefereshTimer = new Timer();
                    this.RefereshTimer.Interval = 5000;
                    this.RefereshTimer.Tick += RefereshTimer_Tick;
                    this.RefereshTimer.Start();
                }
                else
                {
                    if (RefereshTimer != null)
                        this.RefereshTimer.Stop();
                }
            }
        }

        async void RefereshTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                string data = await this.SetUrl(DatUrl);
                // this.DocumentText = String.Empty;
                //this.Navigate("about:blank");
                this.Document.OpenNew(false);
                this.Document.Write(data);
                await VIPBrowserLibrary.Utility.TaskUtility.ThreadStop(1000);
                this.Document.Body.ScrollTop = this.ThreadData.NowScrollHeight;
                //this.Document.Body.ScrollIntoView(false);
            }
            catch (ObjectDisposedException) 
            {
                RefereshTimer.Stop();
                RefereshTimer.Dispose();
            }
        }
        private bool isRefresh = false;
        public VIPBrowserLibrary.Chron.ThreadOrResData.ThreadData ThreadData { get; set; }

        ch2Browser.Popup.HtmlPopup hp = null;
        /// <summary>
        /// このオブジェクトのオーナーであるch2BrowserControlを設定します
        /// </summary>
        public ch2BrowserControl MainForm { get { return mainForm; } set { mainForm = value; } }

        private ch2BrowserControl mainForm;

        private string searchText = String.Empty;

        private bool nowPopup = false;

        private int popupChild = -1;
        /// <summary>
        /// このコントロールが保持しているDatのURLを表します
        /// </summary>
        public string DatUrl { get; set; }

        private List<System.Drawing.Point> popupPlaces = new List<System.Drawing.Point>(20);

        //private int po = 0;
        /// <summary>
        /// プログラムの設定データを表します
        /// </summary>
        public VIPBrowserLibrary.Setting.SettingSerial SettingData { get; set; }
        /// <summary>
        /// このドキュメントに関連付けられているRes配列を返します
        /// </summary>
        public Res[] Reses { get; set; }
        public IEComponentThreadViewer()
        {
            ctor();
            //Reses = (ResCollection)this.Tag;
        }



        void Document_MouseDown(object sender, HtmlElementEventArgs e)
        {
            HtmlElement he = this.Document.GetElementFromPoint(e.MousePosition);
            if (he == null)
                return;
            string htmlCode = he.OuterHtml;
            if (htmlCode.IndexOf("menu:") == 9)
            {
                #region
                int index = int.Parse(new System.Text.RegularExpressions.Regex(@"[<]A\shref=""menu:(?<idx>\d+)"".+[>]").Match(htmlCode).Groups["idx"].Value);
                VIPBrowserLibrary.Chron.ThreadOrResData.Res[] r = (VIPBrowserLibrary.Chron.ThreadOrResData.Res[])this.Tag;
                string id = r[index - 1].ID;
                string name = r[index - 1].Name;
                string sentence = r[index - 1].Sentence;
                string mail = r[index - 1].Sentence;
                this.AboneData.InstLoad();
                #endregion
                System.Windows.Forms.ContextMenuStrip cms = new ContextMenuStrip();
                cms.Items.Add("このレスのIDをあぼーん", null,async (_sender,_e) => 
                {
                    this.AboneData.NGCollection.Add(new NGWord(id/*.Replace("ID:","").Replace("発信元:","")*/, AboneType.ID));
                    await this.AboneData.InstSave();
                });
                cms.Items.Add("このレスの名前をあぼーん", null,async (_sender, _e) =>
                {
                    this.AboneData.NGCollection.Add(new NGWord(name, AboneType.Name));
                    await this.AboneData.InstSave();
                });
                cms.Items.Add("このレスのメール欄をあぼーん", null, async(_sender, _e) =>
                {
                    this.AboneData.NGCollection.Add(new NGWord(mail, AboneType.Mail));
                    await this.AboneData.InstSave();
                });
                cms.Items.Add("このレスの本文をあぼーん", null, async(_sender, _e) =>
                {
                    this.AboneData.NGCollection.Add(new NGWord(sentence, AboneType.Sentence));
                    await this.AboneData.InstSave();
                });
                cms.Show(this, e.MousePosition);
            }
            else if ((htmlCode.Contains(".jpg") || htmlCode.Contains(".png") || htmlCode.Contains(".gif")) && htmlCode.IndexOf("<A href=") == 0)
            {
                var m = System.Text.RegularExpressions.Regex.Match(htmlCode, @"<A href="".+"" target=_blank>(?<url>.+)</A>",System.Text.RegularExpressions.RegexOptions.Compiled | System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                hp.Show("<img src=\"" + m.Groups["url"].Value + "\" height=\"500\" width=\"500\">");
            }
        }

        void Document_MouseMove(object sender, HtmlElementEventArgs e)
        {
            try
            {
                mshtml.IHTMLDocument2 ihtd = this.DomDocument as IHTMLDocument2;
                IHTMLTxtRange ihtr = ihtd.selection.createRange() as mshtml.IHTMLTxtRange;
                if (ihtr.text != null)
                    this.searchText = ihtr.text;
                HtmlElement he = this.Document.GetElementFromPoint(e.MousePosition);
                if (he == null)
                    return;
                string htmlCode = he.OuterHtml;
                if (htmlCode.IndexOf("A") == 1)
                {
                    Res[] rc = (Res[])this.Tag;
                    
                    //ResCollection rrr = new ResCollection();
                    Res[] resData = new Res[this.ThreadData.GetRescount + 1];
                    StringBuilder sb = new StringBuilder();

                    if (htmlCode.IndexOf("method:Extract", StringComparison.CurrentCultureIgnoreCase) != -1)
                    {
                        string method = String.Empty;
                        if (htmlCode.IndexOf("発信元") == -1)
                            method = new System.Text.RegularExpressions.Regex(@"method:Extract[(](?<type>..),(?<id>.{1,15})[)]", System.Text.RegularExpressions.RegexOptions.IgnoreCase).Match(htmlCode).Groups["id"].Value;
                        else
                            method = new System.Text.RegularExpressions.Regex(@"method:Extract[(](?<type>..),(?<id>.{1,25})[)]", System.Text.RegularExpressions.RegexOptions.IgnoreCase).Match(htmlCode).Groups["id"].Value;
                        //foreach (Res item in rc)
                        //{
                        //    if (method.Replace("ID:", "") == item.ID)
                        //    {
                        //        rrr.Add(item);
                        //    }
                        //}
                        int c = 0;
                        for (int i = 0; i < rc.Length; i++)
                        {

                            if (method == rc[i].ID)
                            {
                                resData[c] = rc[i];
                                c++;
                            }
                        }
                        foreach (Res item in resData)
                        {
                            if (String.IsNullOrEmpty(item.Sentence))
                                break;
                            sb.Append(ResConvert.SimpleConvertCore(item));
                        }
                        //MessageBox.Show(sb.ToString().Replace("<br>","\n"));
                        //hp.Show(sb.ToString());

                        hp.Show(sb.ToString());
                        //if (popupChild == -1)
                        //{
                        //    nowPopup = true;
                        //    popupPlaces.Add(new System.Drawing.Point(MousePosition.X, MousePosition.Y));
                        //}
                        //else if (popupChild == 0)
                        //{
                        //    popupPlaces.Add(MousePosition);
                        //    popupChild++;
                        //}
                        //else
                        //{
                        //    popupPlaces.Add(MousePosition);
                        //    popupChild++;
                        //}

                    }
                    else
                    {
                        if (htmlCode.IndexOf("#") == 9 || htmlCode.IndexOf("#") == 20)
                        {
                            string data = new System.Text.RegularExpressions.Regex(@"<A href=(""about:blank#""|""#"")>&gt;&gt;(?<res>\d*)</A>").Match(htmlCode).Groups["res"].Value;
                            int resNumber = int.Parse(data);
                            resNumber--;
                            //ResConvert.SimpleConvertCore(rc[resNumber]);
                            //sb.Append(rc[resNumber].Index).Append(":　").Append(rc[resNumber].Name).Append("[" + rc[resNumber].Mail + "]").Append(rc[resNumber].Date + "<br>").Append(rc[resNumber].Sentence + "<br>");
                            //hp.Show(sb.ToString());

                            hp.Show(ResConvert.SimpleConvertCore(rc[resNumber]));
                        }

                    }
                    
                }
                this.HidePopup();
            }
            catch { return; }
        }

        void Document_MouseUp(object sender, HtmlElementEventArgs e)
        {
            throw new NotImplementedException();
        }

        void IEComponentThreadViewer_StatusTextChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        void IEComponentThreadViewer_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            throw new NotImplementedException();
        }

        void IEComponentThreadViewer_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            throw new NotImplementedException();
        }

        void IEComponentThreadViewer_NewWindow(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                e.Cancel = true;
                if (this.StatusText.IndexOf("menu:") == 0) 
                    return;
                MainForm.AddressTextBoxText = this.StatusText;
                MainForm.UrlEnter_Click(null,null);
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// このオブジェクトのDocumentプロパティに書き込みます
        /// </summary>
        /// <param name="data">書き込むデータ</param>
        public void Write(string data)
        {
            
            this.Document.Write(data);
            Reses = (Res[])this.Tag;
            OnDocumentCompleted(null);
        }
        [STAThread]
        void ctor()
        {
            this.NewWindow += IEComponentThreadViewer_NewWindow;
            //this.PreviewKeyDown += IEComponentThreadViewer_PreviewKeyDown;
            //this.Navigating += IEComponentThreadViewer_Navigating;
            //this.StatusTextChanged += IEComponentThreadViewer_StatusTextChanged;
            this.Navigate("about:blank");

            //this.Document.MouseUp += Document_MouseUp;
            this.Document.MouseMove += Document_MouseMove;
            this.Document.MouseDown += Document_MouseDown;
            this.Document.Click += Document_Click;
            hp = new Popup.HtmlPopup(this);
            this.IsWebBrowserContextMenuEnabled = false;
            System.Windows.Forms.ContextMenuStrip cms = new ContextMenuStrip();
            cms.Items.Add("選択している文を検索する", null, (sender, e) =>
            {
                if (String.IsNullOrEmpty(searchText))
                    MessageBox.Show("選択している文がありません", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                string searchString = StringUtility.URLEncode(searchText);
                VIPBrowserLibrary.Other.SearchEngineService ses = new VIPBrowserLibrary.Other.SearchEngineService(searchText);
                
                string url = ses.GetUrl(this.SettingData.DefalutSearcher);
                this.MainForm.AddressTextBox.Text = url;
                this.MainForm.UrlEnter_Click(null, null);
            });
            cms.Items.Add("お気に入りに追加する", null, (sender, e) => 
            {
                this.MainForm.favoriteTreeView.Nodes.Add(this.DatUrl, this.ThreadName, "Thread");
            });
			cms.Items.Add(new ToolStripSeparator());
            cms.Items.Add("次スレ検索", null,  (sender,e) => 
            {
                foreach (TabPage item in this.MainForm.threadListViewTabControl.TabPages)
                {
                    if (item.Name == VIPBrowserLibrary.Common.URLParse.DatToFolder(this.DatUrl, VIPBrowserLibrary.Common.TypeJudgment.BBSTypeJudg(this.DatUrl)))
                    {
                        var lv = (item.Controls[0] as ListView).Items.Cast<ListViewItem>().ToArray();
						SearchNextThreadDialog sntd = new SearchNextThreadDialog();
						sntd.SearchThreadName = this.ThreadName;
						sntd.SearchItems = lv;
						
						sntd.MainForm = this.MainForm;
						sntd.ShowDialog();
                        break;
                    }
                }
            });
			
            this.ContextMenuStrip = cms;
            this.AboneData = new AboneManagement();

        }

        void Document_Click(object sender, HtmlElementEventArgs e)
        {
            //throw new NotImplementedException();
        }

        ///	<summary>
        ///	AxWebBrowser.Documentプロパティを取得します
        ///	</summary>
        public HTMLDocument DomDocument
        {
            get { return this.Document.DomDocument as HTMLDocument; }
        }

        ///	<summary>
        ///	AxWebBrowser.Document.bodyプロパティを取得します
        ///	</summary>
        ///	<returns>ドキュメントのHTMLBody</returns>
        public HTMLBody HtmlBody
        {
            get { return (HTMLBody)DomDocument.body;}
        }

        public string lastPopupRefImg { get; set; }

        public string lastPopupRef { get; set; }

        public bool clickedPopup { get; set; }
        /// <summary>
        /// ポップアップを隠します
        /// </summary>
        private void HidePopup()
        {
            //if (nowPopup)
            //{
                if (popupChild != 0)
                {
                    hp.Hide();
                    popupChild--;
                }
               else
               {
                   hp.Hide();
                    hp.inPopup = true;
                    nowPopup = false;
                }
            //}
            //isExtracting = false;

            // 強調表示をリセット
            //if (highlighting && extractor != null && !popInterf.Visible)
            //{
            //    extractor.Reset();
            //    extractor = null;
            //    highlighting = false;
            //}
            //		newPopup = false;
        }

        /// <summary>
        /// 現在のドキュメントのスクロール可能な位置を取得します
        /// </summary>
        public int DocumentScrollHeight
        {
            get { return this.Document.Body.ScrollRectangle.Height; }
        }

        /// <summary>
        /// 指定したURLのデータを取得し表示します
        /// </summary>
        /// <param name="url">指定先のURL</param>
        /// <returns>取得後のIEComponentThreadViewer</returns>
        public async Task<string> SetUrl(string url)
        {
            string data = String.Empty;
            VIPBrowserLibrary.Common.Type t;
            VIPBrowserLibrary.Common.BBSType bt;
            VIPBrowserLibrary.Common.TypeJudgment.AllJudg(url, out bt, out t);
            VIPBrowserLibrary.BBS.Common.IThreadReader itr;
            switch (t)
            {
                case VIPBrowserLibrary.Common.Type.thread:
                    switch (bt)
                    {
                        //case VIPBrowserLibrary.Common.BBSType._2ch:
                        //    {
                        //        VIPBrowserLibrary.BBS.X2ch.X2chThreadReader x2r = new VIPBrowserLibrary.BBS.X2ch.X2chThreadReader(url);
                        //        data = await x2r.GetResponse();
                        //        this.Tag = x2r.ResSets;
                        //        this.ThreadData = x2r.ThreadInfo;
                        //        this.ThreadName = x2r.ThreadName;
                        //        break;
                        //    }
                        //case VIPBrowserLibrary.Common.BBSType.jbbs:
                        //    {
                        //        VIPBrowserLibrary.BBS.Jbbs.JbbsThreadReader jtr = new VIPBrowserLibrary.BBS.Jbbs.JbbsThreadReader(url);
                        //        data = await jtr.GetResponse();
                        //        this.Tag = jtr.ResSets;
                        //        this.ThreadData = jtr.ThreadInfo;
                        //        this.ThreadName = jtr.ThreadName;
                        //        break;
                        //    }
                        //case VIPBrowserLibrary.Common.BBSType.machibbs:
                        //    {
                        //        VIPBrowserLibrary.BBS.MachiBBS.MachiBBSThreadReader mbtr = new VIPBrowserLibrary.BBS.MachiBBS.MachiBBSThreadReader(url);
                        //        data = await mbtr.GetResponse();
                        //        this.Tag = mbtr.ResSets;
                        //        this.ThreadData = mbtr.ThreadInfo;
                        //        this.ThreadName = mbtr.ThreadName;
                        //        break;
                        //    }
                        case VIPBrowserLibrary.Common.BBSType._2ch:
                            itr = new VIPBrowserLibrary.BBS.X2ch.X2chThreadReader(url);
                            break;
                        case VIPBrowserLibrary.Common.BBSType.jbbs:
                            itr = new VIPBrowserLibrary.BBS.Jbbs.JbbsThreadReader(url);
                            break;
                        case VIPBrowserLibrary.Common.BBSType.machibbs:
                            itr = new VIPBrowserLibrary.BBS.MachiBBS.MachiBBSThreadReader(url);
                            break;
                        default:
                            throw new ArgumentException();
                    }
                    data = await itr.GetResponse();
                    this.Tag = itr.ResSets;
                    this.ThreadData = itr.ThreadInfo;
                    this.ThreadName = itr.ThreadName;
                    break;
                case VIPBrowserLibrary.Common.Type.threadlist:
                    return null; //いちおうこれ //throw new ArgumentException();//これでもいいかも
                default:
                    throw new ArgumentException();
            } return data;
        }

        public string OfflineSetUrl(string url)
        {
            VIPBrowserLibrary.Common.BBSType bt = VIPBrowserLibrary.Common.TypeJudgment.BBSTypeJudg(url);
            VIPBrowserLibrary.BBS.Common.IThreadReader itr;
            switch (bt)
            {
                case VIPBrowserLibrary.Common.BBSType._2ch:
                    itr = new VIPBrowserLibrary.BBS.X2ch.X2chThreadReader(url);
                    break;
                case VIPBrowserLibrary.Common.BBSType.jbbs:
                    itr = new VIPBrowserLibrary.BBS.Jbbs.JbbsThreadReader(url);
                    break;
                case VIPBrowserLibrary.Common.BBSType.machibbs:
                    itr = new VIPBrowserLibrary.BBS.MachiBBS.MachiBBSThreadReader(url);
                    break;
                default:
                    throw new NotSupportedException();
            }

            string dat = itr.OfflineGetResponse();
            this.Tag = itr.ResSets;
            this.ThreadData = itr.ThreadInfo;
            this.ThreadName = itr.ThreadName;
            return dat;
        }

        public string ThreadName { get; set; }

        protected virtual new void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                this.Reses = null;
                this.popupChild = 0;
                this.popupPlaces = null;
                this.searchText = null;
                //this.SettingData = null;
                this.AboneData = null;
                this.DatUrl = null;
            }
            base.Dispose(true);
        }
        public new void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
