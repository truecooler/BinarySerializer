using System;
using System.Collections.Generic;
using System.Text;

namespace BinarySerialization.Test.ReadOutOfStream
{
	public class CustomSizedStringClass
	{
		[FieldOrder(0)]
		public int Length { get; set; }

		[FieldOrder(1)]
		[FieldLength(nameof(Length))]
		public string String { get; set; }
	}
}
