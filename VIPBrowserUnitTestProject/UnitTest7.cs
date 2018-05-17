using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VIPBrowserLibrary.Setting;

namespace VIPBrowserUnitTestProject
{
    [TestClass]
    public class UnitTest7
    {
        [TestMethod()]
        public void UpdateCheckTest()
        {
            UpdateChecker uc = new UpdateChecker();
            Assert.AreEqual<bool>(false,uc.Check());
        }
    }
}
