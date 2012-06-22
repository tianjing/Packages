using NUnit.Framework;
using System;
using TG.Package.Encryption;

namespace EncryptionTest
{
		[TestFixture()]
		public class AESEncryptorTest
		{
				[Test()]
				public static void AES_BITS128_True_Test ()
				{
						AESEncryptor aes = new AESEncryptor (Resource.Key, AESBits.BITS128);
						String encryString = aes.Encrypt (Resource.SourceString);
						String result = aes.Decrypt (encryString);

						Assert.AreEqual (Resource.SourceString, result);
				}
		}
}

