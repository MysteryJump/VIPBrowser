using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Threading.Tasks;
using VIPBrowserLibrary.BBS.Common;

namespace VIPBrowserUnitTestProject
{
    [TestClass]
    public class BBSTest
    {
        [TestMethod]
        public void HttpClientGetTest()
        {
            WebClient wec = new WebClient();
            string data = wec.DownloadString("http://uravip.tonkotsu.jp/news7vip/subject.txt");
            HttpClient hc = new HttpClient("http://uravip.tonkotsu.jp/news7vip/subject.txt");
            string unko = hc.GetStringSync();
            Assert.AreEqual<string>(data, unko);
        }
        //[TestMethod]
        public async Task HttpClientGetAsyncTest()
        {
            WebClient wec = new WebClient();
            string data = await wec.DownloadStringTaskAsync("http://uravip.tonkotsu.jp/news7vip/subject.txt");
            HttpClient hc = new HttpClient("http://uravip.tonkotsu.jp/news7vip/subject.txt");
            string unko = await hc.GetStringAsync();
            Assert.AreEqual<string>(data, unko);
        }


    }
}
