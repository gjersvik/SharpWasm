
namespace SharpWasm.Internal
{
    internal enum Instructions
    {
        End = 0x0B,
        Call = 0x10,
        CallIndirect = 0x11,
        GetLocal = 0x20,
        I32Const = 0x41,
        I32Add = 0x6A
    }
}
