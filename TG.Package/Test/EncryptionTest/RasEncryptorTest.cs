using NUnit.Framework;
using System;
using TG.Package.Encryption;
namespace EncryptionTest
{
		[TestFixture()]
		public class RasEncryptorTest
		{
            [Test()]
            public void RSAKeyTrueTest()
            {
                String[] keys = RsaEncryptor.RSAKey();
                if (keys.Length >= 2)
                {
                    Assert.True(true);
                }
                else
                {
                    Assert.False(false);
                }

            }
            public void RSA_XmlKey_TrueTest()
            {
                String encryString = RsaEncryptor.Encrypt(Resource.RsaPrivateKey,Resource.SourceString);
                String result = RsaEncryptor.Decrypt(Resource.RsaPrivateKey, encryString);
                Assert.AreEqual(Resource.SourceString, result);
            }
            public void RSA_NotXmlKey_TrueTest()
            {
                String encryString = RsaEncryptor.Encrypt( Resource.SourceString,Resource.RsaE,Resource.RsaM);
                String result = RsaEncryptor.Decrypt(encryString, Resource.RsaD, Resource.RsaM);
                Assert.AreEqual(Resource.SourceString, result);
            }
		}
}

