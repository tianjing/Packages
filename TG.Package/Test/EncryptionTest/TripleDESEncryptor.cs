using NUnit.Framework;
using System;
using TG.Package.Encryption;

namespace EncryptionTest
{
		[TestFixture]
		public class TripleDESEncryptorTest
		{
				[Test]
				public static void TripleDesTest ()
				{
						TripleDESEncryptor des = new TripleDESEncryptor (Resource.Key);
						String encryString = des.Encrypt (Resource.SourceString);
						String result = des.Decrypt (encryString);
						Assert.AreEqual (Resource.SourceString, result);
				}
                [Test]
                public static void TripleDes_ErrorKey_Test()
                {
                    Assert.Catch(() => {
                    TripleDESEncryptor des = new TripleDESEncryptor(Resource.Key);
                    String encryString = des.Encrypt(Resource.SourceString);
                    String result = des.Decrypt(encryString);
                    
                    Assert.AreNotEqual(Resource.SourceString, result);
                    },"");

                }
		}
}

