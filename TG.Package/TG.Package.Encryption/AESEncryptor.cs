using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace TG.Package.Encryption
{
    public sealed class AESEncryptor 
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
        /// <param name="pEncryptionBits">Encryption bits (128,192,256).</param>
        public AESEncryptor(String pKeyString, AESBits pEncryptionBits)
        {
            mKeyString = pKeyString;
            mEncryptionBits = pEncryptionBits;
            mIV = Tools.KeysHelper.VI16Bit;
            GetKey();
        }
        /// <summary>
        /// Initialize new AESEncryptor.
        /// </summary>
        /// <param name="pKeyString">The key to use for encryption/decryption</param>
        /// <param name="pIVString">The IV to use for encryption/decryption</param>
        /// <param name="pEncryptionBits">Encryption bits (128,192,256).</param>
        public AESEncryptor(String pKeyString, String pIVString, AESBits pEncryptionBits)
        {
            mKeyString = pKeyString;
            mEncryptionBits = pEncryptionBits;
            mIVString = pIVString;
            GetIV();
            GetKey();
        }
        /// <summary>
        /// Initialize new AESEncryptor.
        /// </summary>
        /// <param name="pKeyString">The key to use for encryption/decryption.</param>
        /// <param name="pIV">The IV to use for encryption/decryption</param>
        /// <param name="pEncryptionBits">Encryption bits (128,192,256).</param>
        public AESEncryptor(String pKeyString, byte[] pIV, AESBits pEncryptionBits)
        {
            mKeyString = pKeyString;
            mEncryptionBits = pEncryptionBits;
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
            if (IV.Length == 16)
            {
                mIV = iv;
            }

        }
        /// <summary>
        /// Valid IV and Key
        /// </summary>
        private void GetKey()
        {

            switch (mEncryptionBits)
            {
                case AESBits.BITS128:
                    mKey = Tools.KeysHelper.StringToBytes(mKeyString, 16);
                    break;
                case AESBits.BITS192:
                    mKey = Tools.KeysHelper.StringToBytes(mKeyString, 24);
                    break;
                case AESBits.BITS256:
                    mKey = Tools.KeysHelper.StringToBytes(mKeyString, 32);
                    break;
                default: break;
            }

        }

        /// <summary>
        /// String to bytes with IV
        /// </summary>
        private void Valid()
        {
            if (null == mKey)
            { throw new Exception("key is error,please check length"); }
            if (null == mIV || mIV.Length != 16)
            { throw new Exception("IV is error,please check length"); }
        }
        #region AES Encrypt

        /// <summary>
        /// Encrypt to UTF8`s String
        /// </summary>
        /// <param name="data">UTF8`s String to encrypt.</param>
        public String Encrypt(String data)
        {
            byte[] clearBytes = System.Text.Encoding.Unicode.GetBytes(data);

            return Encrypt(data, Encoding.UTF8);
        }
        /// <summary>
        /// Encrypt to String
        /// </summary>
        /// <param name="data">String to encrypt.</param>
        public String Encrypt(String data, Encoding encode)
        {
            byte[] clearBytes = encode.GetBytes(data);

            return Convert.ToBase64String(EncryptToBytes(clearBytes));
        }
        /// <summary>
        /// Encrypt to Bytes
        /// </summary>
        /// <param name="data">Bytes to encrypt.</param>
        public byte[] Encrypt(byte[] data)
        {
            return EncryptToBytes(data);
        }
        /// <summary>
        /// Encrypt to Bytes
        /// </summary>
        /// <param name="data">data to Encrypt</param>
        /// <param name="key"></param>
        /// <param name="iV"></param>
        /// <returns></returns>
        private byte[] EncryptToBytes(byte[] data)
        {
            Valid();

            Rijndael alg = Rijndael.Create();
            try
            {
                alg.Key = mKey;
                alg.IV = mIV;
                alg.Mode = Mode;
                alg.Padding = Padding;
                byte[] result = alg.CreateEncryptor().TransformFinalBlock(data, 0, data.Length);

                return result;
            }
            finally
            {
                if (null != alg)
                {
                    alg.Dispose();
                }
            }

        }




        #endregion

        #region AES Decrypt



        /// <summary>
        /// Decrypt UTF8`s String
        /// </summary>
        /// <param name="data">Encrypted UTF8`s String.</param>
        public String Decrypt(String data)
        {
            return Decrypt(data, Encoding.UTF8);
        }
        /// <summary>
        /// Decrypt String
        /// </summary>
        /// <param name="data">Encrypted String.</param>
        public String Decrypt(String data, Encoding encode)
        {
            byte[] dataToDecrypt = Convert.FromBase64String(data);
            return encode.GetString(DecryptToBytes(dataToDecrypt));
        }
        /// <summary>
        /// Decrypt Bytes
        /// </summary>
        /// <param name="data">Encrypted byte </param>
        public byte[] Decrypt(byte[] data)
        {
            return DecryptToBytes(data);
        }
        /// <summary>
        ///  Decrypt Bytes
        /// </summary>
        /// <param name="data">data to Decrypt</param>
        /// <returns></returns>
        private byte[] DecryptToBytes(byte[] data)
        {
            Valid();

            Rijndael alg = Rijndael.Create();
            try
            {
                alg.Key = mKey;
                alg.IV = mIV;
                alg.Padding = Padding;
                alg.Mode = Mode;
                byte[] result = alg.CreateDecryptor().TransformFinalBlock(data, 0, data.Length);
                return result;
            }
            finally
            {
                if (null != alg)
                {
                    alg.Dispose();
                }
            }

        }
        #endregion

    }

}
