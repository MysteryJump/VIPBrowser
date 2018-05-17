using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VIPBrowserLibrary.Utility;
using VIPBrowserLibrary.Other.MyExtensions;

namespace VIPBrowserLibrary.BBS.Common
{
	/// <summary>
	/// Samba24に関するデータを管理します
	/// </summary>
	public class Samba24
	{
		private string url;
		static Setting.GeneralSetting gs = new Setting.GeneralSetting();
		/// <summary>
		/// Samba24の設定情報を保存しているファイルを取得します。
		/// </summary>
		public static readonly string Samba24SettingPath = gs.NotNecessarySettingDataPath + "\\samba24.txt";
		/// <summary>
		/// 最終書き込み時間を保存しているパスを取得します。
		/// </summary>
		public static readonly string LastWrittenBoardPath = gs.NotNecessarySettingDataPath + "\\lastwritten.dat";
		/// <summary>
		/// Samba24を確認する板のUrlを使用してこのクラスの新しいインスタンスを初期化します
		/// </summary>
		/// <param name="url">Samba24を確認するUrl</param>
		public Samba24(string url)
		{
			if (url == null)
			{
				throw new ArgumentNullException("url");
			}
			this.url = url;
			
		}
		
		/// <summary>
		/// Samba24に引っかかるか確認します
		/// </summary>
		/// <returns>残り時間を表すTimeSpan</returns>
		public async Task<ValueType[]> CheckSambaState()
		{
			TimeSpan ts = new TimeSpan(0, 0, await this.GetSambaTime());
			DateTime lastTime = this.GetLastWrittenTime();
			TimeSpan diffTime = DateTime.Now - lastTime;
			if (diffTime >= ts)
			{
				var time = TimeSpan.Zero;
				ValueType[] vt = { true, time };
				return vt;
			}
			else
			{
				var time = ts - diffTime;
				ValueType[] vt = { false, time };
				return vt;
			}
		}

		private DateTime GetLastWrittenTime()
		{
			var text = File.ReadAllLines(Samba24.LastWrittenBoardPath);
			var normaliUrl = this.NormalizationUrl(this.url);
			foreach (var item in text)
			{
				var splStr = item.Split('=');
				if (splStr[0] == normaliUrl)
				{
					return DateTime.Parse(splStr[1]);
				} 
			}
			return DateTime.MinValue;
		}
		/// <summary>
		/// 書き込んだ時間を記録します
		/// </summary>
		public void WriteSamba()
		{
			var norurl = this.NormalizationUrl(this.url);
			var text = File.ReadAllLines(Samba24.LastWrittenBoardPath);
			var tempDic = new Dictionary<string, string>();
			foreach (var item in text)
			{
				var splStr = item.Split('=');
				tempDic.Add(splStr[0],splStr[1]);
			}
			if (tempDic.ContainsKey(norurl))
			{
				tempDic[norurl] = DateTime.Now.ToString();
			}
			else
			{
				tempDic.Add(norurl, DateTime.Now.ToString());
			}
			using (var stream = File.CreateText(Samba24.LastWrittenBoardPath))
			{
				StringBuilder sb = new StringBuilder();
				foreach (var item in tempDic)
				{
					sb.Append(item.Key).Append("=").Append(item.Value).AppendLine();
				}
				stream.Write(sb.ToString());
				stream.Flush();
			}
		}

		private string NormalizationUrl(string url)
		{
			return url.Replace("http://", "").TrimEnd('/');
		}

		private async Task<int> GetSambaTime()
		{
			var text = File.ReadAllLines(Samba24.Samba24SettingPath);

			foreach (var item in text)
			{
				var splstr = item.Split('=');
				if (this.url.Contains(splstr[0]))
				{
					return splstr[1].Parse();
				}
			}
			var dic = await VIPBrowserLibrary.Common.GetBoardData.GetBoardDictionary(this.url, false);
			return dic["BBS_SAMBATIME"].Parse();

			
		}
		/// <summary>
		/// Samba24の設定ファイルの更新または作成を行います
		/// </summary>
		public static void CreateOrUpdateSamba24SettingFile()
		{
			HttpClient bas = new HttpClient("http://nullpo.s101.xrea.com/samba24/");
			bas.IsOtherSiteRequest = true;
			bas.GetStringSync();
			var se = bas.Headers.GetValues("Set-Cookie")[0];
			var ss = se.Substring(8, 74).Replace(" ","").Replace(";path=/","");
			HttpClient hc = new HttpClient("http://nullpo.s101.xrea.com/samba24/conv.xcg?browser=v2c&decsec=majority&offset=0&newline=crlf&output=view");
			hc.UserAgent = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/33.0.1750.154 Safari/537.36";
			hc.IsOtherSiteRequest = true;
			hc.Referer = "http://nullpo.s101.xrea.com/samba24/";
			hc.Cookies = new System.Net.CookieCollection();
			hc.Cookies.Add(new System.Net.Cookie("form", "v2c&majority&0&view&crlf", "/", "nullpo.s101.xrea.com"));
			hc.Cookies.Add(new System.Net.Cookie("session", ss, "/", "nullpo.s101.xrea.com"));
			string data = hc.GetStringSync();
			using (var str = File.Create(Samba24.Samba24SettingPath))
			{
				var bytes = UTF8Encoding.UTF8.GetBytes(data);
				str.Write(bytes, 0, bytes.Length);
				str.Flush();
			}
		}
	}
}
