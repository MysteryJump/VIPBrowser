using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VIPBrowserLibrary.BBS.Common;
using VIPBrowserLibrary.Chron.ThreadOrResData;
using System.Collections.Generic;

namespace VIPBrowserUnitTestProject
{
    [TestClass]
    public class UnitTest3 : PostBase
    {
        [TestMethod]
        public void ResDictionaryDatasTest()
        {
            Res r = new Res(1, "", "", "うあああああああああああああああああああああああああああああああああああああああああああ",
                "SampleID", "2013/11/16 21:47:59.02", "BE", true);
            Dictionary<string, string> dic1 = new Dictionary<string, string>();
            dic1.Add("MESSAGE", "うあああああああああああああああああああああああああああああああああああああああああああ");
            dic1.Add("FROM", "");
            dic1.Add("mail", "");
            dic1.Add("key", "");
            dic1.Add("bbs", "");
            dic1.Add("subject", "");
            dic1.Add("time", "");
            var data = base.ParseDictionaryToRes(dic1, VIPBrowserLibrary.Common.BBSType._2ch);
            Assert.AreEqual<Res>(r, data);
        }
    }
}
