using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace VIPBrowserLibrary.Chron.ThreadOrResData.Abone
{
    /// <summary>
    /// スレ一覧におけるNGワードの管理を行うクラスです
    /// </summary>
    public class ThreadListAbone
    {
        Setting.GeneralSetting gs = null;
        /// <summary>
        /// NGワードのコレクションを取得または設定します
        /// </summary>
        public List<string> NGCollection { get; set; }
        /// <summary>
        /// このクラスの新しいインスタンスを初期化します
        /// </summary>
        public ThreadListAbone()
        {
            gs = new Setting.GeneralSetting();
            NGCollection = new List<string>();
        }
        /// <summary>
        /// このインスタンスにデータを読み込みます
        /// </summary>
        public void InstLoad()
        {
            string ngData = String.Empty;
            using (FileStream fs = new FileStream(gs.OtherFolderPath + "\\thng.dat", FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                StreamReader sr = new StreamReader(fs,Encoding.Default);
                ngData = sr.ReadToEnd();

            }
            string[] ngLines = ngData.Split(new string[] { "\r\n" }, StringSplitOptions.None);

            foreach (string item in ngLines)
            {
                if (String.IsNullOrEmpty(item))
                    return;
                //AboneType at = (AboneType)Enum.Parse(typeof(AboneType), splitData[0], true);
                this.NGCollection.Add(item);
                //this.NGWordCollection.Add(at, splitData[1]);
            }
            return;
        }
        /// <summary>
        /// このクラスのデータを保存します
        /// </summary>
        /// <returns></returns>
        public async Task InstSave()
        {
            StringBuilder sb = new StringBuilder();
            foreach (string item in NGCollection)
                sb.AppendLine(item);
            //using(FileStream fs = new FileStream(gs.OtherFolderPath + "\\thng.dat", FileMode.OpenOrCreate, FileAccess.Write))
            //{
            using (StreamWriter sw = new StreamWriter(gs.OtherFolderPath + "\\thng.dat", false, Encoding.Default))
            {
                await sw.WriteAsync(sb.ToString());
                await sw.FlushAsync();
                //}
            } return;
            
        }
        /// <summary>
        /// このテキストが閲覧可能か確認します
        /// </summary>
        /// <param name="text">確認するテキスト</param>
        /// <returns>可能な場合にはtrueを,不可能な場合にはfalseを返します</returns>
        public bool IsVisible(string text)
        {
            foreach (string s in NGCollection)
                if (text.IndexOf(s, StringComparison.CurrentCultureIgnoreCase) != -1)
                    return false;
            return true;
        }
    }
}
