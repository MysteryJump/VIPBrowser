using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace VIPBrowserLibrary.BBS.Common
{
	/// <summary>
	/// 板のカテゴリを表すクラスです
	/// </summary>
	[Serializable]
	[DataContract(Namespace ="", Name="BoardCategory")]
	public sealed class BoardCategory
	{
		/// <summary>
		/// このクラスの新しいインスタンスを初期化します。
		/// </summary>
		public BoardCategory()
		{
			this.child = new List<BoardChild>();
		}
		
		[DataMember(Name="Category")]
		private string category;
		/// <summary>
		/// このクラスに関連付けられたカテゴリ名を取得または設定します。
		/// </summary>
		[IgnoreDataMember]
		public string Category
		{
			get { return category; }
			set { category = value; }
		}
		[DataMember(Name = "Children")]
		private List<BoardChild> child;
		/// <summary>
		/// このクラスに関連付けられた板を取得または設定します
		/// </summary>
		[IgnoreDataMember]
		public List<BoardChild> Children
		{
			get { return child; }
			set { child = value; }
		}
		
		
	}
}
