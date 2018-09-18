using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using SharpWasm.Core.Code;
using ValueType = SharpWasm.Core.Types.ValueType;

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

        private int Run(ImmutableArray<IInstruction> code, int[] param)
        {
            RunCode(code, param);
            return _stack.Count == 1 ? _stack.Pop() : 0;
        }

        // ReSharper disable once SuggestBaseTypeForParameter
        [ExcludeFromCodeCoverage]
        private void RunCode(ImmutableArray<IInstruction> code, int[] locals)
        {
            for (var ip = 0; ip < code.Length; ip += 1)
            {
                var op = code[ip];
                // ReSharper disable once SwitchStatementMissingSomeCases
                switch (op.OpCode)
                {
                    case OpCode.End:
                        return;
                    case OpCode.I32Const:
                        _stack.Push(((Instruction<int>)op).Immediate);
                        break;
                    case OpCode.GetLocal:
                        _stack.Push(locals[((Instruction<uint>)op).Immediate]);
                        break;
                    case OpCode.I32Add:
                        _stack.Push(_stack.Pop() + _stack.Pop());
                        break;
                    case OpCode.Call:
                        Call(((Instruction<uint>)op).Immediate);
                        break;
                    case OpCode.CallIndirect:
                        CallIndirect(((Instruction<CallIndirect>)op).Immediate);
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }
        }

        [ExcludeFromCodeCoverage]
        private void Call(uint id)
        {
            var func = _module.GetFunction(id);
            var param = func.Param.Select(t => _stack.Pop()).Reverse().ToArray();
            switch (func)
            {
                case Function bodyFunc:
                    RunCode(bodyFunc.Body, param);
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
        [ExcludeFromCodeCoverage]
        private void CallIndirect(CallIndirect callIndirect)
        {
            var index = _instance.Table.Get(_stack.Pop());
            var func = _module.GetFunction(index);
            if(func.TypeId != callIndirect.TypeIndex) throw new Exception("Wrong type on indirect call");
            Call(index);
        }
    }
}
