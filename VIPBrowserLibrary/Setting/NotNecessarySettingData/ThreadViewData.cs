using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Windows.Forms;

namespace VIPBrowserLibrary.Setting.NotNecessarySettingData
{
    /// <summary>
    /// アプリケーションの終了時または開始時にスレッドを保管および読み込みを行うクラスです
    /// </summary>
    public class ThreadViewData
    {
        private Setting.GeneralSetting gs = new GeneralSetting();
        private bool isReadOnly = false;
        private List<TabPage> tabPages = new List<TabPage>();
        /// <summary>
        /// TabControlに格納されているTabPagesを使用してこのクラスの新しいインスタンスを初期化します
        /// </summary>
        /// <param name="tabColection">格納されていたIEnumerable&lt;TabPage&gt;</param>
        public ThreadViewData(IEnumerable tabColection)
        {
            tabPages.AddRange(tabColection.OfType<TabPage>());
            this.isReadOnly = false;
        }
        /// <summary>
        /// 読み込み専用のモードでこのクラスの新しいインスタンスを初期化します
        /// </summary>
        public ThreadViewData()
        {
            this.isReadOnly = true;
        }
        /// <summary>
        /// このインスタンスが読み込み専用か取得します
        /// </summary>
        public bool IsReadOnly { get { return this.isReadOnly; } }

        //private List<TabPage> tabPages;
        /// <summary>
        /// このインスタンスに関連付けられている<seealso cref="T:List&lt;System.Windows.Forms.TabPage&gt;"/>を取得または設定します。
        /// </summary>
        public List<TabPage> TabPages
        {
            get { return this.tabPages; }
            set { this.tabPages = value; this.isReadOnly = false; }
        }
        /// <summary>
        /// このクラスのインスタンスを維持したままマネージドメモリを開放します
        /// </summary>
        public void Clear()
        {
            this.tabPages.Clear();
            this.isReadOnly = true;
        }
        /// <summary>
        /// スレッドのデータを書きこみます
        /// </summary>
        public void WriteThreadData()
        {
            if (this.isReadOnly)
                throw new InvalidOperationException();
            int i = 0;
            StringBuilder sb = new StringBuilder().Clear();
            foreach (var item in this.tabPages)
            {
                VIPBrowserLibrary.Common.IThreadViewer itv = item.Controls[0] as VIPBrowserLibrary.Common.IThreadViewer;
                sb.Append(itv.ThreadData.ThisFilePath).Append('&').Append(i).AppendLine();
                i++;
            }
            Utility.TextUtility.Write(gs.NotNecessarySettingDataPath + "\\thre.dat", sb.ToString(), false);
        }
        /// <summary>
        /// スレッドのデータを読み込みます
        /// </summary>
        /// <returns>読み込んだデータ</returns>
        public IDictionary<SaveThreadData, Chron.ThreadOrResData.ThreadData> ReadThreadData()
        {
            IDictionary<SaveThreadData, VIPBrowserLibrary.Chron.ThreadOrResData.ThreadData> sort = new SortedDictionary<SaveThreadData, VIPBrowserLibrary.Chron.ThreadOrResData.ThreadData>(new SaveThreadData.SaveThreadListCompare());
            string[] data = System.IO.File.ReadAllLines(gs.NotNecessarySettingDataPath + "\\thre.dat");
            foreach (var item in data)
            {
                string[] spStr = item.Split('&');
                SaveThreadData std = new SaveThreadData();
                std.BaseFilePath = spStr[0];
                std.Index = int.Parse(spStr[1]);
                sort.Add(std, Chron.ThreadOrResData.ThreadDataWriterAndReader.Read(spStr[0] + ".xml"));
            }
            return sort;
        }
        /// <summary>
        /// スレッドの情報を保存したクラスです
        /// </summary>
        public sealed class SaveThreadData
        {
            /// <summary>
            /// スレッドのインデックスを表します
            /// </summary>
            public int Index;
            /// <summary>
            /// スレッドの保管されている基本パスを表します
            /// </summary>
            public string BaseFilePath;

            /// <summary>
            /// SaveThreadDataの比較機能を提供します
            /// </summary>
            public class SaveThreadListCompare : IComparer<SaveThreadData>
            {
                /// <summary>
                /// あるSaveThreadDataと比較対象のもう一つのSaveThreadDataを比較します
                /// </summary>
                /// <param name="x">比較する一つ目のデータ</param>
                /// <param name="y">比較する二つめのデータ</param>
                /// <returns>比較結果</returns>
                public int Compare(SaveThreadData x, SaveThreadData y)
                {
                    return x.Index.CompareTo(y.Index);
                }
            }
        }
    }
}
