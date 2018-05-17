using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace VIPBrowserLibrary.Chron.ThreadOrResData
{
    /// <summary>
    /// IDのカラーリングデータを表します。このクラスは継承できません。
    /// </summary>
    [Serializable]
    public sealed class ColoringData
    {
        /// <summary>
        /// 設定するID色
        /// </summary>
        public Color IDColor;
        /// <summary>
        /// この色が適用される最大値
        /// </summary>
        public int Max;
        /// <summary>
        /// この色が適用される最小値
        /// </summary>
        public int Min;
        /// <summary>
        /// 適用される間の数
        /// </summary>
        public int RangeValue { get { return this.Max - this.Min; } }
    }
}
