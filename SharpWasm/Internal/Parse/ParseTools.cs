using System;
using System.Collections.Immutable;
using System.IO;
using System.Text;
using SharpWasm.Internal.Parse.Types;

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

        public static ExternalKind ToExternalKind(BinaryReader reader)
        {
            return (ExternalKind)reader.ReadByte();
        }

        public static string ToUtf8(BinaryReader reader, uint length)
        {
            return Encoding.UTF8.GetString(reader.ReadBytes((int)length));
        }

        public static ImmutableArray<byte> ToBytes(BinaryReader reader)
        {
            using (var ms = new MemoryStream())
            {
                reader.BaseStream.CopyTo(ms);
                return ms.ToArray().ToImmutableArray();
            }
        }
    }
}