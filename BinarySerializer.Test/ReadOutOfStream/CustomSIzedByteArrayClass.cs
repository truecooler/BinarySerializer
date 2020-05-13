using System;
using System.Collections.Generic;
using System.Text;

namespace BinarySerialization.Test.ReadOutOfStream
{
	public class CustomSizedByteArrayClass
	{
		[FieldOrder(0)]
		public int Length { get; set; }

		[FieldOrder(1)]
		[FieldLength(nameof(Length))]
		public byte[] Array { get; set; }
	}
}
