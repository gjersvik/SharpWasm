using System;
using System.IO;
using SharpWasm.Core.Types;

namespace SharpWasm.Internal.Parse.Types
{
    internal static class FuncType
    {
        public static FunctionType Parse(BinaryReader reader)
        {
            if (VarIntSigned.ToSByte(reader) != Form) throw new NotImplementedException();
            return new FunctionType(
                ParseTools.ToVector(reader, VarIntSigned.ToValueType),
                ParseTools.ToVector(reader, VarIntSigned.ToValueType)
            );
        }

        private const sbyte Form = -0x20;
    }
}