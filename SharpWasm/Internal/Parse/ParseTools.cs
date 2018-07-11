using System;
using System.Collections.Immutable;
using System.IO;

namespace SharpWasm.Internal.Parse
{
    internal static class ParseTools
    {
        public static ImmutableArray<T> ToArray<T>(BinaryReader reader, uint count, Func<BinaryReader, T> parser)
        {
            var builder = ImmutableArray.CreateBuilder<T>((int) count);

            for (var i = 0; i < count; i++)
            {
                builder.Add(parser(reader));
            }

            return builder.MoveToImmutable();
        }

        public static BinaryReader FromBytes(byte[] bytes)
        {
            return new BinaryReader(new MemoryStream(bytes));
        }
    }
}