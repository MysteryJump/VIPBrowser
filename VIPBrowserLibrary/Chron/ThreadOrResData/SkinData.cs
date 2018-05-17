using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIPBrowserLibrary.Chron.ThreadOrResData
{
    /// <summary>
    /// スキンのデータを表します。このクラスは継承できません。
    /// </summary>
    public sealed class SkinData
    {
        /// <summary>
        /// スキン名
        /// </summary>
        public string SkinName;
        /// <summary>
        /// スキンパス
        /// </summary>
        public string SkinPath;
        /// <summary>
        /// スキンのプレビューイメージの所持状況
        /// </summary>
        public bool IsHaveImagePicture;
        /// <summary>
        /// プレビュー画像のパス
        /// </summary>
        public string ImagePath;

    }
}
