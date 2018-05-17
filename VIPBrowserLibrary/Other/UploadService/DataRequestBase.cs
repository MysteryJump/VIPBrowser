using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Net;
using System.Diagnostics;

namespace VIPBrowserLibrary.Other.UploadService
{
    /// <summary>
    /// データー関連のネットへのリクエストを行います
    /// </summary>
    public abstract class DataRequestBase// : HttpWebRequest
    {
        /// <summary>
        /// binaryデータを送信します
        /// </summary>
        /// <param name="data">送信するデータ</param>
        /// <returns>送信後の変装データ</returns>
        public virtual string UploadBinary(string data)
        {
            return String.Empty;
        }
    }
}
