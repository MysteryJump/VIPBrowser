namespace VIPBrowserLibrary.Other.MyExtensions
{
    /// <summary>
    /// アセンブリに関連付けられたコードネームを表します
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Assembly, Inherited = false, AllowMultiple = true),System.Runtime.InteropServices.ComVisible(true)]
    public sealed class AssemblyCodeName : System.Attribute
    {
        // See the attribute guidelines at 
        //  http://go.microsoft.com/fwlink/?LinkId=85236
        readonly string codeName;

        // This is a positional argument
        /// <summary>
        /// このクラスの新しいインスタンスを初期化します
        /// </summary>
        /// <param name="codeName">コードネーム</param>
        public AssemblyCodeName(string codeName)
        {
            this.codeName = codeName;

        }
        /// <summary>
        /// コードネーム
        /// </summary>
        public string CodeName
        {
            get { return codeName; }
        }
    }
}