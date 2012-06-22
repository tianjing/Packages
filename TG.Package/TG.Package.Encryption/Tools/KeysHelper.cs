using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TG.Package.Encryption.Tools
{
  public   class KeysHelper
    {
      public readonly static  byte[] VI16Bit  ={
        0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF, 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF};

      public static readonly byte[] VI8Bit= {
       0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF
      };
      /// <summary>
      /// String to byte[]
      /// </summary>
      /// <param name="pKeyString">Key String</param>
      /// <param name="pCount">Result is Count</param>
      /// <returns></returns>
      public static byte[] StringToBytes(String pKeyString, Int32 pCount)
      {
          byte[] keys = Encoding.UTF8.GetBytes(pKeyString);
          byte[] result = null;
          if (keys.Length >= pCount)
          {
              result = new byte[pCount];
              Array.Copy(keys, result, pCount);
          }
          return result;
      }
    }
}
