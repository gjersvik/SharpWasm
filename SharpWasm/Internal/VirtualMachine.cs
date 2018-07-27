using System;
using System.Collections.Generic;
using System.Linq;
using SharpWasm.Internal.Parse.Code;
using ValueType = SharpWasm.Internal.Parse.Types.ValueType;

namespace SharpWasm.Internal
{
    internal class VirtualMachine
    {
        private readonly Stack<int> _stack = new Stack<int>();
        private readonly WebAssemblyInstance _instance;
        private readonly WebAssemblyImports _imports;
        private readonly Module _module;

        public VirtualMachine(WebAssemblyInstance instance)
        {
            _instance = instance;
            _imports = _instance.Imports;
            _module = _instance.Module;
        }

        public int Run(Function func, params int[] args)
        {
            return Run(func.Body, args.ToArray());
        }

        private int Run(byte[] code, int[] param)
        {
            using (var reader = new WasmReader(code))
            {
                Run(reader, param);
            }
            return _stack.Count == 1 ? _stack.Pop() : 0;
        }

        private void Run(WasmReader reader, int[] locals)
        {
            while (true)
            {
                var opCode = (OpCode)reader.ReadUInt8();
                switch (opCode)
                {
                    case OpCode.End:
                        return;
                    case OpCode.I32Const:
                        _stack.Push(reader.ReadVarInt32());
                        break;
                    case OpCode.GetLocal:
                        _stack.Push(locals[reader.ReadVarUInt32()]);
                        break;
                    case OpCode.I32Add:
                        _stack.Push(_stack.Pop() + _stack.Pop());
                        break;
                    case OpCode.Call:
                        Call(reader.ReadVarUInt32());
                        break;
                    case OpCode.CallIndirect:
                        CallIndirect(reader.ReadVarUInt32(), reader.ReadVarUInt1());
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }
        }

        private void Call(uint id)
        {
            var func = _module.GetFunction(id);
            var param = func.Param.Select(t => _stack.Pop()).Reverse().ToArray();
            if (func is Function bodyFunc)
            {
                using (var reader = new WasmReader(bodyFunc.Body))
                {
                    Run(reader, param);
                }
            }

            if (func is ImportFunction importFunc)
            {
                var output = _imports.Call(_instance,importFunc, param);
                if (importFunc.Return == ValueType.I32)
                {
                    _stack.Push(output);
                }
            }
        }

        // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Local
        // ReSharper disable once UnusedParameter.Local
        private void CallIndirect(uint type, bool reserved)
        {
            var index = _instance.Table.Get(_stack.Pop());
            var func = _module.GetFunction(index);
            if(func.TypeId != type) throw new Exception("Wrong type on indirect call");
            Call(index);
        }
    }
}
