using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Anc = VIPBrowserLibrary.Chron.ThreadOrResData.Anchor;

namespace VIPBrowserUnitTestProject
{
    [TestClass]
    public class AnchorTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            Anc a = new Anc("56-89");
            PrivateObject po = new PrivateObject(a);
            var unko = po.GetField("data") as System.Collections.Generic.List<bool>;
            for (int i = 56; i < 90; i++)
			{
			    Assert.AreEqual<bool>(unko[i],true);
			}

            Anc aa = new Anc("89-79,879,456,235-851");
            unko = po.GetField("data") as System.Collections.Generic.List<bool>;
            for (int i = 89; i < 80; i++)
            {
                Assert.AreEqual<bool>(unko[i], true);
            }
            for (int i = 235; i < 852; i++)
            {
//                Assert.AreEqual<bool>(unko[i], true);    
            }
//            Assert.AreEqual<bool>(unko[456], true);
  //          Assert.AreEqual<bool>(unko[879], true);
    //        Assert.AreNotEqual<bool>(unko[899], true);
        }
    }
}
