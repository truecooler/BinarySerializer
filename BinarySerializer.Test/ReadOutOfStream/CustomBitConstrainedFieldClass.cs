using System;
using System.Collections.Generic;
using System.Text;

namespace BinarySerialization.Test.ReadOutOfStream
{
	public class CustomBitConstrainedFieldClass
	{
		[FieldBitLength(20)]
		[FieldOrder(0)]
		public CustomBitLengthConstrainedClass Field;
	}
}
