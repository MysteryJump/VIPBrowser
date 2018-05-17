using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VIPBrowserLibrary.BBS.Common
{
    /// <summary>
    /// 板一覧を取得するクラスです
    /// </summary>
    public class GetBoardList
    {
        private TreeNode tn = new TreeNode("標準板");
        private Setting.GeneralSetting gs = new Setting.GeneralSetting();
        private string DownloadHtml = String.Empty;
        /// <summary>
        /// 非同期で板一覧を取得し解析します
        /// </summary>
        private async Task LoadBoardList(string bbsmenuUrl, bool isUpdate)
        {
            string boardListHtml = String.Empty;
            if (isUpdate)
            {
                HttpClient hc = new HttpClient(bbsmenuUrl);
                boardListHtml = await hc.GetStringAsync();
                DownloadHtml = boardListHtml;
            }
            else
            {
                boardListHtml = await Utility.TextUtility.ReadAsync(gs.CurrentDirectory + "\\normalboard.bor");
                if (boardListHtml == String.Empty)
                {
                    HttpClient hc = new HttpClient("http://menu.2ch.net/bbsmenu.html");
                    boardListHtml = await hc.GetStringAsync();
                    DownloadHtml = boardListHtml;
                }
            }
            boardListHtml = boardListHtml.Replace("TARGET=_blank", "");
            if (boardListHtml.IndexOf("もうずっと人大杉") != -1)
            {
                MessageBox.Show("取得に失敗しますたｗｗｗ", "Error");
                return;
            }
            Timer t = new Timer();
            t.Interval = 5000;
            t.Tick += (sender, e) => { throw new TimeoutException("解析にかかる時間がオーバーしました"); };
            State state = State.Category;
            string category = "", Boardurl = "", boardName = "";
            t.Start();
            for (int i = 0; i < boardListHtml.Length; )
            {
                switch (state)
                {
                    case State.Category:
                        int tmp = boardListHtml.IndexOf("<B>", i) + 3;
                        int tmp2 = boardListHtml.IndexOf("</B>", tmp);
                        t.Stop();
                        if (tmp != -1 && tmp2 != -1)
                        {
                            category = boardListHtml.Substring(tmp, tmp2 - tmp);
                            i = tmp2 + 4;
                            state++;
                        }
                        break;
                    case State.Url:
                        int tmp1 = boardListHtml.IndexOf("HREF=", i) + 5;
                        int tmp12 = boardListHtml.IndexOf(">", tmp1);
                        if (tmp1 != -1 && tmp12 != -1)
                        {
                            Boardurl = boardListHtml.Substring(tmp1, tmp12 - tmp1);
                            i = tmp12 + 1;
                            state++;
                        }
                        break;
                    case State.BoardName:
                        int tmp3 = boardListHtml.IndexOf("</A>", i);
                        if (tmp3 != -1)
                        {
                            boardName = boardListHtml.Substring(i, tmp3 - i);
                            i = tmp3 + 4;
                            addItem(category, Boardurl, boardName);
                            int hrefIndex = boardListHtml.IndexOf("HREF=", i);
                            int bTagIndex = boardListHtml.IndexOf("<B>", i);
                            if (hrefIndex == -1)
                            {
                                return;
                            }
                            else
                            {
                                if (hrefIndex > bTagIndex && bTagIndex != -1)
                                {
                                    state = State.Category;
                                }
                                else
                                {
                                    state = State.Url;
                                }
                            }
                        }
                        break;
                }
            }
            return;
        }
        /// <summary>
        /// 種類
        /// </summary>
        private enum State
        {
            /// <summary>
            /// カテゴリ
            /// </summary>
            Category,
            /// <summary>
            /// Url
            /// </summary>
            Url,
            /// <summary>
            /// 板名
            /// </summary>
            BoardName,
        }
        /// <summary>
        /// TreeNodeに板及びカテゴリを追加します
        /// </summary>
        /// <param name="category">カテゴリ</param>
        /// <param name="url">Url</param>
        /// <param name="name">名前</param>
        private void addItem(string category, string url, string name)
        {
            if (!this.tn.Nodes.ContainsKey(category))
                tn.Nodes.Add(category, category);

            this.tn.Nodes[category].Nodes.Add(url, name);
        }
        /// <summary>
        /// BBSMENUをダウンロードファイルから取得します
        /// </summary>
        /// <returns>板一覧を格納したTreeNode</returns>
        public async Task<TreeNode> GetBoardLists()
        {
            return await GetBoardListCore(String.Empty, false);
        }
        /// <summary>
        /// BBSMENUをurlを指定して更新します
        /// </summary>
        /// <param name="bbsmenuUrl">板一覧のUrl</param>
        /// <returns>板一覧を格納したTreeNode</returns>
        public async Task<TreeNode> GetBoardLists(string bbsmenuUrl)
        {
            return await GetBoardListCore(bbsmenuUrl, true);
        }
        /// <summary>
        /// BBSMENUを更新するかどうか指定して取得します
        /// </summary>
        /// <param name="bbsmenuUrl">要求先のURL</param>
        /// <param name="isUpdate">更新の真偽を表すbool値</param>
        /// <returns>板一覧を格納したTreeNode</returns>
        private async Task<TreeNode> GetBoardListCore(string bbsmenuUrl, bool isUpdate)
        {
            if (isUpdate)
            {
                
                await LoadBoardList(bbsmenuUrl, true);
                await Utility.TextUtility.WriteAsync(gs.CurrentDirectory + "\\normalboard.bor", DownloadHtml, false);
                return tn;
            }
            else
            {
                await LoadBoardList(String.Empty, false);
                return tn;
            }
            
        }
        /// <summary>
        /// ユーザー定義板の読み込みを行います
        /// </summary>
        /// <returns>板一覧を格納したTreeNode</returns>
        public async Task<TreeNode> GetUserBoardList() 
        {
            if(!System.IO.File.Exists(gs.CurrentDirectory + "\\userboard.bor"))
                return null;
            string analysisData = await Utility.TextUtility.ReadAsync(gs.CurrentDirectory + "\\userboard.bor");
            if (analysisData == String.Empty)
                return null;
            TreeNode tn = new TreeNode("ユーザー定義板");
            try
            {
                foreach (string item in analysisData.Split('\n'))
                {
                    if (item == String.Empty)
                        break;
                    string[] nodeData = item.Split('\t');
                    tn.Nodes.Add(nodeData[1], nodeData[0]);
                }
            }
            catch { return null; }
            return tn;
        }
    }
}
