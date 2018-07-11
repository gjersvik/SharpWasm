using System.IO;

namespace SharpWasm.Internal.Parse
{
    internal class TableType
    {
        public readonly ElemType ElementType;
        public readonly ResizableLimits Limits;

        public TableType(BinaryReader reader)
        {
            ElementType = VarIntSigned.ToElemType(reader);
            Limits = new ResizableLimits(reader);
        }

        public TableType(ResizableLimits limits)
        {
            ElementType = ElemType.AnyFunc;
            Limits = limits;
        }
    }
}