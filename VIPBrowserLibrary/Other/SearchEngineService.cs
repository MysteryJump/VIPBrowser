/* 各検索エンジンのRESTAPI
 * GoogleSearch 
 * https://www.google.co.jp/search?q=*****
 *
 * YahooSearch 
 * http://search.yahoo.co.jp/search?p=*****
 *
 * BingSearch 
 * http://www.bing.com/search?q=*****
 *
 * AmazonSearch 
 * http://www.amazon.co.jp/s/field-keywords=*****
 */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIPBrowserLibrary.Other
{
    /// <summary>
    /// 各検索エンジンへの検索機能を提供するクラスです
    /// </summary>
    public class SearchEngineService
    {
        private string SearchText = String.Empty;
        /// <summary>
        /// 指定した検索ワードを使用してこのインスタンスを初期化します
        /// </summary>
        /// <param name="searchText">検索するワード</param>
        public SearchEngineService(string searchText)
        {
            this.SearchText = searchText;
        }
        /// <summary>
        /// 検索に使用する検索エンジンを使用してこのインスタンスに関連付けられたUrlを取得します
        /// </summary>
        /// <param name="se">検索に使用する検索エンジン</param>
        /// <returns>検索するUrl</returns>
        public string GetUrl(VIPBrowserLibrary.Setting.DefaultSearchEngine se)
        {
            if (se == Setting.DefaultSearchEngine.Bing || se == Setting.DefaultSearchEngine.Yahoo)
            {
                this.SearchText = Utility.StringUtility.URLEncode(this.SearchText, Encoding.UTF8);
            }
            if (se == VIPBrowserLibrary.Setting.DefaultSearchEngine.Google)
            {
                return "http://www.google.co.jp/search?q=" + this.SearchText;
            }
            else if (se == VIPBrowserLibrary.Setting.DefaultSearchEngine.Yahoo)
            {

                return "http://search.yahoo.co.jp/search?p=" + this.SearchText;
            }
            else if (se == VIPBrowserLibrary.Setting.DefaultSearchEngine.Bing)
            {
                return "http://www.bing.com/search?q=" + this.SearchText;
            }
            else if (se == VIPBrowserLibrary.Setting.DefaultSearchEngine.Amazon)
            {
                return "http://www.amazon.co.jp/s/field-keywords=" + this.SearchText;
            }
            else
                throw new ArgumentException();
        }
    }
}
