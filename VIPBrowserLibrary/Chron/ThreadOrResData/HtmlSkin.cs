using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using VIPBrowserLibrary.Utility;
using SU = VIPBrowserLibrary.Utility.StringUtility;
using System.Drawing;

namespace VIPBrowserLibrary.Chron.ThreadOrResData
{
    /// <summary>
    /// Htmlのスキンを用いたレスの変換を行います
    /// </summary>
    public class HtmlSkin : ResConvert
    {
        private Setting.GeneralSetting gs = new Setting.GeneralSetting();
        StringBuilder SkinBuilder { get; set; }
        private string HeaderSkin = @"<html>
<head>
<title><THREADNAME/></title>
<style type=""text/css"">
a:hover {
	color: #ff0000;
	text-decoration: underline;
}
</style>
</head>
<body bgcolor=""#efefef"">
<font face=""ＭＳ Ｐゴシック"">
<dl>";
        private string SkinPath = "";
        private string ResSkin = @"<dt><INDEX/><NUMBER/> 名前：<font color=""green""><NAME/></b></font> [<MAIL/>] 投稿日：<DATEONLY/><a href="" method:extract($3,{0})"" style="" color:black;"" target="" _blank""><ID/></a> <BE /></dt><dd><MESSAGE/><br><br></dd>";
        /// <summary>
        /// このクラスの新しいインスタンスを初期化します
        /// </summary>
        public HtmlSkin()
        {
            SkinBuilder = new StringBuilder();
            this.ColorDataList = LoadColoringData();
        }
        /// <summary>
        /// IDデータのリストを表します
        /// </summary>
        protected List<ColoringData> ColorDataList { get; set; }
        /// <summary>
        /// IDのカラーリング情報を読み込みます
        /// </summary>
        /// <returns>読み込んだデータ</returns>
        public static List<ColoringData> LoadColoringData()
        {
            Setting.GeneralSetting gs = new Setting.GeneralSetting();
            var dat = new List<ColoringData>();
            string path = gs.OtherFolderPath + "\\coloring.dat";
            string[] data = System.IO.File.ReadAllLines(path,Encoding.UTF8);
            foreach (var item in data)
            {
                string[] splitString = item.Split('\\');
                ColoringData cd = new ColoringData();
                cd.IDColor = ColorTranslator.FromHtml(splitString[0]);
                cd.Max = Int32.Parse(splitString[1]);
                cd.Min = Int32.Parse(splitString[2]);
                dat.Add(cd);
            }
            return dat;
        }
        /// <summary>
        /// スキンを読み込みます
        /// </summary>
        /// <param name="skinPath">読み込むスキンのパス</param>
        public void LoadSkin(string skinPath)
        {
            if (String.IsNullOrWhiteSpace(skinPath))
                return;
            //Header,Res,NewRes,Footer
            string header = TextUtility.Read(skinPath + "\\Header.html");
            string res = TextUtility.Read(skinPath + "\\Res.html");
            //string foot = TextUtility.Read(skinPath + "\\Footer.html");
            if (!String.IsNullOrEmpty(header) || !String.IsNullOrWhiteSpace(res) /*|| !String.IsNullOrEmpty(foot)*/) 
            {
                this.ResSkin = res;
                this.HeaderSkin = header;
            }
        }
        /// <summary>
        /// ヘッダーを変換し追加します
        /// </summary>
        /// <param name="td">使用するスレッド名</param>
        /// <returns>変換後のデータ</returns>
        public string AddHeader(string td)  
        {
            HeaderSkin.Replace("<THREADNAME/>", td);
            HeaderSkin.Replace("<SKINPATH/>", this.SkinPath);

            return this.HeaderSkin;
        }
        /// <summary>
        /// レスの変換を行います
        /// </summary>
        /// <param name="r">変換するレス</param>
        /// <param name="idData">レスに関連付けられたIDデータ</param>
        /// <param name="rs">元のRes</param>
        /// <returns>変換したString</returns>
        protected override string SimpleConvertCore(Res r, Dictionary<string, int[]> idData, out Res rs)
        {
            rs = r;
            if (!r.Visible)
                return "";

            StringBuilder sb = new StringBuilder();
            string id = r.ID;

            //string data = idData[id][0].ToString() + "/" + idData[id][1].ToString();
            string idString = this.ColoringId(id, idData);
            //if (idData[id][1] > 4)
            //{
            //    //idString = "<span style='color:#00F; text-decoration:underline;'>" + idData + "</span>" + " [" + ((int[])id[idData])[0] + "/" + ((int[])id[idData])[1] + "]";
            //    idString = String.Format(@"<a href=""method:Extract($3,{0})"" style=""color:red;"" target=""_blank"">{0}</a>({1})", id, data);
            //}
            //else if ((idData[id])[1] > 1)
            //{
            //    // idString = "<span style='color:#F00; text-decoration:underline;'>" + idData + "</span>" + " [" + ((int[])id[idData])[0] + "/" + ((int[])id[idData])[1] + "]";
            //    idString = String.Format(@"<a href=""method:Extract($3,{0})"" style=""color:blue;"" target=""_blank"">{0}</a>({1})", id, data);
            //}
            //else if ((idData[id])[1] == 1)
            //{
            //    //   idString = "<span style='color:#000; text-decoration:underline;'>" + idData + "</span>" + " [" + ((int[])id[idData])[0] + "/" + ((int[])id[idData])[1] + "]";
            //    idString = String.Format(@"<a href=""method:Extract($3,{0})"" style=""color:black;"" target=""_blank"">{0}</a>({1})", id, data);
            //}
            //else
            //{
            //    idString = "<span style='color:#000; text-decoration:underline;'>" + id + "</span>";
            //}
            string sentence = Regex.Replace(r.Sentence, @"<a\s+.*?>", "");
            sentence = sentence.Replace("</a>", "");
            sentence = SU.Linking(sentence);
            sentence = rs.Sentence = Regex.Replace(sentence, @"&gt;&gt;(?<res>\d+)",
                (m) =>
                {
                    return "<a href=\"#\">&gt;&gt;" + m.Groups["res"].Value + "</a>";
                });//<a +.*?>(.*?)</a>
            string idDatas = String.Empty;
            idDatas = r.ID + "(" + idData[r.ID][0].ToString() + "/" + idData[r.ID][1].ToString() + ")";
            sb.Append(this.ResSkin);
            //<b><a href=""menu:{0}"" name=""{0}"" target=""_blank"">{0}</a></b>
            string idx = r.Index.ToString();
            sb.Replace("<PLAINNUMBER/>", @"<a href=""menu:" + idx + @""" name="""  + idx + @""" target=""_blank"">" + idx + "</a>");
            sb.Replace("<NUMBER/>", @"<a href=""menu:" + idx + @""" name=""" + idx + @""" target=""_blank"">" + idx + "</a>");
            sb.Replace("<MAILNAME/>", r.Mail);
            sb.Replace("<SKINPATH/>", this.SkinPath);
            sb.Replace("<ID/>", idString);
            sb.Replace("<BE/>", r.BE);
            sb.Replace("<NAME/>", r.Name);
            sb.Replace("<MAIL/>", r.Mail);
            sb.Replace("<DATE/>", r.Date);
            sb.Replace("<DATEONLY/>", r.Date);
            sb.Replace("<MESSAGE/>", sentence);
            return sb.ToString();
        }
        /// <summary>
        /// IDに色付けします
        /// </summary>
        /// <param name="id">色付け対象のID</param>
        /// <param name="idCollection">スレのIDデータ</param>
        /// <returns>色付けを行ったID</returns>
        protected string ColoringId(string id,Dictionary<string,int[]> idCollection) 
        {
            bool isComplete = false;
            string idString = String.Empty;
            int count = idCollection[id][1];
            foreach (var item in this.ColorDataList)
            {
                if (count >= item.Min && count <= item.Max)
                {
                    isComplete = true;
                    string ids = idCollection[id][0].ToString() + "/" + idCollection[id][1].ToString();
                    idString = String.Format("<a href=\"method:Extract($3,{0})\" style=\"color:"+System.Drawing.ColorTranslator.ToHtml(item.IDColor)+"\" target=\"_blank\">{0}</a>({1})",id,ids);
                    break;
                }
            }
            if (!isComplete) 
            {
                idString = "<span style='color:#000; text-decoration:underline;'>" + id + "</span>";
            }
            return idString;
        }

    }
}
