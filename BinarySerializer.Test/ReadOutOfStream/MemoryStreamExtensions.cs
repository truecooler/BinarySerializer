using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace BinarySerialization.Test.ReadOutOfStream
{
	public static class MemoryStreamExtensions
	{
		public static void TruncateEnd(this MemoryStream ms, int truncateBytesCount)
		{
			ms.SetLength(ms.Length - truncateBytesCount);
		}
	}
}
