using System.IO;
using System.Text;
using System;
namespace TG.Package.Compression
{
    public class GzipHelper
    {

        #region Gzip with String
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
        public static String Compress(String strSource, Encoding e)
        {
            return Convert.ToBase64String(Compress(e.GetBytes(strSource)));
        }
        /// <summary>
        /// Base64String Decompress to utf8`s String 
        /// </summary>
        public static String Decompress(String strSource)
        {
            return Decompress(strSource,Encoding.UTF8);
        }
        /// <summary>
        /// Base64String Decompress to encoding`s String
        /// </summary>
        public static String Decompress(String strSource, Encoding e)
        {
            return e.GetString(Compress(Convert.FromBase64String(strSource)));
        }


        #endregion
        #region Gzip with bytes
        /// <summary>
        /// Decompress with bytes
        /// </summary>
        public static byte[] Decompress(byte[] bytes)
        {
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                Decompress(ms);
                return ms.ToArray();
            }
        }
        /// <summary>
        /// Compress with bytes
        /// </summary>
        public static byte[] Compress(byte[] bytes)
        {
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                Compress(ms);
                return ms.ToArray();
            }
        }
        #endregion
        #region Gzip with Stream
        /// <summary>
        /// Decompress with Stream
        /// </summary>
        public static void Decompress(Stream input)
        {
            System.IO.Compression.GZipStream stream = 
                new System.IO.Compression.GZipStream(
                   input, System.IO.Compression.CompressionMode.Decompress);

            stream.Flush();
            
        }
        /// <summary>
        /// Compress with Stream
        /// </summary>
        public static void Compress(Stream input)
        {
            System.IO.Compression.GZipStream stream = 
                new System.IO.Compression.GZipStream(
                    input, System.IO.Compression.CompressionMode.Compress, true);
            
            stream.Flush();

        }
        #endregion

    }
}