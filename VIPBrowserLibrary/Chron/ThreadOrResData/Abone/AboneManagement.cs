using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace VIPBrowserLibrary.Chron.ThreadOrResData.Abone
{
    /// <summary>
    /// あぼーんの管理を行います
    /// </summary>
    public class AboneManagement
    {
        static Setting.GeneralSetting gs = new Setting.GeneralSetting();
        /// <summary>
        /// NGワードのコレクションを表します
        /// </summary>
        public List<NGWord> NGCollection { get; set; }
        /// <summary>
        /// このクラスの新しいインスタンスを初期化します
        /// </summary>
        public AboneManagement()
        {
            this.NGCollection = new List<NGWord>();
        }
        /// <summary>
        /// このインスタンスにデータを読み込みます
        /// </summary>
        public void InstLoad()
        {
            NGCollection.AddRange(ReadAboneFiles());
        }
        /// <summary>
        /// このインスタンスのデータを書き込みます
        /// </summary>
        /// <returns></returns>
        public async Task InstSave()
        {
            await WriteAboneFiles(NGCollection.ToArray());
        }
        /// <summary>
        /// NGWordを読み込みます
        /// </summary>
        /// <returns>読み込まれたNGWordの配列</returns>
        /// <remarks>ネストが深くなりすぎてすまん</remarks>
        public static NGWord[] ReadAboneFiles()
        {

            string data = Utility.TextUtility.Read(gs.OtherFolderPath + "\\ng.dat");
            if (data == null)
                return new NGWord[0];
            string[] lines = data.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            NGWord[] words = new NGWord[lines.Length];
            int i = 0;
            foreach (string item in lines)
            {
                string[] splitData = item.Split('\\');
                NGWord nw;
                AboneType at = (AboneType)Enum.Parse(typeof(AboneType), splitData[1]);
                if (splitData[0] == "False")
                {
                    if (splitData[3] != "Null")
                    {
                        if (splitData[4] != "Null" && splitData[5] != "Null")
                        {
                            nw = new NGWord(splitData[2], at, splitData[3], DateTime.Parse(splitData[4]), TimeSpan.Parse(splitData[5]));
                        }
                        else
                        {
                            nw = new NGWord(splitData[2], at, splitData[3]);
                        }
                    }
                    else
                    {
                        if (splitData[4] != "Null" && splitData[5] != "Null")
                        {
                            nw = new NGWord(splitData[2], at, String.Empty, DateTime.Parse(splitData[4]), TimeSpan.Parse(splitData[5]));
                        }
                        else
                        {
                            nw = new NGWord(splitData[2], at);
                        }
                    }
                }
                else
                {
                    if (splitData[3] != "Null")
                    {
                        if (splitData[4] != "Null" && splitData[5] != "Null")
                        {
                            nw = new NGWord(new Regex(splitData[2], RegexOptions.Compiled), at, splitData[3], DateTime.Parse(splitData[4]), TimeSpan.Parse(splitData[5]));
                        }
                        else
                        {
                            nw = new NGWord(new Regex(splitData[2], RegexOptions.Compiled), at, splitData[3]);
                        }
                    }
                    else
                    {
                        if (splitData[4] != "Null" && splitData[5] != "Null")
                        {
                            nw = new NGWord(new Regex(splitData[2], RegexOptions.Compiled), at, String.Empty, DateTime.Parse(splitData[4]), TimeSpan.Parse(splitData[5]));
                        }
                        else
                        {
                            nw = new NGWord(new Regex(splitData[2], RegexOptions.Compiled), at);
                        }
                    }
                }
                words[i] = nw;
                i++;
            }
            return words;

        }
        /// <summary>
        /// NGWordを保存します
        /// </summary>
        /// <param name="ngws">書き込むNGWordの配列</param>
#pragma warning disable 1998
        public static async Task WriteAboneFiles(NGWord[] ngws)
        {
            //await AwaitSet.Awaitable<DBNull>.Run(() =>
            //{
            
            StringBuilder writeb = new StringBuilder(String.Empty);
            foreach (NGWord n in ngws)
            {
                if (n.IsRegex)
                {
                    writeb.Append(true).Append("\\").Append(n.AboneTypes)
                        .Append("\\").Append(n.RegexWord).Append("\\");
                }
                else
                {
                    writeb.Append(false).Append("\\").Append(n.AboneTypes)
                        .Append("\\").Append(n.Word).Append("\\");
                }

                if (!String.IsNullOrEmpty(n.Url))
                    writeb.Append(n.Url);
                else
                    writeb.Append("Null");
                writeb.Append("\\");
                if (n.SetTime != DateTime.MinValue)
                    writeb.Append(n.SetTime);
                else
                    writeb.Append("Null");
                writeb.Append("\\");
                if (n.ReleaseTime != TimeSpan.Zero)
                    writeb.Append(n.ReleaseTime);
                else
                    writeb.AppendLine("Null");
            }
            Utility.TextUtility.Write(gs.OtherFolderPath + "\\ng.dat", writeb.ToString(), false);
            //return null;
            //});
        }
#pragma warning restore 1998
        /// <summary>
        /// このレスが可視可能か判別します
        /// </summary>
        /// <param name="r">判断するレス</param>
        /// <param name="url">判別の元となるURL</param>
        /// <returns>可能ならtrue,不可能ならfalse</returns>
        public bool IsVisible(Res r,string url)
        {
            
            bool isVisible = false;
            foreach (NGWord ng in NGCollection)
            {
                if (!String.IsNullOrEmpty(ng.Url))
                    if (Regex.IsMatch(url, ng.Url, RegexOptions.Compiled))
                        return true;
                switch (ng.AboneTypes)
                {
                    case AboneType.ID:
                        {
                            if (ng.IsRegex)    
                                isVisible = !ng.RegexWord.IsMatch(r.ID);
                            else
                                isVisible = !r.ID.Contains(ng.Word);
                            break;
                        }
                    case AboneType.Name:
                        {
                            if (ng.IsRegex)
                                isVisible = !ng.RegexWord.IsMatch(r.Name);
                            else
                                isVisible = !r.Name.Contains(ng.Word);
                            break;
                        }
                    case AboneType.Sentence:
                        {
                            if (ng.IsRegex)
                                isVisible = !ng.RegexWord.IsMatch(r.Sentence);
                            else
                                isVisible = !r.Sentence.Contains(ng.Word);
                            break;
                        }
                    case AboneType.Mail:
                        {
                            if (ng.IsRegex)
                                isVisible = !ng.RegexWord.IsMatch(r.Mail);
                            else
                                isVisible = !r.Mail.Contains(ng.Word);
                            break;
                        }
                    case AboneType.ResNumber:
                        throw new NotSupportedException();
                    case AboneType.ChainResNumber:
                        throw new NotSupportedException();
                    default:
                        throw new ArgumentException();
                }
                if (!isVisible)
                    return false;
            }
            return true;
        }
        /// <summary>
        /// 不必要なNGWordを削除します
        /// </summary>
        /// <param name="dt">削除の基準となる時間</param>
        public async static Task RearrangeNGWords(DateTime dt)
        {
            List<NGWord> nh = new List<NGWord>();
            List<NGWord> okWord = new List<NGWord>();
            nh.AddRange(ReadAboneFiles());
            foreach (NGWord n in nh)
            {
                if (n.SetTime != DateTime.MinValue)
                {
                    if (n.SetTime.Add(n.ReleaseTime) >= dt)
                    {
                        okWord.Add(n);
                    }
                }
            }
            await WriteAboneFiles(okWord.ToArray());
        }
    }
    /// <summary>
    /// あぼーんの種類を表します
    /// </summary>
    public enum AboneType
    {
        /// <summary>
        /// IDあぼーん
        /// </summary>
        ID,
        /// <summary>
        /// 名前欄のあぼーん(含スレタイ)
        /// </summary>
        Name,
        /// <summary>
        /// メール欄のあぼーん
        /// </summary>
        Mail,
        /// <summary>
        /// 本文あぼーん
        /// </summary>
        Sentence,
        /// <summary>
        /// レス番あぼーん 
        /// </summary>
        ResNumber,
        /// <summary>
        /// 連鎖レス番あぼーん
        /// </summary>
        ChainResNumber
    }

}
