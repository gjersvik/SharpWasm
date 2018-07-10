using System;
using SharpWasm.Internal;

namespace SharpWasm
{
    public class WebAssemblyMemory
    {
        private readonly byte[] _bytes;

        public WebAssemblyMemory(uint initial)
        {
            _bytes = new byte[initial * 65536];
        }

        public byte[] ReadBytes(int index, int length)
        {
            var output = new byte[length];
            Array.Copy(_bytes,index,output,0, length);
            return output;
        }

        internal void Write(DataSegment segment)
        {
            Array.Copy(segment.Data,0,_bytes, segment.Offset,segment.Data.Length);
        }
    }
}
