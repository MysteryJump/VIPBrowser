using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace VIPBrowserLibrary.Common
{
    /// <summary>
    /// 指定したURLの形式を別のURLの形式に変更します
    /// </summary>
    public static class URLParse
    {
        /// <summary>
        /// DatのURLをbbs.cgiとその他bbs.cgiに送信するデータに変換します
        /// </summary>
        /// <param name="datUrl">datのURL</param>
        /// <param name="bt">板の帰属元</param>
        /// <param name="t">スレッドか新規スレか</param>
        /// <param name="key">抽出したスレッドキー</param>
        /// <param name="folder">抽出したフォルダー</param>
        /// <returns>bbs.cgiのアドレス</returns>
        static public string DatToBBScgi(string datUrl,BBSType bt,Type t,out string key,out string folder)
        {
            if (bt == BBSType._2ch)
            {
                if (Type.thread == t)
                {
                    Match m = new Regex(@"http://(?<host>.+)/(?<folder>.+)/dat/(?<key>\d{9,10})[.]dat",RegexOptions.Compiled).Match(datUrl);
                    key = m.Groups["key"].Value;
                    folder = m.Groups["folder"].Value;
                    return "http://" + m.Groups["host"].Value + "/test/bbs.cgi";
                }
                else
                {
                    Match m = new Regex(@"http://(?<host>.+)/(?<folder>.+)/",RegexOptions.Compiled).Match(datUrl);
                    key = String.Empty;
                    folder = m.Groups["folder"].Value;
                    return "http://" + m.Groups["host"].Value + "/test/bbs.cgi";
                }
            }
            else if(bt == BBSType.jbbs)
            {
                if (t == Type.thread)
                {
                    //http://jbbs.livedoor.jp/bbs/rawmode.cgi/news/5720/1222345678/
                    Match m = Regex.Match(datUrl, @"http://jbbs([.]shitaraba.net|[.]livedoor[.]jp)/bbs/rawmode[.]cgi/(?<category>\w+)/(?<folder>\d+)/(?<key>\d{9,10})/?",RegexOptions.Compiled);
                    key = m.Groups["key"].Value;
                    folder = m.Groups["category"].Value + "/" + m.Groups["folder"].Value;
                    return "http://jbbs.shitaraba.net/bbs/write.cgi";
                }
                else
                {
                    //http://jbbs.livedoor.jp/news/5720/
                    Match m = Regex.Match(datUrl, @"http://jbbs.(shitaraba.net|livedoor.jp)/(?<category>\w+)/(?<folder>\d+)/?", RegexOptions.Compiled);
                    key = String.Empty;
                    folder = m.Groups["category"].Value + "/" + m.Groups["folder"].Value;
                    return "";
                }
            }
            else
            {
                throw new NotSupportedException();
            }
        }
        /// <summary>
        /// DatのURLをread.cgiのURLに変換します
        /// </summary>
        /// <param name="datUrl">datのURL</param>
        /// <param name="bt">板の帰属元</param>
        /// <returns>read.cgi形式のURL</returns>
        static public string DatToReadcgi(string datUrl, BBSType bt)
        {
            if (bt == BBSType._2ch)
            {
                Match m = new Regex(@"http://(?<host>.+)/(?<folder>.+)/dat/(?<key>\d{9,10})/?", RegexOptions.Compiled).Match(datUrl);
                if (m.Success)
                {
                    return "http://" + m.Groups["host"].Value + "/test/read.cgi/" + m.Groups["folder"].Value + "/" + m.Groups["key"].Value + "/";
                }
                else
                    throw new ArgumentException();
            }
            else if (bt ==  BBSType.jbbs)
            {
                Match m = new Regex(@"http://jbbs.(shitaraba.net|livedoor.jp)/bbs/rawmode.cgi/(?<category>\w+)/(?<number>\d+)/(?<key>\d{9,10})/?", RegexOptions.Compiled).Match(datUrl);
                if (m.Success)
                {
                    return "http://jbbs.shitaraba.net/bbs/read.cgi/" + m.Groups["category"].Value + "/" + m.Groups["number"].Value + "/" + m.Groups["key"].Value + "/";
                }
                else
                    throw new ArgumentException();
            }
            else if (bt == BBSType.machibbs)
            {
                Match m = Regex.Match(datUrl, @"http://(?<host>\w+)[.]machi[.]to/bbs/offlaw.cgi/(?<folder>\w+)/(?<key>\d{9,10})/?", RegexOptions.Compiled);
                if (m.Success)
                {
                    return "http://" + m.Groups["host"].Value + ".machi.to/bbs/read.cgi/" + m.Groups["folder"].Value + "/" + m.Groups["key"].Value + "/";
                }
                else
                {
                    var sm = Regex.Match(datUrl, @"http://(?<host>\w+)[.]machi[.]to/(?<folder>\w+)/dat/(?<key>\d{9,10})[.]dat", RegexOptions.Compiled);
                    if (sm.Success)
                    {
                        return "http://" + sm.Groups["host"].Value + ".machi.to/bbs/read.cgi/" + sm.Groups["folder"].Value + "/" + sm.Groups["key"].Value + "/";
                    }
                    throw new ArgumentException();
                }
            }
            else
            {
                throw new ArgumentException();
            }
        }
        /// <summary>
        /// read.cgi形式のURLをdat形式のURLに変換します
        /// </summary>
        /// <param name="readUrl">read.cgiのURL</param>
        /// <param name="bt">板の帰属元</param>
        /// <returns>dat形式のURL</returns>
        static public string ReadcgiToDat(string readUrl, BBSType bt)
        {
            if (bt == BBSType._2ch)
            {
                Match m = new Regex(@"http://(?<host>.+)/test/read.cgi/(?<folder>.+)/(?<key>\d{9,10})/?", RegexOptions.Compiled).Match(readUrl);
                return String.Concat("http://", m.Groups["host"].Value, "/", m.Groups["folder"].Value, "/dat/", m.Groups["key"].Value, ".dat");
            }
            else if (bt == BBSType.jbbs)
            {
                Match m = Regex.Match(readUrl, @"http://jbbs.(shitaraba.net|livedoor.jp)/bbs/read.cgi/(?<category>\w+)/(?<folder>\d+)/(?<key>\d{9,10})/?", RegexOptions.Compiled);
                return "http://jbbs.shitaraba.net/bbs/rawmode.cgi/" + m.Groups["category"].Value + "/" + m.Groups["folder"].Value + "/" + m.Groups["key"].Value + "/";

            }
            else if (bt == BBSType.machibbs)
            {
                // もしかしてString.Replaceでおｋ？まちBBS関連すべて
                //Match m = Regex.Match(readUrl, @"http://(?<host>\w+)[.]machi[.]to/bbs/offlaw.cgi/", RegexOptions.Compiled);
                return readUrl.Replace("read.cgi","offlaw.cgi");
            }
            else
                throw new NotSupportedException();
        }
        /// <summary>
        /// dat形式のURLを板のアドレスに変換します
        /// </summary>
        /// <param name="datUrl">datのURL</param>
        /// <param name="bt">板の帰属元</param>
        /// <returns>板のアドレス</returns>
        static public string DatToFolder(string datUrl,BBSType bt)
        {
            if (bt == BBSType._2ch)
            {
                return Regex.Replace(datUrl, @"dat/\d{9,10}[.]dat", "",RegexOptions.Compiled);
            }
            else if (bt == BBSType.jbbs)
            {
                //http://jbbs.livedoor.jp/news/5720
                //http://jbbs.livedoor.jp/bbs/rawmode.cgi/news/5720/**********/
                Match m = Regex.Match(datUrl, "http://jbbs.(shitaraba.net|livedoor.jp)/bbs/rawmode.cgi/(?<category>.+)/(?<folder>.+)/", RegexOptions.Compiled);
                return "http://jbbs.shitaraba.net/" + m.Groups["category"].Value + "/" + m.Groups["folder"].Value + "/";
            }
            else
            {
                //http://kinki.machi.to/osaka/
                //http://kinki.machi.to/bbs/offlaw.cgi/osaka/*****************/
                Match m = Regex.Match(datUrl, @"http://(?<host>\w+)[.]machi[.]to/bbs/offlaw.cgi/(?<folder>\w+)/", RegexOptions.Compiled);
                return  "http://" + m.Groups["host"] +".machi.to/" + m.Groups["folder"].Value + "/";
            }
        }
        /// <summary>
        /// read.cgi形式のURLをofflaw2形式のURLに変換します
        /// </summary>
        /// <param name="readUrl">read.cgiのURL</param>
        /// <returns>変換後のofflaw2.so</returns>
        static public string ReadcgiToOfflaw2(string readUrl)
        {
            Match m = Regex.Match(readUrl, @"http://(?<host>\w+)[.](?<type>2ch[.]net|bbspink[.]com)/test/read.cgi/(?<folder>\w+)/(?<key>\d{9,10})/?", RegexOptions.Compiled);
            return String.Format("http://{0}.{1}/test/offlaw2.so?shiro=kuma&sid=ERROR&bbs={2}&key={3}",m.Groups["host"].Value, m.Groups["type"].Value,m.Groups["folder"].Value,m.Groups["key"].Value);
        }
    }
}
