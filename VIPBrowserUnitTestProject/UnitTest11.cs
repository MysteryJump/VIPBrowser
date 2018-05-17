using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VIPBrowserLibrary.Chron;
using System.Windows.Forms;
using CH = System.Windows.Forms.ColumnHeader;

namespace VIPBrowserUnitTestProject
{
    [TestClass]
    public class ColumnTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            ThreadColumn tc = new ThreadColumn();
            CH c = new CH();
            c.Name = "Unko";
            CH cc = new CH();
            cc.Name = "Tinko";
            CH ccc = new CH();
            ccc.Name = "Manko";
            tc.ColumnData = new CH[] { c, cc, ccc };
            ListViewItem lvi = new ListViewItem();
            lvi.SubItems.Add(new ListViewItem.ListViewSubItem { Name = "Unko" });
            lvi.SubItems.Add(new ListViewItem.ListViewSubItem { Name = "Manko" });
            lvi.SubItems.Add(new ListViewItem.ListViewSubItem { Name = "Tinko" });

            ListViewItem lvis = new ListViewItem();
            lvis.SubItems.Add(new ListViewItem.ListViewSubItem { Name = "Unko" ,Text = "Fuck"});
            lvis.SubItems.Add(new ListViewItem.ListViewSubItem { Name = "Manko" ,Text = "Sacchi"});
            lvis.SubItems.Add(new ListViewItem.ListViewSubItem { Name = "Tinko",Text = "Ecchi" });
            ListViewItem[] l = new ListViewItem[] { lvi, lvis };
            var dat = tc.ConvertToColumnBaseItem(l);
            Assert.AreNotEqual<ListViewItem>(dat[0], lvi);
            Assert.AreNotEqual<ListViewItem>(dat[1], lvis);

            ListViewItem lvs = new ListViewItem();
            lvs.SubItems.Add(new ListViewItem.ListViewSubItem { Name = "Unko", Text = "Fuck" });
            lvs.SubItems.Add(new ListViewItem.ListViewSubItem { Name = "Tinko", Text = "Ecchi" });
            lvs.SubItems.Add(new ListViewItem.ListViewSubItem { Name = "Manko", Text = "Sacchi" });

            Assert.AreEqual<string>(dat[1].SubItems[1].Name, lvs.SubItems[1].Name);
            Assert.AreEqual<string>(dat[1].SubItems[2].Name, lvs.SubItems[2].Name);

        }
    }
}
