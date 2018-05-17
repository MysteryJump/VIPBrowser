using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using VIPBrowserLibrary.Setting;

namespace VIPBrowserLibrary.Other.ServerService.ShareUserData
{
	/// <summary>
	/// Setting.xmlの同期を行うクラスです
	/// </summary>
	[Serializable]
	public class SettingXml
	{
		private GeneralSetting gs = new GeneralSetting();
		/// <summary>
		/// ユーザーのアカウントデータを格納したUserDataインスタンスを用いてログインします
		/// </summary>
		/// <param name="ud">ログインするUserData</param>
		public SettingXml(UserData ud)
		{
			this.AccountData = ud;
		}
		/// <summary>
		/// Setting.xmlを取得します
		/// </summary>
		public void GetXmlFile()
		{
			var wec = new WebClient();
			var res = wec.DownloadString("http://vipbrowser.s601.xrea.com/api/setting.php?user=" + this.AccountData.UserName + "&pass=" + this.AccountData.Password);
			File.Delete(gs.CurrentDirectory + "\\setting.xml");
			File.AppendAllText(gs.CurrentDirectory + "\\setting.xml",res);
		}
		/// <summary>
		/// Setting.xmlを送信します
		/// </summary>
		public void SetXmlFile()
		{
			var wec = new WebClient();

			try
			{
				var str = File.ReadAllText(gs.CurrentDirectory + "\\setting.xml");
				var res = wec.UploadString("http://vipbrowser.s601.xrea.com/api/setting.php?user=" + this.AccountData.UserName + "&pass=" + this.AccountData.Password, "POST", str);
			}
			catch
			{
				this.GetXmlFile();
			}
		}
		/// <summary>
		/// このインスタンスに関連付けられたUserDataインスタンスを取得します
		/// </summary>
		public UserData AccountData { get; set; }
	}
}
