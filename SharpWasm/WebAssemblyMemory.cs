using System;

namespace SharpWasm
{
    public class WebAssemblyMemory
    {
        public readonly byte[] Buffer;

        public WebAssemblyMemory(ulong initial, ulong maximum = 0)
        {
            throw new NotImplementedException();
        }

        public ulong Grow(ulong delta)
        {
            throw new NotImplementedException();
        }
    }
}
