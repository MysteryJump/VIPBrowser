/*コメントアウト部分はすべてTwintailより拝借
 * それ以外はすべて独自コード
 */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using VIPBrowserLibrary.Utility;
using System.IO;
using SU = VIPBrowserLibrary.Utility.StringUtility;


namespace VIPBrowserLibrary.Chron.ThreadOrResData
{
    /// <summary>
    /// 指定されたレスを一つづつ変換します
    /// </summary>
    public abstract class ResConvert
    {
        //private HtmlSkin Skin;
        VIPBrowserLibrary.Setting.GeneralSetting gs = new Setting.GeneralSetting();
        //private static Regex rexBRSpace = new Regex(" <br> ", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        //private static Regex coloringId = new Regex("<COLORINGID(?<attr>.*?)/>", RegexOptions.Compiled);
        //private static Regex coloringIdAttribute = new Regex(@"n(?<key>[\d]+?)\=(?<value>[^\s]+)", RegexOptions.Compiled);
        //private string colorWordSkin = String.Empty;

        //private SortedList<int, string> newResIDColors = new SortedList<int, string>();
        //private SortedList<int, string> oldResIDColors = new SortedList<int, string>();

        //protected string newResSkin;
        //protected string resSkin;

        //private bool existColoringId = false;

//        public string Convert(Res r)
//        {
//            return ConvertCore(r);
//        }
//        public string Convert(ResCollection rc)
//        {
//            StringBuilder sb = new StringBuilder();
//            foreach (Res r in rc)
//            {
//                sb.AppendLine(ConvertCore(r));
//            }
//            return sb.ToString();
//        }
//        public string Convert(Res[] rc)
//        {
//            StringBuilder sb = new StringBuilder();
//            foreach (Res r in rc)
//            {
//                sb.AppendLine(ConvertCore(r));
//            }
//            return sb.ToString();
//        }

        //protected virtual string ConvertCore(/*string skinhtml, */Res res)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    string name;
        //    string mailname;
        //    string dateonly, dateString;
        //    string body;
        //    string mail = res.Mail;

        //    #region 名前の作成

        //    sb.Append("<b>");
        //    sb.Append(res.Name);
        //    sb.Append("</b>");
        //    name = sb.ToString();
        //    sb.Clear();
        //    #endregion

        //    #region Email付き名前の作成
        //    if (mail != String.Empty)
        //    {
                

        //        if (mail == "sage")
        //        {
        //            sb.Append("<font color=\"red\">↓</font>");
        //        }
        //        else
        //        {
        //            sb.Append("<a href=\"mailto:");
        //            sb.Append(res.Mail);
        //            sb.Append("\">");
        //            sb.Append(name);
        //            sb.Append("</a>");
        //        }
        //        mailname = sb.ToString();
        //        sb.Clear();
        //    }
        //    else
        //    {
        //        mailname = name;
        //    }
        //    #endregion

        //    #region 日付とIDを作成
        //    dateString = res.Date;
        //    dateonly = res.Date;
        //    Match m = Regex.Match(res.Date, "( ID:)|(\\[)");

        //    if (m.Success)
        //    {
        //        dateonly = res.Date.Substring(0, m.Index);
        //    }
        //    #endregion

        //    #region Be2chIDのリンクを貼る
        //    //            // BE:0123456-# または <BE:0123456:0> 形式の二つあるみたい
        //    //            dateString =
        //    //                Regex.Replace(dateString, @"BE:(?<id>\d+)\-(?<rank>.+)",
        //    //                "<a href=\"http://be.2ch.net/test/p.php?i=${id}\" target=\"_blank\">?${rank}</a>", RegexOptions.IgnoreCase);

        //    //            dateString =// 面白ネタnews形式
        //    //                Regex.Replace(dateString, @"<BE:(?<id>\d+):(?<rank>.+)>",
        //    //                "<a href=\"http://be.2ch.net/test/p.php?i=${id}\" target=\"_blank\">Lv.${rank}</a>", RegexOptions.IgnoreCase);
        //    #endregion

