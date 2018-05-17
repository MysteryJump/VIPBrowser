using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace VIPBrowserLibrary.Common
{
    /// <summary>
    /// 形を確認するクラス
    /// </summary>
    public static class TypeJudgment
    {
        /// <summary>
        /// 指定したURLの形式を表示します
        /// </summary>
        /// <param name="url">形式を示すためのURL</param>
        /// <param name="bt">指定されたURLの板の帰属元</param>
        /// <param name="t">指定されたURLの形式</param>
        public static void AllJudg(string url, out BBSType bt, out Type t)
        {
            Setting.GeneralSetting gs = new Setting.GeneralSetting();
            if (url.Contains(gs.DatFilePath)) 
            {
                t = Type.thread;
                if (url.Contains("machi.to-"))
                {
                    bt = BBSType.machibbs;
                }
                else if(url.Contains("jbbs-"))
                {
                    bt = BBSType.jbbs;
                }
                else
                {
                    bt = BBSType._2ch;
                }
                return;
            }
            if (url.IndexOf("2ch.net") != -1 || url.IndexOf("bbspink.com") != -1)
            {
                bt = BBSType._2ch;
            }
            else
            {
                if (url.IndexOf("jbbs.livedoor.jp") != -1 || url.IndexOf("jbbs.shitaraba.net") != -1)
                {
                    bt = BBSType.jbbs;
                }
                else if(url.IndexOf("machi.to") != -1)
                {
                    bt = BBSType.machibbs;
                }
                else
                {
                    bt = BBSType._2ch;
                }
            }
            if (bt == BBSType._2ch)
            {
                if (new Regex(@"http://.+/test/read[.]cgi/.+/(\d{9,10}/.*|\d{9,10})", RegexOptions.Compiled).IsMatch(url) || new Regex(@"http://.+/.+/dat/\d{9,10}[.]dat", RegexOptions.Compiled).IsMatch(url))
                {
                    t = Type.thread;
                }
                else
                {
                    t = Type.threadlist;
                }
            }
            else if (bt == BBSType.jbbs)
            {
                if (new Regex(@"http://jbbs([.]shitaraba.net|[.]livedoor[.]jp)/bbs/read[.]cgi/.+/\d+/(\d{9,10}/.*|\d{9,10})", RegexOptions.Compiled).IsMatch(url) || Regex.IsMatch(url,@"http://jbbs.(livedoor.jp|shitaraba.net)/bbs/rawmode.cgi/\w+/\d+/\d{9,10}/?", RegexOptions.Compiled))
                {
                    t = Type.thread;
                }
                else
                {
                    t = Type.threadlist;
                }
            }
            else
            {
                if (new Regex(@"http://.+[.]machi[.]to/bbs/(read|offlaw)[.]cgi/.+/(\d{9,10}/.*|\d{9,10})", RegexOptions.Compiled).IsMatch(url))
                {
                    t = Type.thread;
                }
                else
                {
                    t = Type.threadlist;
                }
            }
        }
        /// <summary>
        /// 指定したURLの形式を表示します
        /// </summary>
        /// <param name="url">形式を表すURL</param>
        /// <returns>指定されたURLの形式</returns>
        public static Type TypeJudg(string url)
        {
            BBSType bt = BBSTypeJudg(url);
            if (bt == BBSType._2ch)
                if (new Regex(@"http://.+/test/read[.]cgi/.+/(\d{9,10}/.*|\d{9,10})", RegexOptions.Compiled).IsMatch(url) || new Regex(@"http://.+/.+/dat/\d{9,10}[.]dat", RegexOptions.Compiled).IsMatch(url))
                    return Type.thread;
                else
                    return Type.threadlist;
            else if (bt == BBSType.jbbs)
                if (new Regex(@"http://jbbs.(shitaraba.net|livedoor[.]jp)/bbs/read[.]cgi/.+/\d+/(\d{9,10}/.*|\d{9,10})", RegexOptions.Compiled).IsMatch(url) || Regex.IsMatch(url, @"http://jbbs.(livedoor.jp|shitaraba.net)/bbs/rawmode.cgi/\w+/\d+/\d{9,10}/?", RegexOptions.Compiled))
                    return Type.thread;
                else
                    return Type.threadlist;
            else
                if (new Regex(@"http://.+[.]machi[.]to/bbs/read[.]cgi/.+/(\d{9,10}/.*|\d{9,10})", RegexOptions.Compiled).IsMatch(url))
                    return Type.thread;
                else
                    return Type.threadlist;
        }
        /// <summary>
        /// 板の帰属元を判断します
        /// </summary>
        /// <param name="url">判断先のURL</param>
        /// <returns>判断したURLの帰属元</returns>
        public static BBSType BBSTypeJudg(string url)
        {
            if (url.Contains(".2ch.net") || url.Contains(".bbspink.com"))
                return BBSType._2ch;
            else if (url.Contains("jbbs.livedoor.jp") || url.Contains("jbbs.shitaraba.net"))
                return BBSType.jbbs;
            else if (url.Contains(".machi.to"))
                return BBSType.machibbs;
            else
                return BBSType._2ch;
        }
    }
    /// <summary>
    /// 板の帰属元を指定します
    /// </summary>
    public enum BBSType
    {
        /// <summary>
        /// 2ch
        /// </summary>
        _2ch = 0,
        /// <summary>
        /// したらば
        /// </summary>
        jbbs = 1,
        /// <summary>
        /// まちBBS
        /// </summary>
        machibbs = 2
    }
    /// <summary>
    /// URLの種類を指定します
    /// </summary>
    public enum Type
    {
        /// <summary>
        /// スレッドを表します
        /// </summary>
        thread = 10,
        /// <summary>
        /// スレッドリストを表します
        /// </summary>
        threadlist = 20
    }

}
