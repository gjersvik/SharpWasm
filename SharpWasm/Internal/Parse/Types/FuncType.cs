using System;
using System.IO;
using SharpWasm.Core.Parser;
using SharpWasm.Core.Types;

namespace SharpWasm.Internal.Parse.Types
{
    internal static class FuncType
    {
        public static FunctionType Parse(BinaryReader reader)
        {
            if (Values.ToSByte(reader) != Form) throw new NotImplementedException();
            return new FunctionType(
                ParseTools.ToVector(reader, ParseTools.ToValueType),
                ParseTools.ToVector(reader, ParseTools.ToValueType)
            );
        }

        private const sbyte Form = -0x20;
    }
}