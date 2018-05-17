using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VIPBrowserLibrary.Utility;

namespace VIPBrowserUnitTestProject
{
    [TestClass]
    public class UnitTest8
    {
        private readonly string[] urls = 
        {
            "http://uravip.tonkotsu.jp/news7vip/",
            "https://translate.google.co.jp/?hl=ja&tab=wT&authuser=0#auto/ja/Url%20To%20Host%20Address",
            "http://ja.wikipedia.org/wiki/%E6%B0%B4%E5%95%86%E5%A3%B2",
            "http://u-note.me/note/1037?page=2"
        };
        private readonly string[] hosts = 
        {
            "uravip.tonkotsu.jp",
            "translate.google.co.jp",
            "ja.wikipedia.org",
            "u-note.me"
        };
        [TestMethod]
        public void HostToUrlTest()
        {
            for (int i = 0; i < hosts.Length; i++)
            {
                string baseHost = hosts[i];
                string convertHost = StringUtility.HostToUrl(urls[i]);
                Assert.AreEqual<string>(baseHost, convertHost);
            }
        }
        [TestMethod]
        public void MyTestMethod()
        {

        }
    }
}
