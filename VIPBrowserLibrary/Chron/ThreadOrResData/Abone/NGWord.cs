using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using VIPBrowserLibrary.Other.MyExtensions;
using System.Text.RegularExpressions;
namespace VIPBrowserLibrary.Chron.ThreadOrResData.Abone
{
   
    /// <summary>
    /// NGWordを表す構造体
    /// </summary>
    [Serializable]
    public unsafe struct NGWord// : System.Runtime.Serialization.ISerializable
    {
        #region フィールド
        private string url;
        private DateTime setTime;
        private TimeSpan releaseTime;
        private bool isRegex;
        private Regex regexWord;
        private string word;
        private AboneType ab;
        #endregion

        #region プロパティ
        /// <summary>
        /// 正規表現を使用するUrlを設定または取得します
        /// </summary>
        public string Url
        {
            get { return this.url; }
            set { this.url = value; }
        }
        /// <summary>
        /// NGWordが設定された時間を設定または取得します
        /// </summary>
        public DateTime SetTime
        {
            get { return this.setTime; }
            set { this.setTime = value; }
        }
        /// <summary>
        /// NGWordを解除する時間を設定または取得します
        /// </summary>
        public TimeSpan ReleaseTime
        {
            get { return this.releaseTime; }
            set { this.releaseTime = value; }
        }
        /// <summary>
        /// 通常文字列処理の場合のワードを設定または取得します
        /// </summary>
        public string Word
        {
            get { return this.word; }
            set { this.word = value; }
        }
        /// <summary>
        /// 正規表現を用いる場合の正規表現パターンを設定または取得します
        /// </summary>
        public Regex RegexWord
        {
            get { return this.regexWord; }
            set { this.regexWord = value; }
        }
        /// <summary>
        /// 正規表現どうかを取得します
        /// </summary>
        public bool IsRegex
        {
            get { return this.isRegex; }

        }
        /// <summary>
        /// このNGワードのあぼーんされている種類を設定します
        /// </summary>
        public AboneType AboneTypes
        {
            get { return this.ab; }
            set { this.ab = value; }
        }
#endregion

        #region コンストラクター
        /// <summary>
        /// NGWordに必要な条件を設定して初期化します
        /// </summary>
        /// <param name="word">設定するNGWord</param>
        /// <param name="url">利用先のURL</param>
        /// <param name="setTime">設定した時間</param>
        /// <param name="releaseTime">設定を解除する時間</param>
        /// <param name="ab">設定するNGWordのあぼーんタイプ</param>
        public NGWord(string word,AboneType ab,string url,DateTime setTime,TimeSpan releaseTime)
        {
            this.word = word;
            this.url = url;
            this.setTime = setTime;
            this.releaseTime = releaseTime;
            this.isRegex = false;
            this.regexWord = null;
            this.ab = ab;
        }
        /// <summary>
        /// NGWordに必要な条件を正規表現を用いて初期化します
        /// </summary>
        /// <param name="re">設定するNGWordの正規表現パターン</param>
        /// <param name="url">利用先のURL</param>
        /// <param name="setTime">設定した時間</param>
        /// <param name="releaseTime">設定を解除する時間</param>
        /// <param name="ab">設定するNGWordのあぼーんタイプ</param>
        public NGWord(Regex re,AboneType ab, string url, DateTime setTime, TimeSpan releaseTime)
        {
            this.regexWord = re;
            this.url = url;
            this.setTime = setTime;
            this.releaseTime = releaseTime;
            this.isRegex = true;
            this.word = null;
            this.ab = ab;
        }
        /// <summary>
        /// NGWordを時間を設定せずに初期化します
        /// </summary>
        /// <param name="url">利用先のURL</param>
        /// <param name="word">設定するNGWord</param>
        /// <param name="ab">設定するNGWordのあぼーんタイプ</param>
        public NGWord(string word, AboneType ab, string url)
        {
            this.word = word;
            this.url = url;
            this.setTime = DateTime.MinValue;
            this.releaseTime = TimeSpan.Zero;
            this.regexWord = null;
            this.isRegex = false;
            this.ab = ab;
        }
        /// <summary>
        /// NGWordを時間を設定せずに正規表現を用いて初期化します
        /// </summary>
        /// <param name="re">設定するNGWordの正規表現パターン</param>
        /// <param name="url">利用先のURL</param>
        /// <param name="ab">設定するNGWordのあぼーんタイプ</param>
        public NGWord(Regex re, AboneType ab, string url)
        {
            this.regexWord = re;
            this.url = url;
            this.setTime = DateTime.MinValue;
            this.releaseTime = TimeSpan.Zero;
            this.isRegex = true;
            this.word = null;
            this.ab = ab;
        }
        /// <summary>
        /// 全板共通のNGWordを初期化します
        /// </summary>
        /// <param name="word">設定するNGWord</param>
        /// <param name="ab">設定するNGWordのあぼーんタイプ</param>
        public NGWord(string word, AboneType ab)
        {
            this.regexWord = null;
            this.url = String.Empty;
            this.setTime = DateTime.MinValue;
            this.releaseTime = TimeSpan.Zero;
            this.isRegex = false;
            this.word = word;
            this.ab = ab;
        }
        /// <summary>
        /// 全板共通のNGWordを正規表現を用いて初期化します
        /// </summary>
        /// <param name="re">設定するNGWordの正規表現パターン</param>
        /// <param name="ab">設定するNGWordのあぼーんタイプ</param>
        public NGWord(Regex re, AboneType ab)
        {
            this.regexWord = re;
            this.url = String.Empty;
            this.setTime = DateTime.MinValue;
            this.releaseTime = TimeSpan.Zero;
            this.isRegex = true;
            this.word = null;
            this.ab = ab;
        }

        #endregion
        /// <summary>
        /// このクラスのシリアル化に用いるメソッド
        /// </summary>
        /// <param name="info">シリアル化のデータ</param>
        /// <param name="context">シリアル化のStream</param>
        public void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            info.AddValue("regexWord", this.regexWord);
            info.AddValue("isRegex",this.isRegex);
            info.AddValue("word", this.word);
            info.AddValue("ab", this.ab);
            info.AddValue("url", this.url);
            info.AddValue("releaseTime", this.releaseTime);
            info.AddValue("setTime", this.setTime);
        }
    }
}
