namespace SharpWasm.Internal.Parse
{
    internal enum BlockType: sbyte
    {
        I32 = -0x01,
        I64 = -0x02,
        F32 = -0x03,
        F64 = -0x04,
        EmptyBlock = -0x40
    }
}
