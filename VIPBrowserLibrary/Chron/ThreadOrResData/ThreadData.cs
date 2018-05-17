using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

namespace VIPBrowserLibrary.Chron.ThreadOrResData
{
    /// <summary>
    /// スレッドデータを設定します。このクラスは継承できません。
    /// </summary>
    [Serializable]
    public sealed class ThreadData
    {
        /// <summary>
        /// スレッド名
        /// </summary>
        public string ThreadName;
        /// <summary>
        /// スレッドに関連付けられたアドレス
        /// </summary>
        public string ThreadAddress;
        /// <summary>
        /// スレッドのキー
        /// </summary>
        public string ThreadKey;
        /// <summary>
        /// スレッドのDatサイズ
        /// </summary>
        public string DatSize;
        /// <summary>
        /// 既に取得済みのレス数
        /// </summary>
        public int GetRescount;
        /// <summary>
        /// スクロール可能な高さ
        /// </summary>
        public int CanScrollHeight;
        /// <summary>
        /// 現在のスクロール位置
        /// </summary>
        public int NowScrollHeight;
        /// <summary>
        /// Datファイルが保存されているパス
        /// </summary>
        public string ThisFilePath;
        /// <summary>
        /// DatファイルのByte数
        /// </summary>
        public int DatByte;
        /// <summary>
        /// Datファイルに関連付けられているHTTP1.1に準拠したE-Tag
        /// </summary>
        public string ETag;
        /// <summary>
        /// 最終取得時間
        /// </summary>
        public string LastModified;
        /// <summary>
        /// このBBSのタイプ
        /// </summary>
        public Common.BBSType ThisBBS;
    }
    /// <summary>
    /// ThreadDataの読み書きを行います。このクラスは継承できません。
    /// </summary>
    public static class ThreadDataWriterAndReader
    {
        /// <summary>
        /// スレッドデータを書き込みます
        /// </summary>
        /// <param name="td">書き込むスレッドデータ</param>
        /// <param name="path">書き込み先のパス</param>
        public static void Write(ThreadData td,string path)
        {
            try
            {
                XmlSerializer xs = new XmlSerializer(typeof(ThreadData));

                using (FileStream fs = File.Open(path, FileMode.Create, FileAccess.ReadWrite))
                {
                    xs.Serialize(fs, td);
                }
            }
            catch 
            {
                throw;
            }
        }
        /// <summary>
        /// スレッドデータを読み込みます
        /// </summary>
        /// <param name="path">読み込み先のパス</param>
        /// <returns>読み込んだスレッドデータ</returns>
        public static ThreadData Read(string path)
        {
            try
            {
                XmlSerializer xs = new XmlSerializer(typeof(ThreadData));
                if (!File.Exists(path))
                    return null;
                using (FileStream fs = File.Open(path,FileMode.Open,FileAccess.ReadWrite))
                {
                    return (ThreadData)xs.Deserialize(fs);
                }
                
            }
            catch (Exception)
            {

                return null;
            }
        }
    }
}
