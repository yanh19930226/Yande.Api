using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;

namespace OfficeToPdf
{
    public class Convertor
    {


        /// 序列化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static byte[] Serialize(object obj)
        {
            if (obj == null)
                return null;

            using var memoryStream = new MemoryStream();
            DataContractSerializer ser = new DataContractSerializer(typeof(object));
            ser.WriteObject(memoryStream, obj);
            var data = memoryStream.ToArray();
            return data;
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static T Deserialize<T>(byte[] data)
        {
            if (data == null)
                return default(T);

            using var memoryStream = new MemoryStream(data);
            XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(memoryStream, new XmlDictionaryReaderQuotas());
            DataContractSerializer ser = new DataContractSerializer(typeof(T));
            var result = (T)ser.ReadObject(reader, true);
            return result;
        }


        /// <summary> 
        /// 将一个object对象序列化，返回一个byte[]         
        /// </summary> 
        /// <param name="obj">能序列化的对象</param>         
        /// <returns></returns> 
        public static byte[] ObjectToBytes(object obj)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                return ms.GetBuffer();
            }
        }

        /// <summary> 
        /// 将一个序列化后的byte[]数组还原         
        /// </summary>
        /// <param name="Bytes"></param>         
        /// <returns></returns> 
        public static object BytesToObject(byte[] Bytes)
        {
            using (MemoryStream ms = new MemoryStream(Bytes))
            {
                IFormatter formatter = new BinaryFormatter();
                return formatter.Deserialize(ms);
            }
        }


        /// <summary>
        /// 二进制反序列化：byte=>实体object
        /// </summary>
        /// <param name="SerializedObj"></param>
        /// <param name="ThrowException"></param>
        /// <returns></returns>
        public static object ByteArrayToObject(byte[] SerializedObj, bool ThrowException)
        {
            if (SerializedObj == null)
            {
                return null;
            }
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                MemoryStream serializationStream = new MemoryStream(SerializedObj);
                return formatter.Deserialize(serializationStream);
            }
            catch (Exception exception)
            {
                if (ThrowException)
                {
                    throw exception;
                }
                return null;
            }
        }

        /// <summary>
        /// 二进制序列化：实体=》byte
        /// </summary>
        /// <param name="Obj"></param>
        /// <param name="ThrowException"></param>
        /// <returns></returns>
        public static byte[] ObjectToByteArray(object Obj, bool ThrowException)
        {
            if (Obj == null)
            {
                return null;
            }
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                MemoryStream serializationStream = new MemoryStream();
                formatter.Serialize(serializationStream, Obj);
                return serializationStream.ToArray();
            }
            catch (Exception exception)
            {
                if (ThrowException)
                {
                    throw exception;
                }
                return null;
            }
        }
    }
}
