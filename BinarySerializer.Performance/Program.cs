﻿namespace BinarySerializerTester
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 1000; i++)
            {
                var test = new BinarySerializer.Test.BinarySerializerTests();
                test.Roundtrip();
            }
        }
    }
}
