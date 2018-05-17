using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace VIPBrowserLibrary.Chron.ThreadOrResData
{
    /// <summary>
    /// アンカー関連の処理を行います
    /// </summary>
    public class AnchorProcessing
    {
        /// <summary>
        /// 参照アンカー >>XX を検索する正規表現
        /// </summary>
        public static readonly Regex RefAnchor =
            new Regex(@"&gt;(?<num>[,\d\-\+]+)", RegexOptions.Compiled);

		//public Res[] AnalysisDereference(IEnumerable<Res> resSet)
		//{
		//	int i = 0;
		//	List<Res> res = new List<Res>();
		//	foreach (var item in resSet)
		//	{
		//		AnchorCollection ac = new AnchorCollection();
		//		MatchCollection mc = RefAnchor.Matches(item.Sentence);
		//		foreach (Match anc in mc)
		//		{
		//			ac.Add(new Anchor(anc.Groups["num"].Value));
		//		}
		//		res.Add(item);
		//		foreach (var anc in ac)
		//		{
		//			var num = anc.AnchorNumber;
		//		}
		//	}
		//	throw new NotImplementedException();
		//}
		///// <summary>
		///// 
		///// </summary>
		///// <param name="p"></param>
		///// <returns></returns>
		//public static AnchorCollection Parse(Res p)
		//{
		//	AnchorCollection ac = new AnchorCollection();
            
		//	MatchCollection mc = RefAnchor.Matches(p.Sentence);
		//	if (mc.Count > 0)
		//	{
		//		foreach (Match item in mc)
		//		{
		//			Anchor anc = new Anchor(item.Groups["num"].Value);
		//			ac.Add(anc);
		//		}
		//	}
		//	return ac;
		//}
    }
}
