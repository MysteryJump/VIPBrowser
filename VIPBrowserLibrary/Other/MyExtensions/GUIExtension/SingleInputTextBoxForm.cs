using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VIPBrowserLibrary.Other.MyExtensions.GUIExtension
{
    /// <summary>
    /// 一つのラベルとテキストボックス、Okボタンとキャンセルボタンによって構成されるフォームです
    /// </summary>
    public partial class SingleInputTextBoxForm : Form
    {
        /// <summary>
        /// このクラスの新しいインスタンスを初期化します
        /// </summary>
        public SingleInputTextBoxForm()
        {
            InitializeComponent();
        //    this.FormClosing += SingleInputTextBoxForm_FormClosing;
        }
        private bool isCancel = true;
        
        /// <summary>
        /// フォームに存在する唯一のラベルが表す文字列を取得または設定します
        /// </summary>
        public string ShowLabelText 
        {
            get { return this.label1.Text; }
            set { this.label1.Text = value; }
        }
        /// <summary>
        /// このフォームの設問の答えを設定または取得します。
        /// </summary>
        public string TextBoxText
        {
            get { return this.textBox1.Text; }
            set { this.textBox1.Text = value; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.isCancel = false;
            this.Close();
        }

        /// <summary>
        /// フォームをモーダル ダイアログ ボックスとして表示します。
        /// </summary>
        /// <returns>System.Windows.Forms.DialogResult 値のいずれか。</returns>
        public new DialogResult ShowDialog()
        {
            base.ShowDialog();
            return this.OutDialogResult();
        }
        /// <summary>
        /// 指定した所有者を持つモーダル ダイアログ ボックスとしてフォームを表示します。
        /// </summary>
        /// <param name="owner">モーダル ダイアログ ボックスを所有するトップレベル ウィンドウを表す System.Windows.Forms.IWin32Window を実装しているオブジェクト。</param>
        /// <returns>System.Windows.Forms.DialogResult 値のいずれか。</returns>
        public new DialogResult ShowDialog(IWin32Window owner)
        {
            base.ShowDialog();
            return this.OutDialogResult();
        }

        private DialogResult OutDialogResult()
        {
            if (this.isCancel)
            {
                return System.Windows.Forms.DialogResult.Cancel;
            }
            else
            {
                return System.Windows.Forms.DialogResult.OK;
            }
        }
        
        //private new FormBorderStyle FormBorderStyle { get; set; }
        //private new ControlCollection Controls { get; set; }
        //private new Icon Icon { get; set; }
        //private new HScrollProperties HorizontalScroll { get; set; }
        //private new Menu MainMenu { get; set; }
        //private new MenuStrip MainMenuStrip { get; set; }
        //private new bool MaximizeBox { get; set; }
        //private new Size MaximizeSize { get; set; }
        //private new SizeGripStyle SizeGripStyle { get; set; }
    }
}
