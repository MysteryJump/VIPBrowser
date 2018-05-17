using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIPBrowserLibrary.Chron.ThreadOrResData
{
    /// <summary>
    /// Resを格納するコレクションクラスです
    /// </summary>
    public class ResCollection : CollectionBase , ICollection , IEnumerable<Res>,IEnumerable
    {
        /// <summary>
        /// ResCollectionへのアクセスを同期するために使用できるオブジェクトを取得します。
        /// </summary>
        public object SyncRoot { get { return List.SyncRoot; } }
        /// <summary>
        /// 指定されたインデックスのレスを取得/設定します
        /// </summary>
        /// <param name="index">指定するインデックス</param>
        /// <returns>レス</returns>
        public Res this[int index]
        {
            get { return (Res)List[index]; }
            set { List[index] = value; }
        }
        /// <summary>
        /// レスを追加します
        /// </summary>
        /// <param name="r">Res構造体</param>
        /// <returns></returns>
        public int Add(Res r)
        {
            return this.List.Add(r);
        }
        /// <summary>
        /// レスの配列を追加します
        /// </summary>
        /// <param name="rCollection">Res構造体の配列</param>
        public void AddRange(Res[] rCollection)
        {
            this.InnerList.AddRange(rCollection);
        }
        /// <summary>
        /// レスコレクションを追加します
        /// </summary>
        /// <param name="rc">追加するResCollection</param>
        public void AddRange(ResCollection rc)
        {
            this.InnerList.AddRange(rc);
        }

        //public new IEnumerator GetEnumerator()
        //{
        //    for (int i = 0; i < this.Count ; i++)
        //    {
        //        yield return this[i];
        //    }
        //}


        /// <summary>
        /// ResCollectionをResで返す反復処理子を返します
        /// </summary>
        /// <returns>ResのIEnumerator</returns>
        public new IEnumerator<Res> GetEnumerator()
        {
            for (int i = 0; i < this.Count; i++)
            {
                yield return (Res)this.InnerList[i];
            }
        }
    }
}
