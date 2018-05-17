using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VIPBrowserLibrary.Chron;
using VIPBrowserLibrary.Utility;

namespace VIPBrowserUnitTestProject
{
    [TestClass]
    public class CalutureTest
    {
        [TestMethod]
        public void UnixTimeTest()
        {
            DateTime now = DateTime.Now;
            int i = Calture.GetTime(now) + 5;
            DateTime dt = Calture.UnixTimeToDateTime(i.ToString());
            Assert.AreEqual<int>(now.Second, dt.Second);
            Assert.AreEqual<int>(now.Minute, dt.Minute);
            int nowTime = 1384429170;
            dt = Calture.UnixTimeToDateTime(nowTime.ToString());
            int unko = Calture.GetTime(dt) + 5;
            Assert.AreEqual<int>(nowTime, unko - 32400);
        }
        [TestMethod]
        public void EncryptTest()
        {
            string data = "mfwopgjneipognapiongfvpaegnepognepoigrjnioreg";
            const string key = "bethrehrj5ewjrfrhd5r6jhh";
            string encryptData = Encrypt.EncryptString(data,key);
            string decryptData = Encrypt.DecryptString(encryptData,key);
            Assert.AreEqual<string>(data, decryptData);
        }
        [TestMethod]
        public void UnkoTest()
        {
            Assert.AreEqual<Type>(typeof(int), new int().GetType());
        }
        [TestMethod]
        public void ReadTest()
        {
			
        }
    }
}
