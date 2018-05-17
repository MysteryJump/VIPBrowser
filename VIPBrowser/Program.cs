/*
 * 将来的にはランチャー作ってこれはただのdllにするかも
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using VIPBrowserLibrary.Setting.ConvertOtherBrowserData;
using VIPBrowserLibrary.BBS.Common;

namespace VIPBrowser
{
    /// <summary>
    /// アプリケーションのエントリポイントがあるクラスです。このクラスは継承できません。
    /// </summary>
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.SetCompatibleTextRenderingDefault(false);
            VIPBrowserLibrary.Setting.GeneralSetting gs = new VIPBrowserLibrary.Setting.GeneralSetting();
            if (File.Exists(gs.CurrentDirectory + "\\lock"))
            {
                MessageBox.Show("多重起動ではない場合lockファイルを削除してください");
#if DEBUG
                File.Delete(gs.CurrentDirectory + "\\lock");
#else
                return;
#endif
            }
            bool isFirst = false;
            Log.Logger.WriteLog("Start Application");
            var form = new SplashWindow();
            Version ver = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            form.FormBorderStyle = FormBorderStyle.None;
            form.ProgramVersion = ver.ToString();
            form.StartPosition = FormStartPosition.CenterScreen;
            form.Size = new System.Drawing.Size(480, 232);

            if (Environment.CommandLine.IndexOf("/up", StringComparison.CurrentCultureIgnoreCase) != -1)
            {
                try
                {
                    string[] argsa = Environment.GetCommandLineArgs();
                    int pid = Convert.ToInt32(argsa[2]);
                    Process.GetProcessById(pid).WaitForExit();    // 終了待ち
                    Log.Logger.WriteLog("Success Application Update");
                }
                catch (Exception)
                {
                    throw;
                }
            }
			//form.Show();
            //System.Threading.Thread.Sleep(50000);
            //try
            //{
            //    if (args[0] == "test")
            //    {
            //        Console.WriteLine("Start Hidden Mode\r\n");
            //        NotifyIcon ni = new NotifyIcon();
            //        ni.Visible = true;
            //        ni.BalloonTipShown += (sender, e) => MessageBox.Show("Test");
            //        ni.BalloonTipText = "htthththth";
            //        ni.BalloonTipTitle = "hthththh";
            
            //        ni.Text = "testだよーん";
            //        ni.ShowBalloonTip(50000, "rrr", "fffff", ToolTipIcon.Error);

            //    }
            //    else if (args[0] == "console")
            //    {
            //        Console.WriteLine("Start Console Mode");
            //    }
            //}
            //catch (IndexOutOfRangeException) { Console.WriteLine("Mode: Normal\r\n"); }
            if (!File.Exists(gs.CurrentDirectory + "\\Setting.xml"))
                isFirst = true;
			if (isFirst)
			{
				Dialogs.FirstStartUpDialog fsud = new Dialogs.FirstStartUpDialog();
				fsud.ShowDialog();
			}
            VIPBrowserLibrary.Setting.Serializer sr = new VIPBrowserLibrary.Setting.Serializer();
            VIPBrowserLibrary.Setting.SettingSerial ss = sr.Deserilize();
            Form1 f = new Form1();
            VIPBrowserLibrary.Setting.UpdateChecker uc = new VIPBrowserLibrary.Setting.UpdateChecker();
            if (uc.Check())
            {
                uc.Update();
            }
            //bool isFirstUp = false;
            VIPBrowserLibrary.Setting.Starting.CheckNeedStartFoldersAndFiles();
            PluginInfo[] pi = PluginInfo.FindPlugins(gs.PluginFilePath);
            if (!Directory.Exists(gs.CurrentDirectory + "\\old"))
                Directory.CreateDirectory(gs.CurrentDirectory + "\\old");

            using (FileStream fs = new FileStream(gs.CurrentDirectory + "\\lock", FileMode.CreateNew, FileAccess.Write, FileShare.Read))
            {

                VIPBrowserLibrary.BBS.Common.HttpClient.CookieManagement.RearrangeCookie();
                if (ss.IsPeriodicallyGC)
                {
                    System.Timers.Timer t = new System.Timers.Timer();
                    t.Interval = 5000;
                    t.Elapsed += (se, see) =>
                    {
                        Console.WriteLine("GarbageCollect Before\t" + GC.GetTotalMemory(false));
                        GC.Collect();
                        Console.WriteLine("GarbageCollect After\t\t" + GC.GetTotalMemory(false));
                        Console.WriteLine("---------------------------------");
                    };
                    t.Start();
                }

                System.Timers.Timer ngArrange = new System.Timers.Timer();
                ngArrange.Interval = 30000;
                ngArrange.Elapsed += async (sender, e) =>
                {
                    ngArrange.Stop();
                    await VIPBrowserLibrary.Chron.ThreadOrResData.Abone.AboneManagement.RearrangeNGWords(DateTime.Now);
                    ngArrange.Start();
                };
                ngArrange.Start();

				//if (File.Exists(VIPBrowserLibrary.Other.ServerService.UserData.UserDataSettingPath))
				//{
				//	VIPBrowserLibrary.Other.ServerService.ShareUserData.SettingXml sx = new VIPBrowserLibrary.Other.ServerService.ShareUserData.SettingXml(VIPBrowserLibrary.Other.ServerService.UserData.Load());
				//	sx.GetXmlFile();
				//}

                if (ss.IsUseVisualStyle)
                    Application.EnableVisualStyles();

                f.Plugins = pi;
                f.SettingData = ss;
                fs.Lock(0, 500);
                f.FormClosing += (sender, e) =>
                {
                    fs.Close();
                    File.Delete(gs.CurrentDirectory + "\\lock");

                };
                try
                {
                    form.Close();
                    form = null;
                    Application.Run(f);
                }
                catch (Exception e)
                {
                    Log.Logger.WriteLog(e);
                    throw;
                }
                sr.Serialize(f.SettingData);
                Log.Logger.WriteLog("Exit Application");
            }

            return;

        }


    }
}
