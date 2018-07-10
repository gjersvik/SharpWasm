using System;
using SharpWasm.Internal;

namespace SharpWasm
{
    public class WebAssemblyTable
    {
        private readonly uint[] _functions;


        public WebAssemblyTable(ulong initial)
        {
            _functions = new uint[initial];
        }

        public uint Get(int index)
        {
            return _functions[index];
        }

        public void Set(int index, uint value)
        {
            _functions[index] = value;
        }

        internal void Write(ElementSegment segment)
        {
            Array.Copy(segment.Elems, 0, _functions, segment.Offset, segment.Elems.Length);
        }
    }
}
