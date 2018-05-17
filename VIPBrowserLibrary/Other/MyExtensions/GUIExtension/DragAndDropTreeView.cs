using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Design;
using System.Windows.Forms;
using System.Drawing;

namespace VIPBrowserLibrary.Other.MyExtensions.GUIExtension
{
    /// <summary>
    /// ドラッグとドロップできる機能を追加したTreeViewを提供します
    /// </summary>
    public class DragAndDropTreeView : TreeView
    {
        /// <summary>
        /// このクラスの新しいインスタンスを初期化します
        /// </summary>
        public DragAndDropTreeView() : base()
        {
            this.Form1_Load();
        }
        private void Form1_Load()
        {
            this.AllowDrop = true;

            //イベントハンドラを追加する
            this.ItemDrag +=
                new ItemDragEventHandler(TreeView1_ItemDrag);
            this.DragOver +=
                new DragEventHandler(TreeView1_DragOver);
            this.DragDrop +=
                new DragEventHandler(TreeView1_DragDrop);
        }

        //ノードがドラッグされた時
        private void TreeView1_ItemDrag(object sender, ItemDragEventArgs e)
        {
            TreeView tv = sender as TreeView;
            tv.SelectedNode = e.Item as TreeNode;
            if (tv == null||tv.SelectedNode == null)
            {
                return;
            }
            tv.Focus();
            //ノードのドラッグを開始する
            DragDropEffects dde =
                tv.DoDragDrop(e.Item, DragDropEffects.All);
            //移動した時は、ドラッグしたノードを削除する
            if ((dde & DragDropEffects.Move) == DragDropEffects.Move)
                tv.Nodes.Remove((TreeNode)e.Item);
        }

        //ドラッグしている時
        private void TreeView1_DragOver(object sender, DragEventArgs e)
        {
            //ドラッグされているデータがTreeNodeか調べる
            if (e.Data.GetDataPresent(typeof(TreeNode)))
            {
                if ((e.KeyState & 8) == 8 &&
                    (e.AllowedEffect & DragDropEffects.Copy) ==
                    DragDropEffects.Copy)
                    //Ctrlキーが押されていればCopy
                    //"8"はCtrlキーを表す
                    e.Effect = DragDropEffects.Copy;
                else if ((e.AllowedEffect & DragDropEffects.Move) ==
                    DragDropEffects.Move)
                    //何も押されていなければMove
                    e.Effect = DragDropEffects.Move;
                else
                    e.Effect = DragDropEffects.None;
            }
            else
                //TreeNodeでなければ受け入れない
                e.Effect = DragDropEffects.None;

            //マウス下のNodeを選択する
            if (e.Effect != DragDropEffects.None)
            {
                TreeView tv = (TreeView)sender;
                //マウスのあるNodeを取得する
                TreeNode target =
                    tv.GetNodeAt(tv.PointToClient(new Point(e.X, e.Y)));
                //ドラッグされているNodeを取得する
                TreeNode source =
                    (TreeNode)e.Data.GetData(typeof(TreeNode));
                //マウス下のNodeがドロップ先として適切か調べる
                if (target != null && target != source &&
                        !IsChildNode(source, target))
                {
                    //Nodeを選択する
                    if (target.IsSelected == false)
                        tv.SelectedNode = target;
                }
                else
                    e.Effect = DragDropEffects.None;
            }
        }

        //ドロップされたとき
        private void TreeView1_DragDrop(object sender, DragEventArgs e)
        {
            //ドロップされたデータがTreeNodeか調べる
            if (e.Data.GetDataPresent(typeof(TreeNode)))
            {
                TreeView tv = (TreeView)sender;
                //ドロップされたデータ(TreeNode)を取得
                TreeNode source =
                    (TreeNode)e.Data.GetData(typeof(TreeNode));
                //ドロップ先のTreeNodeを取得する
                TreeNode target =
                    tv.GetNodeAt(tv.PointToClient(new Point(e.X, e.Y)));
                //マウス下のNodeがドロップ先として適切か調べる
                if (target != null && target != source &&
                    !IsChildNode(source, target))
                {
                    //ドロップされたNodeのコピーを作成
                    TreeNode cln = (TreeNode)source.Clone();
                    //Nodeを追加
                    target.Nodes.Add(cln);
                    //ドロップ先のNodeを展開
                    target.Expand();
                    //追加されたNodeを選択
                    tv.SelectedNode = cln;

                }
                else
                    e.Effect = DragDropEffects.None;
            }
            else
                e.Effect = DragDropEffects.None;
        }

        /// <summary>
        /// あるTreeNodeが別のTreeNodeの子ノードか調べる
        /// </summary>
        /// <param name="parentNode">親ノードか調べるTreeNode</param>
        /// <param name="childNode">子ノードか調べるTreeNode</param>
        /// <returns>子ノードの時はTrue</returns>
        private static bool IsChildNode(TreeNode parentNode, TreeNode childNode)
        {
            if (childNode.Parent == parentNode)
                return true;
            else if (childNode.Parent != null)
                return IsChildNode(parentNode, childNode.Parent);
            else
                return false;
        }
    }
}
