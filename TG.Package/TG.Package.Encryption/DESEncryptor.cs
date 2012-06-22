using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace TG.Package.Encryption
{
    public sealed class DESEncryptor
    {
        #region member
        private String mKeyString;
        /// <summary>
        /// Encryption/Decryption key.
        /// </summary>
        public String KeyString
        {
            get { return mKeyString; }
        }

        private String mIVString;
        /// <summary>
        ///  IV`s String 
        /// </summary>
        public String IVString { get { return mIVString; } }

        private byte[] mKey = null;
        /// <summary>
        ///  AES`s Key
        /// </summary>
        public byte[] Key { get { return mKey; } set { mKey = value; } }

        private byte[] mIV = null;
        /// <summary>
        /// AES`s IV
        /// </summary>
        public byte[] IV { get { return mIV; } set { mIV = value; } }

        private AESBits mEncryptionBits;
        /// <summary>
        /// Encryption/Decryption bits.
        /// </summary>
        public AESBits EncryptionBits
        {
            get { return mEncryptionBits; }
            set { mEncryptionBits = value; }
        }

        private CipherMode mMode = CipherMode.CBC;
        /// <summary>
        /// CipherMode Mode
        /// </summary>
        public CipherMode Mode { set { mMode = value; } get { return mMode; } }

        private PaddingMode mPaddingMode = PaddingMode.PKCS7;
        /// <summary>
        /// Padding Mode
        /// </summary>
        public PaddingMode Padding { get { return mPaddingMode; } set { mPaddingMode = value; } }
        #endregion

        #region Structure
        /// <summary>
        /// Initialize new AESEncryptor.
        /// </summary>
        /// <param name="pKeyString">The key to use for encryption/decryption.</param>
        public DESEncryptor(String pKeyString)
        {
            mKeyString = pKeyString;
            mIV = Tools.KeysHelper.VI8Bit;
            GetKey();
        }
        /// <summary>
        /// Initialize new AESEncryptor.
        /// </summary>
        /// <param name="pKeyString">The key to use for encryption/decryption</param>
        /// <param name="pIVString">The IV to use for encryption/decryption</param>
        public DESEncryptor(String pKeyString, String pIVString)
        {
            mKeyString = pKeyString;
            mIVString = pIVString;
            GetKey();
            GetIV();
        }
        /// <summary>
        /// Initialize new AESEncryptor.
        /// </summary>
        /// <param name="pKeyString">The key to use for encryption/decryption</param>
        /// <param name="pIVString">The IV to use for encryption/decryption</param>
        public DESEncryptor(String pKeyString, byte[] pIV)
        {
            mKeyString = pKeyString;
            mIV = pIV;
            GetKey();
        }
        #endregion
        /// <summary>
        /// String to bytes with IV
        /// </summary>
        private void GetIV()
        {
            byte[] iv = null;
            if (null == mIV)
            {
                iv = Encoding.UTF8.GetBytes(mIVString);
            }
            if (IV.Length == 8)
            {
                mIV = iv;
            }

        }

        /// <summary>
        /// Valid IV and Key
        /// </summary>
        private void GetKey()
        {
            mKey = Tools.KeysHelper.StringToBytes(mKeyString, 8);
        }

        /// <summary>
        /// String to bytes with IV
        /// </summary>
        private void Valid()
        {
            if (null == mKey)
            { throw new Exception("key is error,please check length is 24"); }
            if (null == mIV || mIV.Length != 8)
            { throw new Exception("IV is error,please check length is 8"); }
        }
        #region Encrypt
        /// <summary>
        /// Encrypt UTF8`s String
        /// </summary>
        /// <param name="data">UTF8`s data to Encrypt</param>
        /// <returns></returns>
        public String Encrypt(String data)
        {
            return Encrypt(data, Encoding.UTF8);
        }
        /// <summary>
        /// Encrypt String
        /// </summary>
        /// <param name="data">data to Encrypt</param>
        /// <param name="encode">set data`s Encrypt</param>
        /// <returns></returns>
        public String Encrypt(String data, Encoding encode)
        {
            return Convert.ToBase64String(EncryptToBytes(encode.GetBytes(data)));
        }
        /// <summary>
        /// Encrypt to bytes
        /// </summary>
        /// <param name="data">data to Encrypt</param>
        /// <returns></returns>
        public byte[] Encrypt(byte[] data)
        {
            return EncryptToBytes(data);
        }
        /// <summary>
        /// Encrypt byte 
        /// </summary>
        /// <param name="data">Bytes to encrypt.</param>
        /// <returns></returns>
        private byte[] EncryptToBytes(byte[] data)
        {
            DESCryptoServiceProvider desc = new DESCryptoServiceProvider();
            try
            {
                desc.Key = mKey;
                desc.IV = mIV;
                desc.Mode = mMode;
                desc.Padding = mPaddingMode;
                return desc.CreateEncryptor().TransformFinalBlock(data, 0, data.Length);
            }
            finally
            {
                if (null != desc)
                {
                    desc.Dispose();
                }
            }

        }
        #endregion

        #region Decrypt
        /// <summary>
        /// Encrypt UTF8`s String 
        /// </summary>
        /// <param name="data">data to Encrypt</param>
        /// <returns></returns>
        public String Decrypt(String data)
        {
            return Decrypt(data, Encoding.UTF8);
        }
        /// <summary>
        /// Encrypt String
        /// </summary>
        /// <param name="data">data to Encrypt</param>
        /// <returns></returns>
        public String Decrypt(String data, Encoding encode)
        {
            return encode.GetString(DecryptToBytes(Convert.FromBase64String(data)));
        }

        /// <summary>
        /// Encrypt to bytes
        /// </summary>
        /// <param name="data">data to Encrypt</param>
        /// <returns></returns>
        public byte[] Decrypt(byte[] data)
        {
            return DecryptToBytes(data);
        }
        /// <summary>
        /// Encrypt to bytes
        /// </summary>
        /// <param name="data">data to Encrypt</param>
        /// <returns></returns>
        private byte[] DecryptToBytes(byte[] data)
        {
            DESCryptoServiceProvider desc = new DESCryptoServiceProvider();
            try
            {
                desc.Mode = mMode;
                desc.Padding = mPaddingMode;
                return desc.CreateDecryptor(mKey, mIV).TransformFinalBlock(data, 0, data.Length);
            }
            finally
            {
                if (null != desc)
                {
                    desc.Dispose();
                }
            }
        }
        #endregion


    }
}
