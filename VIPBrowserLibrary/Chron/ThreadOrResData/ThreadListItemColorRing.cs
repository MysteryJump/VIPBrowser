using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Specialized;
using VIPBrowserLibrary.Other.MyExtensions;
using System.Drawing;

namespace VIPBrowserLibrary.Chron.ThreadOrResData
{
    /// <summary>
    /// スレッドリストの指定したスレッドの色付けを行います
    /// </summary>
    public class ThreadListItemColorRing
    {
        /// <summary>
        /// このインスタンスのdataがnullかどうか確認します
        /// </summary>
        public bool IsNull { get { return String.IsNullOrEmpty(data); } }
        private string data = null;
		/// <summary>
		/// ThreadListItemColorRingの設定ファイルのパスを表します。
		/// </summary>
		public static readonly string ThreadListItemColorRingSettingDataPath = gs.OtherFolderPath + "\\listcolor.dat";
		private static Setting.GeneralSetting gs { get { return new Setting.GeneralSetting();} }
        private string[] SubjectKeys;
        /// <summary>
        /// 色付けのデータコレクション
        /// </summary>
        public NameValueCollection ChangeColorCollection { get; set; }
        /// <summary>
        /// このクラスの新しいインスタンスを指定したパスを使用して初期化します
        /// </summary>
        public ThreadListItemColorRing(string filePath)
        {
            this.ChangeColorCollection = new NameValueCollection(10);
            this.data = this.Read();
            this.ChangeColorCollection.Add(this.ConvertValueCollectionFromText(data));
            this.SubjectKeys =  this.ReadSubjectData(filePath);
        }
        /// <summary>
        /// このクラスの新しいインスタンスを初期化します
        /// </summary>
        public ThreadListItemColorRing()
        {
            this.ChangeColorCollection = new NameValueCollection(10);
            this.data = this.Read();
            this.ChangeColorCollection.Add(this.ConvertValueCollectionFromText(data));
        }
        /// <summary>
        /// 色付けを行います
        /// </summary>
        /// <returns>変換したListViewItem</returns>
        public ListViewItem ChangeColor(ListViewItem lvi)
        {
            string[] speed = this.ChangeColorCollection.GetValues(ChangeColorTypeConditions.Speed.ToString());
            string[] resCount = this.ChangeColorCollection.GetValues(ChangeColorTypeConditions.ResCount.ToString());
            string[] name = this.ChangeColorCollection.GetValues(ChangeColorTypeConditions.ThreadName.ToString());
            lvi.UseItemStyleForSubItems = false;
            string listName = lvi.SubItems["Name"].Text;
            int listSpeed = lvi.SubItems["Speed"].Text.Parse();
            int listRes = lvi.SubItems["ResCount"].Text.Parse();
            {
                while (true)
                {
                    if (speed != null)
                    {
                        foreach (string item in speed)
                        {

                            int upResSpeed;
                            string[] spli = item.Split('&');
                            Color cl = spli[0].ParseColor();
                            upResSpeed = spli[1].Parse();

                            if (listSpeed > upResSpeed)
                            {
                                lvi.SubItems["Speed"].BackColor = cl;
                                break;
                            }
                        }
                    }
                    if (resCount != null)
                    {
                        foreach (string item in resCount)
                        {
                            string[] sli = item.Split('&');
                            Color cl = sli[0].ParseColor();
                            int c = sli[1].Parse();
                            if (listRes > c)
                            {
                                lvi.SubItems["ResCount"].BackColor = cl;
                                break;
                            }
                        }
                    }
                    if (name != null)
                    {
                        foreach (string item in name)
                        {
                            string[] sli = item.Split('&');
                            Color cl = sli[0].ParseColor();
                            if (sli[1].Contains(listName))
                            {
                                lvi.SubItems["Name"].BackColor = cl;
                                lvi.BackColor = cl;
                                break;
                            }
                        }
                    }
                    break;
                }
            }

            return lvi;
        }
        /// <summary>
        /// 前回スレッドリストにあったスレッドの色を変化させます
        /// </summary>
        /// <param name="lvi">確認するListViewItem</param>
        /// <param name="key">確認するスレッドのキー</param>
        /// <returns></returns>
        public ListViewItem ChangeAlreadyListColor(ListViewItem lvi, string key)
        {
            lvi.BackColor = Color.Azure;
            if (SubjectKeys == null)
                return lvi;
            foreach (string da in SubjectKeys)
            {
                if (key == da)
                {
                    lvi.BackColor = Color.White;
                    break;
                }
            }
            return lvi;
        }
        /// <summary>
        /// データを読み込みます
        /// </summary>
        /// <returns>成功した場合は読み込んだデータ,失敗した場合はnull</returns>
        public string Read()
        {
            string path = gs.OtherFolderPath + "\\listcolor.dat";
            return Utility.TextUtility.Read(path,Encoding.Default);
        }
        /// <summary>
        /// データを書き込みます
        /// </summary>
        /// <param name="data">書き込むデータ</param>
        public void Write(string data)
        {
            string path = gs.OtherFolderPath + "\\listcolor.dat";
            Utility.TextUtility.Write(path, data, Encoding.Default,false);
        }
        /// <summary>
        /// テキストをカラーリングする属性を表したNameValuecollection形式に変換します
        /// </summary>
        /// <param name="text">変換対象のテキスト</param>
        /// <returns>変換したNameValueCollection</returns>
        public NameValueCollection ConvertValueCollectionFromText(string text)
        {
            if (String.IsNullOrEmpty(text))
                return new NameValueCollection();
            NameValueCollection nvc = new NameValueCollection();
            string[] lines = text.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            foreach (string line in lines)
            {
                if (String.IsNullOrEmpty(line))
                    break;
                string[] splitWord = line.Split(':');
                nvc.Add(splitWord[0], splitWord[1]);
            }
            return nvc;
        }
        /// <summary>
        /// カラーリングする属性を示したNameValueCollectionをString形式に変換します
        /// </summary>
        /// <param name="nvc">変換対象のNameValueCollection</param>
        /// <returns>変換したString</returns>
        public string ConvertTextFromValueCollection(NameValueCollection nvc)
        {
            StringBuilder writeBuilder = new StringBuilder();
            foreach (string key in nvc)
            {
                string[] values = nvc.GetValues(key);
                foreach (string value in values)
                {
                    writeBuilder.Append(key).Append(":").Append(value);
                }
            }
            return writeBuilder.ToString();
        }
        //このメソッドは効率化の必要あり
        /// <summary>
        /// スレッドリストのデータを読み込みます
        /// </summary>
        /// <param name="path">読み込むパス</param>
        /// <returns>読み込まれたdatKeys</returns>
        public string[] ReadSubjectData(string path)
        {
            string[] returnData = null;
            string data = Utility.TextUtility.Read(path, Encoding.Default);
            if (String.IsNullOrEmpty(data))
                return null;
            string[] lines = data.Split('\n');
            returnData = new string[lines.Length];
            int i = 0;
            foreach (string item in lines)
            {
                if (String.IsNullOrEmpty(item))
                    break;
                string[] dats = item.Split(new string[] { "<>" ,","}, StringSplitOptions.None);
                string unko = dats[0].Replace(".dat", "").Replace(".cgi", "");
                int u = 0;
                if (!Int32.TryParse(dats[1], out u))
                {
                    returnData[i] = unko;
                }
                else
                {
                    returnData[i] = dats[1].Replace(".cgi", "");
                }
                
                i++;
            }
            return returnData;
        }

    }
    /// <summary>
    /// 色付けのタイプを表します
    /// </summary>
    public enum ChangeColorTypeConditions
    {
        /// <summary>
        /// 勢い
        /// </summary>
        Speed,
        /// <summary>
        /// レス数
        /// </summary>
        ResCount,
        /// <summary>
        /// スレッド名
        /// </summary>
        ThreadName
    }
}
