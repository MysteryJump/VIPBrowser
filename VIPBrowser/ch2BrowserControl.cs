/*
 * VIPBrowser.ch2BrowserControluserControlクラス
 * 
 * いい加減URL_Enterをイベントハンドラーではなくメソッドとして呼び出したい
 * */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VIPBrowserLibrary.Common;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace VIPBrowser
{
    public partial class ch2BrowserControl : UserControl, VIPBrowserPlugin.IPluginHostCh2Browser
    {
        #region タブ管理関係
        int ThreadListCount = 0;
        int openListViewCount = 0;
        int openThreadViewCount = 0;
        private int ThreadTabCount = 0;
        #endregion

        #region コンストラクターらへん
        public ch2BrowserControl()
        {
            InitializeComponent();

        }
        /// <summary>
        /// オーナーのフォーム
        /// </summary>
        public Form1 fff;
        /// <summary>
        /// オーナーのフォームを指定してコントロールのインスタンスを生成します
        /// </summary>
        /// <param name="f">オーナーのフォーム</param>
        public ch2BrowserControl(Form f)
        {
            InitializeComponent();
            Form1 ff = f as Form1;
            fff = ff;
            this.dockWritePanel1.cbc = this;
            this.threadListViewTabControl.MouseWheel += TabControl_MouseWheel;
            this.threadViewTabControl.MouseWheel += TabControl_MouseWheel;
            this.IsOnlineState = true;
            this.listView1.MaxLength = 20;
			
        }

        #endregion
        private VIPBrowserLibrary.Setting.GeneralSetting gs = new VIPBrowserLibrary.Setting.GeneralSetting();
        public VIPBrowserLibrary.Setting.SettingSerial SettingData { get; set; }
        /// <summary>
        /// スレッドのソートに用いる変数
        /// </summary>
        private bool isAssecing = false;
        /// <summary>
        /// 多重非同期防止用のフィールド
        /// </summary>
        private bool IsAsyncState = false;
        /// <summary>
        /// 多重非同期防止用のフィールド
        /// </summary>
        private string NowURL = String.Empty;
        /// <summary>
        /// デザイン設定用
        /// </summary>
        private bool? IsThread = null;
        /// <summary>
        /// デザイン設定用
        /// </summary>
        private int Heights;
        /// <summary>
        /// デザイン設定用
        /// </summary>
        private int Heightes;
        /// <summary>
        /// デザイン設定用
        /// </summary>
        private int Heighteses;
        /// <summary>
        /// デザイン設定用
        /// </summary>
        private bool IsOpenWriteBox = true;
        /// <summary>
        /// 板一覧の可視状況
        /// </summary>
        private bool isHideBoardList = false;
        /// <summary>
        /// 板一覧の横幅
        /// </summary>
        private int BoardListWidth;
        /// <summary>
        /// 現在オンラインモードかどうか表します
        /// </summary>
        public bool IsOnlineState { get; set; }

        private List<ListViewItem> itemList = new List<ListViewItem>();

        public async void UrlEnter_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Stopwatch sdw = new System.Diagnostics.Stopwatch();
            sdw.Start();
            try
            {
                if (!IsOnlineState && this.AddressTextBox.Text.IndexOf("http") == 0)
                {
                    MessageBox.Show("オフラインモードです");
                    return;
                }
                if (NowURL == this.AddressTextBox.Text)
                    if (IsAsyncState)
                        throw new VIPBrowserLibrary.Utility.Exception.AsyncProcessMultipleAccessException();
                IsAsyncState = true;
                NowURL = this.AddressTextBox.Text;
                #region 操作
                try
                {
                    BBSType bt;
                    VIPBrowserLibrary.Common.Type t;
                    TypeJudgment.AllJudg(AddressTextBox.Text, out bt, out t);
                    string url = AddressTextBox.Text;
                    if (url.IndexOf("http") == 0)
                        this.toolStripButton2.Enabled = true;
                    switch (t)
                    {
                        #region スレッドを取得する際の動作
                        case VIPBrowserLibrary.Common.Type.thread:
                            if (!url.Contains(gs.DatFilePath))
                                url = URLParse.ReadcgiToDat(url, bt);
                            bool ifSameNameTabs = false;
                            int SameArrayNumbers = 0;
                            string doubleName = null;
                            foreach (TabPage item in threadViewTabControl.TabPages)
                            {

                                if (item.Name == url)
                                {
                                    ifSameNameTabs = true;
                                    doubleName = item.Name;
                                    break;
                                }
                                SameArrayNumbers++;

                            }


                            ch2Browser.IEComponentThreadViewer web = new ch2Browser.IEComponentThreadViewer();
                            web.Document.MouseMove += Document_MouseMove;
                            web.DatUrl = url;
                            web.Parent = this;
                            web.SettingData = this.SettingData;
                            // WebBrowser web = new WebBrowser();
                            web.MainForm = this;
                            web.ScriptErrorsSuppressed = true;
                            web.Dock = DockStyle.Fill;
                            ListViewItem lvi = null;
                            //string datData = await new VIPBrowserLibrary.BBS.X2ch.X2chSimpleThreadReader(url).GetResponseMethod();
                            TabPage ta = null;
                            int scrollPalce = 0;
                            string datData = String.Empty;
                            datData = await web.SetUrl(url);
                            lvi = new ListViewItem(new string[] { "スレッド", web.ThreadName });
                            ta = new TabPage(web.ThreadName);
                            ta.Name = url; try
                            {
                                scrollPalce = web.ThreadData.NowScrollHeight;
                            }
                            catch { }
                            finally { }

                            if (ifSameNameTabs)
                            {
                                web.DocumentCompleted += (_sender, _e) =>
                                {
                                    web.Document.Body.ScrollTop = scrollPalce;
                                    if (web.Document.Body.ScrollTop == 49 && (scrollPalce - web.Document.Body.ScrollTop) <= 235)
                                    {
                                        web.Document.Body.ScrollIntoView(false);
                                    }
                                };
                                //Console.WriteLine("Start DocumentText");
                                web.Write(datData);
                                // web.DocumentText = datData;
                                //Console.WriteLine("End DocumentText");
                                //将来的にバグの原因がわかったら削除したい
                                //System.Threading.Thread.Sleep(850);
                                await VIPBrowserLibrary.Utility.TaskUtility.ThreadStop(1000);
                                web.Document.Body.ScrollTop = scrollPalce * 2;

                                threadViewTabControl.SelectedIndex = SameArrayNumbers;
                                threadViewTabControl.TabPages[SameArrayNumbers].Controls.Clear();
                                threadViewTabControl.TabPages[url].Controls.Add(web);
                            }
                            else
                            {


                                ta.Controls.Add(web);
                                //ta.t = contextMenuStrip2;
                                threadViewTabControl.Controls.Add(ta);
                                threadViewTabControl.SelectedIndex = ThreadTabCount;

                                lvi.Tag = new string[] { "Thread", url };
                                lvi.Name = url;
                                //listView1.Items.Add(lvi);
                                //listView1.Items[(openThreadViewCount + openListViewCount)].Group = listView1.Groups["threadGroup"];
                                openListView.Items.Add(lvi);

                                web.DocumentCompleted += (_sender, _e) =>
                                {
                                    web.Document.Body.ScrollTop = scrollPalce;
                                    if (web.Document.Body.ScrollTop == 49 && (scrollPalce - web.Document.Body.ScrollTop) <= 235)
                                    {
                                        web.Document.Body.ScrollIntoView(false);
                                    }
                                };

                                //web.DocumentText = datData;
                                web.Write(datData);
                                //将来的にバグの原因がわかったら削除したい
                                //System.Threading.Thread.Sleep(850);
                                await VIPBrowserLibrary.Utility.TaskUtility.ThreadStop(1000);
                                web.Document.Body.ScrollTop = scrollPalce;
                                if (web.Document.Body.ScrollTop == 49 && (scrollPalce - web.Document.Body.ScrollTop) <= 235)
                                {
                                    web.Document.Body.ScrollIntoView(false);
                                }

                                openThreadViewCount++;
                                ThreadTabCount++;
                                ifSameNameTabs = false;
                            }
                            break;
                        #endregion
                        #region スレッド一覧のURLを指定した際の動作
                        case VIPBrowserLibrary.Common.Type.threadlist:
                            int SameArrayNumber = 0;
                            bool ifSameNameTab = false;
                            string doubleNames = null;
                            string boardUrl = AddressTextBox.Text;
                            string boardName = "未確認";

                            if (AddressTextBox.Text == String.Empty)
                            {
                                boardUrl = AddressTextBox.Text = "http://uravip.tonkotsu.jp/news7vip/";
                                boardName = "裏ニュー速VIP";
                            }
                            boardName = await GetBoardData.GetBoardName(boardUrl, false);
                            //boardName = (await GetBoardData.GetBoardDictionary(boardUrl, false))["BBS_TITLE"];
                            if (boardName == String.Empty)
                            {
                                TreeView tv = sender as TreeView;
                                if (tv != null)
                                {
                                    boardName = tv.SelectedNode.Text;
                                }
                                else
                                {
                                    boardName = "未確認";
                                }
                            }
                            foreach (TabPage item in threadListViewTabControl.TabPages)
                            {
                                if (item.Name == boardUrl)
                                {
                                    ifSameNameTab = true;
                                    doubleNames = item.Name;
                                    break;
                                }
                                SameArrayNumber++;
                            }

                            #region デザイナー構成要素
                            TabPage tp = new TabPage(boardName);
                            ListView lv = new ListView();
                            //lv.BackgroundImage = Properties.Resources.お遊び;
                            //lv.BackgroundImageTiled = true;
                            lv.MouseEnter += ControlMouseEnter;
                            lv.Parent = this;
                            //ColumnHeader countCh = new ColumnHeader();
                            //countCh.Text = "#";
                            //countCh.Width = 25;

                            //ColumnHeader threadName = new ColumnHeader();
                            //threadName.Text = "スレッドタイトル";
                            //threadName.Width = 350;

                            //ColumnHeader resCount = new ColumnHeader();
                            //resCount.Text = "スレ立て";
                            //resCount.Width = 50;

                            //ColumnHeader dateCount = new ColumnHeader();
                            //dateCount.Text = "レス数";
                            //resCount.Width = 200;

                            //ColumnHeader speed = new ColumnHeader();
                            //speed.Text = "勢い";
                            //speed.Width = 100;

                            //ColumnHeader datSize = new ColumnHeader();
                            //datSize.Text = "サイズ";
                            //datSize.Width = 50;

                            //ColumnHeader lastRes = new ColumnHeader();
                            //lastRes.Text = "既読";
                            //lastRes.Width = 50;

                            //ColumnHeader newRes = new ColumnHeader();
                            //newRes.Text = "新着";
                            //newRes.Width = 50;

                            //ColumnHeader isRead = new ColumnHeader();
                            //isRead.Text = "!";
                            //isRead.Width = 25;

                            //ColumnHeader[] ch = { isRead, countCh, threadName, dateCount, resCount, speed, datSize, lastRes, newRes };
                            var chs = new VIPBrowserLibrary.Chron.ThreadColumn();
                            chs.ReadColumnData(gs.NotNecessarySettingDataPath + "\\column.dat");
                            var ch = chs.ColumnData;
                            lv.AllowColumnReorder = true;
                            lv.MultiSelect = false;
                            lv.FullRowSelect = true;
                            lv.Columns.AddRange(ch);
                            lv.ColumnClick += (_sender, _e) =>
                            {
                                lv.ListViewItemSorter = new VIPBrowserLibrary.Common.ThreadListSorter(_e.Column, isAssecing);
                                isAssecing = !isAssecing;
                            };
                            lv.Font = global::VIPBrowser.Properties.Settings.Default.UseFontfamily;
                            lv.Click += lv_Click;
                            lv.View = View.Details;
                            lv.Dock = DockStyle.Fill;
                            ListViewItem[] li = null;
                            #endregion
                            switch (bt)
                            {
                                case BBSType._2ch:
                                    li = await new VIPBrowserLibrary.BBS.X2ch.X2chThreadListReader(boardUrl).GetThreadList();
                                    break;
                                case BBSType.jbbs:
                                    li = await new VIPBrowserLibrary.BBS.Jbbs.JbbsThreadListReader(boardUrl).GetThreadList();
                                    break;
                                case BBSType.machibbs:
                                    li = await new VIPBrowserLibrary.BBS.MachiBBS.MachiBBSThreadListReader(boardUrl).GetThreadList();
                                    //throw new NotSupportedException();
                                    break;
                                default:
                                    throw new NotSupportedException();
                            }
                            foreach (ListViewItem item in li)
                            {
                                if (item != null)
                                    lv.Items.Add(item);
                            }
                            if (ifSameNameTab)
                            {
                                threadListViewTabControl.SelectedIndex = SameArrayNumber;
                                threadListViewTabControl.TabPages[SameArrayNumber].Controls.Clear();
                                threadListViewTabControl.TabPages[boardUrl].Controls.Add(lv);
                            }
                            else
                            {
                                tp.Controls.Add(lv);
                                tp.Name = boardUrl;
                                threadListViewTabControl.Controls.Add(tp);
                                threadListViewTabControl.SelectedIndex = ThreadListCount;

                                ListViewItem lvis = new ListViewItem(new string[] { "スレッドリスト", boardName });
                                //lvis.Text = boardName;
                                lvis.Name = boardUrl;
                                lvis.Tag = new string[] { "ThreadList", boardUrl };
                                //listView1.Items.Add(lvis);
                                //listView1.Items[openListViewCount + openThreadViewCount].Group = listView1.Groups["threadListgroup"];
                                openListView.Items.Add(lvis);
                                openListViewCount++;
                                ThreadListCount++;
                            }
                            break;
                        #endregion
                        #region いずれにも当てはまらなかった際の動作
                        default:
                            throw new System.Net.WebException();
                        #endregion
                    }
                }
                #endregion
                #region エラー処理
                catch (System.Net.WebException)
                {
                    // throw new NotImplementedException();
                    WebBrowser wb = new WebBrowser();
                    wb.ScriptErrorsSuppressed = true;
                    wb.Dock = DockStyle.Fill;
                    // tabControl2 = new TabControl();
                    TabPage tp = new TabPage();
                    wb.Navigate(AddressTextBox.Text);
                    wb.Navigated += (_sender, _e) =>
                    {
                        // Console.WriteLine(_sender);
                        WebBrowser wbb = _sender as WebBrowser;
                        string title = wbb.Document.Title;
                        string enc = wbb.Document.Encoding;
                        byte[] data = Encoding.Convert(Encoding.GetEncoding(enc), Encoding.Unicode, Encoding.GetEncoding(enc).GetBytes(title));
                        tp.Text = Encoding.Unicode.GetString(data);
                    };
                    wb.NewWindow += (_sender, _e) =>
                    {
                        WebBrowser w = _sender as WebBrowser;
                        _e.Cancel = true;
                        Console.WriteLine(w.StatusText + w.Url.ToString());
                        TabPage t = new TabPage();
                        WebBrowser ww = new WebBrowser();
                        ww.Navigate(w.StatusText);
                        ww.Dock = DockStyle.Fill;
                        t.Controls.Add(ww);
                        fff.tabControl2.TabPages.Add(t);
                        fff.tabControl2.SelectTab(fff.tabControl2.TabPages.Count - 1);
                    };
                    tp.Controls.Add(wb);
                    fff.tabControl2.Controls.Add(tp);
                    fff.MainTabControl.SelectTab("WebBrowserPage");
                }
                //catch (ArgumentException) { goto erro; }
                #endregion
                IsAsyncState = false;
                NowURL = String.Empty;
            }

            catch (VIPBrowserLibrary.Utility.Exception.AsyncProcessMultipleAccessException)
            {
                return;
            }
            catch { throw; }
            sdw.Stop();
            sdw = null;
            //MessageBox.Show(sdw.ElapsedMilliseconds.ToString());
        }

        void Document_MouseMove(object sender, HtmlElementEventArgs e)
        {
            this.ControlMouseEnter(sender, null);
        }
		
        void lv_Click(object sender, EventArgs e)
        {
			
            ListView lv = sender as ListView;
            if (lv.SelectedItems.Count > 0)
            {
                string url = lv.SelectedItems[0].ImageKey.Replace(" ", "");
                BBSType bt;
                VIPBrowserLibrary.Common.Type t;
                TypeJudgment.AllJudg(url, out bt, out t);
                AddressTextBox.Text = URLParse.DatToReadcgi(url, bt);
                UrlEnter_Click(null, null);
                string f = URLParse.DatToFolder(url, bt);
                //System.Threading.Thread.Sleep(50);
                toolStripButton5_Click(null, null);
                //int ind = lv.SelectedItems[0].Index;
                //lv.Items.Clear();
                //lv.Items.AddRange(await new VIPBrowserLibrary.BBS.X2ch.X2chThreadListReader(f).OfflineGetThreadList());

                return;
            }

        }


        private async void ch2BrowserControl_Load(object sender, EventArgs e)
        {
            VIPBrowserLibrary.BBS.Common.GetBoardList gbl = new VIPBrowserLibrary.BBS.Common.GetBoardList();
            TreeNode tn = await gbl.GetUserBoardList();
            this.treeView1.Nodes.Clear();
            treeView1.Nodes.Add(await gbl.GetBoardLists());
            if (tn != null)
                treeView1.Nodes.Add(tn);

			this.AddressTextBox.Text = this.SettingData.DefaultAddressBarText;

        }
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Name.IndexOf("http://") != -1)
            {
                AddressTextBox.Text = e.Node.Name;
                UrlEnter_Click(sender, null);
                if (IsThread == true)
                    toolStripButton5_Click(null, null);
            }
        }
        private void BoardRefreshTool_Clicked(object sender, EventArgs e)
        {
            ch2Browser.RefreshBoardList rbd = new ch2Browser.RefreshBoardList(this);
            rbd.SettingData = this.SettingData;
            rbd.ShowDialog();
            rbd.Dispose();
        }

        private void SearchToolStripButton_Click(object sender, EventArgs e)
        {
            if (threadListViewTabControl.SelectedTab != null)
            {
                ListView lv = threadListViewTabControl.SelectedTab.Controls[0] as ListView;
                if (!String.IsNullOrEmpty(toolStripTextBox2.Text))
                {
                    string keyword = toolStripTextBox2.Text;
                    ListViewItem[] li = new ListViewItem[lv.Items.Count];
                    List<ListViewItem> lvi = new List<ListViewItem>();
                    int i = 0;
                    foreach (ListViewItem item in lv.Items)
                    {
                        li[i] = item;
                        i++;
                    }
                    foreach (ListViewItem mmm in lv.Items)
                    {
                        string unko = mmm.SubItems["Name"].Text;

                        if (unko.IndexOf(keyword, StringComparison.CurrentCultureIgnoreCase) != -1)
                        {
                            lvi.Add(mmm);
                        }


                    }
                    lv.Items.Clear();
                    lv.Items.AddRange(lvi.ToArray());
                    if (lv.Tag == null)
                        lv.Tag = li;
                }
                else if (String.IsNullOrEmpty(toolStripTextBox2.Text))
                {
                    lv.Items.Clear();
                    lv.Items.AddRange((ListViewItem[])lv.Tag);
                }
            }
        }
        private void ThreadWriteButton_Clicked(object sender, EventArgs e)
        {
            if (threadViewTabControl.SelectedTab != null)
            {
                Console.WriteLine(threadViewTabControl.SelectedTab.Name);
            }
        }

        private void appendNewBoardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Dialogs.AddNewBoardDialog().ShowDialog();
        }
        private void TabDeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {

            this.ThreadListCount--;
            this.openListViewCount--;
            this.openListView.Items.RemoveByKey(this.threadListViewTabControl.SelectedTab.Name);
            this.threadListViewTabControl.SelectedTab.Controls[0].Dispose();
            this.threadListViewTabControl.SelectedTab.Dispose();

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (threadListViewTabControl.SelectedTab.Name.IndexOf("http") == 0 && threadListViewTabControl.SelectedTab.Text != "既得ログ一覧")
                {
                    AddressTextBox.Text = threadListViewTabControl.SelectedTab.Name;
                    toolStripButton2.Enabled = true;
                }
                else
                    toolStripButton2.Enabled = false;
                this.toolStripTextBox2.Text = String.Empty;
            }
            catch (NullReferenceException) { }
            catch (ArgumentNullException) { }
            catch { throw; }
        }

        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ch2Browser.IEComponentThreadViewer iectv = this.threadViewTabControl.SelectedTab.Controls[0] as ch2Browser.IEComponentThreadViewer;
                this.autoReloadButton.Checked = iectv.IsRefresh;
                string selectName = threadViewTabControl.SelectedTab.Name;
                BBSType bt = TypeJudgment.BBSTypeJudg(selectName);
                AddressTextBox.Text = URLParse.DatToReadcgi(selectName, bt);
            }
            catch (NullReferenceException) { this.autoReloadButton.Checked = false; }
            catch (ArgumentNullException) { }
            catch { throw; }
        }

        private void closeThreadViewTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ThreadTabCount--;
            this.openThreadViewCount--;
            this.openListView.Items.RemoveByKey(this.threadViewTabControl.SelectedTab.Name);
            ch2Browser.IEComponentThreadViewer iectv = this.threadViewTabControl.SelectedTab.Controls[0] as ch2Browser.IEComponentThreadViewer;

            //毎回思うんだけどなんでこのクラス静的クラス（静的メソッド）にしなかったんだろ←えっ、なんのこと？まさかGeneralSttingのこと？
            //だからと言って今からこのソリューションの核になる部分変えられないし
            VIPBrowserLibrary.Chron.ThreadOrResData.ThreadData td = iectv.ThreadData;
            td.CanScrollHeight = iectv.DocumentScrollHeight;
            td.NowScrollHeight = iectv.Document.Body.ScrollTop;
            VIPBrowserLibrary.Chron.ThreadOrResData.ThreadDataWriterAndReader.Write(td, td.ThisFilePath + ".xml");
            this.threadViewTabControl.SelectedTab.Controls[0].Dispose();
            this.threadViewTabControl.SelectedTab.Dispose();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            try
            {
                this.AddressTextBox.Text = this.threadListViewTabControl.SelectedTab.Name;
            }
            catch (NullReferenceException) { return; }
            UrlEnter_Click(null, null);
        }

        private void threadWriteToolStripButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                this.threadWriteContextMenuStrip.Show(MousePosition);
            else if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                int height = this.splitContainer3.Height;
                int nowHeight = this.splitContainer3.SplitterDistance;
                if (!IsOpenWriteBox)
                {
                    this.splitContainer3.SplitterDistance = (int)(height * 0.3);
                    IsOpenWriteBox = true;
                }
                else
                {
                    this.splitContainer3.SplitterDistance = height;
                    IsOpenWriteBox = false;
                }
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            try
            {
                string name = this.threadViewTabControl.SelectedTab.Name;
                BBSType bt;
                VIPBrowserLibrary.Common.Type t;
                TypeJudgment.AllJudg(name, out bt, out t);
                AddressTextBox.Text = URLParse.DatToReadcgi(name, bt);
                ch2Browser.IEComponentThreadViewer iectv = this.threadViewTabControl.SelectedTab.Controls[0] as ch2Browser.IEComponentThreadViewer;


                VIPBrowserLibrary.Chron.ThreadOrResData.ThreadData td = iectv.ThreadData;
                td.CanScrollHeight = iectv.DocumentScrollHeight;
                td.NowScrollHeight = iectv.Document.Body.ScrollTop;
                VIPBrowserLibrary.Chron.ThreadOrResData.ThreadDataWriterAndReader.Write(td, td.ThisFilePath + ".xml");
            }
            catch (NullReferenceException) { return; }
            this.UrlEnter_Click(null, null);
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

            }
        }

        private void NormalWriteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.dockWritePanel1.Visible = true;
        }

        private void threadWriteNewWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ch2Browser.ThreadWriteDialog twd = new ch2Browser.ThreadWriteDialog();
            if (this.threadViewTabControl.SelectedTab != null)
            {
                twd.Url = this.threadViewTabControl.SelectedTab.Name;
                ch2Browser.IEComponentThreadViewer iectv = (ch2Browser.IEComponentThreadViewer)this.threadViewTabControl.SelectedTab.Controls[0];
                twd.NowResCount = iectv.Reses.Length;
                twd.ThreadData = iectv.ThreadData;
            }
            else
                return;
            twd.Show(this);
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {

            //VIPBrowserLibrary.Other.MicrosoftTranslateService mts = new VIPBrowserLibrary.Other.MicrosoftTranslateService();
            //mts.GetAccessToken();
            string text = this.threadviewSearchTextBox.Text;
            if (this.threadViewTabControl.SelectedTab == null)
                return;
            var iectv = this.threadViewTabControl.SelectedTab.Controls[0] as ch2Browser.IEComponentThreadViewer;
            if (iectv == null)
                return;
            var doc = iectv.Document;
            mshtml.IHTMLDocument2 doc2 = doc.DomDocument as mshtml.IHTMLDocument2;
            mshtml.IHTMLTxtRange textRange = ((mshtml.IHTMLBodyElement)doc2.body).createTextRange();
            bool isFound;

            textRange = doc2.selection.createRange() as mshtml.IHTMLTxtRange;
            if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift) //Shift押してたら上方向に検索
            {
                textRange.moveStart("textedit", -1);
                if (textRange.text != null) textRange.moveEnd("character", -1);
                else textRange.moveEnd("textedit", 1);
                isFound = textRange.findText(text, -1, 2);
            }
            else //Shift押してなかったら下方向に検索
            {
                if (textRange.text != null) textRange.moveStart("character", 1);
                textRange.moveEnd("textedit", 1);
                isFound = textRange.findText(text, 1, 2);
            }
            if (isFound)
            {
                textRange.scrollIntoView(true);//見つかったらスクロール
                iectv.Document.Window.MoveTo(new Point(0, 50));
                textRange.select();//そして範囲選択
            }
            else
            {
                textRange.moveStart("textedit", -1);
                if (textRange.text != null) textRange.moveEnd("character", -1);
                else textRange.moveEnd("textedit", 1);
                isFound = textRange.findText(text, -1, 2);
            }

        }

        private void DeleteUserBoardToolStripMenuItem_Clicked(object sender, EventArgs e)
        {
            TreeNode tn = this.treeView1.SelectedNode;
            if (tn == null)
                return;
            if (tn.FullPath.Contains("標準板\\"))
            {
                MessageBox.Show("標準板は削除できません");
                return;
            }
            string url = tn.Name;
            if (String.IsNullOrEmpty(url))
                return;
            string name = tn.Text;
            string data = VIPBrowserLibrary.Utility.TextUtility.Read(gs.CurrentDirectory + "\\userboard.bor");
            string deletedata = name + "\t" + url + "\n";
            data = data.Replace(deletedata, "");
            VIPBrowserLibrary.Utility.TextUtility.Write(gs.CurrentDirectory + "\\userboard.bor", data, false);
            this.ch2BrowserControl_Load(null, null);
        }

        private void AllCloseThreadListViewTabToolStripMenuItem_Clicked(object sender, EventArgs e)
        {
            this.ThreadListCount = 0;
            this.openListViewCount = 0;
            List<ListViewItem> lviList = new List<ListViewItem>();
            foreach (ListViewItem item in this.openListView.Items)
            {
                string type = (((string[])item.Tag)[0]);
                if (type == "Thread")
                {
                    lviList.Add(item);
                }
            }
            this.openListView.Items.Clear();
            this.openListView.Items.AddRange(lviList.ToArray());
            this.threadListViewTabControl.Enabled = false;
            this.threadListViewTabControl.TabPages.Clear();


        }
        #region デザイン
        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (IsThread == false)
            {
                splitContainer2.SplitterDistance = 0;
                IsThread = true;
            }
            else if (IsThread == true)
            {
                splitContainer2.SplitterDistance = splitContainer2.DisplayRectangle.Bottom;
                IsThread = false;
            }
            else
            {
                return;
            }
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {

            if (IsThread != null)
            {
                IsThread = null;
                splitContainer2.SplitterDistance = this.Heights;
                toolStripButton5.Enabled = false;
                toolStripButton6.Text = "入れ替えを有効化";
                return;
            }
            else
            {
                toolStripButton5.Enabled = true;
                toolStripButton6.Text = "入れ替えを無効化";
                this.Heights = splitContainer2.SplitterDistance;
            }
            IsThread = true;
            toolStripButton5_Click(null, null);
        }



        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            if (IsThread == null)
            {
                if (this.splitContainer2.Orientation == Orientation.Vertical)
                {
                    this.Heighteses = this.splitContainer2.SplitterDistance;
                    this.splitContainer2.Orientation = Orientation.Horizontal;
                    if (this.Heightes == 0)
                    {
                        int distance = this.splitContainer2.SplitterDistance;
                        this.splitContainer2.SplitterDistance = distance / 2;
                    }
                    else
                        this.splitContainer2.SplitterDistance = this.Heightes;
                }
                else
                {
                    this.Heightes = this.splitContainer2.SplitterDistance;
                    this.splitContainer2.Orientation = Orientation.Vertical;
                    if (Heighteses == 0)
                    {
                        int distance = this.splitContainer2.SplitterDistance;
                        this.splitContainer2.SplitterDistance = distance * 2;
                    }
                    else
                        this.splitContainer2.SplitterDistance = this.Heighteses;
                }
            }
            else
            {
                return;
            }
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            if (isHideBoardList)
            {
                this.splitContainer1.SplitterDistance = this.BoardListWidth;
                this.toolStripButton8.Text = "板一覧を隠す";
                isHideBoardList = false;
            }
            else
            {
                this.BoardListWidth = this.splitContainer1.SplitterDistance;
                this.splitContainer1.SplitterDistance = 0;
                this.toolStripButton8.Text = "板一覧を表示";
                isHideBoardList = true;
            }
        }
        #endregion
        private void tabControl2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                for (int i = 0; i < this.threadViewTabControl.TabCount; i++)
                {
                    if (this.threadViewTabControl.GetTabRect(i).Contains(e.X, e.Y))
                    {
                        this.threadViewTabControl.SelectedIndex = i;
                        return;
                    }
                }
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Middle)
            {
                for (int i = 0; i < this.threadViewTabControl.TabCount; i++)
                {
                    if (this.threadViewTabControl.GetTabRect(i).Contains(e.X, e.Y))
                    {
                        this.ThreadTabCount--;
                        this.openListViewCount--;
                        this.openListView.Items.RemoveByKey(this.threadViewTabControl.TabPages[i].Name);

                        ch2Browser.IEComponentThreadViewer iectv = this.threadViewTabControl.TabPages[i].Controls[0] as ch2Browser.IEComponentThreadViewer;
                        VIPBrowserLibrary.Chron.ThreadOrResData.ThreadData td = iectv.ThreadData;
                        td.CanScrollHeight = iectv.DocumentScrollHeight;
                        td.NowScrollHeight = iectv.Document.Body.ScrollTop;
                        VIPBrowserLibrary.Chron.ThreadOrResData.ThreadDataWriterAndReader.Write(td, td.ThisFilePath + ".xml");
                        this.threadViewTabControl.TabPages[i].Controls[0].Dispose();

                        this.threadViewTabControl.TabPages.RemoveAt(i);

                        return;
                    }
                }
            }
        }
        private void tabControl1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                for (int i = 0; i < this.threadListViewTabControl.TabCount; i++)
                {
                    if (this.threadListViewTabControl.GetTabRect(i).Contains(e.X, e.Y))
                    {
                        this.threadListViewTabControl.SelectedIndex = i;
                        return;
                    }
                }
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Middle)
            {
                for (int i = 0; i < this.threadListViewTabControl.TabCount; i++)
                {
                    if (this.threadListViewTabControl.GetTabRect(i).Contains(e.X, e.Y))
                    {
                        this.ThreadListCount--;
                        this.openListViewCount--;
                        //this.listView1.Items.RemoveByKey(this.threadListViewTabControl.TabPages[i].Name);
                        this.openListView.Items.RemoveByKey(this.threadListViewTabControl.TabPages[i].Name);
                        this.threadListViewTabControl.TabPages.RemoveAt(i);
                        return;
                    }
                }
            }
        }

        private void threadListViewTabControl_ControlRemoved(object sender, ControlEventArgs e)
        {
            if (threadListViewTabControl.SelectedTab.Name.IndexOf("http") == 0 && threadListViewTabControl.SelectedTab.Text != "既得ログ一覧")
            {
                if (threadListViewTabControl.SelectedTab.Name.IndexOf("全板横断検索") == -1)
                {
                    VIPBrowserLibrary.Chron.ThreadColumn tc = new VIPBrowserLibrary.Chron.ThreadColumn();
                    tc.WriteColumnData((this.threadListViewTabControl.SelectedTab.Controls[0] as ListView), gs.NotNecessarySettingDataPath + "\\column.dat");
                }
            }
            if (this.threadListViewTabControl.Enabled)
            {
                int count = this.threadListViewTabControl.SelectedIndex;
                var v = this.threadListViewTabControl.SelectedTab;
                ListViewItem lvi = new ListViewItem(new string[] { "スレッドリスト", v.Text }, v.Name);
                this.listView1.AddItem(lvi);
                if (count <= 0)
                    return;
                this.threadListViewTabControl.SelectTab(count);
            }
            else
            {
                this.threadListViewTabControl.Enabled = true;
                foreach (TabPage v in this.threadListViewTabControl.TabPages)
                {
                    ListViewItem lvi = new ListViewItem(new string[] { "スレッドリスト", v.Text }, v.Name);
                    this.listView1.AddItem(lvi);

                }

            } 
        }

        private void threadViewTabControl_ControlRemoved(object sender, ControlEventArgs e)
        {
            if (this.threadViewTabControl.Enabled)
            {
                int count = this.threadViewTabControl.SelectedIndex;
                var v = this.threadViewTabControl.SelectedTab;
                var lvi = new ListViewItem(new string[] { "スレッド", v.Text }, v.Name);
                this.listView1.AddItem(lvi);
                if (count <= 0)
                    return;
                this.threadViewTabControl.SelectTab(count);
            }
            else
            {
                this.threadViewTabControl.Enabled = true;
                foreach (TabPage v in this.threadViewTabControl.TabPages)
                {
                    var lvi = new ListViewItem(new string[] { "スレッド", v.Text }, v.Name);
                    this.listView1.AddItem(lvi);
                }
            }
        }
        void TabControl_MouseWheel(object sender, MouseEventArgs e)
        {
            return;//バグが発生する可能性があるためコメントアウト
            //TabControl tc = sender as TabControl;
            //int count = tc.SelectedIndex;

            //if (Math.Sign(e.Delta) == -1)
            //{
            //    if (count == 0)
            //    {
            //        tc.SelectedIndex = tc.TabCount -1;
            //    }
            //    else
            //    {
            //        tc.SelectedIndex = count - 1;
            //    }
            //}
            //else
            //{
            //    if (count + 1 == tc.TabCount)
            //    {
            //        tc.SelectedIndex = 0;
            //    }
            //    else
            //    {
            //        tc.SelectedIndex = count + 1;
            //    }
            //}
        }



        private void printThreadButton_Click(object sender, EventArgs e)
        {
            if (this.threadViewTabControl.TabPages == null || this.threadViewTabControl.Controls != null)
                return;
            WebBrowser wb = this.threadViewTabControl.SelectedTab.Controls[0] as WebBrowser;
            wb.ShowPrintPreviewDialog();
        }

        private void UrlEnter_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != System.Windows.Forms.MouseButtons.Right)
                return;
            urlEnterContextMenuStrip.Show(MousePosition);
        }

        private async void allBoardSearchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string searchString = AddressTextBox.Text;
            TabPage tp = new TabPage("全板横断検索 : " + searchString);
            VIPBrowserLibrary.BBS.X2ch.AllBoardSercher abs = new VIPBrowserLibrary.BBS.X2ch.AllBoardSercher(searchString);

            ListView lv = new ListView();
            lv.Parent = this;
            ColumnHeader count = new ColumnHeader();
            count.Text = "#";
            count.Width = 25;
            ColumnHeader name = new ColumnHeader();
            name.Text = "スレタイ";
            name.Width = 350;
            ColumnHeader res = new ColumnHeader();
            res.Text = "レス数";
            res.Width = 50;
            ColumnHeader date = new ColumnHeader();
            date.Text = "日時";
            date.Width = 200;
            ColumnHeader speed = new ColumnHeader();
            speed.Text = "勢い";
            speed.Width = 50;
            lv.ColumnClick += (_sender, _e) =>
            {
                lv.ListViewItemSorter = new VIPBrowserLibrary.Common.ThreadListSorter(_e.Column, isAssecing);
                isAssecing = !isAssecing;
            };
            lv.MultiSelect = false;
            lv.FullRowSelect = true;
            lv.Columns.AddRange(new ColumnHeader[] { count, name, res, date, speed });
            lv.Dock = DockStyle.Fill;
            lv.View = View.Details;
            lv.MouseEnter += ControlMouseEnter;
            foreach (ListViewItem m in await abs.SearchThread())
            {
                lv.Items.Add(m);
            }
            tp.Controls.Add(lv);
            lv.Click += lv_Click;
            tp.Name = "全板横断検索 : " + searchString;
            threadListViewTabControl.Controls.Add(tp);
            threadListViewTabControl.SelectedIndex = ThreadListCount;

            ListViewItem lvis = new ListViewItem(new string[] { "スレッドリスト", "全板横断検索 : " + searchString });
            //lvis.Text = "全板横断検索 : " + searchString;
            lvis.Name = "全板横断検索 : " + searchString;
            lvis.Tag = new string[] { "ThreadList", "全板横断検索 : " + searchString };
            //listView1.Items.Add(lvis);
            //listView1.Items[openListViewCount + openThreadViewCount].Group = listView1.Groups["threadListgroup"];
            openListViewCount++;
            ThreadListCount++;

            //"",(i + 1).ToString(),m.Groups["threadName"].Value,m.Groups["resCount"].Value ,standtime.ToString(),speed.ToString()}
        }

        private void changeColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ch2Browser.ThreadListStyleChange tcsc = new ch2Browser.ThreadListStyleChange();
            tcsc.ShowDialog();
            //tcsc.Dispose();
        }

        private void threadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ch2Browser.NGFunctionDialog ngfd = new ch2Browser.NGFunctionDialog();
            ch2Browser.HighNGFunctionDialog nngfd = new ch2Browser.HighNGFunctionDialog();
            nngfd.ShowDialog();
            //ngfd.ShowDialog(this);
        }

        private void autoReloadButton_Click(object sender, EventArgs e)
        {
            if (this.threadViewTabControl.TabCount == 0)
                return;
            bool check = autoReloadButton.Checked;
            autoReloadButton.Checked = !check;
            ch2Browser.IEComponentThreadViewer iectv = this.threadViewTabControl.SelectedTab.Controls[0] as ch2Browser.IEComponentThreadViewer;
            if (iectv == null)
                return;
            iectv.IsRefresh = !check;
        }

        private void threadListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ch2Browser.ThreadListNGFunctionDialog tlngfd = new ch2Browser.ThreadListNGFunctionDialog();
            tlngfd.ShowDialog();
        }


        #region プラグインインターフェースラップ
        public string AddressTextBoxText
        {
            get { return this.AddressTextBox.Text; }
            set { this.AddressTextBox.Text = value; }
        }

        public string LogText
        {
            set { Log.Logger.WriteLog(value); }
        }


        public void AddThreadListViewToolStripItem(ToolStripItem tsi)
        {
            this.threadListViewToolStrip.Items.Add(tsi);
            return;
        }

        public void AddThreadViewToolStripItem(ToolStripItem tsi)
        {
            this.threadViewToolStrip.Items.Add(tsi);
        }

        #endregion







        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ch2Browser.AllLoginDialog ald = new ch2Browser.AllLoginDialog();
            ald.SettingData = this.SettingData;
            ald.ShowDialog();
            this.SettingData = ald.SettingData;
        }

        public void ControlMouseEnter(object sender, EventArgs e)
        {
            if (sender == null)
                return;
            Control c = sender as Control;
            if (c != null)
                c.Focus();
            else
            {
                HtmlDocument hd = sender as HtmlDocument;
                hd.Focus();
            }
        }

        private void changeNetworkStateButton_Click(object sender, EventArgs e)
        {
            bool state = this.IsOnlineState;
            if (state)
            {
                this.changeNetworkStateButton.Text = "オンラインモード";
            }
            else
            {
                this.changeNetworkStateButton.Text = "オフラインモード";
            }
            this.IsOnlineState = !state;
        }

        private async void readLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VIPBrowserLibrary.BBS.Common.ReadDotNetFile rdnf = new VIPBrowserLibrary.BBS.Common.ReadDotNetFile();
            var data = await rdnf.GetDotNetFiles();
            ListView lv = new ListView();
            lv.Parent = this;
            lv.MouseEnter += ControlMouseEnter;
            lv.Items.AddRange(data);
            TabPage tp = new TabPage("既得ログ一覧");
            lv.Dock = DockStyle.Fill;
            lv.View = View.Details;
            ColumnHeader ch = new ColumnHeader();
            ch.Text = "#";
            ch.Width = 20;
            ColumnHeader chh = new ColumnHeader();
            chh.Text = "スレタイ";
            chh.Width = 200;
            ColumnHeader chhh = new ColumnHeader();
            chhh.Text = "スレ立て";
            lv.Columns.AddRange(new ColumnHeader[] { ch, chh, chhh });
            lv.FullRowSelect = true;
            lv.MultiSelect = false;
            lv.ColumnClick += (s, ee) =>
            {
                lv.ListViewItemSorter = new ThreadListSorter(ee.Column, this.isAssecing);
            };
            lv.SelectedIndexChanged += (_s, _e) =>
            {
                ListView lvv = _s as ListView;
                if (lvv.SelectedItems.Count < 0 || lvv.SelectedItems.Count == 0)
                    return;
                this.AddressTextBoxText = lvv.SelectedItems[0].Name;
                this.UrlEnter_Click(null, null);
            };
            tp.Controls.Add(lv);
            tp.Name = "既得ログ一覧";
            this.threadListViewTabControl.TabPages.Add(tp);
            this.openListViewCount++;
            this.ThreadListCount++;
            ListViewItem lvi = new ListViewItem(new string[] { "スレッドリスト", "既得ログ一覧" });
            lvi.Name = "既得ログ一覧";
            lvi.Tag = new string[] { "ThreadList", "既得ログ一覧" };
            this.openListView.Items.Add(lvi);
            this.threadListViewTabControl.SelectedIndex = ThreadListCount;
        }

        private void AddressTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.UrlEnter_Click(null, null);
            return;
        }

        private void openListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (openListView.SelectedItems.Count > 0)
            {
                if (((string[])(openListView.SelectedItems[0].Tag))[0] == "ThreadList")
                {
                    threadListViewTabControl.SelectTab(((string[])(openListView.SelectedItems[0].Tag))[1]);
                }
                else
                {
                    threadViewTabControl.SelectTab(((string[])(openListView.SelectedItems[0].Tag))[1]);
                }
            }
        }

        private void allThreadCloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.openThreadViewCount = 0;
            this.ThreadTabCount = 0;
            List<ListViewItem> lvis = new List<ListViewItem>();
            foreach (ListViewItem item in this.openListView.Items)
                if ((((string[])item.Tag)[0]) == "ThreadList")
                    lvis.Add(item);
            this.openListView.Items.Clear();
            this.openListView.Items.AddRange(lvis.ToArray());
            this.threadViewTabControl.Enabled = false;
            this.threadViewTabControl.TabPages.Clear();

        }

        private void openListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            this.openListView.ListViewItemSorter = new ThreadListSorter(e.Column, isAssecing);
            this.isAssecing = !this.isAssecing;
        }

        private async void readLocalRuleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.treeView1.SelectedNode == null)
                return;
            if (String.IsNullOrEmpty(this.treeView1.SelectedNode.Name))
            {
                return;
            }
            string data = await GetBoardData.GetLocalRule(this.treeView1.SelectedNode.Name);
            Form f = new Form();
            WebBrowser wb = new WebBrowser();
            wb.Navigate("about:blank");
            wb.Document.Write(data);
            wb.Dock = DockStyle.Fill;
            f.Size = new System.Drawing.Size(600, 400);
            f.Controls.Add(wb);
            f.Show();
        }

        private void treeView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
                this.treeView1.SelectedNode = this.treeView1.GetNodeAt(e.X, e.Y);
        }

        private async void readSettingtxtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.treeView1.SelectedNode == null)
                return;
            if (String.IsNullOrEmpty(this.treeView1.SelectedNode.Name))
            {
                return;
            }
            var data = await GetBoardData.GetBoardDictionary(this.treeView1.SelectedNode.Name, true);
            StringBuilder sb = new StringBuilder();
            foreach (var item in data)
            {
                sb.Append(item.Key).Append("=").Append(item.Value).AppendLine("<br/>");
            }
            WebBrowser web = new WebBrowser();
            web.Navigate("about:blank");
            web.Document.Write(sb.ToString());
            web.Dock = DockStyle.Fill;
            Form f = new Form();
            f.Size = new System.Drawing.Size(600, 400);
            f.Controls.Add(web);
            f.Show();
        }

        private void listView1_ControlAdded(object sender, ControlEventArgs e)
        {

        }

        private void manegementUploaderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ch2Browser.UploadManagementDialog umd = new ch2Browser.UploadManagementDialog();
            umd.Show(this);
        }

        public void SaveThreadListViewData()
        {
            VIPBrowserLibrary.Setting.NotNecessarySettingData.ThreadListViewData stlvd = new VIPBrowserLibrary.Setting.NotNecessarySettingData.ThreadListViewData(this.threadListViewTabControl.TabPages);
            stlvd.WriteListViewData();
        }

        public async void LoadThreadListViewData()
        {

            VIPBrowserLibrary.Setting.NotNecessarySettingData.ThreadListViewData stlvd = new VIPBrowserLibrary.Setting.NotNecessarySettingData.ThreadListViewData();
			IDictionary<VIPBrowserLibrary.Setting.NotNecessarySettingData.ThreadListViewData.SaveThreadListData, ListViewItem[]> data = null;
	
				data = await stlvd.ReadListViewData();
	
            foreach (var item in data)
            {
                int SameArrayNumber = 0;
                bool ifSameNameTab = false;
                string doubleNames = null;
                string boardUrl = item.Key.Url;
                string boardName = item.Key.Name;
                //boardName = (await GetBoardData.GetBoardDictionary(boardUrl, false))["BBS_TITLE"];
                foreach (TabPage items in threadListViewTabControl.TabPages)
                {
                    if (items.Name == boardUrl)
                    {
                        ifSameNameTab = true;
                        doubleNames = items.Name;
                        break;
                    }
                    SameArrayNumber++;
                }

                #region デザイナー構成要素
                TabPage tp = new TabPage(boardName);
                ListView lv = new ListView();
                //lv.BackgroundImage = Properties.Resources.お遊び;
                //lv.BackgroundImageTiled = true;
                lv.MouseEnter += ControlMouseEnter;
                lv.Parent = this;
                ColumnHeader countCh = new ColumnHeader();
                countCh.Text = "#";
                countCh.Width = 25;

                ColumnHeader threadName = new ColumnHeader();
                threadName.Text = "スレッドタイトル";
                threadName.Width = 350;

                ColumnHeader resCount = new ColumnHeader();
                resCount.Text = "スレ立て";
                resCount.Width = 50;

                ColumnHeader dateCount = new ColumnHeader();
                dateCount.Text = "レス数";
                resCount.Width = 200;

                ColumnHeader speed = new ColumnHeader();
                speed.Text = "勢い";
                speed.Width = 100;

                ColumnHeader datSize = new ColumnHeader();
                datSize.Text = "サイズ";
                datSize.Width = 50;

                ColumnHeader lastRes = new ColumnHeader();
                lastRes.Text = "既読";
                lastRes.Width = 50;

                ColumnHeader newRes = new ColumnHeader();
                newRes.Text = "新着";
                newRes.Width = 50;

                ColumnHeader isRead = new ColumnHeader();
                isRead.Text = "!";
                isRead.Width = 25;

                ColumnHeader[] ch = { isRead, countCh, threadName, dateCount, resCount, speed, datSize, lastRes, newRes };
                lv.AllowColumnReorder = true;
                lv.MultiSelect = false;
                lv.FullRowSelect = true;
                lv.Columns.AddRange(ch);
                lv.ColumnClick += (_sender, _e) =>
                {
                    lv.ListViewItemSorter = new VIPBrowserLibrary.Common.ThreadListSorter(_e.Column, isAssecing);
                    isAssecing = !isAssecing;
                };
                lv.Font = global::VIPBrowser.Properties.Settings.Default.UseFontfamily;
                lv.Click += lv_Click;
                lv.View = View.Details;
                lv.Dock = DockStyle.Fill;
                foreach (var m in item.Value)
                {
                    if (m != null)
                        lv.Items.Add(m);
                }
                //lv.Items.AddRange(item.Value);
                #endregion
                if (ifSameNameTab)
                {
                    threadListViewTabControl.SelectedIndex = SameArrayNumber;
                    threadListViewTabControl.TabPages[SameArrayNumber].Controls.Clear();
                    threadListViewTabControl.TabPages[boardUrl].Controls.Add(lv);
                }
                else
                {
                    tp.Controls.Add(lv);
                    tp.Name = boardUrl;
                    threadListViewTabControl.Controls.Add(tp);
                    threadListViewTabControl.SelectedIndex = ThreadListCount;

                    ListViewItem lvis = new ListViewItem(new string[] { "スレッドリスト", boardName });
                    //lvis.Text = boardName;
                    lvis.Name = boardUrl;
                    lvis.Tag = new string[] { "ThreadList", boardUrl };
                    //listView1.Items.Add(lvis);
                    //listView1.Items[openListViewCount + openThreadViewCount].Group = listView1.Groups["threadListgroup"];
                    openListView.Items.Add(lvis);
                    openListViewCount++;
                    ThreadListCount++;
                }
            }
        }


        private void closeNoSelectedTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        public void SaveThreadViewData()
        {
            VIPBrowserLibrary.Setting.NotNecessarySettingData.ThreadViewData stlvd = new VIPBrowserLibrary.Setting.NotNecessarySettingData.ThreadViewData(this.threadViewTabControl.TabPages);
            stlvd.WriteThreadData();

        }

        public void LoadThreadViewData()
        {
            VIPBrowserLibrary.Setting.NotNecessarySettingData.ThreadViewData stvd = new VIPBrowserLibrary.Setting.NotNecessarySettingData.ThreadViewData();
            var dat = stvd.ReadThreadData();
            foreach (var item in dat)
            {

                string url = item.Value.ThreadAddress;
                BBSType bt;
                VIPBrowserLibrary.Common.Type t;
                TypeJudgment.AllJudg(url, out bt, out t);

                bool ifSameNameTabs = false;
                int SameArrayNumbers = 0;
                string doubleName = null;
                foreach (TabPage items in threadViewTabControl.TabPages)
                {

                    if (items.Name == url)
                    {
                        ifSameNameTabs = true;
                        doubleName = items.Name;
                        break;
                    }
                    SameArrayNumbers++;

                }


                ch2Browser.IEComponentThreadViewer web = new ch2Browser.IEComponentThreadViewer();
                web.Document.MouseMove += Document_MouseMove;
                web.DatUrl = url;
                web.Parent = this;
                web.SettingData = this.SettingData;
                // WebBrowser web = new WebBrowser();
                web.MainForm = this;
                web.ScriptErrorsSuppressed = true;
                web.Dock = DockStyle.Fill;
                ListViewItem lvi = null;
                //string datData = await new VIPBrowserLibrary.BBS.X2ch.X2chSimpleThreadReader(url).GetResponseMethod();
                TabPage ta = null;
                int scrollPalce = 0;
                string datData = web.OfflineSetUrl(url);
                lvi = new ListViewItem(new string[] { "スレッド", web.ThreadName });
                ta = new TabPage(web.ThreadName);
                ta.Name = url; try
                {
                    scrollPalce = web.ThreadData.NowScrollHeight;
                }
                catch { }
                finally { }

                if (ifSameNameTabs)
                {
                    web.DocumentCompleted += (_sender, _e) =>
                    {
                        web.Document.Body.ScrollTop = scrollPalce;
                        if (web.Document.Body.ScrollTop == 49 && (scrollPalce - web.Document.Body.ScrollTop) <= 235)
                        {
                            web.Document.Body.ScrollIntoView(false);
                        }
                    };
                    //Console.WriteLine("Start DocumentText");
                    web.Write(datData);
                    // web.DocumentText = datData;
                    //Console.WriteLine("End DocumentText");
                    //将来的にバグの原因がわかったら削除したい
                    //System.Threading.Thread.Sleep(850);
                    web.Document.Body.ScrollTop = scrollPalce * 2;

                    threadViewTabControl.SelectedIndex = SameArrayNumbers;
                    threadViewTabControl.TabPages[SameArrayNumbers].Controls.Clear();
                    threadViewTabControl.TabPages[url].Controls.Add(web);
                }
                else
                {


                    ta.Controls.Add(web);
                    //ta.t = contextMenuStrip2;
                    threadViewTabControl.Controls.Add(ta);
                    threadViewTabControl.SelectedIndex = ThreadTabCount;

                    lvi.Tag = new string[] { "Thread", url };
                    lvi.Name = url;
                    //listView1.Items.Add(lvi);
                    //listView1.Items[(openThreadViewCount + openListViewCount)].Group = listView1.Groups["threadGroup"];
                    openListView.Items.Add(lvi);

                    web.DocumentCompleted += (_sender, _e) =>
                    {
                        web.Document.Body.ScrollTop = scrollPalce;
                        if (web.Document.Body.ScrollTop == 49 && (scrollPalce - web.Document.Body.ScrollTop) <= 235)
                        {
                            web.Document.Body.ScrollIntoView(false);
                        }
                    };

                    //web.DocumentText = datData;
                    web.Write(datData);
                    //将来的にバグの原因がわかったら削除したい
                    //System.Threading.Thread.Sleep(850);
                    //await VIPBrowserLibrary.Utility.TaskUtility.ThreadStop(1000);
                    web.Document.Body.ScrollTop = scrollPalce;
                    if (web.Document.Body.ScrollTop == 49 && (scrollPalce - web.Document.Body.ScrollTop) <= 235)
                    {
                        web.Document.Body.ScrollIntoView(false);
                    }

                    openThreadViewCount++;
                    ThreadTabCount++;
                    ifSameNameTabs = false;
                }
            }


        }

        private void AddFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode tn = new TreeNode();
            VIPBrowserLibrary.Other.MyExtensions.GUIExtension.SingleInputTextBoxForm sitb = new VIPBrowserLibrary.Other.MyExtensions.GUIExtension.SingleInputTextBoxForm();
            sitb.Text = "フォルダ名";
            sitb.ShowLabelText = "フォルダ名";
            tn.ImageKey = "Folder";
            if (favoriteTreeView.SelectedNode != null)
            {
                string key = favoriteTreeView.SelectedNode.ImageKey;
                if (key != "Thread" || key != "ThreadList")
                {
                    if (sitb.ShowDialog() == DialogResult.OK)
                    {
                        tn.Text = sitb.TextBoxText;
                        favoriteTreeView.BeginUpdate();
                        favoriteTreeView.SelectedNode.Nodes.Add(tn);
                        favoriteTreeView.EndUpdate();
                        favoriteTreeView.Update();
                    }
                }
                else
                {
                    MessageBox.Show("スレッドやスレ一覧のお気に入りにフォルダを追加することはできません");
                    return;
                }
            }
            else
            {
                if (sitb.ShowDialog() == DialogResult.OK)
                {
                    tn.Text = sitb.TextBoxText;
                    favoriteTreeView.BeginUpdate();
                    favoriteTreeView.Nodes.Add(tn);
                    favoriteTreeView.EndUpdate();
                    favoriteTreeView.Update();
                }
            }
        }
        public void SaveFavoriteListTreeView()
        {
            var fs = System.IO.File.Open(gs.OtherFolderPath + "\\favorite.dat", System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite);
            VIPBrowserLibrary.Chron.Serializer.Serialize<TreeNode[]>(this.favoriteTreeView.Nodes.OfType<TreeNode>().ToArray(), fs);
            fs.Dispose();
        }

        public void LoadFavoriteListTreeView()
        {
            var fs = System.IO.File.OpenRead(gs.OtherFolderPath + "\\favorite.dat");
            if (fs == System.IO.Stream.Null || fs == null)
                return;
            try
            {
                this.favoriteTreeView.Nodes.AddRange(VIPBrowserLibrary.Chron.Serializer.Deserialize<TreeNode[]>(fs));
            }
            catch
            {
                return;
            }
        }

        public async Task MakedNewThread(string threadName,string boardurl)
        {
            bool flag = false;
            this.AddressTextBox.Text = boardurl;
            this.UrlEnter_Click(null, EventArgs.Empty);
            await VIPBrowserLibrary.Utility.TaskUtility.ThreadStop(1500);
            foreach (TabPage item in this.threadListViewTabControl.TabPages)
            {
                if (item.Name == boardurl)
                {
                    ListView lv = item.Controls[0] as ListView;
                    foreach (ListViewItem lvi in lv.Items)
                    {
                        if (lvi.SubItems["Name"].Text == threadName)
                        {
                            this.AddressTextBox.Text = URLParse.DatToReadcgi(lvi.ImageKey, TypeJudgment.BBSTypeJudg(lvi.ImageKey));
                            this.UrlEnter_Click(null, EventArgs.Empty);
                            flag = true;
                            break;
                        }
                    }
                    break;
                }
            }
            if (!flag)
            {
                MessageBox.Show(threadName　+　"のスレッドは見つかりませんでした");
            }
        }

        private void favoriteTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var data = this.favoriteTreeView.SelectedNode;
            string key = data.ImageKey;
            if (key == "Thread" || key == "ThreadList")
            {
                string url = data.Name;
                var jud = TypeJudgment.TypeJudg(url);
                var bt = TypeJudgment.BBSTypeJudg(url);
                switch (jud)
                {
                    case VIPBrowserLibrary.Common.Type.thread:
                        this.AddressTextBox.Text = URLParse.DatToReadcgi(url, bt);
                        break;
                    case VIPBrowserLibrary.Common.Type.threadlist:
                        this.AddressTextBox.Text = url;
                        break;
                    default:
                        throw new ArgumentException();
                }
                this.UrlEnter_Click(null, EventArgs.Empty);
            }
        }

        private void deleteFavoriteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var node = this.favoriteTreeView.SelectedNode;
            if (node == null)
                return;
            if (node.ImageKey == "Folder")
                if (MessageBox.Show("フォルダー内のすべてのお気に入りが削除されますがよろしいですか？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
                    return;
            var n = node.Parent;
            if (n == null)
                this.favoriteTreeView.Nodes.Remove(node);
            else
                n.Nodes.Remove(node);
        }

		private void makeNewThreadButton_Click(object sender, EventArgs e)
		{
			try
			{
				string board = this.threadListViewTabControl.SelectedTab.Name;
				ch2Browser.NewThreadWindow ntw = new ch2Browser.NewThreadWindow();
				ntw.OwnerControlForm = this;
				ntw.BoardAddress = board;
				ntw.IsNextThread = false;
				ntw.Owner = this.ParentForm;
				ntw.BoardName = this.threadListViewTabControl.SelectedTab.Text;
				ntw.Show();
			}
			catch (NullReferenceException)
			{
				return;
			}
		}
    }
}

