/* This Class Used Twintail Library.
 * 
 * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mshtml;
using System.Windows.Forms;
using System.Drawing;
using VIPBrowserLibrary.Chron.ThreadOrResData;
using System.Text.RegularExpressions;

namespace VIPBrowser.ch2Browser.Popup
{
    /// <summary>
    /// HTMLのポップアップを行うクラスです
    /// </summary>
    public class HtmlPopup
    {
        private IEComponentThreadViewer iectv = null;


        // NTwin23
        //		private Point adjust = new Point(10, 10);
        private Point adjust = new Point(8, 8);
        private Point adjustImg = new Point(8, 0);
        // NTwin23

        private PopupPosition position;
        private Size maximum;
        private Font font;
        private string foreColorHtml;
        private string backColorHtml;
        private string fontHtml;
        private int width, height;
        private Size imageSize;

        private int childCount;
        internal bool inPopup = false;
        #region プロパティー
        public int Count
        {
            get
            {
                return childCount;
            }
        }

        /// <summary>
        /// ポップアップする座標を取得
        /// </summary>
        protected Point PopupLocation
        {
            get
            {
                HTMLBody body = iectv.HtmlBody;
                Point pt = iectv.PointToClient(Control.MousePosition);
                pt.Y += body.scrollTop;
                pt.X += body.scrollLeft;
                switch (position)
                {
                    case PopupPosition.TopLeft:
                        return new Point(pt.X - Width + adjust.X, pt.Y - Height + adjust.Y);

                    case PopupPosition.TopRight:
                        return new Point(pt.X - adjust.X, pt.Y - Height + adjust.Y);

                    case PopupPosition.BottomLeft:
                        return new Point(pt.X - Width + adjust.X, pt.Y - adjust.Y);

                    case PopupPosition.BottomRight:
                        return new Point(pt.X - adjust.X, pt.Y - adjust.Y);
                    default:
                        return new Point();
                }
            }
        }

        /// <summary>
        /// ポップアップする位置を取得または設定
        /// </summary>
        public PopupPosition Position
        {
            set
            {
                if (position != value)
                    position = value;
            }
            get
            {
                return position;
            }
        }

        /// <summary>
        /// ポップアップの最大サイズを取得または設定
        /// </summary>
        public Size Maximum
        {
            set
            {
                maximum = value;
            }
            get
            {
                return maximum;
            }
        }

        /// <summary>
        /// 常に表示するかどうかを示す値を取得
        /// </summary>
        public bool ShowAlways
        {
            set
            {
                throw new NotSupportedException("ShowAlwaysプロパティはサポートしていません");
            }
            get
            {
                return false;
            }
        }

        /// <summary>
        /// ポップアップする文字のフォントを取得または設定
        /// </summary>
        public Font Font
        {
            set
            {
                font = value;
                fontHtml = String.Format("font-family:{0}; font-size:{1}pt;",
                        value.Name, value.Size);
            }
            get
            {
                return font;
            }
        }

        /// <summary>
        /// ポップアップする文字色を取得または設定
        /// </summary>
        public Color ForeColor
        {
            set
            {
                foreColorHtml = ColorTranslator.ToHtml(value);
            }
            get
            {
                return ColorTranslator.FromHtml(foreColorHtml);
            }
        }

        /// <summary>
        /// ポップアップの背景色を取得または設定
        /// </summary>
        public Color BackColor
        {
            set
            {
                backColorHtml = ColorTranslator.ToHtml(value);
            }
            get
            {
                return ColorTranslator.FromHtml(backColorHtml);
            }
        }

        /// <summary>
        /// ポップアップの幅を取得
        /// </summary>
        public int Width
        {
            get
            {
                return width;
            }
        }

        /// <summary>
        /// ポップアップの高さを取得
        /// </summary>
        public int Height
        {
            get
            {
                return height;
            }
        }

        /// <summary>
        /// 画像ポップアップの画像サイズを取得または設定
        /// </summary>
        public Size ImageSize
        {
            set
            {
                imageSize = new Size(
                    Math.Max(0, value.Width),
                    Math.Max(0, value.Height));
            }
            get
            {
                return imageSize;
            }
        }
#endregion
        /// <summary>
        /// HtmlPopupクラスのインスタンスを初期化
        /// </summary>
        /// <param name="wb">オーナーのIEComponentThreadViewer</param>
        public HtmlPopup(IEComponentThreadViewer wb)
        {
            // 
            // TODO: コンストラクタ ロジックをここに追加してください。
            //
            this.iectv = wb;
            this.Font = new Font("ＭＳ Ｐゴシック", 9);

            position = PopupPosition.TopRight;
            maximum = new Size(500, 350);
            imageSize = new Size(200, 0);
            foreColorHtml = "#000000";
            backColorHtml = "#ffffff";
            width = height = 0;
        }

        /// <summary>
        /// ポップアップを表示します
        /// </summary>
        /// <param name="html">表示するHTML</param>
        public void Show(string html)
        {
            HTMLDocument hd = iectv.DomDocument;
            HTMLBody body = (HTMLBody)hd.body;
            IHTMLElement root = hd.getElementById("popupBase");

            // ポップアップのルートが作成されていなければ
            if (root == null)
            {
                // ポップアップの基本となる要素を挿入
                body.insertAdjacentHTML("afterBegin", "<dl><div id=\"popupBase\" style=\"" + fontHtml + "\"></div></dl>");
                root = hd.getElementById("popupBase");
            }

            // ポップアップ元を作成
            root.insertAdjacentHTML("beforeEnd",
                String.Format("<div id=\"p{0}\" style=\"border: solid gray 1px; padding: 3px; background: window; overflow: auto; position: absolute;\"></div>", childCount));

            // 表示内容を設定
            IHTMLElement div = hd.getElementById("p" + childCount);
            div.innerHTML = html;


            // ポップアップの最大幅を求める
            int maxWidth = body.clientWidth;
            int maxHeight = body.clientHeight / 2 + body.clientHeight / 3; // 画面の3/2

            // 要素が最大縦幅より大きくならないように調整
            width = Math.Min(div.offsetWidth + 25, maxWidth);
            height = Math.Min(div.offsetHeight, maxHeight);

            // ポップアップ位置を調整 (レスの右上に配置)
            Point point = PopupLocation;
            // NTwin23
            //if (img)
            //{
            //    point = new Point(point.X + adjustImg.X, point.Y + adjustImg.Y);
            //}
            // NTwin23

            // pointをクライアント座標に変換
            Point clientPos = new Point(
                point.X - body.scrollLeft,
                point.Y - body.scrollTop);

            // 上と左のはみ出しチェック
            if (clientPos.X < 0)
                point.X = body.scrollLeft;
            if (clientPos.Y < 0)
                point.Y = body.scrollTop;

            // 幅がクライアント幅を超えていたら調整
            if (clientPos.X + width > body.clientWidth)
            {
                int sub_w = (body.clientWidth - width);
                point.X = (body.scrollLeft + Math.Max(0, sub_w));
            }

            // 高さがクライアント幅を超えていたら調整
            if (clientPos.Y + height > body.clientHeight)
            {
                int sub_h = (body.clientHeight - height);
                point.Y = (body.scrollTop + Math.Max(0, sub_h));
            }

            // サイズを設定
            div.style.pixelLeft = point.X;
            div.style.pixelTop = point.Y;
            div.style.pixelWidth = width;
            div.style.pixelHeight = height;

            // 色を設定
            div.style.backgroundColor = backColorHtml;
            div.style.color = foreColorHtml;
            childCount++;


        }
        /// <summary>
        /// ポップアップを隠します
        /// </summary>
        public void Hide()
        {
            if (childCount > 0)
            {
                HTMLDocument doc = iectv.DomDocument;
                IHTMLWindow2 window = (IHTMLWindow2)doc.parentWindow;
                IHTMLElement element = (window.@event != null) ? window.@event.srcElement : doc.body;
                bool onPopup = false;
                string data = iectv.DocumentText;
                while (element.tagName != "BODY")
                {
                    if (element.id != null &&
                        Regex.IsMatch(element.id, "p\\d+"))
                    {
                        onPopup = true;
                        break;
                    }
                    element = element.parentElement;
                }

                if (onPopup)
                {
                    for (; ; )
                    {
                        IHTMLDOMNode node = (IHTMLDOMNode)element.parentElement;
                        IHTMLElement temp = (IHTMLElement)node.lastChild;

                        if (element.id != temp.id)
                        {
                            ((IHTMLDOMNode)temp).removeNode(true);
                            iectv.lastPopupRef = String.Empty;
                            iectv.lastPopupRefImg = String.Empty;
                            childCount--;
                        }
                        else
                            break;
                    }
                }
                else if (inPopup)
                {
                    // すべてのポップアップを削除
                    IHTMLDOMNode node = (IHTMLDOMNode)doc.getElementById("popupBase");
                    if (node != null)
                        node.removeNode(true);

                    iectv.lastPopupRef = String.Empty;
                    iectv.lastPopupRefImg = String.Empty;	// NTwin23
                    iectv.clickedPopup = false;
                    childCount = 0;
                }
                else
                {
                    for (; ; )
                    {
                        IHTMLDOMNode node = (IHTMLDOMNode)element.parentElement;
                        IHTMLElement temp = (IHTMLElement)node.lastChild;

                        if (element.id != temp.id)
                        {
                            ((IHTMLDOMNode)temp).removeNode(true);
                            iectv.lastPopupRef = String.Empty;
                            iectv.lastPopupRefImg = String.Empty;
                            childCount--;
                        }
                        else
                            break;
                    }
                }
                inPopup = onPopup;
            }
        }

        /// <summary>
        /// ポップアップする位置を表す列挙体
        /// </summary>
        public enum PopupPosition
        {
            /// <summary>左上に表示</summary>
            TopLeft,
            /// <summary>右上に表示</summary>
            TopRight,
            /// <summary>左下に表示</summary>
            BottomLeft,
            /// <summary>右下に表示</summary>
            BottomRight
        }
    }
}
