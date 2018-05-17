using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;

namespace VIPBrowserLibrary.Chron
{
    /// <summary>
    /// ZIPファイルの管理を行うクラスです
    /// </summary>
    public class ZIPData
    {
        /// <summary>
        /// zipファイルを解凍します
        /// </summary>
        /// <param name="extractPath">解凍するzipファイルのパス</param>
        /// <param name="path">解凍先のパス</param>
        public void ExtractZipFile(string extractPath,string path) 
        {
            ZipFile.ExtractToDirectory(extractPath, path);
        }
        /// <summary>
        /// zipファイルを作成します
        /// </summary>
        /// <param name="createPath">作成先のパス</param>
        /// <param name="zipPath">作成するzipファイルのパス</param>
        public void CreateZipFile(string createPath, string zipPath) 
        {
            ZipFile.CreateFromDirectory(zipPath, createPath);
        }
        /// <summary>
        /// This is the NotSupportedMethod
        /// </summary>
        /// <param name="openPath"></param>
        public void OpenZipFile(string openPath) 
        {
            ZipArchive za = ZipFile.Open(openPath, ZipArchiveMode.Update);
            throw new NotSupportedException();
        }
    }
}
