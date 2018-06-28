using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpWasm.Internal
{
    internal class VirtualMachine
    {
        private readonly Stack<int> _stack = new Stack<int>();
        private int[] _locals;

        public int Run(FunctionBody body, params int[] args)
        {
            _locals = args.ToArray();
            return Run(body.Code);
        }

        private int Run(byte[] code)
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
                    case Instructions.GetLocal:
                        _stack.Push(_locals[reader.ReadVarUInt32()]);
                        break;
                    case Instructions.I32Add:
                        _stack.Push(_stack.Pop() + _stack.Pop());
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }
        }
    }
}
