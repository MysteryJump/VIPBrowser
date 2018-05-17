using System;
using System.Text;
using VIPBrowserLibrary.BBS.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VIPBrowserLibrary.BBS.X2ch
{
    /// <summary>
    /// 2chおよびそれに準じる掲示板にレスをポストします
    /// </summary>
    public class X2chPoster : PostBase
    {
        private HttpClient hc = null;
        private string hostAddress = String.Empty;
        /// <summary>
        /// 送信先のホストアドレスを使用してこのインスタンスを初期化します
        /// </summary>
        /// <param name="host">送信先のホスト</param>
        public X2chPoster(string host)
        {

            hostAddress = host;
        }
        /// <summary>
        /// System.Uri形式のホストアドレスを使用してこのインスタンスを初期化します
        /// </summary>
        /// <param name="host"></param>
        public X2chPoster(Uri host)
        {

            hostAddress = host.ToString();
        }
        /// <summary>
        /// 指定したホストに向けてデータを送信します
        /// </summary>
        /// <param name="post">送信するデータ</param>
        /// <param name="isThread">スレ立てかどうか</param>
        /// <returns>成功したらtrue,失敗したらfalse</returns>
        /// <param name="td">送信するスレッドのデータ</param>
        public async Task<bool> Post2ch(Dictionary<string, string> post,bool isThread,Chron.ThreadOrResData.ThreadData td)
        {
			// SambaCheck
			var v = await base.CheckSamba(hostAddress.Replace("test/bbs.cgi", "") + post["bbs"] + "/");
			if (!((bool)v[0]))
			{
				System.Windows.Forms.MessageBox.Show("Please wait " + (((TimeSpan)(v[1])).TotalSeconds.ToString()) + " seconds.","Samba24 warning");
				return false;
			}
            //ここっていらないよね
            System.Text.RegularExpressions.Match m = new System.Text.RegularExpressions.Regex("http://(?<host>.+)/.+/bbs.cgi",System.Text.RegularExpressions.RegexOptions.Compiled).Match(hostAddress);
            hostAddress = String.Format("http://{0}/test/bbs.cgi", m.Groups["host"].Value);
            bool isCan = false;
            StringBuilder postStringBuilder = new StringBuilder().Clear();
            foreach (var item in post)
            {
                postStringBuilder.Append(item.Key).Append("=").Append(item.Value).Append("&");
            }
            string poststr = postStringBuilder.ToString();
            bool firstPost = false;
            while (true)
            {
                hc = new HttpClient(hostAddress);
                hc.Host = hostAddress.Trim('/').Replace("http://", "").Replace("bbs.cgi", "");
                hc.IsReadCookieFromHost = true;
                if (!isThread)
                    hc.Referer = String.Format("{0}read.cgi/{1}/{2}", hostAddress.Replace("bbs.cgi", ""), post["bbs"], post["key"]);
                else
                    hc.Referer = String.Format("{0}/{1}",hostAddress.Replace("test/bbs.cgi",""),post["bbs"]);
                if (!isThread)
                    poststr = firstPost ? poststr + "submit=上記全てを承諾して書き込む" : poststr + "submit=書き込む";
                else
                    poststr = firstPost ? poststr + "submit=上記全てを承諾して書き込む" : poststr + "submit=新規スレッド作成";
				poststr = this.RoninProcesing(poststr);
                string res = await hc.PostStringAsync(poststr);
                string hidden = String.Empty;
                if (res.IndexOf("<!-- 2ch_X:cookie -->") != -1)
                {
                    if (hostAddress.IndexOf("2ch.net") != -1 || hostAddress.IndexOf("bbspink.com") != -1)
                    {
                        ResponseParse(res, out hidden);
                        firstPost = true;
                        string[] hiddencookie = hidden.Split('=');
                        System.Net.CookieCollection cc = new System.Net.CookieCollection();
                        cc.Add(new System.Net.Cookie(hiddencookie[0], hiddencookie[1], "/", hostAddress.Replace("/test/bbs.cgi", "").Replace("http://", "")));
                        HttpClient.CookieManagement.WriteCookieToDisk(cc);
                    }
                    if (System.Windows.Forms.DialogResult.No == System.Windows.Forms.MessageBox.Show(WarningText,"投稿確認",System.Windows.Forms.MessageBoxButtons.YesNo,System.Windows.Forms.MessageBoxIcon.Information))
                    {
                        isCan = false;
                        break;
                    }
                }
                else if (res.IndexOf("書きこみました。") != -1)
                {
                    isCan = true;
                    base.WriteRecords(post, td);
					base.WriteSamba(hostAddress.Replace("test/bbs.cgi", "") + post["bbs"] + "/");
                    break;
                }
                else
                {
                    System.Windows.Forms.Form f = new System.Windows.Forms.Form();
                    System.Windows.Forms.WebBrowser wb = new System.Windows.Forms.WebBrowser();
                    wb.Dock = System.Windows.Forms.DockStyle.Fill;
                    wb.DocumentText = res;
                    f.Controls.Add(wb);
                    f.Width = 250;
                    f.Show();
                    isCan = false;
                    break;
                }
                hc = null;
                poststr = poststr.Replace("submit=書き込む", "").Replace("submit=上記全てを承諾して書き込む", "").Replace("submit=新規スレッド作成", "");
            }
            return isCan;

        }
        /// <summary>
        /// 警告画面のデータを抽出します
        /// </summary>
        /// <param name="responseData">警告画面のhtml</param>
        /// <param name="hiddenword">抽出した文字</param>
        /// <returns>成功したかどうか</returns>
        private bool ResponseParse(string responseData, out string hiddenword)
        {
            System.Text.RegularExpressions.Match m = new System.Text.RegularExpressions.Regex(@"<input type=hidden name=""(?<name>.+)"" value=""(?<value>.{5,15})"">",System.Text.RegularExpressions.RegexOptions.Compiled).Match(responseData);
            string[] hiddenwords = { m.Groups["name"].Value, m.Groups["value"].Value };
            string hiddensentence = hiddenwords[0] + "=" + hiddenwords[1];
            hiddenword = hiddensentence;
            return true;
        }

		private string RoninProcesing(string data)
		{
			if (this.SettingData.IsRoninLogined && (hostAddress.IndexOf("2ch.net") != -1 || hostAddress.IndexOf("bbspink.com") != -1))
			{
				Ronin r = new Ronin();
				data.TrimEnd('&');
				data += "&sid=" + r.SecretKey;
				return data;
			}
			return data;
		}

        private string WarningText = @"
・投稿者は、投稿に関して発生する責任が全て投稿者に帰すことを承諾します。
・投稿者は、話題と無関係な広告の投稿に関して、相応の費用を支払うことを承諾します
・投稿者は、投稿された内容及びこれに含まれる知的財産権、（著作権法第21条ないし第28条に規定される権利も含む）その他の権利につき（第三者に対して再許諾する権利を含みます。）、掲示板運営者に対し、無償で譲渡することを承諾します。ただし、投稿が別に定める削除ガイドラインに該当する場合、投稿に関する知的財産権その他の権利、義務は一定期間投稿者に留保されます。
・掲示板運営者は、投稿者に対して日本国内外において無償で非独占的に複製、公衆送信、頒布及び翻訳する権利を投稿者に許諾します。また、投稿者は掲示板運営者が指定する第三者に対して、一切の権利（第三者に対して再許諾する権利を含みます）を許諾しないことを承諾します。
・投稿者は、掲示板運営者あるいはその指定する者に対して、著作者人格権を一切行使しないことを承諾します。
";
    }
}