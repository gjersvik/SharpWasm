using System;
using System.Linq;
using SharpWasm.Core.Code;
using SharpWasm.Internal.Parse.Sections;

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
            var offset = ((Instruction<int>)segment.Offset[0]).Immediate;

            Array.Copy(segment.Data.ToArray(),0,_bytes, offset,segment.Data.Length);
        }
    }
}
