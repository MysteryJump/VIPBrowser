using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace VIPBrowserLibrary.BBS.Common
{
	/// <summary>
	/// 板一覧における板を表します
	/// </summary>
	[DataContract(Namespace = "", Name = "BoardChild")]
	[Serializable]
	public sealed class BoardChild
	{
		[DataMember(Name = "BoardName")]
		private string boardName;
		/// <summary>
		/// 板の名前を取得または設定します
		/// </summary>
		[IgnoreDataMember]
		public string BoardName
		{
			get { return boardName; }
			set { boardName = value; }
		}
		[DataMember(Name = "BoardUrl")]
		private string boardAddress;
		/// <summary>
		/// 板のアドレスを取得または設定します
		/// </summary>
		[IgnoreDataMember]
		public string BoardAddress
		{
			get { return boardAddress; }
			set { boardAddress = value; }
		}
		
		
	}
}
