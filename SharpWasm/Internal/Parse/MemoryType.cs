using System.IO;

namespace SharpWasm.Internal.Parse
{
    internal class MemoryType
    {
        public readonly ResizableLimits Limits;

        public MemoryType(BinaryReader reader)
        {
            Limits = new ResizableLimits(reader);
        }

        public MemoryType(ResizableLimits limits)
        {
            Limits = limits;
        }
    }
}
