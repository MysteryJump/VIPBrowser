using System;
using VIPBrowserLibrary.Other.MyExtensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;

namespace VIPBrowserUnitTestProject
{
    [TestClass]
    public class UnitTest6
    {
        [TestMethod]
        public void DynamicParseTest()
        {
            string[] strings = 
            {
                "gwg5vsbn6",
                "46949",
                "7*[]5r2",
                "-----89-----",
                "fewge4wgegvresgwgvewrgREGWQEGVeg"
            };
            int[] ints = 
            {
               56,
               46949,
               752,
               89,
               4 
            };
            for (int i = 0; i < strings.Length; i++)
            {
                int p = strings[i].DynamicParse();
                Assert.AreEqual<int>(ints[i], p);
            }
        }
        [TestMethod]
        public void Int32ParseTest()
        {
            string[] strings = 
            {
                "5467",
                "74694",
                "9749",
                "8412",
                "265"
            };
            int[] ints = 
            {
                5467,
                74694,
                9749,
                8412,
                265
            };
            for (int i = 0; i < strings.Length; i++)
            {
                int p = strings[i].Parse();
                Assert.AreEqual<int>(ints[i], p);
            }
        }
        [TestMethod]
        public void ColorParseTest()
        {
            string[] strings = 
            {
                "Black",
                "Red",
                "White",
                "Azure",
                "Blue"
            };
            Color[] colors = 
            {
                Color.Black,
                Color.Red,
                Color.White,
                Color.Azure,
                Color.Blue
            };
            for (int i = 0; i < strings.Length; i++)
            {
                Color c = strings[i].ParseColor();
                Assert.AreEqual<Color>(colors[i], c);
            }
        }
        [TestMethod]
        public void SplitStringTest()
        {
            string unko = "ffffff|Ffffffgewgre|ghewheh";
            string[] sp = unko.SplitString("|");
            Assert.AreEqual<string>(sp[0],"ffffff");
            Assert.AreEqual<string>(unko,sp.Join("|"));
        }
        [TestMethod]
        public void NullObjectTest()
        {
            object ob = new object();
            long l = 0;
            Region r = null;
            Assert.AreEqual<bool>(ob.IsNullObject(), false);
            Assert.AreEqual<bool>(l.IsNullObject(), false);
            Assert.AreEqual<bool>(r.IsNullObject(), true);
            // suppression warnings.
            l.ToString();
            r = new Region();
        }
    }
}
