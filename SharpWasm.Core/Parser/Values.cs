using System;
using System.Collections.Immutable;
using System.IO;

namespace SharpWasm.Core.Parser
{
    public static class Values
    {
        public static uint ToUInt(BinaryReader reader) => UnsignedVar(reader, out _ );
        public static byte ToByte(BinaryReader reader) => Convert.ToByte(ToUInt(reader));
        public static bool ToBool(BinaryReader reader) => Convert.ToBoolean(ToUInt(reader));
        public static long ToLong(BinaryReader reader) => SignedVar(reader, out _);
        public static int ToInt(BinaryReader reader) => Convert.ToInt32(ToLong(reader));
        public static sbyte ToSByte(BinaryReader reader) => Convert.ToSByte(ToLong(reader));

        public static uint UnsignedVar(BinaryReader reader, out uint count)
        {
            count = 0;
            uint uInt = 0;
            var shift = 0;

            while (true)
            {
                var bt = reader.ReadByte();

                uInt += (uint)(bt & 0x7f) << shift;
                count += 1;
                if (bt < 128) break;

                shift += 7;
            }

            return uInt;
        }

        public static long SignedVar(BinaryReader reader, out uint count)
        {
            count = 0;
            var Long = 0L;
            var shift = 0;
            byte bt;
            do
            {
                var ibt = reader.ReadByte();

                bt = ibt;

                Long |= (long)(bt & 0x7f) << shift;
                shift += 7;
                count += 1;
            } while (bt >= 128);

            // Sign extend negative numbers.
            if ((bt & 0x40) != 0)
                Long |= -1L << shift;

            return Long;
        }

        public static ImmutableArray<T> ToVector<T>(BinaryReader reader, Func<BinaryReader, T> parser)
        {
            var count = ToUInt(reader);
            var builder = ImmutableArray.CreateBuilder<T>((int)count);

            for (var i = 0; i < count; i++)
            {
                builder.Add(parser(reader));
            }

            return builder.MoveToImmutable();
        }
    }
}
