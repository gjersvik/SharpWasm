using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SharpWasm.Internal.Parse;
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
            using (var reader = ParseTools.FromBytes(code))
            {
                Run(reader, param);
            }
            return _stack.Count == 1 ? _stack.Pop() : 0;
        }

        // ReSharper disable once SuggestBaseTypeForParameter
        private void Run(BinaryReader reader, int[] locals)
        {
            while (true)
            {
                var opCode = (OpCode)reader.ReadByte();
                // ReSharper disable once SwitchStatementMissingSomeCases
                switch (opCode)
                {
                    case OpCode.End:
                        return;
                    case OpCode.I32Const:
                        _stack.Push(VarIntSigned.ToInt(reader));
                        break;
                    case OpCode.GetLocal:
                        _stack.Push(locals[VarIntUnsigned.ToUInt(reader)]);
                        break;
                    case OpCode.I32Add:
                        _stack.Push(_stack.Pop() + _stack.Pop());
                        break;
                    case OpCode.Call:
                        Call(VarIntUnsigned.ToUInt(reader));
                        break;
                    case OpCode.CallIndirect:
                        CallIndirect(VarIntUnsigned.ToUInt(reader), VarIntUnsigned.ToBool(reader));
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
            switch (func)
            {
                case Function bodyFunc:
                    using (var reader = ParseTools.FromBytes(bodyFunc.Body))
                    {
                        Run(reader, param);
                    }

                    break;
                case ImportFunction importFunc:
                    var output = _imports.Call(_instance,importFunc, param);
                    if (importFunc.Return == ValueType.I32)
                    {
                        _stack.Push(output);
                    }

                    break;
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
