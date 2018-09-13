using System;
using System.IO;
using SharpWasm.Core.Types;
using SharpWasm.Internal.Parse.Types;
using ValueType = SharpWasm.Core.Types.ValueType;

namespace SharpWasm.Internal.Parse
{
    internal class VarIntSigned
    {
        public static long ToLong(BinaryReader reader) => new VarIntSigned(reader).Long;
        public static int ToInt(BinaryReader reader) => new VarIntSigned(reader).Int;
        public static sbyte ToSByte(BinaryReader reader) => new VarIntSigned(reader).SByte;
        public static ValueType ToValueType(BinaryReader reader) => new VarIntSigned(reader).ValueType;
        public static BlockType ToBlockType(BinaryReader reader) => new VarIntSigned(reader).BlockType;
        public static ElemType ToElemType(BinaryReader reader) => new VarIntSigned(reader).ElemType;
        
        public readonly long Long;
        public int Int => Convert.ToInt32(Long);
        public sbyte SByte => Convert.ToSByte(Long);
        public ValueType ValueType => (ValueType)SByte;
        public BlockType BlockType => (BlockType)SByte;
        public ElemType ElemType => (ElemType)SByte;

        public readonly byte Count;

        public VarIntSigned(BinaryReader reader)
        {
            Count = 0;
            Long = 0;
            var shift = 0;
            byte bt;
            do
            {
                var ibt = reader.ReadByte();

                bt = ibt;

                Long |= (long)(bt & 0x7f) << shift;
                shift += 7;
                Count += 1;
            } while (bt >= 128);

            // Sign extend negative numbers.
            if ((bt & 0x40) != 0)
                Long |= -1L << shift;
        }

        public VarIntSigned(long value)
        {
            Long = value;
            Count = 0;
        }
    }
}
