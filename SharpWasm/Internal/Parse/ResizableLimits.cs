using System.IO;

namespace SharpWasm.Internal.Parse
{
    internal class ResizableLimits
    {
        public readonly bool Flags;
        public readonly uint Initial;
        public readonly uint? Maximum;

        public ResizableLimits(BinaryReader reader)
        {
            Flags = VarIntUnsigned.ToBool(reader);
            Initial = VarIntUnsigned.ToUInt(reader);
            if (Flags) Maximum = VarIntUnsigned.ToUInt(reader);
        }

        public ResizableLimits(uint initial, uint? maximum = null)
        {
            Initial = initial;
            Maximum = maximum;
            Flags = Maximum != null;
        }
    }
}