        //    #region 本分を作成
        //    body = res.Sentence;
        //    body = Regex.Replace(body, @"&gt;&gt;(?<res>\d+)",
        //                     (d) =>
        //                     {
        //                         return "<a href=\"#\">&gt;&gt;" + d.Groups["res"].Value + "</a>";
        //                     });
        //    #endregion

        //    /* #region レス参照を作成
        //    /* body = StringUtility.RefRegex.Replace(body, "<a href=\"" + baseUri + "${num}\" target=\"_blank\">${ref}</a>");
        //    /* body = StringUtility.ExRefRegex.Replace(body, "<a href=\"#\">${num}</a>");
        //    /* #endregion */


        //    //            sb.Remove(0, sb.Length);
        //    //            sb.Append(skinhtml);
        //    //            sb.Replace("<PLAINNUMBER/>", res.Index.ToString());
        //    //            sb.Replace("<NUMBER/>", res.Index.ToString());
        //    //            sb.Replace("<MAILNAME/>", mailname);
        //    //            sb.Replace("<ID/>", res.ID);
        //    //            sb.Replace("<BE/>", res.BE);
        //    //            sb.Replace("<NAME/>", name);
        //    //            sb.Replace("<MAIL/>", res.Mail);
        //    //            sb.Replace("<DATE/>", dateString);
        //    //            sb.Replace("<DATEONLY/>", dateonly);
        //    //            sb.Replace("<MESSAGE/>", body);
        //    //            sb.Replace("<SKINPATH/>", skinPath);

        //    //            skinhtml = sb.ToString();

        //    //            return skinhtml;

        //    return String.Empty;
        //}
        /// <summary>
        /// 単純なレスの変換を行います
        /// </summary>
        /// <param name="r">指定したレスのデータ</param>
        /// <returns>変換したHTML</returns>
        public static string SimpleConvertCore(Res r)
        {
            StringBuilder sb = new StringBuilder();
            string sentenceData = r.Sentence;
            sentenceData = Regex.Replace(sentenceData, @"&gt;&gt;(?<res>\d+)",
                (m) =>
                {
                    return "<a href=\"#\">&gt;&gt;" + m.Groups["res"].Value + "</a>";
                });
            
            sb.Append("<dt id=\"s").Append(r.Index).Append("\" class=\"\">");
            sb.Append("<indices id=\"").Append(r.Index).Append("\"></indices>");
            sb.AppendFormat(@"<b><a href=""menu:{0}"" name=""{0}"" target=""_blank"">{0}</a></b>", r.Index);
            sb.AppendFormat(@" 名前：<font color=""green""><b>{0}</b></font>", r.Name);
            sb.AppendFormat(@" [{0}] ", r.Mail);
            sb.AppendFormat(@"投稿日：{0} ", r.Date);
            sb.Append(/*@"<a href=""method:Extract($3,{0})"" style=""color:black;"" target=""_blank"">{1}</a>", idData, */r.ID);
            sb.AppendFormat(@"  <a href=""method:Extract($5,)"" style=""color:black;"" target=""_blank""></a> </dt><dd>{0}<br><br></dd>", sentenceData);
            sb.AppendLine();

            return sb.ToString();
        }
        /// <summary>
        /// レスの変換を行います
        /// </summary>
        /// <param name="r">指定したレスのデータ</param>
        /// <param name="idData">IDの情報を収めたデータ</param>
        /// <param name="rs">元のRes構造体</param>
        /// <returns>変換したHTML</returns>
        protected virtual String SimpleConvertCore(Res r, Dictionary<string, int[]> idData,out Res rs)
        {
            rs = r;
            if (!r.Visible) 
                return "";
            
            StringBuilder sb = new StringBuilder();
            string id = r.ID;
            string data = idData[id][0].ToString() + "/" + idData[id][1].ToString();
            string idString = String.Empty;
            if (idData[id][1] > 1)
            {
                //idString = "<span style='color:#00F; text-decoration:underline;'>" + idData + "</span>" + " [" + ((int[])id[idData])[0] + "/" + ((int[])id[idData])[1] + "]";
                idString = String.Format(@"<a href=""method:Extract($3,{0})"" style=""color:black;"" target=""_blank"">{0}</a>({1})", id, data);
            }
            else if ((idData[id])[1] > 3)
            {
                // idString = "<span style='color:#F00; text-decoration:underline;'>" + idData + "</span>" + " [" + ((int[])id[idData])[0] + "/" + ((int[])id[idData])[1] + "]";
                idString = String.Format(@"<a href=""method:Extract($3,{0})"" style=""color:black;"" target=""_blank"">{0}</a>({1})", id, data);
            }
            else if ((idData[id])[1] == 1)
            {
                //   idString = "<span style='color:#000; text-decoration:underline;'>" + idData + "</span>" + " [" + ((int[])id[idData])[0] + "/" + ((int[])id[idData])[1] + "]";
                idString = String.Format(@"<a href=""method:Extract($3,{0})"" style=""color:black;"" target=""_blank"">{0}</a>({1})", id, data);
            }
            else
            {
                idString = "<span style='color:#000; text-decoration:underline;'>" + id + "</span>";
            }
            string sentence = Regex.Replace(r.Sentence, @"<a\s+.*?>", "");
            sentence = sentence.Replace("</a>", "");
            sentence = SU.Linking(sentence);
            sentence = rs.Sentence = Regex.Replace(sentence, @"&gt;&gt;(?<res>\d+)",
                (m) =>
                {
                    return "<a href=\"#\">&gt;&gt;" + m.Groups["res"].Value + "</a>";
                });//<a +.*?>(.*?)</a>
            string idDatas = String.Empty;
            idDatas = r.ID+"("+idData[r.ID][0].ToString() +"/"+ idData[r.ID][1].ToString()+")";
            sb.Append("<dt id=\"s").Append(r.Index).Append("\" class=\"\">");
            sb.Append("<indices id=\"").Append(r.Index).Append("\"></indices>");
            sb.AppendFormat(@"<b><a href=""menu:{0}"" name=""{0}"" target=""_blank"">{0}</a></b>", r.Index);
            sb.AppendFormat(@" 名前：<font color=""green""><b>{0}</b></font>", r.Name);
            sb.AppendFormat(@" [{0}] ", r.Mail);
            sb.AppendFormat(@"投稿日：{0} ", r.Date);
            sb.AppendFormat(@"<a href=""method:Extract($3,{0})"" style=""color:black;"" target=""_blank"">{1}</a>", r.ID,idDatas);
            sb.AppendFormat(@"  <a href=""method:Extract($5,)"" style=""color:black;"" target=""_blank""></a> </dt><dd>{0}<br><br></dd>", sentence);
            sb.AppendLine();
            return sb.ToString();
        }
        ///// <summary>
        ///// 指定した
        ///// </summary>
        //public ResConvert() 
        //{
        //    //this.Skin = new HtmlSkin();
        //    //Skin.LoadSkin();
        //}
        
