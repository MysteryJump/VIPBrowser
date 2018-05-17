using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIPBrowserLibrary.Setting.ConvertOtherBrowserData
{
    /// <summary>
    /// 指定されたフォルダーがいずれかのブラウザに準拠していてかつ変換可能か確認します
    /// </summary>
    public class CheckCanConvert
    {
        /// <summary>
        /// 予測される変換元を指定してこのクラスの新しいインスタンスを初期化します
        /// </summary>
        /// <param name="ct">予測されるタイプ</param>
        public CheckCanConvert(ConvertFrom ct)
        {
            this.instConvertType = ct;
        }
        /// <summary>
        /// 変換可能か確認します。
        /// </summary>
        /// <returns>変換可能な場合はtrue,不可能な場合はfalse</returns>
        public bool TryConvertTest(string path)
        {
            ConvertFrom basecon = instConvertType;
            bool isClear = true;
            IOtherDataConverter iodc;
            if (instConvertType == ConvertFrom.JaneStyle)
            {
                iodc = new JaneConverter(path);
                try { iodc.ConvertCookie(); }
                catch { isClear = false; }
            }
            else if (instConvertType == ConvertFrom.V2C)
            {
                iodc = new V2CConverter(path);
                try { iodc.ConvertCookie(); }
                catch { isClear = false; }
            }
            else
            {
                isClear = false;
            }
            if (isClear)
                return true;
            else
            {
                //bool isSecondClear = true;
                if (basecon == 0)
                {

                }
                else if (instConvertType == ConvertFrom.V2C)
                {
                    iodc = new JaneConverter(path);
                    try { iodc.ConvertCookie(); }
                    catch { isClear = false; }
                }
                else if (instConvertType == ConvertFrom.JaneStyle)
                {
                    iodc = new V2CConverter(path);
                    try { iodc.ConvertCookie(); }
                    catch { isClear = false; }
                }
                
            }
            throw new NotImplementedException();
        }
        private ConvertFrom canConvertType { get; set; }
        private ConvertFrom instConvertType;
        /// <summary>
        /// コンバート可能な場合コンバート可能な元を表します
        /// </summary>
        public ConvertFrom CanConvertType 
        {
            get
            {
                if (canConvertType == 0)
                    throw new InvalidOperationException();
                return canConvertType;
            }
        }
    }
}
