using System;
using System.Linq;
using SharpWasm.Internal.Parse.Code;
using SharpWasm.Internal.Parse.Sections;

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

        internal void Write(ElementSegment segment)
        {
            var offset = ((Instruction<int>)segment.Offset.Instructions[0]).Immediate;

            Array.Copy(segment.Elements.ToArray(), 0, _functions, offset, segment.Elements.Length);
        }
    }
}
