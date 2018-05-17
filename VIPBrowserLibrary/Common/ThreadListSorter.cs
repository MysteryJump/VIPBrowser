using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIPBrowserLibrary.Common
{
    /// <summary>
    /// スレッドリストのソートを提供します
    /// </summary>
    public class ThreadListSorter : IComparer
    {
        /// <summary>
        /// ソートを行うカラム
        /// </summary>
        public int column;
        /// <summary>
        /// ソートの降順また昇順
        /// </summary>
        public bool isAssecing = false;
        /// <summary>
        /// カラムと降順か昇順かを設定してこのクラスの新しいインスタンスを初期化します
        /// </summary>
        /// <param name="_column">カラムのインデックス</param>
        /// <param name="isa">昇順ならばtrue,降順ならばfalse</param>
        public ThreadListSorter(int _column,bool isa)
        {
            column = _column;
            isAssecing = isa;
        }
        int i;
        int j;

        DateTime id;
        DateTime jd;

        int ans;
        /// <summary>
        /// オブジェクト同士の比較を行います
        /// </summary>
        /// <param name="xx">比較の元となるオブジェクト</param>
        /// <param name="yy">比較対象のオブジェクト</param>
        /// <returns>比較の結果</returns>
        public int Compare(object xx, object yy)
        {
            
            System.Windows.Forms.ListViewItem x = (System.Windows.Forms.ListViewItem)xx;
            System.Windows.Forms.ListViewItem y = (System.Windows.Forms.ListViewItem)yy;
            if (int.TryParse(x.SubItems[column].Text, out i) && int.TryParse(y.SubItems[column].Text, out j))
            {
                if (isAssecing)
                    ans = i.CompareTo(j);
                else
                    ans = (-i.CompareTo(j));
            }
            else if (DateTime.TryParse(x.SubItems[column].Text, out id) && DateTime.TryParse(y.SubItems[column].Text, out jd))
            {
                if (isAssecing)
                    ans = id.CompareTo(jd);
                else
                    ans = (-id.CompareTo(jd));
            }
            else
            {
                if (isAssecing)
                    ans = String.Compare(x.SubItems[column].Text, y.SubItems[column].Text);
                else
                    ans = (-String.Compare(x.SubItems[column].Text, y.SubItems[column].Text));
            }

            return ans;
        }
    }
}
