using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIPBrowserLibrary.BBS.Common
{
    /// <summary>
    /// レスを送信するクラスの基底クラスです
    /// </summary>
    public abstract class PostBase
    {
		/// <summary>
		/// このクラスに関連付けられたSettingSerialインスタンスを取得または設定します
		/// </summary>
		protected Setting.SettingSerial SettingData { get; set; }
		/// <summary>
		/// このクラスの新しいインスタンスを初期化します
		/// </summary>
		public PostBase()
		{
			this.SettingData = new Setting.Serializer().Deserilize();
		}
        /// <summary>
        /// kakikomi.txtに追記します
        /// </summary>
        public void WriteRecords(Dictionary<string, string> dic, Chron.ThreadOrResData.ThreadData td)
        {
            WriteRecord wr = new WriteRecord();
            var r = this.ParseDictionaryToRes(dic, td.ThisBBS);
            wr.Write(r, td,this.SettingData);
        }
        /// <summary>
        /// Dictionary形式のレスデータをRes構造体に変換します
        /// </summary>
        /// <param name="data">変換するデータ</param>
        /// <param name="bt">変換元の板の帰属元</param>
        /// <returns>変換したRes構造体</returns>
        public Chron.ThreadOrResData.Res ParseDictionaryToRes(Dictionary<string, string> data, VIPBrowserLibrary.Common.BBSType bt)
        {
            Chron.ThreadOrResData.Res r;
            switch (bt)
            {
                case VIPBrowserLibrary.Common.BBSType._2ch:
                    {
                        r = new Chron.ThreadOrResData.Res(1, data["FROM"], data["mail"], data["MESSAGE"], "SampleID", "2013/11/16 21:47:59.02", "BE", true);
                    }
                    break;
                case VIPBrowserLibrary.Common.BBSType.jbbs:
                    {
                        r = new Chron.ThreadOrResData.Res(1, data["NAME"], data["MAIL"], data["MESSAGE"], "SampleKey", "2013/11/16 21:47:59.02","BE", true);
                    }
                    break;
                case VIPBrowserLibrary.Common.BBSType.machibbs:
                    {
                        throw new NotSupportedException();
                        //r = new Chron.ThreadOrResData.Res(0, data[""], data[""], data[""], data[""], data[""], data[""], true);
                    }
                default:
                    throw new ArgumentException();
            }
            return r;
        }
		/// <summary>
		/// Sambaに引っかかるか確認します
		/// </summary>
		/// <param name="url">対象の板Url</param>
		/// <returns>引っかかるかbool値で</returns>
		public async Task<ValueType[]> CheckSamba(string url)
		{
			Samba24 samba = new Samba24(url);
			return await samba.CheckSambaState(); 
		}
		/// <summary>
		/// Samba24用に最終書き込み時間を記録します
		/// </summary>
		/// <param name="url">対象の板Url</param>
		public void WriteSamba(string url)
		{
			Samba24 samba = new Samba24(url);
			samba.WriteSamba();
		}
    }
}