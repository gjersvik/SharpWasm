using System;
using System.Linq;
using SharpWasm.Core.Code;
using SharpWasm.Core.Segments;

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

        internal void Write(Element element)
        {
            var offset = ((Instruction<int>)element.Offset[0]).Immediate;

            Array.Copy(element.Init.ToArray(), 0, _functions, offset, element.Init.Length);
        }
    }
}
