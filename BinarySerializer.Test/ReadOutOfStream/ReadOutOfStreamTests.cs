using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;

namespace BinarySerialization.Test.ReadOutOfStream
{
	public class ReadOutOfStreamTests
	{
		private BinarySerializer _serializer;

		private MemoryStream _memoryStream;

		public ReadOutOfStreamTests()
		{
			_serializer = new BinarySerializer();

			_memoryStream = new MemoryStream();
		}


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

		[Fact]
		public void DeserializeBeyondBitLengthConstrainedField_ShouldThrowEndOfStreamException()
		{
			//var obj = new CustomBitConstrainedFieldClass() { Field = new CustomBitLengthConstrainedClass() { A = 1, B = 2, C = 3 } };
			var obj = new CustomBitLengthConstrainedClass() { A = 1, B = 2, C = 3 };

			_serializer.Serialize(_memoryStream, obj);

			_serializer.Deserialize<CustomBitConstrainedFieldClass>(_memoryStream);
			//Assert.Throws<EndOfStreamException>(() => _serializer.Deserialize<CustomBitConstrainedFieldClass>(_memoryStream));
		}
	}
}
