using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VIPBrowserLibrary.Chron
{
    /// <summary>
    /// スレッドのカラムデータを表すクラスです。このクラスは継承できません。
    /// </summary>
    public sealed class ThreadColumn
    {
        private bool isLoaded;
        private static Setting.GeneralSetting gs = new Setting.GeneralSetting();
        /// <summary>
        /// このクラスの設定ファイルが保存されるパスを表します
        /// </summary>
        public static readonly string ColumnDataPath = gs.NotNecessarySettingDataPath + "\\column.dat";
        /// <summary>
        /// カラムのデータを読み込みます
        /// </summary>
        /// <param name="filePath">読みこむファイルのパス</param>
        public void ReadColumnData(string filePath)
        {
            string data = this.rawData = Utility.TextUtility.Read(filePath);
            string[] columnArray = data.Split(new char[]{','}, StringSplitOptions.RemoveEmptyEntries);
            List<ColumnHeader> clms = new List<ColumnHeader>();
            foreach (var item in columnArray)
            {
                string[] splStr = item.Split('.');
                ColumnHeader ch = new ColumnHeader();
                ch.Name = splStr[0];
                switch (splStr[0])
                {
                    case "IsRead":
                        ch.Text = "!";
                        ch.Width = int.Parse(splStr[1]);
                        break;
                    case "Count":
                        ch.Text = "#";
                        ch.Width = int.Parse(splStr[1]);
                        break;
                    case "Name":
                        ch.Text = "スレッドタイトル";
                        ch.Width = int.Parse(splStr[1]);
                        break;
                    case "ResCount":
                        ch.Text = "レス数";
                        ch.Width = int.Parse(splStr[1]);
                        break;
                    case "Time":
                        ch.Text = "スレ立て";
                        ch.Width = int.Parse(splStr[1]);
                        break;
                    case "Speed":
                        ch.Text = "勢い";
                        ch.Width = int.Parse(splStr[1]);
                        break;
                    case "Size":
                        ch.Text = "サイズ";
                        ch.Width = int.Parse(splStr[1]);
                        break;
                    case "OldResCount":
                        ch.Text = "既読";
                        ch.Width = int.Parse(splStr[1]);
                        break;
                    case "NewResCount":
                        ch.Text = "新着";
                        ch.Width = int.Parse(splStr[1]);
                        break;
                    default:
                        throw new ArgumentException();
                }
                clms.Add(ch);
            }
            this.ColumnData = clms.ToArray();
            this.isLoaded = true;
        }
        /// <summary>
        /// カラムのデータを書き込みます
        /// </summary>
        /// <param name="columns">保存対象の<see cref="T:IEnumerable&lt;ColumnHeader&gt;"/></param>
        /// <param name="filePath">保存するパス</param>
        public void WriteColumnData(IEnumerable<ColumnHeader> columns,string filePath)
        {
            StringBuilder rtxt = new StringBuilder();
            foreach (var item in columns)
            {
                rtxt.Append(item.Name).Append(".").Append(item.Width).Append(',');
            }
            Utility.TextUtility.Write(filePath, rtxt.ToString(), false);
            
        }
        /// <summary>
        /// カラムのデータを書き込みます
        /// </summary>
        /// <param name="lv">保存対象の<see cref="T:System.Windows.Forms.ListView"/></param>
        /// <param name="filePath">保存するパス</param>
        public void WriteColumnData(ListView lv, string filePath)
        {
            var col = lv.Columns;
            var l = new SortedDictionary<int,ColumnHeader>();
            for (int i = 0; i < col.Count; i++)
            {
                l.Add(col[i].DisplayIndex, col[i]);
            }
            StringBuilder rtxt = new StringBuilder();
            foreach (var item in l)
            {
                rtxt.Append(item.Value.Name).Append(".").Append(item.Value.Width).Append(',');
            }
            Utility.TextUtility.Write(filePath, rtxt.ToString(), false);
           
        }
        /// <summary>
        /// カラムヘッダーに合わせてListViewItem[]を変換します
        /// </summary>
        /// <param name="items">変換元の<see cref="T:IEnumerable&lt;ColumnHeader&gt;"/></param>
        /// <returns>変換後のListViewItem[]</returns>
        public ListViewItem[] ConvertToColumnBaseItem(IEnumerable<ListViewItem> items)
        {
            if (ColumnData == null)
                return items.ToArray<ListViewItem>();
            List<ListViewItem> list = new List<ListViewItem>();
            foreach (var item in items)
            {
                
                ListViewItem lvi = (ListViewItem)item.Clone();
                lvi.SubItems.Clear();
                SortedDictionary<int, ListViewItem.ListViewSubItem> sd = new SortedDictionary<int, ListViewItem.ListViewSubItem>();
                foreach (ListViewItem.ListViewSubItem sub in item.SubItems)
                {
                    for (int i = 0; i < this.ColumnData.Length; i++)
                    {
                        if (this.ColumnData[i].Name == sub.Name)
                        {
                            sd.Add(i, sub);
                        }
                    }
                }
                for (int i = 0; i < sd.Count; i++)
                {
                    lvi.SubItems.Add(sd[i]);
                }
                lvi.SubItems.RemoveAt(0);
                list.Add(lvi);
                
            }
            return list.ToArray();
        }

        /// <summary>
        /// このクラスに格納されているColumnHeaderの配列を取得または設定します
        /// </summary>
        public ColumnHeader[] ColumnData { get; set; }

        private string rawData;
        /// <summary>
        /// 設定データ用にデータを変換します
        /// </summary>
        /// <returns>データを格納した<see cref="T:System.Collections.Generic.List&lt;System.Collections.Generic.KeyValuePair&lt;string,int&gt;&gt;"/></returns>
        public List<KeyValuePair<string,int>>[] ConvertToSettingData()
        {
            if (!this.isLoaded)
                throw new InvalidOperationException();
            string[] strs = {
                                "IsRead",
                                "NewResCount",
                                "Count",
                                "Name",
                                "ResCount",
                                "Time",
                                "Speed",
                                "Size",
                                "OldResCount",
                            };
            var noneDispData = new List<KeyValuePair<string, int>>();
            foreach (var item in strs)
            {
                noneDispData.Add(new KeyValuePair<string, int>(item, 250));
            }
            var dir = new List<KeyValuePair<string, int>>();
            foreach (var cl in this.ColumnData)
            {
                dir.Add(new KeyValuePair<string,int>(cl.Name, cl.Width));
                noneDispData.Remove(new KeyValuePair<string,int>(cl.Name,250));
            }
            Func<string, int> f = new Func<string, int>((s) =>
            {
                return 50;
            });

            
            return new List<KeyValuePair<string, int>>[2] { dir, noneDispData };
        }
        /// <summary>
        /// 設定データを保存します
        /// </summary>
        /// <param name="settingData">保存する<see cref="T:System.Collections.Generic.List&lt;System.Collections.Generic.KeyValuePair&lt;string,int&gt;&gt;"/></param>
        public void SaveSettingData(List<KeyValuePair<string,int>>[] settingData) 
        {
            StringBuilder sb = new StringBuilder();
            var data = settingData[0];
            foreach (var item in data)
            {
                sb.Append(item.Key).Append(".").Append(item.Value).Append(",");
            }
            Utility.TextUtility.Write(ThreadColumn.ColumnDataPath, sb.ToString(), false);
        }
        /// <summary>
        /// 表示対象の文字と値の間の相互変換を行います
        /// </summary>
        /// <param name="str">変換対象の文字</param>
        /// <param name="isDisplayValue">表示する文字の場合はtrue,keyの場合はfalse</param>
        /// <returns>変換後の値</returns>
        public static string ExchangeKeyOrDisplayValue(string str,bool isDisplayValue)
        {
            string[] keys = {
                                "IsRead",
                                "NewResCount",
                                "Count",
                                "Name",
                                "ResCount",
                                "Time",
                                "Speed",
                                "Size",
                                "OldResCount",
                            };
            string[] displayValues = {
                                         "!",
                                         "新着",
                                         "#",
                                         "スレタイ",
                                         "レス数",
                                         "スレ立て",
                                         "勢い",
                                         "サイズ",
                                         "既読"
                                     };
            string value = null;
			object locker = new object();
            if (isDisplayValue)
            {
                Parallel.For(0, keys.Length, (a,b) =>
                {
                    if (keys[a] == str)
                    {
                        lock (locker)
                        {
                            value = keys[a];
                            b.Stop();
                            return;
                        }
                    }
                });
            }
            else
            {
                Parallel.For(0, displayValues.Length, (a,b) =>
                {
                        
                    if (displayValues[a] == str)
                    {
                        lock (locker)
                        {
                            value = displayValues[a];
                            b.Stop();
                            
                            return;
                        }
                    }
                });
            }
            return value;
        }
    }
}
