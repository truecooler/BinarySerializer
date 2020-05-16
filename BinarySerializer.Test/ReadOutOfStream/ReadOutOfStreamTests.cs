using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;

namespace BinarySerialization.Test.ReadOutOfStream
{
	class Nested
	{
		[FieldOrder(0)]
		public int A;

		[FieldOrder(1)]
		public int B;

		[FieldOrder(2)]
		public int C;
	}

	class Person
	{
		[FieldLength(2)]
		[FieldOrder(0)]
		public int Test;

		[FieldOrder(1)]
		public float Fl;

		[FieldOrder(2)]
		public int Age;

		[FieldLength(4)]
		[FieldOrder(3)]
		public Nested Nested;
	}

	public class ReadOutOfStreamTests
	{
		private BinarySerializer _serializer;

		private MemoryStream _memoryStream;

		public ReadOutOfStreamTests()
		{
			_serializer = new BinarySerializer();

			_memoryStream = new MemoryStream();
		}

		//[Fact]
		//public void Ttest()
		//{
		//	_memoryStream.Write(Enumerable.Repeat(0xFF, 1024).Select(x => (byte)x).ToArray(), 0, 1024);
		//	_memoryStream.Position = 0;

		//	var _serializer = new BinarySerializer();

		//	var r = _serializer.Deserialize<Person>(_memoryStream);

		//	//_serializer.Serialize(_memoryStream, new Person() { Age = 10, Fl = 0, Test = 20, Nested = new Nested() { A = 1, B = 2, C = 3 } });
		//}

		[Fact]
		public void DeserializeIntFromEmptyStream_ShouldThrowEndOfStreamException()
		{
			Assert.Throws<EndOfStreamException>(() => _serializer.Deserialize<int>(_memoryStream));
		}

		[Fact]
		public void DeserializeIntFromShortStream_ShouldThrowEndOfStreamException()
		{
			_serializer.Serialize(_memoryStream, short.MaxValue);
			_memoryStream.Position = 0;
			Assert.Throws<EndOfStreamException>(() => _serializer.Deserialize<int>(_memoryStream));
		}

		[Fact]
		public void DeserializeCustomClassFromEmptryStream_ShouldThrowEndOfStreamException()
		{
			Assert.Throws<EndOfStreamException>(() => _serializer.Deserialize<CustomSizedStringClass>(_memoryStream));
		}

		[Fact]
		public void DeserializeSizedStringFromTruncatedStream_ShouldThrowEndOfStreamException()
		{
			var obj = new CustomSizedStringClass() { String = "123456" };

			_serializer.Serialize(_memoryStream, obj);

			_memoryStream.TruncateEnd(2);

			_memoryStream.Position = 0;

			Assert.Throws<EndOfStreamException>(() => _serializer.Deserialize<CustomSizedStringClass>(_memoryStream));
		}

		[Fact]
		public void DeserializeSizedByteArrayFromTruncatedStream_ShouldThrowEndOfStreamException()
		{
			var obj = new CustomSizedByteArrayClass() { Array = new byte[] { 0xFF, 0xFF, 0xFF } };

			_serializer.Serialize(_memoryStream, obj);

			_memoryStream.TruncateEnd(1);

			_memoryStream.Position = 0;

			Assert.Throws<EndOfStreamException>(() => _serializer.Deserialize<CustomSizedByteArrayClass>(_memoryStream));
		}
	}
}
