using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIPBrowserLibrary.BBS.X2ch
{
    /// <summary>
    /// Rokkaへの機能を提供します
    /// </summary>
    public class RokkaLogin
    {
        /// <summary>
        /// Rokkaにログインします
        /// </summary>
        /// <param name="id">ログインID</param>
        /// <param name="password">パスワード</param>
        /// <returns>成功したか</returns>
        public Task<bool> Login(string id,string password)
        {
            string url = String.Format("https://2chv.tora3.net/futen.cgi?ID={0}&PW={1}", id, password);
            throw new NotSupportedException();
        }

    }
}
