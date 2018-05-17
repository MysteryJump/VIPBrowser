using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.IO;

namespace VIPBrowserLibrary.Chron
{
    /// <summary>
    /// ジェネリックシリアライズを行うクラスです。このクラスは継承できません。
    /// </summary>
    public static class Serializer
    {
        /// <summary>
        /// 指定した型のオブジェクトを指定したストリームにシリアライズします
        /// </summary>
        /// <typeparam name="T">シリアライズ対象の型</typeparam>
        /// <param name="obj">シリアライズするオブジェクト</param>
        /// <param name="s">シリアライズ先のStream</param>
        /// <param name="st">シリアライズする際に用いる方法</param>
        public static void Serialize<T>(T obj,Stream s, SerializeType st)
        {
            switch (st)
            {
                case SerializeType.BinarySerialize:
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(s, obj);
                    break;
                case SerializeType.SoapSerialize:
                    SoapFormatter sf = new SoapFormatter();
                    sf.Serialize(s, obj);
                    break;
                case SerializeType.XmlSerialize:
                    XmlSerializer xs = new XmlSerializer(typeof(T));
                    xs.Serialize(s, obj);
                    break;
                default:
                    throw new ArgumentException();
            }
            
        }
        /// <summary>
        /// 指定した型のオブジェクトを指定したストリームから逆シリアライズします
        /// </summary>
        /// <typeparam name="T">逆シリアライズ対象の型</typeparam>
        /// <param name="s">逆シリアライズされるStream</param>
        /// <param name="st">逆シリアライズする際に用いる方法</param>
        /// <returns>逆シリアライズされたジェネリックオブジェクト</returns>
        public static T Deserialize<T>(Stream s,SerializeType st)
        {
            
            switch (st)
            {
                case SerializeType.BinarySerialize:
                    BinaryFormatter bf = new BinaryFormatter();
                    return (T)bf.Deserialize(s);
                case SerializeType.SoapSerialize:
                    SoapFormatter sf = new SoapFormatter();
                    return (T)sf.Deserialize(s);
                case SerializeType.XmlSerialize:
                    XmlSerializer xs = new XmlSerializer(typeof(T));
                    return (T)xs.Deserialize(s);
                default:
                    throw new ArgumentException();
            }
        }
        /// <summary>
        /// バイナリフォーマット形式を用いて逆シリアライズ化します
        /// </summary>
        /// <typeparam name="T">逆シリアライズする型</typeparam>
        /// <param name="s">逆シリアライズされるStream</param>
        /// <returns>逆シリアライズされたジェネリックオブジェクト</returns>
        public static T Deserialize<T>(Stream s)
        {
            return Deserialize<T>(s,SerializeType.BinarySerialize);
        }
        /// <summary>
        /// バイナリフォーマット形式を用いてシリアライズします
        /// </summary>
        /// <typeparam name="T">シリアライズする型</typeparam>
        /// <param name="obj">シリアライズするオブジェクト</param>
        /// <param name="s">シリアライズ先のStream</param>
        public static void Serialize<T>(T obj,Stream s)
        {
            Serialize<T>(obj,s, SerializeType.BinarySerialize);
        }
    }
    /// <summary>
    /// 永続化させる際の方法を表す列挙体です
    /// </summary>
    public enum SerializeType
    {
        /// <summary>
        /// バイナリシリアライズを表します
        /// </summary>
        BinarySerialize,
        /// <summary>
        /// ソープシリアライズを表します
        /// </summary>
        SoapSerialize,
        /// <summary>
        /// Xmlシリアライズを表します
        /// </summary>
        XmlSerialize
    }
}
