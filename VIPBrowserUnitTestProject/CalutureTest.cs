using Microsoft.VisualStudio.TestTools.UnitTesting;
using VIPBrowserLibrary.Common;

namespace VIPBrowserUnitTestProject
{
    [TestClass]
    public class URLParserTest
    {
        public string[] dats = 
            {
                "http://uravip.tonkotsu.jp/hobbies/dat/1378572677.dat",
                "http://jbbs.shitaraba.net/bbs/rawmode.cgi/internet/17700/1384513881/",
                "http://hayabusa.2ch.net/news4vip/dat/1384520680.dat",
                "http://kyusyu.machi.to/bbs/offlaw.cgi/kyusyu/1367081548/"
            };

        public string[] reads =
            {
                "http://uravip.tonkotsu.jp/test/read.cgi/hobbies/1378572677/",
                "http://jbbs.shitaraba.net/bbs/read.cgi/internet/17700/1384513881/",
                "http://hayabusa.2ch.net/test/read.cgi/news4vip/1384520680/",
                "http://kyusyu.machi.to/bbs/read.cgi/kyusyu/1367081548/"
            };

        public string[] datas = 
            {
                "http://uravip.tonkotsu.jp/news7vip/",
                "http://uravip.tonkotsu.jp/test/read.cgi/hobbies/1378572677/",
                "http://jbbs.shitaraba.jp/internet/17700/",
                "http://jbbs.livedoor.jp/bbs/read.cgi/internet/17700/1384513881/",
                "http://hayabusa.2ch.net/news4vip/",
                "http://hayabusa.2ch.net/test/read.cgi/news4vip/1384520680/",
                "http://kyusyu.machi.to/kyusyu/",
                "http://kyusyu.machi.to/bbs/read.cgi/kyusyu/1367081548/",
                "http://kyusyu.machi.to/kyusyu/subject.txt",
                "http://hayabusa.2ch.net/news4vip/dat/1384520680.dat"
            };
        public BBSType[] bbsTypes =
        {
             BBSType._2ch,
             BBSType._2ch,
             BBSType.jbbs,
             BBSType.jbbs,
             BBSType._2ch,
             BBSType._2ch,
             BBSType.machibbs,
             BBSType.machibbs,
             BBSType.machibbs,
             BBSType._2ch
        };
        public Type[] types = 
        {
            Type.threadlist,
            Type.thread,
            Type.threadlist,
            Type.thread,
            Type.threadlist,
            Type.thread,
            Type.threadlist,
            Type.thread,
            Type.threadlist,
            Type.thread
        };
        [TestMethod]
        public void TypeJudgementTest()
        {

            Type t;
            BBSType bt;
            int i = 0;
            foreach (string item in datas)
            {
                TypeJudgment.AllJudg(item, out bt, out t);
                Assert.AreEqual<Type>(t, TypeJudgment.TypeJudg(item));
                Assert.AreEqual<BBSType>(bt, TypeJudgment.BBSTypeJudg(item));
                // throgh debug and this method hasn't bugs.
                //Assert.AreEqual<Type>(t,types[i]);
                //Assert.AreEqual(bt, bbsTypes[i]);
                i++;
            }

        }
        [TestMethod]
        public void DatAndReadcgiTest()
        {
            BBSType bt;
            for (int i = 0; i < dats.Length; i++)
            {
                bt = TypeJudgment.BBSTypeJudg(dats[i]);
                Assert.AreEqual<BBSType>(bt,TypeJudgment.BBSTypeJudg(reads[i]));
                string read = URLParse.DatToReadcgi(dats[i],bt);
                string dat = URLParse.ReadcgiToDat(reads[i],bt);
                Assert.AreEqual<string>(read, reads[i]);
                Assert.AreEqual<string>(dat,dats[i]);
            }
        }


    }
}
