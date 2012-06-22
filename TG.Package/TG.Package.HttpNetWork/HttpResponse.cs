using System;
using System.Text;
using System.Net;
using System.IO;

namespace TG.Package.HttpNetWork
{
    public class HttpResponse:IDisposable
    {
        private HttpWebResponse mHttpWebResponse;
        public HttpResponse(WebResponse hwr)
        {
            mHttpWebResponse = hwr as HttpWebResponse;
        }
        /// <summary>
        /// Get HttpWebResponse
        /// </summary>
        /// <returns></returns>
        public HttpWebResponse GetResponse()
        {
            return mHttpWebResponse;
        }

        public MemoryStream ToMemoryStream()
        {
            return CopyStream();
        }

        public Byte[] ToBytes()
        {
            return CopyToBytes();
        }

        private Byte[] CopyToBytes()
        { 
            return ToMemoryStream().ToArray();
        }
        /// <summary>
        /// 拷贝复制流字节
        /// </summary>
        /// <param name="input"></param>
        /// <param name="output"></param>
        private MemoryStream CopyStream()
        {
            MemoryStream output = new MemoryStream();
            byte[] buffer = new byte[2000];
            int len;
            Stream input = mHttpWebResponse.GetResponseStream();
            while ((len = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, len);
            }
            output.Flush();
            return output;
        }
        /// <summary>
        /// Get Result Utf-8 String
        /// </summary>
        /// <returns></returns>
        public override String ToString()
        {
            return GetString(Encoding.UTF8);
        }
        /// <summary>
        /// Get Result Utf-8 String
        /// </summary>
        /// <returns></returns>
        public String ToString(Encoding encode)
        {
            return GetString(encode);
        }
        /// <summary>
        /// GetString by Encoding
        /// </summary>
        /// <returns></returns>
        private String GetString(Encoding encode)
        {
            String result = String.Empty;
            using (Stream stream = mHttpWebResponse.GetResponseStream())
            {
                if (null != stream)
                {
                    StreamReader sr = new StreamReader(stream, encode);
                    result = sr.ReadToEnd();
                }

                return result;
            }

        }

        public void Dispose()
        {
            mHttpWebResponse.Close();
        }
    }

}
