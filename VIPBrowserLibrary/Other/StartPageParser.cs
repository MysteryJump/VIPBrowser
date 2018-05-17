using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIPBrowserLibrary.Other
{
    /// <summary>
    /// スタートページに用いるhtmlを指定した形式に変換します
    /// </summary>
    public sealed class StartPageParser
    {
        //private bool isUse2chDeny;
        private string filePath;
        
        private string data;
        Setting.GeneralSetting gs = new Setting.GeneralSetting();
        /// <summary>
        /// 使用するファイルのパスを指定してスタートページを読み込みます
        /// </summary>
        /// <param name="path">ファイルのパス</param>
        public StartPageParser(string path)
        {
            this.filePath = path;
            this.data = Utility.TextUtility.Read(path);
        }
        /// <summary>
        /// このインスタンスにおいて使用する機能を取得します
        /// </summary>
        /// <returns>使用する機能を収めたビットフラグ</returns>
        public StartPageParseKind CheckUseFunction() 
        {
            StartPageParseKind spp = 0;
            if (this.data.IndexOf("<denylist/>", StringComparison.CurrentCultureIgnoreCase) != -1)
                spp = StartPageParseKind.TwochDeny | spp;
            if (this.data.IndexOf("<server/>", StringComparison.CurrentCultureIgnoreCase) != -1)
                spp = StartPageParseKind.TwochServer | spp;
            if (this.data.IndexOf("<release/>", StringComparison.CurrentCultureIgnoreCase) != -1)
                spp = StartPageParseKind.ReleaseNote | spp;
            if (this.data.IndexOf("<boardlist/>", StringComparison.CurrentCultureIgnoreCase) != -1)
                spp = StartPageParseKind.BoardList | spp;
            if (this.data.IndexOf("<favoritethread/>", StringComparison.CurrentCultureIgnoreCase) != -1)
                spp = StartPageParseKind.FavoriteThreads | spp;
            if (this.data.IndexOf("<recentlythread/>", StringComparison.CurrentCultureIgnoreCase) != -1)
                spp = StartPageParseKind.RecentlyThread | spp;
            return spp;
        }
        /// <summary>
        /// スタートページの変換を行います
        /// </summary>
        /// <param name="kindFlags">変換を行う種類のビットフラグ</param>
        /// <returns>変換したデータ</returns>
        public string Parse(StartPageParseKind kindFlags)
        {
            if (kindFlags.HasFlag(StartPageParseKind.BoardList))
                this.BoardListParse();
            if (kindFlags.HasFlag(StartPageParseKind.FavoriteThreads))
                this.FavoriteListParse();
            if (kindFlags.HasFlag(StartPageParseKind.RecentlyThread))
                this.RecentlyListParse();
            if (kindFlags.HasFlag(StartPageParseKind.ReleaseNote))
                this.ReleaseNoteParse();
            if (kindFlags.HasFlag(StartPageParseKind.TwochDeny))
                this.TwochDenyParse();
            if (kindFlags.HasFlag(StartPageParseKind.TwochServer))
                this.TwochServerParse();
            return this.data;
        }

        private void BoardListParse()
        {
            throw new NotImplementedException();
        }

        private void FavoriteListParse()
        {
            throw new NotImplementedException();
        }

        private void RecentlyListParse()
        {
            throw new NotImplementedException();
        }

        private void ReleaseNoteParse()
        {
            throw new NotImplementedException();
        }

        private void TwochDenyParse()
        {   
            string[] strs = new string[50];
            string denyData = "";
            var denyFlag = SimpleDenyChecker.Check(ref strs);
            if (denyFlag.HasFlag(DenyType.AllBoard))
                denyData += "全板規制されています<br>";
            if (denyFlag.HasFlag(DenyType.Pink))
                denyData += "PINKちゃんねるにおいて規制されています<br>";
            if (denyFlag.HasFlag(DenyType.Twoch))
                denyData += "2chにおいて全板規制されています<br>";
            if (denyFlag.HasFlag(DenyType.Hana))
                denyData += "花園規制されています<br>";
            if (denyFlag.HasFlag(DenyType.SpecificBoard))
            {
                StringBuilder sb = new StringBuilder("下記の板において規制されています");
                sb.Append("<ul>");
                foreach (var item in strs)
                {
                    sb.Append("<li>").Append(item).Append("</li>");
                }
                sb.Append("</ul>");
                denyData += sb.ToString();
                sb.Clear();
            }
            if (denyData.Length == 0)
            {
                denyData += "規制されていません<br>";
            }
            this.data = this.data.Replace("<denylist/>", denyData);
            
        }

        private void TwochServerParse()
        {
            throw new NotImplementedException();
        }
    }
    /// <summary>
    /// スタートページで使用する可変変数の定義
    /// </summary>
    [Flags]
    public enum StartPageParseKind
    {
        /// <summary>
        /// 2ch規制状況確認
        /// </summary>
        TwochDeny = 1,
        /// <summary>
        /// 2chサーバー負荷状況確認
        /// </summary>
        TwochServer = 2,
        /// <summary>
        /// 更新履歴
        /// </summary>
        ReleaseNote = 4,
        /// <summary>
        /// 板一覧
        /// </summary>
        BoardList = 8,
        /// <summary>
        /// お気に入りのスレ
        /// </summary>
        FavoriteThreads = 16,
        /// <summary>
        /// 最近見たスレッド
        /// </summary>
        RecentlyThread = 32
        
    }
}
