using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using VIPBrowserLibrary.Other.MyExtensions;
using Sub = System.Windows.Forms.ListViewItem.ListViewSubItem;

namespace VIPBrowserLibrary.BBS.Common
{
    /// <summary>
    /// スレッド一覧を取得する基本クラスです
    /// </summary>
    public abstract class CommonThreadListReader
    {
        /// <summary>
        /// スレッド一覧を取得し、ListViewItem[]形式に変換します。
        /// </summary>
        /// <param name="re">変換に用いるRegex</param>
        /// <param name="url">取得先のURL</param>
        /// <param name="hc">取得に用いる、HttpClientクラス</param>
        /// <returns>スレッド一覧を格納したListViewItem[]</returns>
        protected virtual async Task<System.Windows.Forms.ListViewItem[]> ParseThreadList(Regex re,string url,Common.HttpClient hc)
        {
            VIPBrowserLibrary.Common.BBSType bt;

            string datNameBase = String.Empty;

            Chron.ThreadOrResData.Abone.ThreadListAbone tla = new Chron.ThreadOrResData.Abone.ThreadListAbone();
            tla.InstLoad();
            Encoding enc;
            Setting.GeneralSetting gs = new Setting.GeneralSetting();
            ListViewItem[] liCollection = null;
            try
            {
                string folder = String.Empty;
                string address = url.Replace("http://", "").TrimEnd('/');
                if (address.IndexOf("jbbs.livedoor.jp") == -1 && address.IndexOf("jbbs.shitaraba.net") == -1)
                {
                    string[] filePath = address.Split('/');
                    folder = gs.DatFilePath + "\\" + filePath[0] + "-" + filePath[1];
                    enc = Encoding.GetEncoding("Shift-JIS");
                    if (!address.Contains("machi.to"))
                    {


                        bt = VIPBrowserLibrary.Common.BBSType._2ch;
                        datNameBase = url + "dat/";
                    }
                    else
                    {
                        bt = VIPBrowserLibrary.Common.BBSType.machibbs;
                        Match m = Regex.Match(url, @"http://(?<host>\w+)[.]machi[.]to/(?<folder>\w+)/?", RegexOptions.Compiled);
                        datNameBase = "http://" + m.Groups["host"].Value + ".machi.to/bbs/offlaw.cgi/" + m.Groups["folder"].Value + "/";
                        hc.Host = m.Groups["host"].Value + ".machi.to";
                    }
                }
                else
                {
                    address = address.Replace("jbbs.livedoor.jp", "").Replace("jbbs.shitaraba.net","").TrimStart('/').Replace('/', '-');
                    folder = gs.DatFilePath + "\\" + "jbbs-" + address;
                    enc = Encoding.GetEncoding("EUC-JP");
                    bt = VIPBrowserLibrary.Common.BBSType.jbbs;
                    Match m = new Regex(@"http://jbbs.(shitaraba.net|livedoor.jp)/(?<category>.+)/(?<number>\d+)/").Match(url);
                    datNameBase = String.Concat("http://jbbs.shitaraba.net/bbs/rawmode.cgi/", m.Groups["category"].Value, "/", m.Groups["number"].Value, "/");
                }
                string getData = await hc.GetStringAsync();
                if (!System.IO.Directory.Exists(folder))
                    System.IO.Directory.CreateDirectory(folder);
                
                
                MatchCollection mc = re.Matches(getData);
                liCollection = new ListViewItem[mc.Count];
                int i = 1;
                Chron.ThreadOrResData.ThreadListItemColorRing cl = new Chron.ThreadOrResData.ThreadListItemColorRing(folder + "\\subject.txt");
                bool isColorring = cl.IsNull;
                foreach (Match item in mc)
                {
                    Chron.ThreadOrResData.ThreadData td = Chron.ThreadOrResData.ThreadDataWriterAndReader.Read(folder + "\\" + item.Groups["datName"].Value + ".xml");
                    //Console.WriteLine(item.Groups["threadTitle"].Value); Console.WriteLine(item.Groups["datName"].Value); Console.WriteLine(item.Groups["resCount"].Value);
                    ListViewItem li = null;
                    DateTime dt = Chron.Calture.UnixTimeToDateTime(item.Groups["datName"].Value);
                    string resCount = item.Groups["resCount"].Value;
                    if (td == null)
                    {
                        //li = new ListViewItem(new string[] 
                        //{
                        //    "　",
                        //    (i++).ToString(),
                        //    Utility.StringUtility.HTMLDecode(item.Groups["threadTitle"].Value),
                        //    resCount,
                        //    dt.ToString(),
                        //    Chron.Calture.ThreadAuthority(dt,resCount).ToString(),
                        //    "0",
                        //    "0",
                        //    "0"

                        //});
                        #region やっつけで書いた、後悔はしていない
                        Sub read = new Sub();
                        read.Text = "　";
                        read.Name = "IsRead";

                        Sub count = new Sub();
                        count.Text = i.ToString();
                        count.Name = "Count";

                        Sub name = new Sub();
                        name.Text = Utility.StringUtility.HTMLDecode(item.Groups["threadTitle"].Value);
                        name.Name = "Name";

                        Sub res = new Sub();
                        res.Text = resCount.ToString();
                        res.Name = "ResCount";

                        Sub time = new Sub();
                        time.Text = dt.ToString();
                        time.Name = "Time";

                        Sub speed = new Sub();
                        speed.Text = Chron.Calture.ThreadAuthority(dt, resCount).ToString();
                        speed.Name = "Speed";

                        Sub size = new Sub();
                        size.Text = "";
                        size.Name = "Size";

                        Sub oldRes = new Sub();
                        oldRes.Text = "";
                        oldRes.Name = "OldResCount";

                        Sub newRes = new Sub();
                        newRes.Text = "";
                        newRes.Name = "NewResCount";

                        li = new ListViewItem(new Sub[] { read, count, name, res, time, speed, size, oldRes, newRes }, "");
                        #endregion


                    }
                    else
                    {
                        int newRes
                            = (resCount.Parse() - (td.GetRescount - 1));
                        string isNewState = String.Empty;
                        if (newRes == 0)
                            isNewState = "○";
                        else
                            isNewState = "●";
                        //li = new ListViewItem(new string[] 
                        //{
                        //    isNewState,
                        //    (i++).ToString(),
                        //    Utility.StringUtility.HTMLDecode(item.Groups["threadTitle"].Value),
                        //    resCount,
                        //    dt.ToString(),
                        //    Chron.Calture.ThreadAuthority(dt,resCount).ToString(),
                        //    td.DatSize,
                        //    (td.GetRescount - 1).ToString(),
                        //    newRes.ToString()

                        //});
                        #region やっつけで書いた、後悔はしていない


                        Sub read = new Sub();
                        read.Text = isNewState;
                        read.Name = "IsRead";

                        Sub count = new Sub();
                        count.Text = i.ToString();
                        count.Name = "Count";

                        Sub name = new Sub();
                        name.Text = Utility.StringUtility.HTMLDecode(item.Groups["threadTitle"].Value);
                        name.Name = "Name";

                        Sub res = new Sub();
                        res.Text = resCount.ToString();
                        res.Name = "ResCount";

                        Sub time = new Sub();
                        time.Text = dt.ToString();
                        time.Name = "Time";

                        Sub speed = new Sub();
                        speed.Text = Chron.Calture.ThreadAuthority(dt, resCount).ToString();
                        speed.Name = "Speed";

                        Sub size = new Sub();
                        size.Text = td.DatSize;
                        size.Name = "Size";

                        Sub oldRes = new Sub();
                        oldRes.Text = (td.GetRescount - 1).ToString();
                        oldRes.Name = "OldResCount";

                        Sub newRess = new Sub();
                        newRess.Text = newRes.ToString();
                        newRess.Name = "NewResCount";

                        li = new ListViewItem(new Sub[] { read, count, name, res, time, speed, size, oldRes, newRess }, "");
                        #endregion

                    }
                    if (!isColorring)
                        li = cl.ChangeColor(li);
                    if (tla.IsVisible(li.SubItems["Name"].Text))
                    {
                        string datName = item.Groups["datName"].Value;
                        li = cl.ChangeAlreadyListColor(li, datName);
                        liCollection[i - 1] = li;
                        if (bt == VIPBrowserLibrary.Common.BBSType._2ch)
                            liCollection[i - 1].ImageKey = datNameBase + datName + ".dat";
                        else
                            liCollection[i - 1].ImageKey = datNameBase + datName;
                    }
                    i++;
                }
                await Utility.TextUtility.WriteAsync(folder + "\\subject.txt", getData, enc, false);
                Chron.ThreadColumn tc = new Chron.ThreadColumn();
                tc.ReadColumnData(gs.NotNecessarySettingDataPath + "\\column.dat");
                return tc.ConvertToColumnBaseItem(liCollection);
                //return liCollection;
            }
            catch (ArgumentNullException)
            {
                return null;
            }
            catch (IndexOutOfRangeException e) 
            {
                throw new System.Net.WebException("配列の対象外：CommonThreadReader行231", e);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// オフラインでスレッド一覧を取得し、listViewItem[]形式に変換します
        /// </summary>
        /// <param name="re">変換に用いるRegex</param>
        /// <param name="url">取得先のURL</param>
        /// <returns>スレッド一覧を格納したListViewItem</returns>
        protected virtual async Task<ListViewItem[]> ParseThreadList(Regex re, string url)
        {
            VIPBrowserLibrary.Common.BBSType bt;

            string datNameBase = String.Empty;
            bt = VIPBrowserLibrary.Common.TypeJudgment.BBSTypeJudg(url);

            Encoding enc;
            Setting.GeneralSetting gs = new Setting.GeneralSetting();
            ListViewItem[] liCollection = null;
            try
            {
                string folder = String.Empty;
                string address = url.Replace("http://", "").TrimEnd('/');
                if (address.IndexOf("jbbs.livedoor.jp") == -1 && address.IndexOf("jbbs.shitaraba.net") == -1)
                {
                    string[] filePath = address.Split('/');
                    folder = gs.DatFilePath + "\\" + filePath[0] + "-" + filePath[1];
                    enc = Encoding.GetEncoding("Shift-JIS");
                    bt = VIPBrowserLibrary.Common.BBSType._2ch;
                    datNameBase = url + "dat/";
                }
                else
                {
                    address = address.Replace("jbbs.livedoor.jp", "").Replace("jbbs.shitaraba.net", "").TrimStart('/').Replace('/', '-');
                    folder = gs.DatFilePath + "\\jbbs-" + address;
                    enc = Encoding.GetEncoding("EUC-JP");
                    bt = VIPBrowserLibrary.Common.BBSType.jbbs;
                    Match m = new Regex(@"http://jbbs.(shitaraba.net|livedoor.jp)/(?<category>.+)/(?<number>\d+)/?").Match(url);
                    datNameBase = String.Concat("http://jbbs.shitaraba.net/bbs/rawmode.cgi/", m.Groups["category"].Value, "/", m.Groups["number"].Value, "/");
                }

                string getData = Utility.TextUtility.Read(folder + "\\subject.txt", enc);
                MatchCollection mc = re.Matches(getData);
                liCollection = new ListViewItem[mc.Count];
                int i = 1;
                foreach (Match item in mc)
                {
                    Chron.ThreadOrResData.ThreadData td = Chron.ThreadOrResData.ThreadDataWriterAndReader.Read(folder + "\\" + item.Groups["datName"].Value + ".xml");
                    //Console.WriteLine(item.Groups["threadTitle"].Value); Console.WriteLine(item.Groups["datName"].Value); Console.WriteLine(item.Groups["resCount"].Value);
                    ListViewItem li = null;
                    DateTime dt = Chron.Calture.UnixTimeToDateTime(item.Groups["datName"].Value);

                    //if (!dt.IsNullObject())
                    //{
                    //    li = new ListViewItem(new string[] 
                    //{
                    //        (i++).ToString(),
                    //        Utility.StringUtility.HTMLDecode(item.Groups["threadTitle"].Value),
                    //        item.Groups["resCount"].Value,
                    //        dt.ToString(),
                    //        Chron.Calture.ThreadAuthority(dt,item.Groups["resCount"].Value).ToString(),
                    //        td.DatSize
                    //});
                    //}
                    //else
                    //{
                    //    li = new ListViewItem(new string[] 
                    //{
                    //        (i++).ToString(),
                    //        Utility.StringUtility.HTMLDecode(item.Groups["threadTitle"].Value),
                    //        item.Groups["resCount"].Value,
                    //        dt.ToString(),
                    //        Chron.Calture.ThreadAuthority(dt,item.Groups["resCount"].Value).ToString(),
                    //        td.DatSize
                    //});
                    //}
                    Chron.ThreadOrResData.ThreadListItemColorRing cl = new Chron.ThreadOrResData.ThreadListItemColorRing(folder + "\\subject.txt");
                    bool isColorring = cl.IsNull;
                    Chron.ThreadOrResData.Abone.ThreadListAbone tla = new Chron.ThreadOrResData.Abone.ThreadListAbone();
                    tla.InstLoad();
                    string resCount = item.Groups["resCount"].Value;
                    if (td == null)
                    {
                        //li = new ListViewItem(new string[] 
                        //{
                        //    "　",
                        //    (i++).ToString(),
                        //    Utility.StringUtility.HTMLDecode(item.Groups["threadTitle"].Value),
                        //    resCount,
                        //    dt.ToString(),
                        //    Chron.Calture.ThreadAuthority(dt,resCount).ToString(),
                        //    "0",
                        //    "0",
                        //    "0"

                        //});
                        #region やっつけで書いた、後悔はしていない
                        Sub read = new Sub();
                        read.Text = "　";
                        read.Name = "IsRead";

                        Sub count = new Sub();
                        count.Text = i.ToString();
                        count.Name = "Count";

                        Sub name = new Sub();
                        name.Text = Utility.StringUtility.HTMLDecode(item.Groups["threadTitle"].Value);
                        name.Name = "Name";

                        Sub res = new Sub();
                        res.Text = resCount.ToString();
                        res.Name = "ResCount";

                        Sub time = new Sub();
                        time.Text = dt.ToString();
                        time.Name = "Time";

                        Sub speed = new Sub();
                        speed.Text = Chron.Calture.ThreadAuthority(dt, resCount).ToString();
                        speed.Name = "Speed";

                        Sub size = new Sub();
                        size.Text = "";
                        size.Name = "Size";

                        Sub oldRes = new Sub();
                        oldRes.Text = "";
                        oldRes.Name = "OldResCount";

                        Sub newRes = new Sub();
                        newRes.Text = "";
                        newRes.Name = "NewResCount";

                        li = new ListViewItem(new Sub[] { read, count, name, res, time, speed, size, oldRes, newRes }, "");
                        #endregion


                    }
                    else
                    {
                        int newRes
                            = (resCount.Parse() - (td.GetRescount - 1));
                        string isNewState = String.Empty;
                        if (newRes == 0)
                            isNewState = "○";
                        else
                            isNewState = "●";
                        //li = new ListViewItem(new string[] 
                        //{
                        //    isNewState,
                        //    (i++).ToString(),
                        //    Utility.StringUtility.HTMLDecode(item.Groups["threadTitle"].Value),
                        //    resCount,
                        //    dt.ToString(),
                        //    Chron.Calture.ThreadAuthority(dt,resCount).ToString(),
                        //    td.DatSize,
                        //    (td.GetRescount - 1).ToString(),
                        //    newRes.ToString()

                        //});
                        #region やっつけで書いた、後悔はしていない


                        Sub read = new Sub();
                        read.Text = isNewState;
                        read.Name = "IsRead";

                        Sub count = new Sub();
                        count.Text = i.ToString();
                        count.Name = "Count";

                        Sub name = new Sub();
                        name.Text = Utility.StringUtility.HTMLDecode(item.Groups["threadTitle"].Value);
                        name.Name = "Name";

                        Sub res = new Sub();
                        res.Text = resCount.ToString();
                        res.Name = "ResCount";

                        Sub time = new Sub();
                        time.Text = dt.ToString();
                        time.Name = "Time";

                        Sub speed = new Sub();
                        speed.Text = Chron.Calture.ThreadAuthority(dt, resCount).ToString();
                        speed.Name = "Speed";

                        Sub size = new Sub();
                        size.Text = td.DatSize;
                        size.Name = "Size";

                        Sub oldRes = new Sub();
                        oldRes.Text = (td.GetRescount - 1).ToString();
                        oldRes.Name = "OldResCount";

                        Sub newRess = new Sub();
                        newRess.Text = newRes.ToString();
                        newRess.Name = "NewResCount";

                        li = new ListViewItem(new Sub[] { read, count, name, res, time, speed, size, oldRes, newRess }, "");
                        #endregion

                    }
                    if (!isColorring)
                        li = cl.ChangeColor(li);
                    if (tla.IsVisible(li.SubItems["Name"].Text))
                    {
                        string datName = item.Groups["datName"].Value;
                        li = cl.ChangeAlreadyListColor(li, datName);
                        liCollection[i - 1] = li;
                        if (bt == VIPBrowserLibrary.Common.BBSType._2ch)
                            liCollection[i - 1].ImageKey = datNameBase + datName + ".dat";
                        else
                            liCollection[i - 1].ImageKey = datNameBase + datName;
                    }
                    //liCollection[i - 2] = li;
                    //if (bt == VIPBrowserLibrary.Common.BBSType._2ch)
                    //    liCollection[i - 2].ImageKey = datNameBase + item.Groups["datName"].Value + ".dat";
                    //else
                    //    liCollection[i - 2].ImageKey = datNameBase + item.Groups["datName"].Value;
                    i++;
                }
                Chron.ThreadColumn tc = new Chron.ThreadColumn();
                tc.ReadColumnData(gs.NotNecessarySettingDataPath + "\\column.dat");
                return tc.ConvertToColumnBaseItem(liCollection);
            }
            catch
            {
                throw;
            }
        }
    }
}
