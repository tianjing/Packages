using NUnit.Framework;
using System;
using TG.Package.Encryption;

namespace EncryptionTest
{
		[TestFixture]
		public class DESEncryptorTest
		{
				[Test]
				public static void DesTest ()
				{
                    DESEncryptor des = new DESEncryptor(Resource.Key);
                    String encryString = des.Encrypt(Resource.SourceString);
                    String result = des.Decrypt( encryString);
				    Assert.AreEqual (Resource.SourceString, result);

				}
                [Test]
                public static void Des_ErrorKey_Test()
                {
                    Assert.Catch(() => { 
                    DESEncryptor des = new DESEncryptor(Resource.ErrorKey);
                    String encryString = des.Encrypt( Resource.SourceString);
                    String result = des.Decrypt(encryString);
                    Assert.AreNotEqual(Resource.SourceString, result);
                    
                    },"");

                }
		}
}

