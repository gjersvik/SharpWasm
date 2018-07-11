using System.IO;

namespace SharpWasm.Internal.Parse
{
    internal class GlobalType
    {
        public readonly ValueType ContentType;
        public readonly bool Mutability;

        public GlobalType(BinaryReader reader)
        {
            ContentType = VarIntSigned.ToValueType(reader);
            Mutability = VarIntUnsigned.ToBool(reader);
        }

        public GlobalType(ValueType contentType, bool mutability)
        {
            ContentType = contentType;
            Mutability = mutability;
        }
    }
}