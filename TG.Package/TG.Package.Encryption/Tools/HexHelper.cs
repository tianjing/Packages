using System;
using System.Collections.Generic;
using System.Text;

namespace TG.Package.Encryption.Tools
{
   public class HexHelper
    {
        /// <summary>
        /// Converts a hexadecimal string to a byte array
        /// </summary>
        /// <param name="hexString">hex value</param>
        /// <returns>byte array</returns>
        public static byte[] HexToByte(String hexString)
        {
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] =
                Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }
        /// <summary>
        /// Converts a byte array to a hexadecimal string
        /// </summary>
        /// <param name="byteArray">byte array</param>
        /// <returns>hex string</returns>
        public static String ByteToHex(byte[] byteArray)
        {
            String outString = "";
            foreach (Byte b in byteArray)
                outString += b.ToString("X2");
            return outString;
        }



    }
}
