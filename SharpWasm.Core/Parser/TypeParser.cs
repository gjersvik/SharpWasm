using System;
using System.IO;
using SharpWasm.Core.Types;
using ValueType = SharpWasm.Core.Types.ValueType;

namespace SharpWasm.Core.Parser
{
    internal static class TypeParser
    {
        private static ValueType ToValueType(BinaryReader reader) => (ValueType)Values.ToSByte(reader);
        public static BlockType ToBlockType(BinaryReader reader) => (BlockType)Values.ToSByte(reader);

        public static FunctionType ToFunctionType(BinaryReader reader)
        {
            const sbyte form = -0x20;
            if (Values.ToSByte(reader) != form) throw new NotImplementedException();
            return new FunctionType(
                Values.ToVector(reader, ToValueType),
                Values.ToVector(reader, ToValueType)
            );
        }

        public static Limits ToLimits(BinaryReader reader)
        {
            var flags = Values.ToBool(reader);
            var initial = Values.ToUInt(reader);
            uint? maximum = null;
            if (flags) maximum = Values.ToUInt(reader);
            return new Limits(initial, maximum);
        }

        public static MemoryType ToMemoryType(BinaryReader reader)
        {
            return new MemoryType(ToLimits(reader));
        }

        public static ElemType ToElemType(BinaryReader reader) => (ElemType)Values.ToSByte(reader);

        public static TableType ToTableType(BinaryReader reader)
        {
            var elemType = ToElemType(reader);
            var limits = ToLimits(reader);
            return new TableType(limits, elemType);
        }

        public static GlobalType ToGlobalType(BinaryReader reader)
        {
            var valueType = ToValueType(reader);
            var mutable = Values.ToBool(reader);
            return new GlobalType(valueType, mutable);
        }

        public static ExternalKind ToExternalKind(BinaryReader reader)
        {
            return (ExternalKind)reader.ReadByte();
        }
    }
}
