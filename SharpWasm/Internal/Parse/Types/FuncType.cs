using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;

namespace SharpWasm.Internal.Parse.Types
{
    internal class FuncType
    {
        public const sbyte Form = -0x20;
        public readonly uint ParamCount;
        public readonly ImmutableArray<ValueType> ParamTypes;
        public readonly bool ReturnCount;
        public readonly ValueType? ReturnType;

        public FuncType(BinaryReader reader)
        {
            if (VarIntSigned.ToSByte(reader) != Form) throw new NotImplementedException();
            ParamCount = VarIntUnsigned.ToUInt(reader);
            ParamTypes = ParseTools.ToArray(reader, ParamCount, VarIntSigned.ToValueType);
            ReturnCount = VarIntUnsigned.ToBool(reader);
            if (ReturnCount) ReturnType = VarIntSigned.ToValueType(reader);
        }

        public FuncType(IEnumerable<ValueType> paramTypes, ValueType? returnType = null)
        {
            ParamTypes = paramTypes.ToImmutableArray();
            ReturnType = returnType;
            ParamCount = (uint) ParamTypes.Length;
            ReturnCount = ReturnType != null;
        }
    }
}