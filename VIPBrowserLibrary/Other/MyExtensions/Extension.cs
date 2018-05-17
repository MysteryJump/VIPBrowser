using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Text.RegularExpressions;

namespace VIPBrowserLibrary.Other.MyExtensions
{
    /// <summary>
    /// 拡張メソッドです
    /// </summary>
    public static class Extension
    {
        /// <summary>
        /// 数値の文字列形式を、それと等価な32ビット符号付整数に変換します
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int Parse(this string str)
        {
            return int.Parse(str);
        }
        /// <summary>
        /// 指定した文字列形式の日付と時刻を等価の<see cref="T:System.DateTime"/>の値に変換します
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static DateTime DParse(this string str)
        {
            return System.DateTime.Parse(str);
        }
        /// <summary>
        /// 指定したstringを色に変換します
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Color ParseColor(this string str)
        {
            KnownColor kc = (KnownColor)Enum.Parse(typeof(KnownColor), str);
            return Color.FromKnownColor(kc);
        }
        /// <summary>
        /// 積極的な<see cref="T:System.Int32"/>型への変換を行います
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int DynamicParse(this string str)
        {
            int i = 0;
            if (Int32.TryParse(str, out i))
            {
                return i;
            }
            else
            {
                char[] strs = str.ToCharArray();
                string part = String.Empty;
                foreach (char item in strs)
                {
                    Match m = Regex.Match(item.ToString(), @"(?<m>\d)");
                    if (m.Success)
                        part += m.Groups["m"].Value;
                }
                return int.Parse(part);
            }
        }
        /// <summary>
        /// 指定されたオブジェクトがNullかどうか確認します
        /// </summary>
        /// <param name="target">確認するSystem.Object型およびに派生したクラス</param>
        /// <returns>Nullかどうか</returns>
        public static bool IsNullObject(this object target)
        {
            if (target == null)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 文字列配列のすべての要素を連結します。各要素の間には、指定した区切り文字が挿入されます。
        /// </summary>
        /// <param name="strs"></param>
        /// <param name="separator">区切り文字として使用する文字列</param>
        /// <returns></returns>
        public static string Join(this string[] strs,string separator)
        {
            string[] s = new string[5];
            return String.Join(separator, strs);
        }
        /// <summary>
        /// 指定した区切り文字を使用して文字列配列に分割します
        /// </summary>
        /// <param name="str"></param>
        /// <param name="separator">区切り文字として使用する文字列</param>
        /// <returns></returns>
        public static string[] SplitString(this string str,string separator) 
        {
            return str.Split(new string[]{separator}, StringSplitOptions.RemoveEmptyEntries);
        }

    }


}
