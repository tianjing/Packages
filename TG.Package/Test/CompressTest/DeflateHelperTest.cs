using NUnit.Framework;
using System;
using TG.Package.Compression;

namespace CompressionTest
{
		[TestFixture()]
		public class DeflateHelperTest
		{
				[Test()]
				private static void DeflateTest ()
				{
						String Compressresult = DeflateHelper.Compress (Resource.SourceString);
						String DeCompressresult = DeflateHelper.Decompress (Compressresult);
						Assert.AreEqual (Resource.SourceString, DeCompressresult);
				}

		}
}

