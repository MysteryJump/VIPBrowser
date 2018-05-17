using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VIPBrowserLibrary.BBS.Common;
using System.IO;
using System.Diagnostics;

namespace VIPBrowserLibrary.Setting
{
    /// <summary>
    /// アップデートの確認を行うクラスです
    /// </summary>
    public class UpdateChecker
    {
        private const string CheckUrl = "http://uravip.tonkotsu.jp/updatecheck.pl";
        private string downUrl = String.Empty;
        private readonly GeneralSetting gs = new GeneralSetting();
        /// <summary>
        /// アップデート可能か確認します
        /// </summary>
        /// <returns>可能な場合にはtrueを不可能な場合にはfalseを返します</returns>
        public bool Check()
        {
            System.Version ver = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            string versionText = ver.ToString().Replace(".", "");
            string requestUrl = CheckUrl + "?" + versionText + "=false";
            HttpClient hc = new HttpClient(requestUrl);
            string data = hc.GetStringSync();
            if (data.Contains("not"))
                return false;
            this.downUrl = data;
            return true;
            
        }
        /// <summary>
        /// 現在のバージョンの上位バージョンにアップデートします
        /// </summary>
        public void Update()
        {
            if (String.IsNullOrEmpty(this.downUrl))
                throw new InvalidOperationException("ダウンロード先のURLが指定されていません。先にCheckメソッドを実行してから呼び出してください。");
            var net = new Microsoft.VisualBasic.Devices.Network();
            string c = gs.CurrentDirectory;
            net.DownloadFile(this.downUrl, c + "\\new.zip", "", "", true, 25, true, Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing);
            net.DownloadFile(this.downUrl + ".txt", c + "\\new.txt", "", "", false, 10, true, Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing);
            string cc = c + "\\old";
           // using (File.Create(cc + "\\browser.exe")) { }
            //using (File.Create(cc + "\\library.dll")) { }
           // using (File.Create(cc + "\\plugin.dll")) { }
            File.Delete(cc + "\\browser.exe");
            File.Delete(cc + "\\library.dll");
            File.Delete(cc + "\\plugin.dll");

            File.Move(c + "\\VIPBrowser.exe", cc + "\\browser.exe");
            File.Move(c + "\\VIPBrowserLibrary.dll", cc + "\\library.dll");
            File.Move(c + "\\VIPBrowserPlugin.dll", cc + "\\plugin.dll");

            Chron.ZIPData zd = new Chron.ZIPData();
            zd.ExtractZipFile(c + "\\new.zip", c);
            Process.Start(c + "\\VIPBrowser.exe", "/up " + Process.GetCurrentProcess().Id);
            System.Windows.Forms.Application.Exit();
            File.Delete(c + "\\lock");
            return;
        }
        /// <summary>
        /// 指定したバージョンにダウングレードします
        /// </summary>
        /// <param name="ver">使用するバージョン</param>
        public void Downgrade(Version ver)
        {
            throw new NotImplementedException();
        }
    }
}
