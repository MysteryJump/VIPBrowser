using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using VIPBrowserLibrary.Setting;

namespace VIPBrowserLibrary.BBS.Common
{
	/// <summary>
	/// XMLを使用して板一覧を表すクラスです。
	/// </summary>
	public class XmlBoardList
	{
		/// <summary>
		/// XMLの板一覧データを取得します
		/// </summary>
		public void GetXmlBoardList()
		{
			WebClient wec = new WebClient();
			wec.Encoding = Encoding.UTF8;
			var data = wec.DownloadString("http://38-ch.net/bbsmenu/get?format=xml&charset=UTF-8&callback=callback");
			this.XmlBoardData = XDocument.Parse(data);
		}
		/// <summary>
		/// XMLの板一覧データを非同期で取得します
		/// </summary>
		/// <returns></returns>
		public async Task GetXmlBoardAsync()
		{
			WebClient wec = new WebClient();
			wec.Encoding = Encoding.UTF8;
			var data = await wec.DownloadStringTaskAsync("http://38-ch.net/bbsmenu/get?format=xml&charset=UTF-8&callback=callback");
			this.XmlBoardData = XDocument.Parse(data);
		}
		/// <summary>
		/// ボードツリーをXmlBoardDataプロパティより読み込み、List&lt;BoardCategory&gt;を取得します。
		/// </summary>
		/// <returns>List&lt;BoardCategory&gt;インスタンス</returns>
		public List<BoardCategory> GetBoardTree()
		{
			var cate = new List<BoardCategory>();
			var first = this.XmlBoardData.Element(XName.Get("bbsmenu"));
			var element = first.Elements();
			foreach (var item in element)
			{
				var atri = item.Attributes();
				var bt = new BoardCategory();
				foreach (var item2 in atri)
				{
					if (item2.Name.LocalName == "title")
					{
						bt.Category = item2.Value;
					}
				}
				foreach (var item2 in item.Elements())
				{
					var bc = new BoardChild();
					foreach (var item3 in item2.Attributes())
					{
						if (item3.Name.LocalName == "title")
						{
							bc.BoardName = item3.Value;
						}
						else if (item3.Name.LocalName == "url")
						{
							bc.BoardAddress = item3.Value;
						}
					}
					bt.Children.Add(bc);
				}
				cate.Add(bt);
			}
			return cate;
		}
		/// <summary>
		/// XML板一覧データを表します
		/// </summary>
		public XDocument XmlBoardData { get; set; }
		/// <summary>
		/// List&lt;BoardCategory&gt;インスタンスをList&lt;TreeNode&gt;に変換します。
		/// </summary>
		/// <param name="boards">変換するList&lt;TreeNode&gt;インスタンス</param>
		/// <returns>変換されたList&lt;TreeNode&gt;インスタンス</returns>
		public List<TreeNode> ConvertWindowsFormsTreeView(List<BoardCategory> boards)
		{
			var nodesParent = new List<TreeNode>();
			foreach (var item in boards)
			{
				var categoryNode = new TreeNode();
				categoryNode.Text = item.Category;
				foreach (var item2 in item.Children)
				{
					var childNode = new TreeNode();
					childNode.Text = item2.BoardName;
					childNode.Name = item2.BoardAddress;
					categoryNode.Nodes.Add(childNode);
				}
				nodesParent.Add(categoryNode);
			}
			return nodesParent;
		}
		/// <summary>
		/// 指定のパスからデータを読み込みます
		/// </summary>
		/// <param name="path">データを読み込むパス</param>
		/// <returns>読み込まれたList&lt;BoardCategory&gt;</returns>
		public static List<BoardCategory> Deserialize(string path)
		{
			// こういうときにusingを使うべきだと思う
			var reader = XmlReader.Create(path);
			var ser = new DataContractSerializer(typeof(List<BoardCategory>));
			var data = (List<BoardCategory>)ser.ReadObject(reader);
			reader.Close();
			return data;
		}
		/// <summary>
		/// 指定のパスにデータを保存します
		/// </summary>
		/// <param name="path">データを保存するパス</param>
		/// <param name="boardList">保存するList&lt;BoardCategory&gt;</param>
		public static void Serialize(string path, List<BoardCategory> boardList)
		{
			var ser = new DataContractSerializer(typeof(List<BoardCategory>));
			if (System.IO.File.Exists(path))
				using (System.IO.File.Create(path)) { };
			var reader = XmlWriter.Create(path);
			ser.WriteObject(reader, boardList);
			reader.Close();
		}
		/// <summary>
		/// Xml板一覧データの設定パスを表します
		/// </summary>
		public static string XmlBoardListSettingPath
		{
			get
			{
				var gs = new GeneralSetting();
				return gs.CurrentDirectory + "\\xmlboards.xml";
			}
		}
	}
}
