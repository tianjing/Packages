using NUnit.Framework;
using System;
using TG.Package.Compression;

namespace CompressionTest
{
		[TestFixture()]
		public class GzipHelperTest
		{
				[Test()]
				private static void GzipTrueTest ()
				{
						String Compressresult = GzipHelper.Compress (Resource.SourceString);
						String DeCompressresult = GzipHelper.Decompress (Compressresult);

						Assert.AreEqual (Resource.SourceString, DeCompressresult);
				}
		}
}

