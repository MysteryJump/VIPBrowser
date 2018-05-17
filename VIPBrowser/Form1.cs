using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using System.Diagnostics;

namespace VIPBrowser
{
    
    
    public partial class Form1 : Form,VIPBrowserPlugin.IPluginHost
    {
        public VIPBrowserLibrary.Setting.SettingSerial SettingData { get; set; }
        private VIPBrowserLibrary.Setting.GeneralSetting gs = new VIPBrowserLibrary.Setting.GeneralSetting();
        public VIPBrowser.PluginInfo[] Plugins { get; set; }

        public VIPBrowserPlugin.IPlugin[] Plugin { get; set; }

        public Form1()
        {
            InitializeComponent();
            this.startPageWebPage.NewWindow += startPageWebPage_NewWindow;
			ToolStripMenuItem tsmi = new ToolStripMenuItem("ライセンス情報");
			this.otherToolStripDropDownButton.DropDownItems.Add(tsmi);
			

			tsmi.Click += (sender, e) =>
			{
				VIPBrowser.Dialogs.ReadProductWindow rpw = new Dialogs.ReadProductWindow();
				rpw.ShowDialog();
			};
			#region loading lambda
			this.Load += (sender, e) =>
            {
				if (!this.SettingData.IsShowStartPage)
				{
					this.StartPage.Hide();
					this.MainTabControl.TabPages.Remove(this.StartPage);
				}
				ParallelOptions po = new ParallelOptions();
				this.ThreadSaveDataLoad();
				
				Parallel.Invoke(this.ch2BrowserControl1.LoadFavoriteListTreeView, this.ThreadListSaveDataLoad,this.LoadPlugin);
				//this.ch2BrowserControl1.LoadFavoriteListTreeView();
				
                Console.WriteLine(Plugins.Length);

                this.ch2BrowserControl1.SettingData = this.SettingData;
                if (this.SettingData.IsSaveFormLocation)
                {
                    this.Height = this.SettingData.FormHeight;
                    this.Width = this.SettingData.FormWidth;
                    this.StartPosition = this.SettingData.StartPosition;
                    if (this.SettingData.IsMaximized)
                        this.WindowState = FormWindowState.Maximized;
                    else
                        this.WindowState = FormWindowState.Normal;
                    
                    Point p = new Point(this.SettingData.FormX, this.SettingData.FormY);
                    Console.WriteLine(p);
                    this.Location = p;
                    
                }
                else
                {
                    this.StartPosition = FormStartPosition.CenterScreen;
                }
//                this.startPageWebPage.DocumentText = @"<a href=""http://px.a8.net/svt/ejp?a8mat=260B0D+8VPSMQ+348+68MF5"" target=""_blank"">
//<img border=""0"" width=""468"" height=""60"" alt="""" src=""http://www26.a8.net/svt/bgt?aid=131024317537&wid=001&eno=01&mid=s00000000404001048000&mc=1""></a>
//<img border=""0"" width=""1"" height=""1"" src=""http://www10.a8.net/0.gif?a8mat=260B0D+8VPSMQ+348+68MF5"" alt="""">";
                #region お遊び
                //                this.startPageWebPage.DocumentText = @"
//<!DOCTYPE html>
//<!--元からあったものにわざわざUrlを入れているのはIISのエラー防止-->
//<html>
//<head>
//    <link type=""text/css"" href=""http://uravip.tonkotsu.jp/css/menu.css"" rel=""stylesheet"" />
//    <link type=""text/css"" href=""http://uravip.tonkotsu.jp/css/main.css"" rel=""stylesheet"" />
//    <!--<link type=""text/javascript"" href=""slideshow.js"" rel=""archives"" />-->
//    <!--    <script src=""slideshow.js"" type=""text/javascript""></script>
//    <script src=""jquery.js"" type=""text/javascript""></script>-->
//    <meta charset=""utf-8"" />
//    <title>裏ニュー速VIP</title>
//</head>
//<body>
//    <header>
//        <!-- style=""text-align:center;"">-->
//        <h1 class=""Header"">裏ニュー速VIP
//        </h1>
//        <h3 class=""Header"">～規制VIPPER最後の楽園～
//        </h3>
//    </header>
//    <ul id=""dropmenu"">
//        <li>
//            <a href=""http://uravip.tonkotsu.jp/welcome.html"">トップページ</a>
//        </li>
//        <li><a>板一覧</a>
//            <ul>
//                <li><a>雑談カテゴリ</a>
//                    <ul>
//                        <li><a href=""http://uravip.tonkotsu.jp/news7vip/"">裏VIP</a></li>
//                        <li><a href=""http://uravip.tonkotsu.jp/coffeehouse/"">雑談ルノワール</a></li>
//                    </ul>
//                </li>
//                <li><a>趣味カテゴリ</a>
//                    <ul>
//                        <li><a href=""http://uravip.tonkotsu.jp/hobbies/"">ホビー全般</a></li>
//                    </ul>
//                </li>
//                <li><a>ニュースカテゴリ</a>
//                    <ul>
//                        <li><a href=""http://uravip.tonkotsu.jp/newsflash/"">ニュース超速報</a></li>
//                    </ul>
//                </li>
//                <li><a>運営カテゴリ</a>
//                    <ul>
//                        <li><a href=""http://uravip.tonkotsu.jp/operateandsaku/"">運用情報・削除依頼</a></li>
//                        <li><a href=""http://uravip.tonkotsu.jp/labo/"">実験板</a></li>
//                    </ul>
//                </li>
//            </ul>
//            <li>
//                <a href=""http://jbbs.livedoor.jp/internet/17700/"">避難所</a>
//            </li>
//        <li>
//            <a>その他機能</a>
//            <ul>
//                <li><a href=""http://uravip.tonkotsu.jp/search.php"">過去ログ検索</a></li>
//                <li><a href=""http://uravip.tonkotsu.jp/test/search.cgi"">現存スレ検索</a></li>
//            </ul>
//        </li>
//        <li>
//            <a>開発中のもの</a>
//            <ul>
//                <li><a href=""http://uravip.tonkotsu.jp/PONNAProject/"">ぽんなプロジェクト</a></li>
//            </ul>
//        </li>
//    </ul>
//    <p style=""text-align: center; margin: 0 auto;"">
//        <img src=""http://uravip.tonkotsu.jp/news7vip/1.jpg"" width=""800"" alt=""裏VIPの画像"" />
//    </p>
//    <article style=""width: 960px; margin: 0 auto;"">
//        <nav id=""columnleft"">
//            <ul id=""nav"">
//                <li>
//                    <a href=""welcome.html"">トップページ</a>
//                </li>
//                <li><a>板一覧</a>
//                    <ul>
//                        <li><a>雑談カテゴリ</a>
//                            <ul>
//                                <li><a href=""http://uravip.tonkotsu.jp/news7vip/"">裏VIP</a></li>
//                                <li><a href=""http://uravip.tonkotsu.jp/coffeehouse/"">雑談ルノワール</a></li>
//                            </ul>
//                        </li>
//                        <li><a>趣味カテゴリ</a>
//                            <ul>
//                                <li><a href=""http://uravip.tonkotsu.jp/hobbies/"">ホビー全般</a></li>
//                            </ul>
//                        </li>
//                        <li><a>ニュースカテゴリ</a>
//                            <ul>
//                                <li><a href=""http://uravip.tonkotsu.jp/newsflash/"">ニュース超速報</a></li>
//                            </ul>
//                        </li>
//                        <li><a>運営カテゴリ</a>
//                            <ul>
//                                <li><a href=""http://uravip.tonkotsu.jp/operateandsaku/"">運用情報・削除依頼</a></li>
//                                <li><a href=""http://uravip.tonkotsu.jp/labo/"">実験板</a></li>
//                            </ul>
//                        </li>
//                    </ul>
//                    <li>
//                        <a href=""http://jbbs.livedoor.jp/internet/17700/"">避難所</a>
//                    </li>
//                <li>
//                    <a>その他機能</a>
//                    <ul>
//                        <li><a href=""http://uravip.tonkotsu.jp/search.php"">過去ログ検索</a></li>
//                        <li><a href=""http://uravip.tonkotsu.jp/test/search.cgi"">現存スレ検索</a></li>
//                    </ul>
//                </li>
//                <li>
//                    <a>開発中のもの</a>
//                    <ul>
//                        <li><a href=""http://uravip.tonkotsu.jp/PONNAProject/"">ぽんなプロジェクト</a></li>
//                    </ul>
//                </li>
//            </ul>
//            <h4>過去ログ検索</h4>
//            <form action=""http://uravip.tonkotsu.jp/search.php"" method=""get"">
//                <p>検索板</p>
//                <select name=""board"">
//                    <option value=""news7vip"">裏VIP</option>
//                    <option value=""coffeehouse"">雑談ルノワール</option>
//                </select>
//                <br />
//                <input type=""text"" name=""search"" />
//                <input type=""submit"" name=""submit"" value=""検索"" />
//            </form>
//            <p><a href=""http://uravip.tonkotsu.jp/old_welcome.html"">旧トップページ</a></p>
//        </nav>
//        <article id=""columnright"">
//            <section>
//                <h3>&nbsp;お知らせ</h3>
//                <ul>
//                    <li>現在ニュース超速報板においてぽんなシステム関連のテストを行っております<br />
//                        (詳細はニュース超速報板のローカルルール参照)</li>
//                    <li>ホビー板においてトップ絵の募集を行っております</li>
//                </ul>
//                <br />
//                <h3>&nbsp;本日のレス数</h3>
//                <iframe src=""http://uravip.tonkotsu.jp/rescount.pl"" width=""700"" height=""160"" style=""margin-left: 10px;"" title=""レス数"">iframeを使用可能なブラウザでご覧下さい</iframe>
//                <br />
//                <!--ここ動的にページ生成したほうが早くね？-->
//                <!--あっ、RSS-->
//                <h3>&nbsp;最近の出来事</h3>
//                <ul>
//                    <li><time>(2013/12/22)</time>裏VIPのトップページを更新しました</li>
//                    <li><time>(2013/12/21)</time>ニュース超速報板を追加しました</li>
//                    <li><time>(2013/11/24)</time>同スレタイの乱立防止機能を導入しました</li>
//                    <li><time>(2013/11/19)</time>実験用の板を追加しました</li>
//                    <li><time>(2013/11/6)</time>スクリプトのアップデートを行いました</li>
//                    <li><time>(2013/11/3)</time>簡単な過去ログ検索機能を実装しました</li>
//                    <li><time>(2013/10/14)</time>本文中に!kakikoにてこれまでの自分自身の書き込み確認できる機能を実装しました</li>
//                    <li><time>(2013/10/6)</time>ぽんなシステムのテストが開始しました</li>
//                    <li><time>(2013/10/5)</time>!774機能を拡張しました</li>
//                    <li><time>(2013/10/5)</time>!rpg機能を追加、変更しました</li>
//                    <li><time>(2013/9/29)</time>スレ立て時にメール欄で!774と入力することによってそのスレ内でのコテ無効化を実装しました</li>
//                    <li><time>(2013/9/23)</time>裏VIPのトップページで本日のレス数を閲覧可能にしました</li>
//                    <li><time>(2013/9/8)</time>ホビー全般板の試験運用を開始しました</li>
//                    <li><time>(2013/9/7)</time>裏VIPのURLを追加したBBSMENUを作成しました(<a href=""http://uravip.tonkotsu.jp/bbsmenu.pl"">http://uravip.tonkotsu.jp/bbsmenu.pl)</a></li>
//                    <li><time>(2013/9/7)</time>!tres,!tsureを実装しました</li>
//                    <li><time>(2013/9/4)</time>裏VIPが復帰しました</li>
//                    <li><time>(2013/8/30)</time>ロリポップメンテナンスのためサーバーがダウンしました</li>
//                    <li><time>(2013/8/1)</time>掲示板一覧に「雑談ルノワール」を追加</li>
//                    <li><time>(2013/5/28)</time>掲示板のセキュリティを向上させました</li>
//                    <li><time>(2013/5/28)</time>掲示板スクリプトをアップデートしました</li>
//                    <li><time>(2013/5/23)</time>嫁機能([名前欄]!yome)を実装しました</li>
//                    <li><time>(2013/5/22)</time>!rank復活</li>
//                    <li><time>(2013/5/19)</time>鯖の安定性が向上しました</li>
//                    <li><time>(2013/5/18)</time>ダイス機能（[名前欄]!D6、!D10、!D20）、RPG機能（[本文]!rpg）を実装しました</li>
//                    <li><time>(2013/5/14)</time>!rank 機能、自動dat落ち機能を一時的に無効化</li>
//                    <li><time>(2013/5/13)</time>管理人★ さんが鯖継続契約をしてくれました。感謝</li>
//                    <li><time>(2013/5/8)</time>15時間書き込みがないと自動でdat落ちするようにした</li>
//                    <li><time>(2013/5/6)</time>運用情報・削除依頼板を開設しました</li>
//                    <li><time>(2013/5/6)</time>裏ニュー速VIP板の1001を変更しました</li>
//                    <li><time>(2013/5/6)</time>鯖のセキュリティを向上させました</li>
//                </ul>
//            </section>
//        </article>
//    </article>
//    <footer style=""text-align:center;margin-left:37%;"">
//        <small>Copyright &copy; 2013 Background Newsflash VIP All right reserved.</small>
//    </footer>
//</body>
//</html>
//";
#endregion
                if (System.IO.File.Exists(gs.OtherFolderPath + "\\startpage.html") && this.SettingData.IsShowStartPage)
                {
                    VIPBrowserLibrary.Other.StartPageParser spp = new VIPBrowserLibrary.Other.StartPageParser(gs.OtherFolderPath + "\\startpage.html");
                    var func = spp.CheckUseFunction();
                    this.startPageWebPage.ScriptErrorsSuppressed = true;
                    //this.startPageWebPage.DocumentText = "<body bgcolor='red'>";
                    this.startPageWebPage.DocumentText = spp.Parse(func);
                }
            };
#endregion
			#region formclosing lambda
			this.FormClosing += (sender, e) =>
            {
                //FormClose();
                Rectangle r = this.ClientRectangle;
                if (SettingData.IsFormClosingWarning)
                {
                    if (DialogResult.OK != MessageBox.Show("アプリケーションを終了しますか？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                    {
                        e.Cancel = true;
                        return;
                    }
                }
                if (this.SettingData.IsSaveFormLocation)
                {
                    this.SettingData.FormHeight = r.Height;
                    this.SettingData.FormWidth = r.Width;
                    this.SettingData.FormX = r.X;
                    this.SettingData.FormY = r.Y;
                    this.SettingData.IsMaximized = (this.WindowState == FormWindowState.Maximized) ? true : false;
                    this.SettingData.StartPosition = this.StartPosition;
                }
                if (this.SettingData.IsSaveThreadListView)
                    this.ch2BrowserControl1.SaveThreadListViewData();
                if (this.SettingData.IsSaveThreadView)
                    this.ch2BrowserControl1.SaveThreadViewData();

                this.ch2BrowserControl1.SaveFavoriteListTreeView();
            };
			#endregion

		}
		void ThreadListSaveDataLoad()
		{
			if (this.SettingData.IsSaveThreadListView)
			{
				var task = Task.Factory.StartNew(this.ch2BrowserControl1.LoadThreadListViewData);
				task.Wait();
			}
		}
		void ThreadSaveDataLoad()
		{
			if (this.SettingData.IsSaveThreadView)
				this.ch2BrowserControl1.LoadThreadViewData();
		}
		void LoadPlugin()
		{
			this.Plugin = new VIPBrowserPlugin.IPlugin[Plugins.Length];
			for (int i = 0; i < Plugins.Length; i++)
			{
				VIPBrowserPlugin.IPlugin pp = Plugins[i].CreateInstance(this);
				Plugin[i] = pp;
				pp.Run();
			}
		}
        void startPageWebPage_NewWindow(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            var baseweb = sender as WebBrowser;
            WebBrowser web = new WebBrowser();
            string url = baseweb.StatusText;
            string title = baseweb.DocumentTitle;
            web.Navigate(url);
            web.Dock = DockStyle.Fill;
            TabPage tp = new TabPage(title);
            tp.Controls.Add(web);
            web.Navigated += (_sender, _e) =>
            {
                var we = _sender as WebBrowser;
                var tabPage = we.Parent as TabPage;
                tabPage.Text = we.DocumentTitle;
            };
        }
        public Form1(bool IsUpIntializeComponent)
        {
            if (IsUpIntializeComponent)
            {
                InitializeComponent();
            }
        }

        private void TabCloseMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void SettingToolStripMenuItem_Clicked(object sender, EventArgs e)
        {
            ch2Browser.Dialogs.Setting s = new ch2Browser.Dialogs.Setting { SettingData = this.SettingData };
            s.FormClosing += (_sender, _e) => 
            {
                this.SettingData = s.SettingData;
            };

            s.ShowDialog();

        }

        private void CloseToolStripMenuItem_Clicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AboutBoxShow_Cliked(object sender, EventArgs e)
        {
            new ch2Browser.Dialogs.AboutBox().ShowDialog();
        }
        private void PluginSetupToolStripMenuItem_Clicked(object sender, EventArgs e) 
        {
            new Dialogs.PluginSetting { Owner = this }.ShowDialog(this);
        }

    }
}
