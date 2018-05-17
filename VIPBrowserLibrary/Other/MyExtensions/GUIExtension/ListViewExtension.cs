using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VIPBrowserLibrary.Other.MyExtensions.GUIExtension
{
    /// <summary>
    /// ListViewコントロールを拡張したカスタムコントロールです
    /// </summary>
    public class ListViewExtension : ListView
    {
        /// <summary>
        /// リストビューにイベントを追加します
        /// </summary>
        /// <param name="item">追加するListViewItem</param>
        public void AddItem(ListViewItem item)
        {
            this.BeginUpdate();
            if (this.isSetMaxLength)
            {
                int c = this.Items.Count;
                if (c >= this.maxLength)
                {
                    Items.RemoveAt(c - 1);
                }
                Items.Insert(0, item);
            }
            else
                Items.Add(item);
            this.EndUpdate();
            if (ItemAdd != null)
                ItemAdd.Invoke(this, new ItemsAddedArgs(item));
        }
        ///// <summary>
        ///// AddItemの際のイベントハンドラー
        ///// </summary>
        private ItemAddedDel ItemAdd { get; set; }
        /// <summary>
        /// ItemAddedイベントを発生させます
        /// </summary>
        protected void OnItemAdded() 
        {
            if (this.ItemAdd != null)
                this.ItemAdd.Invoke(this, null);
        }
        /// <summary>
        /// アイテムがAddItemを使用して追加されたときに発生するイベントです
        /// </summary>
        /// <param name="sender">このオブジェクト</param>
        /// <param name="e">イベントデータ</param>
        public delegate void ItemAddedDel(object sender, ItemsAddedArgs e);
        /// <summary>
        /// アイテムがAddItemを使用して追加されたときに発生するイベントです
        /// </summary>
        [System.ComponentModel.Browsable(true)]
        [System.ComponentModel.Category("動作")]
        public event ItemAddedDel ItemAdded { remove { } add { } }
        /// <summary>
        /// ListView追加時のイベントを表します
        /// </summary>
        public class ItemsAddedArgs : EventArgs
        {
            /// <summary>
            /// このクラスのコンストラクター
            /// </summary>
            /// <param name="item"></param>
            public ItemsAddedArgs(ListViewItem item)
            {
                Item = item;
            }
            /// <summary>
            /// このイベントデータに関連付けられているListViewItem
            /// </summary>
            public ListViewItem Item { get; set; }
        }
        private bool isSetMaxLength;
        private int maxLength;
        /// <summary>
        /// このListViewに収納可能なListViewItemの数を設定または取得します。設定されていない場合は-1を返します
        /// </summary>
        [System.ComponentModel.Browsable(true)]
        [System.ComponentModel.Category("動作")]
        public int MaxLength
        {
            get 
            {
                if (!isSetMaxLength)
                    return -1;
                return maxLength;
            }
            set
            {
                this.isSetMaxLength = true;
                maxLength = value;
            }
        }
        
    }
}
