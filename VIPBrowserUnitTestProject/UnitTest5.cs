using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VIPBrowserLibrary.Chron.ThreadOrResData;
using VIPBrowserLibrary.Utility;

namespace VIPBrowserUnitTestProject
{
    [TestClass]
    public class UnitTest5
    {
        [TestMethod]
        public void MyTestMethod()
        {
            string[] datas = 
            {
                @"
<!DOCTYPE html>
<html>
<head>
</head>
</html>
"
            };
            foreach (var item in datas)
            {
                string data = StringUtility.HTMLEncode(item);
                data = StringUtility.HTMLDecode(data);
                bool s = System.Text.RegularExpressions.Regex.IsMatch(data, item, System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Compiled);
                Assert.AreEqual<bool>(s, true);
            }
            
        }
        [TestMethod]
        public void RemoveTag()
        {
            string[] before = 
            {
                "<html><body><a href=\"#\"></a></body></html>"
            };
            string[] after = 
            {
                "<body></body>"
            };
            Assert.AreEqual<string>(after[0],StringUtility.RemoveTag(before[0], "html|a"));
        }
    }
}
