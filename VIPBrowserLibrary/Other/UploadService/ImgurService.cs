using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Net;
using System.Xml;
using System.Collections.Specialized;

namespace VIPBrowserLibrary.Other.UploadService
{
    /// <summary>
    /// imgur.comへのアップロードサービスを提供します
    /// </summary>
    public sealed class ImgurService : IDisposable
    {
        private string DataString;
        /// <summary>
        /// アップロードした画像のアドレスを取得します
        /// </summary>
        /// <exception cref="T:System.InvalidOperationException">データをアップロードしていません</exception>
        public string DownloadAddress 
        {
            get 
            {
                if (this.downloadAddress == null)
                    throw new InvalidOperationException();
                return this.downloadAddress;
            }
        }
        /// <summary>
        /// アップロードした画像の削除アドレスを取得します
        /// </summary>
        /// <exception cref="T:System.InvalidOperationException">データをアップロードしていません</exception>
        public string DeleteAddress
        {
            get 
            {
                if (this.deleteAddress == null)
                    throw new InvalidOperationException();
                return this.deleteAddress;
            }
        }
        private string downloadAddress;
        private string deleteAddress;
        /// <summary>
        /// アップロードするイメージを指定してこのクラスの新しいインスタンスを初期化します
        /// </summary>
        /// <param name="im">アップロードを行うSystem.Drawing.Image</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="im"/>がnullです</exception>
        public ImgurService(Image im)
        {
            if (im == null)
                throw new ArgumentNullException();
            var format = im.RawFormat;
            MemoryStream ms = new MemoryStream();
            im.Save(ms, format);
            this.DataString = Convert.ToBase64String(ms.ToArray());
            ms.Dispose();
        }
        /// <summary>
        /// アップロードするバイト配列を指定してこのクラスの新しいインスタンスを初期化します
        /// </summary>
        /// <param name="by">バイト配列化されたイメージ</param>
        public ImgurService(byte[] by)
        {
            this.DataString = Convert.ToBase64String(by);
        }
        /// <summary>
        /// アップロードを行うパスを指定してこのクラスの新しいインスタンスを初期化します
        /// </summary>
        /// <param name="path">アップロードを行うイメージのパス</param>
        public ImgurService(string path)
        {
            this.DataString = Convert.ToBase64String(File.ReadAllBytes(path));
        }
        /// <summary>
        /// インスタンスに関連付けられた画像のアップロードを行います
        /// </summary>
        /// <returns>画像に関連付けられたUrl</returns>
        public Dictionary<string,string> UploadImage() 
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            WebClient wec = new WebClient();
            var values = new NameValueCollection() 
            {
                {"image",this.DataString}
            };
            wec.Headers.Add("Authorization", "Client-ID " + "e8c834b4a234156");
            var res = wec.UploadValues("https://api.imgur.com/3/upload.xml", values);
            var sr = new StreamReader(new MemoryStream(res));
            string data = sr.ReadToEnd();
            List<string> datas = new List<string>();
            // 
            XmlReader xr = XmlReader.Create(new MemoryStream(res));
            while (xr.Read())
            {
                switch (xr.NodeType)
                {
                    case XmlNodeType.Attribute:
                        break;
                    case XmlNodeType.CDATA:
                        break;
                    case XmlNodeType.Comment:
                        break;
                    case XmlNodeType.Document:
                        break;
                    case XmlNodeType.DocumentFragment:
                        break;
                    case XmlNodeType.DocumentType:
                        break;
                    case XmlNodeType.Element:
                        //string datas = xr.ReadElementContentAsString();
                        break;
                    case XmlNodeType.EndElement:
                        break;
                    case XmlNodeType.EndEntity:
                        break;
                    case XmlNodeType.Entity:
                        break;
                    case XmlNodeType.EntityReference:
                        break;
                    case XmlNodeType.None:
                        break;
                    case XmlNodeType.Notation:
                        break;
                    case XmlNodeType.ProcessingInstruction:
                        break;
                    case XmlNodeType.SignificantWhitespace:
                        break;
                    case XmlNodeType.Text:
                        datas.Add(xr.ReadContentAsString());
                        break;
                    case XmlNodeType.Whitespace:
                        break;
                    case XmlNodeType.XmlDeclaration:
                        break;
                    default:
                        break;
                }
            }
            dic.Add("Delete", "http://imgur.com/delete/" + datas[10] + "/");
            dic.Add("Link", datas[11]);
            //while (sr.Peek() >= 0)
            //{
            //    var line = sr.ReadLine();
            //    if (line != null && line.Contains("link"))
            //    {
            //        //get substring starting at http
            //        line = line.Substring(line.IndexOf(":", StringComparison.Ordinal) - 4, line.Length - line.IndexOf(":", StringComparison.Ordinal));

            //        //split string starting at </link
            //        line = line.Substring(0, line.IndexOf("<", StringComparison.Ordinal));
            //        this.downloadAddress = line;
            //        dic.Add("Link", line);
            //    }
            //    if (line != null && line.Contains("deletehash"))
            //    {
            //        //http://imgur.com/delete/gIoHAaI4wRXrmjY/
            //        //get substring starting at http
            //        line = System.Text.RegularExpressions.Regex.Replace(line, "<deletehash>(.+)</deletehash>", 
            //            (m) => 
            //            {
            //                return "http://imgur.com/delete/" + m.Groups[0].Value + "/";
            //            });
            //        dic.Add("Delete", line);
            //        this.deleteAddress = line;
            //    }

            //}
            wec.Dispose();
            //dic = null;
            return dic;
        }
        /// <summary>
        /// 指定した画像を削除します
        /// </summary>
        /// <param name="url">削除する画像のUrl</param>
        /// <returns>削除後のテキスト</returns>
        public static string DeleteImage(string url) 
        {
            string hash = url.Replace("http://imgur.com/delete/", "").TrimEnd('/');
            WebClient wec = new WebClient();
            wec.Headers.Add("Authorization", "Client-ID " + "e8c834b4a234156");
            return wec.UploadString("https://api.imgur.com/3/image/" + hash, "DELETE", "");
        }
        /// <summary>
        /// インスタンスに関連付けられた画像のアップロードを非同期で行います
        /// </summary>
        /// <returns>画像に関連付けられたUrl</returns>
        public async Task<Dictionary<string,string>> UploadImageAsync()
        {
            return await Utility.TaskUtility.AsyncStart<Dictionary<string, string>>(this.UploadImage); 
        }
        /// <summary>
        /// このオブジェクトによって使用されているすべてのリソースを解放します
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        /// <summary>
        /// このオブジェクトによって使用されているすべてのリソースを解放します
        /// </summary>
        /// <param name="isDisposing">マネージドリソースのみを解放する場合はfalse,アンマネージドリソースも開放する場合はtrue</param>
        public void Dispose(bool isDisposing)
        {
            this.deleteAddress = null;
            this.DataString = null;
            this.downloadAddress = null;

        }
    }
}
