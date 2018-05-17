using System;

namespace VIPBrowserLibrary.Setting.ConvertOtherBrowserData
{
    /// <summary>
    /// 変換する種類を選択する列挙値を提供します。この列挙体はビットフィールドです。
    /// </summary>
    [Flags]
    public enum ConvertType
    {
        /// <summary>
        /// Cookie
        /// </summary>
        Cookie = 1,
        /// <summary>
        /// 過去ログ
        /// </summary>
        Log = 2,
        /// <summary>
        /// NGリスト
        /// </summary>
        NGList = 4,
        /// <summary>
        /// お気に入り
        /// </summary>
        Favorite = 8,
        /// <summary>
        /// 書き込みログ
        /// </summary>
        Kakikomi = 16,
        /// <summary>
        /// AAリスト
        /// </summary>
        AAList = 32
    }
    /// <summary>
    /// 変換元のブラウザを選択します
    /// </summary>
    public enum ConvertFrom
    {
        /// <summary>
        /// JaneStyle
        /// </summary>
        JaneStyle = 1,
        /// <summary>
        /// V2C
        /// </summary>
        V2C = 2
    }
}
