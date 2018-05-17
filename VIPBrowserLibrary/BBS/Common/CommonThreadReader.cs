using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using VIPBrowserLibrary.Chron.ThreadOrResData;
using VIPBrowserLibrary.Chron.ThreadOrResData.Abone;
using System.IO;


namespace VIPBrowserLibrary.BBS.Common
{
    /// <summary>
    /// スレッド読み込みの共通クラスです
    /// </summary>
    public class CommonThreadReader : Chron.ThreadOrResData.HtmlSkin
    {
		Setting.GeneralSetting gs = new Setting.GeneralSetting();
        /// <summary>
        /// 指定された正規表現を用いてdatを変換します
        /// </summary>
        /// <param name="dat">変換するdat</param>
        /// <param name="re">使用する正規表現</param>
        /// <returns>変換後のHTMLデータ</returns>
        /// <param name="url">読み込み先のアドレス</param>
		/// <param name="isOutHtml">Htmlを出力する表します</param>
        protected string GetResponse(string dat, Regex re,string url,bool isOutHtml = true)
        {
            try
            {
                //SimpleAbone sa = new SimpleAbone();
                //sa.InstLoad();

                AboneManagement am = new AboneManagement();
                am.InstLoad();
				var settingData = new Setting.Serializer().Deserilize();
                base.LoadSkin(settingData.UsingSkinPath);
                int resCount = 1;

                int count = 0;

                
                MatchCollection mc = re.Matches(dat);
                int mcount = mc.Count;
                Dictionary<string, int[]> IDCollection = new Dictionary<string, int[]>();
                Res[] r = new Res[mcount];
                if (mc.Count <= 0)
                    return null;
                threadName = mc[0].Groups["threadName"].Value;
                StringBuilder resData = new StringBuilder().Append(base.AddHeader(threadName));
                //foreach (Match m in mc)
                //{
                //    string id = m.Groups["ID"].Value;
                //    if (IDCollection.ContainsKey(id))
                //    {
                //        IDCollection[id][1]++;
                //    }
                //    else
                //    {
                //        IDCollection.Add(id, new int[] { 0, 1 });
                //    }
                //}
                for (int i = 0; i < mcount; i++)
                {
                    string id = mc[i].Groups["ID"].Value;
                    if (IDCollection.ContainsKey(id))
                    {
                        IDCollection[id][1]++;
                    }
                    else
                    {
                        IDCollection.Add(id, new int[] { 0, 1 });
                    }
                }
                foreach (Match m in mc)
                {


                    r[count] = new Res(resCount, m.Groups["name"].Value, m.Groups["mail"].Value, m.Groups["sentence"].Value, m.Groups["ID"].Value, m.Groups["date"].Value, m.Groups["BE"].Value, true);
                    count++;
                    resCount++;
                }
				if (isOutHtml)
				{
					if (settingData.IsMultiThreading)
					{
						SortedDictionary<int, string> str = new SortedDictionary<int, string>();
						Parallel.For(0, r.Length, (d) =>
						{
							string id = r[d].ID;
							IDCollection[id][0] = IDCollection[id][0] + 1;
							r[d].Visible = am.IsVisible(r[d], url);
							Res rs = new Res();
							string datra = base.SimpleConvertCore(r[d], IDCollection, out rs);
							lock (this)
							{
								str.Add(r[d].Index, datra);
							}
							r[d] = rs;

						});
						foreach (var item in str)
						{
							resData.Append(item.Value);
						}
					}
					else
					{
						for (int i = 0; i < r.Length; i++)
						{

							string id = r[i].ID;
							IDCollection[id][0] = IDCollection[id][0] + 1;
							//r[i].Visible = sa.IsVisible(r[i]);
							r[i].Visible = am.IsVisible(r[i], url);
							Res rs = new Res();
							resData.Append(base.SimpleConvertCore(r[i], IDCollection, out rs));
							r[i] = rs;
						}
					}
				}
				else
				{
					for (int i = 0; i < r.Length; i++)
					{
						string id = r[i].ID;
						IDCollection[id][0] = IDCollection[id][0] + 1;
						r[i].Visible = am.IsVisible(r[i], url);
					}
				}
				this.IdCollection = IDCollection;
				this.resCount = resCount;
				this.resSets = r;
                return resData.ToString();
            }
            catch (ArgumentOutOfRangeException) { return null; }
        }
        /// <summary>
        /// 変換したスレッドのスレ名を返します
        /// </summary>
        protected virtual string ThreadName { get { return threadName; } }
        private string threadName;
        /// <summary>
        /// 変換したスレッドのレス数を返します
        /// </summary>
        protected virtual int ResCount { get { return resCount; } }
        private int resCount;
        /// <summary>
        /// 変換したスレッドのResの配列を返します
        /// </summary>
        protected virtual Chron.ThreadOrResData.Res[] ResSets
        {
            get
            { return resSets; }
        }
        private Chron.ThreadOrResData.Res[] resSets = null;
		/// <summary>
		/// このインスタンスに関連付けられているIdCollectionを取得します
		/// </summary>
		public Dictionary<string, int[]> IdCollection { private set; get; }
    }
}
