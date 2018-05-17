using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VIPBrowserLibrary.Setting;

namespace VIPBrowserLibrary.Common
{
    /// <summary>
    /// 板のデータを取得するクラスです。このクラスは継承できません
    /// </summary>
    public static class GetBoardData
    {
        static GeneralSetting gs = new GeneralSetting();
        private static async Task<string> ReadBoardData(string url, bool isUpdate)
        {
            if (url.IndexOf("jbbs.livedoor.jp") != -1 || url.Contains("jbbs.shitaraba.net"))
            {


                System.Text.RegularExpressions.Match m = new System.Text.RegularExpressions.Regex(@"http://jbbs.(livedoor.jp|shitaraba.net)/(?<category>.+)/(?<number>\d+)/", System.Text.RegularExpressions.RegexOptions.Compiled).Match(url);
                try
                {
                    if (isUpdate)
                        throw new System.IO.FileNotFoundException();
                    string data = await Utility.TextUtility.ReadAsync(gs.BoardInfoFilePath + "\\" + m.Groups["category"].Value + "-" + m.Groups["number"].Value + ".txt");
                    if (data == null)
                        throw new System.IO.FileNotFoundException();
                    return data;
                }
                catch (System.IO.FileNotFoundException)
                {
                    BBS.Common.HttpClient hc = new BBS.Common.HttpClient(String.Format("http://jbbs.shitaraba.net/bbs/api/setting.cgi/{0}/{1}/", m.Groups["category"].Value, m.Groups["number"].Value));
                    hc.Encoding = "EUC-JP";
                    string data = hc.GetStringSync();
                    Utility.TextUtility.Write(gs.BoardInfoFilePath + "\\" + m.Groups["category"].Value + "-" + m.Groups["number"].Value + ".txt", data, false);
                    return data;
                }
            }
            else if (url.Contains("machi.to"))
            {
                return String.Empty;
            }
            else
            {
                System.Text.RegularExpressions.Match m = new System.Text.RegularExpressions.Regex("http://(?<host>.+)/(?<folder>.+/)").Match(url);
                try
                {
                    if (isUpdate)
                        throw new System.IO.FileNotFoundException();
                    string data = await Utility.TextUtility.ReadAsync(gs.BoardInfoFilePath + "\\" + m.Groups["host"].Value + "-" + m.Groups["folder"].Value.Replace("/", "") + ".txt");
                    if (data == null)
                        throw new System.IO.FileNotFoundException();
                    return data;
                }
                catch (System.IO.FileNotFoundException)
                {
                    BBS.Common.HttpClient hc = new BBS.Common.HttpClient(url + "SETTING.TXT");
                    hc.Encoding = "Shift-JIS";
                    string data = hc.GetStringSync();
                    Utility.TextUtility.Write(gs.BoardInfoFilePath + "\\" + m.Groups["host"].Value + "-" + m.Groups["folder"].Value.Replace("/", "") + ".txt", data, false);
                    return data;
                }
            }
        }
        // このメソッドは非能率的だから変更予定
        // と思ったがこのメソッドは今後も使用予定
        /// <summary>
        /// <para>板の名前を取得します</para>
        /// <para>（このメソッドは過去との互換性のためだけに残されています。
        /// 板名を取得する際は基本GetBoardDictionaryメソッドを使用してください）</para>
        /// </summary>
        /// <param name="url">指定するURL</param>
        /// <param name="isUpdate">更新する必要があるか</param>
        /// <returns>String形式の板名</returns>
        //[Obsolete("GetBoardData.GetBoardDataDictionryメソッドを使用してください")]
        public static async Task<string> GetBoardName(string url, bool isUpdate)
        {
            if (url.Contains("machi.to"))
                return String.Empty;
            System.Text.RegularExpressions.Match m = new System.Text.RegularExpressions.Regex("BBS_TITLE=.+").Match(await GetBoardData.ReadBoardData(url, isUpdate));
            if (m.Success)
            {
                return m.Value.Replace("BBS_TITLE=", "").Split('＠')[0];
            }
            else
                throw new System.Net.WebException();
        }
        /// <summary>
        /// 板の情報をDictionary形式で返します
        /// </summary>
        /// <param name="url">指定するURL</param>
        /// <param name="isUpdate">更新する必要があるか</param>
        /// <returns>Dictionary形式の板データー</returns>
        public async static Task<Dictionary<string, string>> GetBoardDictionary(string url, bool isUpdate)
        {
            
            Dictionary<string, string> dic = new Dictionary<string, string>();
            string data = await ReadBoardData(url, isUpdate);
            string[] splitData = data.Split('\n');
            foreach (string item in splitData)
            {
                if (item == String.Empty)
                    break;
                string[] sd = item.Split('=');
                dic.Add(sd[0], sd[1]);
            }
            return dic;


        }
        /// <summary>
        /// 指定されたURL先の板のローカルルールを取得します
        /// </summary>
        /// <param name="boardUrl">取得先の板のURL</param>
        /// <returns>取得したデータ　失敗した場合はnullが入っています</returns>
        public static async Task<string> GetLocalRule(string boardUrl) 
        {
            var type = Common.TypeJudgment.BBSTypeJudg(boardUrl);
            if (type == BBSType._2ch || type == BBSType.jbbs)
                boardUrl += "head.txt";
            else
                return null;
            BBS.Common.HttpClient hc = new BBS.Common.HttpClient(boardUrl);
            return await hc.GetStringAsync();
        }
    }
}
