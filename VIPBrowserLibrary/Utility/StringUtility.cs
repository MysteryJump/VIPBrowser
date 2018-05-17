using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Text.RegularExpressions;


namespace VIPBrowserLibrary.Utility
{
    /// <summary>
    /// 指定された文字の変換作業などを行うクラスです。このクラスは継承できません。
    /// </summary>
    public static class StringUtility
    {


        /// <summary>
        /// ttp://のURLを検索する正規表現
        /// </summary>
        public static readonly Regex ttpToRegex =
            new Regex(@"(?<!h)(?<link>(ttp://[\w\.]+?/wiki/[^\s\<]+)|(ttps?://[a-zA-Z\d/_@#%&+*:;=~',.!()|?[\]\-]+))",
                RegexOptions.Compiled);

        /// <summary>
        /// 基本的なURLを検索する正規表現
        /// </summary>
        public static readonly Regex LinkRegex =
            new Regex(@"(?<link>((http://[\w\.]+?/wiki/[^\s\<]+)|((https?|ftps?|mms|rts?p)://[a-zA-Z\d/_@#%&+*:;=~',.!()|?[\]\-]+)))",
                RegexOptions.Compiled);






        /// <summary>
        /// 指定されたテキストをURLエンコードします(Shift-JIS)
        /// </summary>
        /// <param name="url">エンコードするテキスト</param>
        /// <returns>エンコード後のテキスト</returns>
        public static string URLEncode(string url)
        {
            return HttpUtility.UrlEncode(url,Encoding.GetEncoding("Shift-JIS"));
        }
        /// <summary>
        /// 指定されたテキストをエンコーディングを指定してURLエンコードします
        /// </summary>
        /// <param name="url">エンコードするテキスト</param>
        /// <param name="enc">エンコードする形式</param>
        /// <returns>エンコード後のテキスト</returns>
        public static string URLEncode(string url, Encoding enc)
        {
            return HttpUtility.UrlEncode(url, enc);
        }

        /// <summary>
        /// 指定したタグを取り除きます。
        /// </summary>
        /// <param name="html">除去する対象のHTML</param>
        /// <param name="tag"><para>除去するタグの名前。</para><para>OR演算子により複数指定できます。</para><para>大文字小文字は区別しません。</para></param>
        /// <returns>除去後のHTML</returns>
        public static string RemoveTag(string html, string tag)
        {
            return Regex.Replace(html, "</?(" + tag + ")[^>]*>", "", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 指定したhtml内のURLをAタグで挟みリンクする
        /// </summary>
        /// <param name="html">対象のHTML</param>
        /// <returns>置換後のHTML</returns>
        public static string Linking(string html)
        {
            if (html == null)
            {
                throw new ArgumentNullException("htmlがnullです");
            }
            string r = html;
            r = LinkRegex.Replace(r, "<a href=\"${link}\" target=\"_blank\">${link}</a>");
            r = ttpToRegex.Replace(r, "<a href=\"h${link}\" target=\"_blank\">${link}</a>");

            return r;
        }
        /// <summary>
        /// 指定したHTMLの禁則文字を変換する
        /// </summary>
        /// <param name="html">変換するテキスト</param>
        /// <returns>変換後のテキスト</returns>
        public static string HTMLEncode(string html)
        {
            return HttpUtility.HtmlEncode(html);
        }
        /// <summary>
        /// 指定したHTMLの禁則文字をもとの文字に直す
        /// </summary>
        /// <param name="html">変換するテキスト</param>
        /// <returns>変換後のテキスト</returns>
        public static string HTMLDecode(string html)
        {
            return HttpUtility.HtmlDecode(html);
        }
        /// <summary>
        /// 指定したURLをホストアドレスに変換します
        /// </summary>
        /// <param name="url">変換するURL</param>
        /// <returns>ホストアドレス</returns>
        public static string HostToUrl(string url) 
        {
            url = url.Replace("http://", "");
            url = url.Replace("https://", "");
            int idx = url.IndexOf('/');
            return url.Remove(idx);
        }
    }
}
