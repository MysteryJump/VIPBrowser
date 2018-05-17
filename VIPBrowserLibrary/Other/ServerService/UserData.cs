using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using VIPBrowserLibrary.Setting;

namespace VIPBrowserLibrary.Other.ServerService
{
	/// <summary>
	/// BlueRushのユーザーデータを表します
	/// </summary>
	[Serializable]
	public sealed class UserData
	{
		/// <summary>
		/// ユーザー名を取得または設定します
		/// </summary>
		public string UserName;
		/// <summary>
		/// パスワードを取得または設定します
		/// </summary>
		public string Password;
		/// <summary>
		/// ユーザーデータを読み込みます
		/// </summary>
		/// <returns>読み込んだユーザーデータ</returns>
		public static UserData Load()
		{
			return Chron.Serializer.Deserialize<UserData>(File.Open(UserDataSettingPath, System.IO.FileMode.Open));
		}
		/// <summary>
		/// サーバー上にアカウントが存在するか確認後、サインインします
		/// </summary>
		/// <param name="userName">ユーザー名</param>
		/// <param name="password">パスワード</param>
		/// <returns>サインイン成功した場合はUserData,失敗した場合はnull</returns>
		public static UserData Signin(string userName,string password)
		{
			var wec = new WebClient();
			var res = wec.DownloadString("http://vipbrowser.s601.xrea.com/api/u.php?user=" + userName + "&pass=" + password);
			if (res.Contains("Success"))
			{
				var data = new UserData(userName,password);
				Chron.Serializer.Serialize<UserData>(data, File.Open(UserDataSettingPath, System.IO.FileMode.Create));
				return data;
			}
			else
			{
				return null;
			}
		}
		/// <summary>
		/// アカウントからサインアウトします
		/// </summary>
		public static void Signout()
		{
			File.Delete(UserDataSettingPath);
		}

		private UserData(string user,string pass)
		{
			this.UserName = user;
			this.Password = pass;
		}
		/// <summary>
		/// アカウントの作成を行います
		/// </summary>
		/// <param name="userName">ユーザー名</param>
		/// <param name="password">パスワード</param>
		public static bool CreateAccount(string userName, string password)
		{
			var bas =
@"
<?xml version=""1.0"" encoding=""utf-8""?>
<Account>
	<UserName>" + userName + @"</UserName>
	<Password>" + password + @"</Password>
</Account>
";
			var wec = new WebClient();
			var data = wec.UploadString("http://vipbrowser.s601.xrea.com/api/u.php", "POST", bas);
			if (data.Contains("Failed"))
			{
				return false;
			}
			else
			{
				return true;
			}
		}
		private static GeneralSetting gs { get { return new GeneralSetting(); } }
		/// <summary>
		/// UserDataの設定パスを表します
		/// </summary>
		public static string UserDataSettingPath { get { return gs.CurrentDirectory + "\\userdata.dat"; } }
	}
}
