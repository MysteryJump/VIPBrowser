using System;
using System.Text;

namespace VIPBrowserLibrary.Chron.ThreadOrResData
{
    /// <summary>
    /// レス一つの情報を格納します
    /// </summary>
    public unsafe struct Res
    {
        /// <summary>
        /// 各パラメータに従ってレスの情報を生成します
        /// </summary>
        /// <param name="ind">レス番号</param>
        /// <param name="name">名前</param>
        /// <param name="mail">メール</param>
        /// <param name="sentense">本文</param>
        /// <param name="id">ID</param>
        /// <param name="date">日付・時間</param>
        /// <param name="visible">不可視</param>
        /// <param name="be">BE</param>
        public Res(int ind,string name,string mail,string sentense,string id,string date,string be,bool visible)
        {
            if (ind < 1)
                throw new ArgumentException("レス番号は1以上でなければなりません");
            if (name == null || sentense == null || date == null)
                throw new ArgumentNullException();
            this.index = ind;
            this.name = name;
            this.mail = mail;
            this.sentence = sentense;
            this.id = id;
            this.date = date;
            this.visible = visible;
            this.be = be;
        }

        #region バッキングフィールド
        private int index;
        private string name;
        private string mail;
        private string sentence;
        private string id;
        private string date;
        private bool visible;
        private string  be;
        #endregion
        #region プロパティ
        /// <summary>
        /// BEの情報を設定および取得します
        /// </summary>
        public string BE
        {
            get { return this.be; }
            set { this.be = value; }
        }
        
        /// <summary>
        /// レスが可視可能か設定および取得します
        /// </summary>
        public bool Visible
        {
            get { return this.visible; }
            set { this.visible = value; }
        }
        /// <summary>
        /// レス番を設定および取得します
        /// </summary>
        public int Index 
        {
            get { return this.index; }
            set { index = value; }
        }
        /// <summary>
        /// 名前欄を設定および取得します
        /// </summary>
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }
        /// <summary>
        /// メール欄を設定および取得します
        /// </summary>
        public string Mail
        {
            get { return this.mail; }
            set { this.mail = value; }
        }
        /// <summary>
        /// 本文を設定および取得します
        /// </summary>
        public string Sentence
        {
            get {return this.sentence;}
            set { this.sentence = value;}
        }
        /// <summary>
        /// IDを設定および取得します
        /// </summary>
        public string ID
        {
            get { return this.id; }
            set { this.id = value; }
        }
        /// <summary>
        /// 日付欄を設定および取得します
        /// </summary>
        public string Date
        {
            get { return this.date; }
            set { this.date = value; }
        }
        /// <summary>
        /// このレスに関連付けられたアンカーを取得します
        /// </summary>
        public AnchorCollection Anchors
        {
            get 
            {
                return null;
            //    return AnchorProcessing.Parse(this); 
            }
        }
        /// <summary>
        /// 逆参照されているResの配列を表します
        /// </summary>
        public Anchor[] DereferenceRes
        {
            get { return null; }
            internal set { throw new NotImplementedException(); }
        }
        /// <summary>
        /// DateTime形式の日付を取得します
        /// </summary>
        public DateTime ParseDateTime { get { return DateTime.Parse(Date); } }
        #endregion
        #region メソッド
        /// <summary>
        /// 現在のレスの情報をString形式で返します
        /// </summary>
        /// <returns>String形式のレス情報</returns>
        public override string ToString()
        {
            string data = new StringBuilder().Append("Index=").AppendLine(index.ToString())
                .Append("Name=").AppendLine(name)
                .Append("Mail=").AppendLine(mail)
                .Append("Sentence=").AppendLine(sentence)
                .Append("ID=").AppendLine(id)
                .Append("BE=").AppendLine(be)
                .Append("Date=").AppendLine(date)
                .Append("Visible=").AppendLine(visible.ToString()).ToString();
            return data;
        }
        #endregion
    }
}