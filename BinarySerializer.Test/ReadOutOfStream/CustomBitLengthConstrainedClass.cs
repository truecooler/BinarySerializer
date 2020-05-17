using System;
using System.Collections.Generic;
using System.Text;

namespace BinarySerialization.Test.ReadOutOfStream
{
	public class CustomBitLengthConstrainedClass
	{
		[FieldOrder(0)]
		public int A;

		[FieldOrder(1)]
		public int B;

		[FieldOrder(2)]
		public int C;
	}
}
