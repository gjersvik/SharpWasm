using System;
using System.Collections.Generic;

namespace SharpWasm.Internal
{
    internal class VirtualMachine
    {
        private readonly Stack<int> _stack = new Stack<int>();

        public int Run(FunctionBody body)
        {
            return Run(body.Code);
        }

        public int Run(byte[] code)
        {
            using (var reader = new WasmReader(code))
            {
                Run(reader);
            }
            return _stack.Count == 1 ? _stack.Pop() : 0;
        }

        private void Run(WasmReader reader)
        {
            while (true)
            {
                var opCode = (Instructions)reader.ReadUInt8();
                switch (opCode)
                {
                    case Instructions.End:
                        return;
                    case Instructions.I32Const:
                        _stack.Push(reader.ReadVarInt32());
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }
        }
    }
}
