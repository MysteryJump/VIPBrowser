
namespace VIPBrowserLibrary.Utility.Exception
{
    /// <summary>
    /// 同時に複数の非同期操作を呼び出そうとしたときに発生するエラーを表します。
    /// </summary>
    [System.Serializable]
    public class AsyncProcessMultipleAccessException : System.Exception
    {
        /// <summary>
        /// 基本メッセージ
        /// </summary>
        public static string Messages = "同時に二つの非同期処理は呼びこむことはできません";
        /// <summary>
        /// AsyncProcessMultipleAccessExceptionクラスの新しいインスタンスを初期化します
        /// </summary>
        public AsyncProcessMultipleAccessException():base(Messages) { }
        /// <summary>
        /// AsyncProcessMultipleAccessExceptionクラスの新しいインスタンスを指定したメッセージを使用して初期化します
        /// </summary>
        /// <param name="message">使用するメッセージ</param>
        public AsyncProcessMultipleAccessException(string message) : base(message) { }
        /// <summary>
        /// AsyncProcessMultipleAccessExceptionクラスの新しいインスタンスを指定したメッセージと元の例外を設定して初期化します
        /// </summary>
        /// <param name="message">使用するメッセージ</param>
        /// <param name="inner">例外の元である内部例外</param>
        public AsyncProcessMultipleAccessException(string message, System.Exception inner) : base(message, inner) { }
        /// <summary>
        /// シリアル化したデータを使用してAsyncProcessMultipleAccessExceptionクラスの新しいインスタンスを初期化します
        /// </summary>
        /// <param name="info">シリアル化に必要なデータ</param>
        /// <param name="context">シリアル化のStream</param>
        protected AsyncProcessMultipleAccessException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}

