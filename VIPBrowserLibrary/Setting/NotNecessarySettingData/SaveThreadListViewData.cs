using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading.Tasks;

namespace VIPBrowserLibrary.Setting.NotNecessarySettingData
{
    /// <summary>
    /// アプリケーションの終了時または開始時にスレッドリストを保管および読み込みを行うクラスです
    /// </summary>
    public class ThreadListViewData
    {
        Setting.GeneralSetting gs = new GeneralSetting();
        /// <summary>
        /// このインスタンスが読み込み専用か取得します
        /// </summary>
        public bool IsReadOnly { get; private set; }
        private List<TabPage> tabPages;
        /// <summary>
        /// TabControlに格納されているTabPagesを使用してこのクラスの新しいインスタンスを初期化します
        /// </summary>
        /// <param name="tabPageCollection">格納されていたTabPageCollection</param>
        public ThreadListViewData(System.Windows.Forms.TabControl.TabPageCollection tabPageCollection)
        {
            tabPages = new List<TabPage>();
            foreach (TabPage item in tabPageCollection)
            {
                tabPages.Add(item);
            }
            this.IsReadOnly = false;
        }
        /// <summary>
        /// このクラスのインスタンスを維持したままマネージドメモリを開放します
        /// </summary>
        public void Clear() 
        {
            this.tabPages.Clear();
            this.IsReadOnly = true;
        }
        /// <summary>
        /// 読み込み専用のモードでこのクラスの新しいインスタンスを初期化します
        /// </summary>
        public ThreadListViewData()
        {
            tabPages = new List<TabPage>();
            this.IsReadOnly = true;
        }
        /// <summary>
        /// このインスタンスに関連付けられている<seealso cref="T:List&lt;System.Windows.Forms.TabPage&gt;"/>を取得または設定します。
        /// </summary>
        public List<TabPage> TabPages 
        {
            get { return this.tabPages; }
            set { this.tabPages = value; this.IsReadOnly = false; }
        }
        /// <summary>
        /// スレッドリストのデータを書き込みます
        /// </summary>
        /// <exception cref="T:System.InvalidOperationException">
        /// このインスタンスは読み込み専用です
        /// </exception>
        public void WriteListViewData() 
        {
            if (this.IsReadOnly)
                throw new InvalidOperationException("このインスタンスは読み込み専用です");
            StringBuilder writeData = new StringBuilder();
            int i = 0;
            foreach (var item in this.tabPages)
            {
                writeData.Append(item.Name).Append("\\").Append(item.Text).Append("\\").Append(i++).AppendLine();
            }
            Utility.TextUtility.Write(gs.NotNecessarySettingDataPath + "\\thli.dat", writeData.ToString(), false);
        }
        /// <summary>
        /// リストビューのデータを読み込みます
        /// </summary>
        public async Task<IDictionary<SaveThreadListData, ListViewItem[]>> ReadListViewData() 
        {
            IDictionary<SaveThreadListData, ListViewItem[]> sort = new SortedDictionary<SaveThreadListData, ListViewItem[]>(new SaveThreadListData.SaveThreadListCompare());
            List<SaveThreadListData> stldList = new List<SaveThreadListData>();
            string[] lines = File.ReadAllLines(gs.NotNecessarySettingDataPath + "\\thli.dat", Encoding.GetEncoding("Shift-JIS"));
            foreach (var item in lines)
            {
                string[] splitString = item.Split('\\');
                SaveThreadListData stld = new SaveThreadListData();
                stld.Name = splitString[1];
                stld.Url = splitString[0];
                stld.Index = Int32.Parse(splitString[2]);
                stldList.Add(stld);
            }
            foreach (var item in stldList)
            {
                var bt = Common.TypeJudgment.BBSTypeJudg(item.Url);
                string url = item.Url;
                BBS.Common.IThreadListReader itlr;
                switch (bt)
                {
                    case VIPBrowserLibrary.Common.BBSType._2ch:
                        itlr = new BBS.X2ch.X2chThreadListReader(url);
                        break;
                    case VIPBrowserLibrary.Common.BBSType.jbbs:
                        itlr = new BBS.Jbbs.JbbsThreadListReader(url);
                        break;
                    case VIPBrowserLibrary.Common.BBSType.machibbs:
                        itlr = new BBS.MachiBBS.MachiBBSThreadListReader(url);
                        break;
                    default:
                        throw new ArgumentException();
                }
                var data = await itlr.OfflineGetThreadList();
                sort.Add(item, data);
            }
            return sort;
        }
        /// <summary>
        /// 保存用スレッドリストのデータを表します。このクラスは継承できません。
        /// </summary>
        public sealed class SaveThreadListData
        {
            /// <summary>
            /// スレッドリストを収納しているタブのインデックスを表します
            /// </summary>
            public int Index;
            /// <summary>
            /// スレッドリストのUrlを表します
            /// </summary>
            public string Url;
            /// <summary>
            /// スレッドリストの板名を表します
            /// </summary>
            public string Name;
            /// <summary>
            /// SaveThreadListDataのインスタンスを比較します
            /// </summary>
            public class SaveThreadListCompare: IComparer<SaveThreadListData>
            {
                /// <summary>
                /// 比較します
                /// </summary>
                /// <param name="x">比較対象のインスタンス</param>
                /// <param name="y">比較されるインスタンス</param>
                /// <returns>比較結果</returns>
                public int Compare(SaveThreadListData x, SaveThreadListData y)
                {
                    return x.Index.CompareTo(y.Index);
                }
            }
        }

    }
}
