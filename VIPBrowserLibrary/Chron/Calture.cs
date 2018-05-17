using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIPBrowserLibrary.Chron
{

    /// <summary>
    /// 計算を請け負うクラスです
    /// </summary>
    static public class Calture
    {
        /// <summary>
        /// 時間計算用の読み取り専用変数
        /// </summary>
        private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 9, 0, 0, DateTimeKind.Utc);

        /// <summary>
        ///Unix時間をUTC+9形式に変換します
        /// </summary>
        /// <param name="text">変換されるUNIX時間</param>
        /// <returns>変換せれたUTC+9形式の時間</returns>
        public static DateTime UnixTimeToDateTime(string text)
        {

            double seconds = double.Parse(text, System.Globalization.CultureInfo.InvariantCulture);
            return Epoch.AddSeconds(seconds);
        }

        /// <summary>
        /// スレッドの勢いを計算します
        /// </summary>
        /// <param name="standDateTime">スレッドが立った時間</param>
        /// <param name="threadRes">その時のスレッドのレス数</param>
        /// <returns>勢いをulong形式で返す（数値はほぼint形式で対応可）</returns>
        public static ulong ThreadAuthority(DateTime standDateTime, string threadRes)
        {
            DateTime StandThreadDateTime = standDateTime;
            //TimeSpan ForAuthorityDateTime = DateTime.Now - StandThreadDateTime;
            int ForAuthorityMinutes = (int)((TimeSpan)(DateTime.Now - StandThreadDateTime)).TotalMinutes;
            //ForAuthorityMinutes = int.Parse(ForAuthorityDateTime.TotalMinutes.ToString());
            int threadResInt = int.Parse(threadRes);



            double threadResInts = threadResInt;
            double Authority = threadResInts * 24 / ForAuthorityMinutes * 60;

            ulong Authoritys = (ulong)Authority;
            //double Authoritys = Math.Round(Authority,MidpointRounding.AwayFromZero);

            return Authoritys;

        }
        /// <summary>
        /// 投稿時のtime値を取得
        /// </summary>
        /// <param name="baseTime">基になる日時</param>
        /// <returns>int形式のUnixTime</returns>
        public static int GetTime(DateTime baseTime)
        {
            TimeSpan t = baseTime.ToUniversalTime().Subtract(new DateTime(1970, 1, 1));
            return ((int)(t.TotalSeconds)) - 5;
        }
        /// <summary>
        /// バイトをKBに変換します
        /// </summary>
        /// <param name="amt">変換するバイト数</param>
        /// <param name="rounding">表示する小数点</param>
        /// <returns>変換したstring形式の量</returns>
        public static string DatSizeFormat(long amt, int rounding)
        {
            return Math.Round(amt
                / Math.Pow(2, 10), rounding).ToString(); //kilobyte
        }

        /// <summary>
        /// バイトをMB等の形式に変換するメソッドです
        /// </summary>
        /// <param name="amt">変換するバイト数</param>
        /// <param name="rounding">表示する小数点</param>
        /// <returns>変換したstring形式の量</returns>
        public static string FormatSize(long amt, int rounding)
        {
            if (amt >= Math.Pow(2, 80)) return Math.Round(amt
                / Math.Pow(2, 70), rounding).ToString() + "YB"; //yettabyte
            if (amt >= Math.Pow(2, 70)) return Math.Round(amt
                / Math.Pow(2, 70), rounding).ToString() + "ZB"; //zettabyte
            if (amt >= Math.Pow(2, 60)) return Math.Round(amt
                / Math.Pow(2, 60), rounding).ToString() + "EB"; //exabyte
            if (amt >= Math.Pow(2, 50)) return Math.Round(amt
                / Math.Pow(2, 50), rounding).ToString() + "PB"; //petabyte
            if (amt >= Math.Pow(2, 40)) return Math.Round(amt
                / Math.Pow(2, 40), rounding).ToString() + "TB"; //terabyte
            if (amt >= Math.Pow(2, 30)) return Math.Round(amt
                / Math.Pow(2, 30), rounding).ToString() + "GB"; //gigabyte
            if (amt >= Math.Pow(2, 20)) return Math.Round(amt
                / Math.Pow(2, 20), rounding).ToString() + "MB"; //megabyte
            if (amt >= Math.Pow(2, 10)) return Math.Round(amt
                / Math.Pow(2, 10), rounding).ToString() + "KB"; //kilobyte

            return amt.ToString() + " Bytes"; //byte
        }
    }

}
