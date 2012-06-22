using System;
using System.IO;
using System.Text;

namespace TG.Package.Compression
{
    public class DeflateHelper
    {
        #region Deflate With String
        /// <summary>
        /// Base64String Decompress to utf8`s String 
        /// </summary>
        public static String Decompress(String strSource)
        {
            return Decompress(strSource,Encoding.UTF8);   //转换为普通的字符串
        }
        /// <summary>
        /// Base64String Decompress to encoding`s String
        /// </summary>
        public static String Decompress(String strSource,Encoding encode)
        {
            byte[] buffer = Convert.FromBase64String(strSource);
            byte[] bytes= Decompress(buffer);
            return encode.GetString(bytes); 
        }
        /// <summary>
        /// utf8`s String Compress to Base64String
        /// </summary>
        public static String Compress(String strSource)
        {
            return Compress(strSource,Encoding.UTF8);
        }
        /// <summary>
        /// Encoding`s String Compress to Base64String
        /// </summary>
        public static String Compress(String strSource,Encoding encode)
        {
            byte[] buffer = encode.GetBytes(strSource);
            byte[] bytes = Compress(buffer);
            return Convert.ToBase64String(bytes);
        }
        #endregion
        #region Deflate with Bytes
        /// <summary>
        /// Decompress with bytes
        /// </summary>
        public static Byte[] Decompress(Byte[] input)
        {
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream(input))
            {
                Decompress(ms);
                byte[] decompressBuffer = ms.ToArray();
                return decompressBuffer;
            }
        }
        /// <summary>
        /// Compress with bytes
        /// </summary>
        public static byte[] Compress(byte[] input)
        {
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream(input))
            {
                Compress(ms);
                byte[] result = ms.ToArray();
                return result;
            }
        }
        #endregion
        #region Deflate with Stream
        /// <summary>
        /// Decompress with Stream
        /// </summary>
        public static Stream Decompress(Stream input)
        {

            System.IO.Compression.DeflateStream stream =
            new System.IO.Compression.DeflateStream(
                input, System.IO.Compression.CompressionMode.Decompress);
            stream.Flush();
            return input;

        }
        /// <summary>
        /// Compress with Stream
        /// </summary>
        public static Stream Compress(Stream input)
        {
            System.IO.Compression.DeflateStream stream =
                new System.IO.Compression.DeflateStream(
                    input, System.IO.Compression.CompressionMode.Compress, true);
            stream.Flush();
            return input;
        }
        #endregion
    }
}