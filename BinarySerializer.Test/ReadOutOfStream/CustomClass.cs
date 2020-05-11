using System;
using System.Collections.Generic;
using System.Text;

namespace BinarySerialization.Test.ReadOutOfStream
{
	public class CustomClass
	{
		[FieldOrder(0)]
		public int A;

		[FieldOrder(1)]
		public int B { get; set; }
	}
}
