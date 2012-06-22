using System.IO;
using System;

namespace TG.Package.Compression.Tools
{
   public class StreamHelper
    {
        /**/
        /// <summary>
        /// 拷贝复制流字节
        /// </summary>
        /// <param name="input"></param>
        /// <param name="output"></param>
        internal static void CopyStream(Stream input, Stream output)
        {
            byte[] buffer = new byte[2000];
            int len;
            input.Seek(0, SeekOrigin.Begin);
            while ((len = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, len);
            }
            output.Flush();
        }
        internal static byte[] StreamToBytes(Stream stream)
        {
            if (stream.CanRead && stream.Length > 0)
            {
                byte[] bytes = new byte[stream.Length];
                
                stream.Read(bytes, 0, (Int32)stream.Length);
                return bytes;
            }
            else
            { 
                return new byte[0];
            }
        }
    }
}
