﻿using System.Collections.Generic;
using BinarySerialization;

namespace BinarySerializer.Test.Length
{
    public class ConstCollectionLengthClass
    {
        [FieldLength(6)]
        public List<string> Field { get; set; }
    }
}