namespace SharpWasm.Internal.Parse.Types
{
    internal enum ValueType : sbyte
    {
        I32 = -0x01,
        I64 = -0x02,
        F32 = -0x03,
        F64 = -0x04
    }
}
