using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace VIPBrowserLibrary.Chron.ThreadOrResData.Abone
{
    /// <summary>
    /// 簡単なあぼーんを行うクラスです
    /// </summary>
    public class SimpleAbone
    {
        static Setting.GeneralSetting gs = new Setting.GeneralSetting();
        /// <summary>
        /// NGワードを格納したコレクションです
        /// </summary>
        public Dictionary<AboneType, string> NGWordCollection { get; set; }
        /// <summary>
        /// NGワードを格納したコレクションです
        /// </summary>
        public NameValueCollection NGCollection { get; set; }
        private AboneType aboneType { get; set; }
        /// <summary>
        /// このクラスの新しいインスタンスを初期化します
        /// </summary>
        public SimpleAbone(/*AboneType at*/)
        {
            // aboneType = at;
            NGWordCollection = new Dictionary<AboneType, string>();
            NGCollection = new NameValueCollection();
        }

        /// <summary>
        /// NGワードを読み込みします
        /// </summary>
        /// <returns>読み込んだNGWord</returns>
        static public async Task<HashSet<string>> Load(AboneType aboneType)
        {
            string ngData = String.Empty;
            using (FileStream fs = new FileStream(gs.OtherFolderPath + "\\ng.dat", FileMode.Create, FileAccess.ReadWrite))
            {
                StreamReader sr = new StreamReader(fs);
                ngData = await sr.ReadToEndAsync();
                sr.Dispose();
            }
            string[] ngLines = ngData.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            HashSet<string> ngList = new HashSet<string>();
            foreach (string item in ngLines)
            {
                if (item.IndexOf(aboneType.ToString()) == 0)
                {
                    ngList.Add(item.Replace(aboneType.ToString() + ":", ""));
                }
            }
            return ngList;

        }
        /// <summary>
        /// NGワードを保存します
        /// </summary>
        /// <param name="ngs">保存するデータ</param>
        static public async Task Save(HashSet<string> ngs)
        {

            using (FileStream fs = new FileStream(gs.OtherFolderPath + "\\ng.dat", FileMode.Create, FileAccess.ReadWrite))
            {
                StreamWriter sw = new StreamWriter(fs);
                foreach (string item in ngs)
                {
                    await sw.WriteLineAsync(item);
                }
                await sw.FlushAsync();
            }
            return;
        }
        /// <summary>
        /// NGワードをこのインスタンスに読み込みます
        /// </summary>
        public void InstLoad()
        {
            string ngData = String.Empty;
            using (FileStream fs = new FileStream(gs.OtherFolderPath + "\\ng.dat", FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                StreamReader sr = new StreamReader(fs);
                ngData = sr.ReadToEnd();
               
            }
            string[] ngLines = ngData.Split(new string[] { "\r\n" }, StringSplitOptions.None);

            foreach (string item in ngLines)
            {
                if (String.IsNullOrEmpty(item))
                    break;
                string[] splitData = item.Split(':');
                //AboneType at = (AboneType)Enum.Parse(typeof(AboneType), splitData[0], true);
                this.NGCollection.Add(splitData[0], splitData[1]);
                //this.NGWordCollection.Add(at, splitData[1]);
            }
        }
        /// <summary>
        /// インスタンスのNGワードを保存します
        /// </summary>
        public async Task InstSave()
        {
            StringBuilder sb = new StringBuilder();
            int i = 0;
            foreach (string item in NGCollection)
            {
                string[] ids = NGCollection.GetValues(item);
                foreach (string it in ids)
                    sb.Append(NGCollection.GetKey(i)).Append(":").AppendLine(it);
            }
            using (FileStream fs = new FileStream(gs.OtherFolderPath + "\\ng.dat", FileMode.Create, FileAccess.ReadWrite))
            {
                StreamWriter sw = new StreamWriter(fs);
                await sw.WriteAsync(sb.ToString());
                await sw.FlushAsync();
            }
            return;
        }
        /// <summary>
        /// このレスが可視可能か確認します
        /// </summary>
        /// <param name="r">確認するRes構造体</param>
        /// <returns>確認結果</returns>
        public bool IsVisible(Res r)
        {
            string[] mailNg = NGCollection.GetValues(AboneType.Mail.ToString());
            string[] nameNg = NGCollection.GetValues(AboneType.Name.ToString());
            string[] idNg = NGCollection.GetValues(AboneType.ID.ToString());
            string[] sentenceNg = NGCollection.GetValues(AboneType.Sentence.ToString());
            {
                if (idNg != null)
                    foreach (string item in idNg)
                        if (r.ID.IndexOf(item) != -1)
                            return false;

                if (nameNg != null)
                    foreach (string item in nameNg)
                        if (r.Name.IndexOf(item) != -1)
                            return false;

                if (sentenceNg != null)
                    foreach (string item in sentenceNg)
                        if (r.Sentence.IndexOf(item) != -1)
                            return false;

                if (mailNg != null)
                    foreach (string item in mailNg)
                        if (r.Mail.IndexOf(item) != -1)
                            return false;
            }
            return true;
        }

    }
}
