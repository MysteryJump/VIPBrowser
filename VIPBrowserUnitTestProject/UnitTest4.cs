using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VIPBrowserLibrary.Setting;
using System.IO;

namespace VIPBrowserUnitTestProject
{
    [TestClass]
    public class UnitTest4
    {
        [TestMethod]
        public void GeneralSettingTest()
        {
            GeneralSetting gs = new GeneralSetting();
            string gsc = gs.CurrentDirectory;
            Assert.AreEqual<string>(gsc, Directory.GetCurrentDirectory());
        }
    }
}
