using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using VIPBrowserLibrary.Other.MyExtensions;
using VIPBrowserLibrary.BBS.Common;

namespace VIPBrowserLibrary.Setting.ConvertOtherBrowserData
{
    /// <summary>
    /// V2Cからのデータの変換を行います
    /// </summary>
    public class V2CConverter : ConverterBase , IOtherDataConverter
    {
        Setting.GeneralSetting gs = new GeneralSetting();
        /// <summary>
        /// 読み込み先のパスを使用してこのクラスの新しいインスタンスを初期化します
        /// </summary>
        /// <param name="path"></param>
        public V2CConverter(string path) : base(path)
        {

        }
        /// <summary>
        /// NGワードを変換します
        /// </summary>
        public override void ConvertNGList()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 過去ログファイルを変換します
        /// </summary>
		public override void ConvertLog()
		{
			var logPath = this.ConvertBasePath + "\\log";
			var dirs = Directory.GetDirectories(logPath);
			var boardLists = new List<BoardCategory>();
			if (!File.Exists(XmlBoardList.XmlBoardListSettingPath))
			{
				var xs = new XmlBoardList();
				xs.GetXmlBoardList();
				boardLists = xs.GetBoardTree();
				XmlBoardList.Serialize(XmlBoardList.XmlBoardListSettingPath, boardLists);
			}
			else
			{
				boardLists = XmlBoardList.Deserialize(XmlBoardList.XmlBoardListSettingPath);
			}
			var dirList = new List<string>();
			foreach (var item in dirs)
			{
				var dis = Directory.GetDirectories(item);
				if (item.Contains("jbbs_"))
				{
					foreach (var item2 in dis)
					{
						dirList.AddRange(Directory.GetDirectories(item2));
					}
					continue;
				}
				dirList.AddRange(dis);
			}
			var boards = new List<BoardChild>();
			foreach (var item in boardLists)
			{
				foreach (var item2 in item.Children)
				{
					boards.Add(item2);
				}
			}
			foreach (var item in dirList)
			{
				string unko = item.Replace(this.ConvertBasePath + "\\log\\", "");
				if (unko.Contains("2ch_"))
				{
					//continue
					unko = unko.Replace("2ch_\\", "");
					var child = boards.Find((s) =>
					{
						if (s.BoardAddress.Contains(unko)) 
							return true;
						return false;
					});
					if (child.IsNullObject())
					{
						continue;
					}
					var addr = child.BoardAddress.Replace("http://", "").TrimEnd('/').Replace("https://", "").Replace("/", "-");
					var boardPath = gs.DatFilePath + "\\" + addr;
					Directory.CreateDirectory(boardPath);
					foreach (var dat in Directory.GetFiles(item,"*.dat"))
					{
						var fi = new FileInfo(dat);
						
						File.Copy(dat, boardPath + "\\" + fi.Name);
					}
				}
				else if (unko.Contains("jbbs_"))
				{
					unko = unko.Replace("jbbs_\\", "");
					var splDirArray = unko.Split(new[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
					if (splDirArray.Length != 2)
					{
						continue;
					}
					var dir = "jbbs-" + splDirArray[0] + "-" + splDirArray[1];
					var boardPath = gs.DatFilePath + "\\" + dir;
					Directory.CreateDirectory(boardPath);
					foreach (var dat in Directory.GetFiles(item,"*.cgi"))
					{
						var fi = new FileInfo(dat);
						File.Copy(dat, boardPath + "\\" + fi.Name);
					}

				}
				else if (unko.Contains("bbspink_"))
				{
					unko = unko.Replace("bbspink_\\", "");
					var child = boards.Find((s) =>
					{
						if (s.BoardAddress.Contains(unko))
							return true;
						return false;
					});
					if (child.IsNullObject())
					{
						continue;
					}
					var addr = child.BoardAddress.Replace("http://", "").TrimEnd('/').Replace("https://", "").Replace("/", "-");
					var boardPath = gs.DatFilePath + "\\" + addr;
					Directory.CreateDirectory(boardPath);
					foreach (var dat in Directory.GetFiles(item, "*.dat"))
					{
						var fi = new FileInfo(dat);

						File.Copy(dat, boardPath + "\\" + fi.Name);
					}
				}
				else if(unko.Contains("machi_"))
				{
					unko = unko.Replace("machi_\\", "");
					continue;
				}
				else
				{
					var splDir = unko.Split(new[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
					var dir = splDir[0] + "-" + splDir[1];
					var dirPath = gs.DatFilePath + "\\" + dir;
					Directory.CreateDirectory(dirPath);
					foreach (var dat in Directory.GetFiles(item, "*.dat"))
					{
						var fi = new FileInfo(dat);
						File.Copy(dat, dirPath + "\\" + fi.Name);
					}
				}
			}
		}
        /// <summary>
        /// AAリストを変換します
        /// </summary>
        public override void ConvertAAList()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// お気に入り一覧を変換します
        /// </summary>
        public override void ConvertFavorite()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Cookieファイルを変換します
        /// </summary>
        public override void ConvertCookie()
        {
            CookieCollection cc = new CookieCollection();
            string path = this.ConvertBasePath;
            string[] data = File.ReadAllLines(path + "\\cookie.txt");
            foreach (var item in data)
            {
                int paPlace = item.IndexOf(" path=");
                string pa = item.Substring(paPlace).Remove(0,1).Replace("path=","");
                
                string[] splitData = item.Split('\t');
                string host = Utility.StringUtility.HostToUrl(splitData[0]);
                string[] keys = splitData[1].Split('=');
                Cookie c = new Cookie(keys[0], keys[1].Remove(keys[1].IndexOf(";")), pa, host);
                cc.Add(c);
            }
            BBS.Common.HttpClient.CookieManagement.WriteCookieToDisk(cc);
            BBS.Common.HttpClient.CookieManagement.RearrangeCookie();
        }

    }
}