        //protected virtual string LoadSkin()
        //{
        //    string skinf = gs.SkinFolderPath;

        //    colorWordSkin = new StreamReader(File.Open(Path.Combine(skinf, "ColorWord.html"), FileMode.Open)).ReadToEnd();
    

        //    if (colorWordSkin == String.Empty)
        //        colorWordSkin = "<font color=red><i>|</i></font>";

        //    MakeIDColorTable(newResIDColors, newResSkin);
        //    MakeIDColorTable(oldResIDColors, resSkin);

        //    return String.Empty;
        //}

        //private void MakeIDColorTable(SortedList<int, string> table, string html)
        //{
        //    Match m = coloringId.Match(html);
        //    MatchCollection matches = null;

        //    if (m.Success)
        //    {
        //        matches = coloringIdAttribute.Matches(m.Groups["attr"].Value);
        //        existColoringId = true;
        //    }

        //    if (existColoringId && matches.Count > 0)
        //    {
        //        foreach (Match attr in matches)
        //        {
        //            int count = Int32.Parse(attr.Groups["key"].Value);
        //            string color = attr.Groups["value"].Value;

        //            table.Add(count, color);
        //        }
        //    }
        //    else
        //    {
        //        table[30] = "red";
        //        table[20] = "#FF00FF";
        //        table[15] = "orange";
        //        table[10] = "limegreen";
        //        table[5] = "blue";
        //        table[2] = "purple";
        //    }
        //}
    }
}
