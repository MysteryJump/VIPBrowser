using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIPBrowserLibrary.BBS.X2ch
{
	/// <summary>
	/// 浪人システム用のクラスです。
	/// </summary>
	public class Ronin
	{
		private static Setting.GeneralSetting gs = new Setting.GeneralSetting();
		private const string FormatRequestAddress = "https://2chv.tora3.net/futen.cgi?ID={0}&PW={1}";
		/// <summary>
		/// 浪人の設定パスを表します
		/// </summary>
		public static readonly string RoninSettingPath = gs.NotNecessarySettingDataPath + "\\ronin.bin";
		/// <summary>
		/// 浪人システムにログインします
		/// </summary>
		/// <param name="user">ユーザー名</param>
		/// <param name="pass">パスワード</param>
		public static bool Login(string user,string pass)
		{
			RoninUserData rud = new RoninUserData();
			rud.Password = pass;
			rud.UserName = user;
			var r = new Ronin(false);
			r.GetSecretKey();
			var key = r.SecretKey;
			r = null;
			rud.SecretKey = key;
			if (key.Contains("SESSION-ID=ERROR"))
			{
				return false;
			}
			rud.LastGetSecretKeyTime = DateTime.Now.ToString();
			var s = File.Create(Ronin.RoninSettingPath);
			Chron.Serializer.Serialize<RoninUserData>(rud,s, Chron.SerializeType.BinarySerialize);
			s.Dispose();
			return true;
		}
		/// <summary>
		/// 浪人からログアウトします
		/// </summary>
		public static void Logout()
		{
			File.Delete(Ronin.RoninSettingPath);
		}
		private Ronin(bool isNormal)
		{
			if (isNormal)
			{
				this.GetUserData();
				DateTime lasttime = this.LastGetSecretTime.AddHours(-1);
				if (lasttime.AddHours(24) <= DateTime.Now)
				{
					this.GetSecretKey();
				}
			}
		}
		/// <summary>
		/// 秘密鍵を読み込み、このクラスの新しいインスタンスを初期化します。
		/// </summary>
		public Ronin()
		{
			this.GetUserData();
			DateTime lasttime = this.LastGetSecretTime.AddHours(-1);
			if (lasttime.AddHours(24) <= DateTime.Now)
			{
				this.GetSecretKey();
			}
		}

		private void GetSecretKey()
		{
			BBS.Common.HttpClient hc = new Common.HttpClient(String.Format(FormatRequestAddress, this.User, this.Password));
			hc.IsOtherSiteRequest = true;
			string data = hc.GetStringSync();
			this.SecretKey = data.Replace("SESSION-ID=", "");
			var str = File.Create(Ronin.RoninSettingPath);
			Chron.Serializer.Serialize<RoninUserData>(new RoninUserData() { LastGetSecretKeyTime = DateTime.Now.ToString(), SecretKey = this.SecretKey, Password = this.Password, UserName = this.User },str, Chron.SerializeType.BinarySerialize);
			str.Dispose();
		}

		private void GetUserData()
		{
			var s = File.Open(Ronin.RoninSettingPath, FileMode.Open);
			var data = Chron.Serializer.Deserialize<RoninUserData>(s, Chron.SerializeType.BinarySerialize);
			this.User = data.UserName;
			this.SecretKey = data.SecretKey;
			this.Password = data.Password;
			this.LastGetSecretTime = DateTime.Parse(data.LastGetSecretKeyTime);
		}

		private DateTime LastGetSecretTime { get; set; }
		/// <summary>
		/// パスワードを取得または設定します
		/// </summary>
		public string Password { get; set; }
		/// <summary>
		/// ユーザー名を取得または設定します
		/// </summary>
		public string User { get; set; }

		/// <summary>
		/// 秘密鍵を取得または設定します
		/// </summary>
		public string SecretKey { get; set; }
		/// <summary>
		/// 浪人のユーザーデータを表します。このクラスは継承できません。
		/// </summary>
		[Serializable]
		public sealed class RoninUserData
		{
			/// <summary>
			/// 秘密鍵(SID)を表します
			/// </summary>
			public string SecretKey;
			/// <summary>
			/// ユーザーのパスワードを表します
			/// </summary>
			public string Password;
			/// <summary>
			/// ユーザー名を表します
			/// </summary>
			public string UserName;
			/// <summary>
			/// 最後に秘密鍵を取得した時間を表します
			/// </summary>
			public string LastGetSecretKeyTime;
		}
		
	}
}
