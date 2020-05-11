using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace BinarySerialization.Test.ReadOutOfStream
{
	public class ReadOutOfStreamTests
	{
		private BinarySerializer _serializer = new BinarySerializer();

		private MemoryStream _memoryStream;

		private BinaryWriter _binaryWriter;

		public ReadOutOfStreamTests()
		{
			_memoryStream = new MemoryStream();

			_binaryWriter = new BinaryWriter(_memoryStream);
		}

		[Fact]
		public void DeserializeIntFromEmptyStream_ShouldThrowEndOfStreamException()
		{
			Assert.Throws<EndOfStreamException>(() => _serializer.Deserialize<int>(_memoryStream));
		}

		[Fact]
		public void DeserializeIntFromShortStream_ShouldThrowEndOfStreamException()
		{
			_binaryWriter.Write(short.MaxValue);
			_memoryStream.Position = 0;
			Assert.Throws<EndOfStreamException>(() => _serializer.Deserialize<int>(_memoryStream));
		}

		[Fact]
		public void DeserializeCustomClassFromEmptryStream_ShouldThrowEndOfStreamException()
		{
			Assert.Throws<EndOfStreamException>(() => _serializer.Deserialize<CustomClass>(_memoryStream));
		}
	}
}
