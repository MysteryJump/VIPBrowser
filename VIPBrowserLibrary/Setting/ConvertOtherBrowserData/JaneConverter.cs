using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Net;

namespace VIPBrowserLibrary.Setting.ConvertOtherBrowserData
{
    /// <summary>
    /// JaneStyleからのデータの変換を行います
    /// </summary>
    public class JaneConverter : ConverterBase , IOtherDataConverter
    {
        string settingText = null;
        private Dictionary<string, string>[] SettingDictionary;
        /// <summary>
        /// 読み込み先のパスを使用してこのクラスの新しいインスタンスを初期化します
        /// </summary>
        /// <param name="path"></param>
        public JaneConverter(string path) : base(path)
        {
            this.settingText = File.ReadAllText(path + "\\Jane2ch.ini");
            this.ConvertDictionary();
        }
        /// <summary>
        /// NGワードを変換します
        /// </summary>
        public override void ConvertNGList()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 過去ログファイルを変換します
        /// </summary>
        public override void ConvertLog()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// AAリストを変換します
        /// </summary>
        public override void ConvertAAList()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// お気に入り一覧を変換します
        /// </summary>
        public override void ConvertFavorite()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// クッキーファイルを変換します
        /// </summary>
        public override void ConvertCookie()
        {
            CookieCollection cc = new CookieCollection();
            //10
            var test = this.SettingDictionary[10];
            string cookies = test["WrtCookie"];
            string[] splitCookie = cookies.Split(';');
            foreach (var item in splitCookie)
            {
                string[] splitKeyAndValue = item.Split('=');
                Cookie c = new Cookie(splitKeyAndValue[0], splitKeyAndValue[1],"/",".2ch.net");
                cc.Add(c);
            }
        }

        private void ConvertDictionary() 
        {
            Dictionary<string, string>[] dic;
            int i = 0;
            string[] datas = this.settingText.Split(new string[] { "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            dic = new Dictionary<string, string>[datas.Length];
            foreach (var item in datas)
            {
                dic[i] = new Dictionary<string, string>();
                foreach (var items in item.Split(new string[]{"\r\n"}, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (Regex.IsMatch(items,@"\[\w+\]"))
                        continue;
                    int splitIdx = items.IndexOf("=");
                    string key = items.Substring(0, splitIdx);
                    string value = items.Substring(splitIdx);
                    dic[i].Add(key, value.TrimStart('='));
                }
                i++;
            }
            this.SettingDictionary = dic;
        }

    }
}
