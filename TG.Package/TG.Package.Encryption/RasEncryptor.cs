using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace TG.Package.Encryption
{
     /// <summary>
    /// Support long String 
    /// </summary>
   public sealed class RsaEncryptor
    {
        /// <summary>
        /// Encrypt String 
        /// </summary>
        /// <param name="data">Source data</param>
        /// <param name="e">BigInteger typeof  Exponent </param>
        /// <param name="n">BigInteger typeof  Modulus </param>
        /// <returns></returns>
        private static string EncryptString(string source, BigInteger e, BigInteger n)
        {
          //  source = System.Web.HttpUtility.UrlEncode(source);
            int len = source.Length;
            int len1 = 0;
            int blockLen = 0;
            if ((len % 128) == 0)
                len1 = len / 128;
            else
                len1 = len / 128 + 1;
            string block = "";
            string temp = "";
            for (int i = 0; i < len1; i++)
            {
                if (len >= 128)
                    blockLen = 128;
                else
                    blockLen = len;
                block = source.Substring(i * 128, blockLen);
                byte[] oText = System.Text.Encoding.Default.GetBytes(block);
                BigInteger biText = new BigInteger(oText);
                BigInteger biEnText = biText.modPow(e, n);
                string temp1 = biEnText.ToHexString();
                temp += temp1;
                len -= blockLen;
            }
            return temp;


        }
        /// <summary>
        /// Use BigInteger Encrypt String 
        /// </summary>
        /// <param name="data">Source data</param>
        /// <param name="e">Ras`s Exponent node value</param>
        /// <param name="m">RasKey`s Modulus node value</param>
        public static string Encrypt(string data, string e, string m)
        {
            byte[] N = Convert.FromBase64String(m);
            byte[] D = Convert.FromBase64String(e);
            BigInteger biN = new BigInteger(N);
            BigInteger biD = new BigInteger(D);
            return EncryptString(data, biD, biN);
        }
        /// <summary>
        /// Decrypt String
        /// </summary>
        /// <param name="source">Encrypt data</param>
        /// <param name="d">RRas`s D node value</param>
        /// <param name="n">Ras`s Modulus node value</param>
        private static string DecryptString(string data, BigInteger d, BigInteger n)
        {

            int len = data.Length;
            int len1 = 0;
            int blockLen = 0;
            if ((len % 256) == 0)
                len1 = len / 256;
            else
                len1 = len / 256 + 1;
            string block = "";
            string temp = "";
            for (int i = 0; i < len1; i++)
            {
                if (len >= 256)
                    blockLen = 256;
                else
                    blockLen = len;
                block = data.Substring(i * 256, blockLen);
                BigInteger biText = new BigInteger(block, 16);
                BigInteger biEnText = biText.modPow(d, n);
                string temp1 = System.Text.Encoding.Default.GetString(biEnText.getBytes());
                temp += temp1;
                len -= blockLen;
            }
            return temp;
        }
        /// <summary>
        /// Use BigInteger  Decrypt String 
        /// </summary>
        /// <param name="data">Encrypt data</param>
        /// <param name="d">Ras`s D node value</param>
        /// <param name="m">Ras`s Modulus node value</param>
        public static string Decrypt(string data, string d, string m)
        {
            byte[] N = Convert.FromBase64String(m);
            byte[] E = Convert.FromBase64String(d);
            BigInteger biN = new BigInteger(N);
            BigInteger biE = new BigInteger(E);
            return DecryptString(data, biE, biN);
        }

        /// <summary>   
        /// Encrypt 
        /// </summary>   
        /// <param name="xmlPublicKey">xml PublicKey</param>   
        /// <param name="data">source data</param>    
        public static string Encrypt(string xmlPublicKey, string data)
        {
            byte[] PlainTextBArray;
            byte[] CypherTextBArray;
            string Result = String.Empty;
            System.Security.Cryptography.RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(xmlPublicKey);
            int t = (int)(Math.Ceiling((double)data.Length / (double)50));
            //Split  EncryptString
            for (int i = 0; i <= t - 1; i++)
            {
                PlainTextBArray = (new UnicodeEncoding()).GetBytes(data.Substring(i * 50, data.Length - (i * 50) > 50 ? 50 : data.Length - (i * 50)));
                CypherTextBArray = rsa.Encrypt(PlainTextBArray, false);
                Result += Convert.ToBase64String(CypherTextBArray) + "ThisIsSplit";
            }
            return Result;
        }
        /// <summary>   
        ///  Decrypt 
        /// </summary>   
        /// <param name="xmlPrivateKey">xml PrivateKey</param>   
        /// <param name="data">Encrypt data</param>   
        public static string Decrypt(string xmlPrivateKey, string data)
        {
            byte[] PlainTextBArray;
            byte[] DypherTextBArray;
            String Result = String.Empty;
            System.Security.Cryptography.RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(xmlPrivateKey);
            string[] Split = new string[1];
            Split[0] = "ThisIsSplit";
            //Split  DecryptString
            string[] mis = data.Split(Split, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < mis.Length; i++)
            {
                PlainTextBArray = Convert.FromBase64String(mis[i]);
                DypherTextBArray = rsa.Decrypt(PlainTextBArray, false);
                Result += (new UnicodeEncoding()).GetString(DypherTextBArray);
            }
            return Result;
        }

        /// <summary>   
        /// Get RSA Key   
        /// </summary>   
        /// <returns>string[] 0:PrivateKey;1:PublicKey</returns>   
        public static string[] RSAKey()
        {
            string[] keys = new string[2];
            System.Security.Cryptography.RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            keys[0] = rsa.ToXmlString(true);
            keys[1] = rsa.ToXmlString(false);
            return keys;
        }

    }
}
