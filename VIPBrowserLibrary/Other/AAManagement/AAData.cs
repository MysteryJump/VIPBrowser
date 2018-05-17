using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VIPBrowserLibrary.Other.AAManagement
{
    /// <summary>
    /// AAのデータを表します
    /// </summary>
    [Serializable]
    public class AAData : System.Runtime.Serialization.ISerializable
    {
        /// <summary>
        /// AAのコンテンツ
        /// </summary>
        public string Content;
        /// <summary>
        /// AAの閲覧回数
        /// </summary>
        public int ViewCount;
        /// <summary>
        /// AAのお気に入り登録状況
        /// </summary>
        public bool Favorite;
        /// <summary>
        /// AAに関連付けられたタグ
        /// </summary>
        public string[] Tag;
        /// <summary>
        /// AAのシリアル化に用いるオブジェクトデータを取得します
        /// </summary>
        /// <param name="info">シリアル化の基礎データ</param>
        /// <param name="context">シリアル化の基礎のStream</param>
        public virtual void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            info.AddValue("Content", this.Content);
            info.AddValue("ViewCount", this.ViewCount);
            info.AddValue("Favorite", this.Favorite);
            info.AddValue("Tag",this.Tag,typeof(string[]));
        }
        /// <summary>
        /// このクラスの新しいインスタンスを初期化します
        /// </summary>
        public AAData()
        {

        }
        /// <summary>
        /// AADataの逆シリアル化に用いるクラスです
        /// </summary>
        /// <param name="info">シリアル化の基礎データ</param>
        /// <param name="context">シリアル化の基礎のStream</param>
        protected AAData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            this.Content = info.GetString("Content");
            this.ViewCount = info.GetInt32("ViewCount");
            this.Favorite = info.GetBoolean("Favorite");
            this.Tag = (string[])info.GetValue("Tag", typeof(string[]));
        }

    }
}
