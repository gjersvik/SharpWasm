using System;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using SharpWasm.Internal.Parse.Code;

namespace SharpWasm.Internal.Runtime
{
    internal static class VirtualMachine
    {
        [SuppressMessage("ReSharper", "EqualExpressionComparison")]
        [SuppressMessage("ReSharper", "CompareOfFloatsByEqualityOperator")]
        public static void ExecuteCode(ImmutableArray<IInstruction> code, Local local)
        {
            var stack = local.Stack;
            unchecked
            {
                for (var ip = 0; ip < code.Length; ip += 1)
                {
                    var op = code[ip];
                    switch (op.OpCode)
                    {
                        case OpCode.Unreachable:
                            throw new Exception("Unreachable code reached.");
                        case OpCode.Nop:
                            break;
                        case OpCode.Block:
                            throw new NotImplementedException();
                        case OpCode.Loop:
                            throw new NotImplementedException();
                        case OpCode.If:
                            throw new NotImplementedException();
                        case OpCode.Else:
                            throw new NotImplementedException();
                        case OpCode.End:
                            throw new NotImplementedException();
                        case OpCode.Br:
                            throw new NotImplementedException();
                        case OpCode.BrIf:
                            throw new NotImplementedException();
                        case OpCode.BrTable:
                            throw new NotImplementedException();
                        case OpCode.Return:
                            throw new NotImplementedException();
                        case OpCode.Call:
                            throw new NotImplementedException();
                        case OpCode.CallIndirect:
                            throw new NotImplementedException();
                        case OpCode.Drop:
                            local.Stack.Pop();
                            break;
                        case OpCode.Select:
                            var select = (uint)stack.Pop();
                            var val2 = stack.Pop();
                            var val1 = stack.Pop();
                            if (!Equals(val1.Type, val2.Type))
                                throw new WebAssemblyRuntimeException(
                                    $"Select need two values of same type found {val2.Type} and {val1.Type}");
                            stack.Push(select == 0 ? val2 : val1);
                            break;
                        case OpCode.GetLocal:
                            throw new NotImplementedException();
                        case OpCode.SetLocal:
                            throw new NotImplementedException();
                        case OpCode.TeeLocal:
                            throw new NotImplementedException();
                        case OpCode.GetGlobal:
                            throw new NotImplementedException();
                        case OpCode.SetGlobal:
                            throw new NotImplementedException();
                        case OpCode.I32Load:
                            throw new NotImplementedException();
                        case OpCode.I64Load:
                            throw new NotImplementedException();
                        case OpCode.F32Load:
                            throw new NotImplementedException();
                        case OpCode.F64Load:
                            throw new NotImplementedException();
                        case OpCode.I32Load8S:
                            throw new NotImplementedException();
                        case OpCode.I32Load8U:
                            throw new NotImplementedException();
                        case OpCode.I32Load16S:
                            throw new NotImplementedException();
                        case OpCode.I32Load16U:
                            throw new NotImplementedException();
                        case OpCode.I64Load8S:
                            throw new NotImplementedException();
                        case OpCode.I64Load8U:
                            throw new NotImplementedException();
                        case OpCode.I64Load16S:
                            throw new NotImplementedException();
                        case OpCode.I64Load16U:
                            throw new NotImplementedException();
                        case OpCode.I64Load32S:
                            throw new NotImplementedException();
                        case OpCode.I64Load32U:
                            throw new NotImplementedException();
                        case OpCode.I32Store:
                            throw new NotImplementedException();
                        case OpCode.I64Store:
                            throw new NotImplementedException();
                        case OpCode.F32Store:
                            throw new NotImplementedException();
                        case OpCode.F64Store:
                            throw new NotImplementedException();
                        case OpCode.I32Store8:
                            throw new NotImplementedException();
                        case OpCode.I32Store16:
                            throw new NotImplementedException();
                        case OpCode.I64Store8:
                            throw new NotImplementedException();
                        case OpCode.I64Store16:
                            throw new NotImplementedException();
                        case OpCode.I64Store32:
                            throw new NotImplementedException();
                        case OpCode.CurrentMemory:
                            throw new NotImplementedException();
                        case OpCode.GrowMemory:
                            throw new NotImplementedException();
                        case OpCode.I32Const:
                            stack.Push(((Instruction<int>) op).Immediate);
                            break;
                        case OpCode.I64Const:
                            stack.Push(((Instruction<long>) op).Immediate);
                            break;
                        case OpCode.F32Const:
                            stack.Push(((Instruction<float>) op).Immediate);
                            break;
                        case OpCode.F64Const:
                            stack.Push(((Instruction<double>) op).Immediate);
                            break;
                        case OpCode.I32Eqz:
                            stack.Push((uint)stack.Pop() == 0 ? 1 : 0);
                            break;
                        case OpCode.I32Eq:
                            stack.Push((uint)stack.Pop() == (uint)stack.Pop() ? 1 : 0);
                            break;
                        case OpCode.I32Ne:
                            stack.Push((uint)stack.Pop() != (uint)stack.Pop() ? 1 : 0);
                            break;
                        case OpCode.I32LtS:
                            stack.Push((int)stack.Pop() < (int)stack.Pop() ? 1 : 0);
                            break;
                        case OpCode.I32LtU:
                            stack.Push((uint)stack.Pop() < (uint)stack.Pop() ? 1 : 0);
                            break;
                        case OpCode.I32GtS:
                            stack.Push((int)stack.Pop() > (int)stack.Pop() ? 1 : 0);
                            break;
                        case OpCode.I32GtU:
                            stack.Push((uint)stack.Pop() > (uint)stack.Pop() ? 1 : 0);
                            break;
                        case OpCode.I32LeS:
                            stack.Push((int)stack.Pop() <= (int)stack.Pop() ? 1 : 0);
                            break;
                        case OpCode.I32LeU:
                            stack.Push((uint)stack.Pop() <= (uint)stack.Pop() ? 1 : 0);
                            break;
                        case OpCode.I32GeS:
                            stack.Push((int)stack.Pop() >= (int)stack.Pop() ? 1 : 0);
                            break;
                        case OpCode.I32GeU:
                            stack.Push((uint)stack.Pop() >= (uint)stack.Pop() ? 1 : 0);
                            break;
                        case OpCode.I64Eqz:
                            stack.Push((ulong)stack.Pop() == 0 ? 1 : 0);
                            break;
                        case OpCode.I64Eq:
                            stack.Push((ulong)stack.Pop() == (ulong)stack.Pop() ? 1 : 0);
                            break;
                        case OpCode.I64Ne:
                            stack.Push((ulong)stack.Pop() != (ulong)stack.Pop() ? 1 : 0);
                            break;
                        case OpCode.I64LtS:
                            stack.Push((long)stack.Pop() < (long)stack.Pop() ? 1 : 0);
                            break;
                        case OpCode.I64LtU:
                            stack.Push((ulong)stack.Pop() < (ulong)stack.Pop() ? 1 : 0);
                            break;
                        case OpCode.I64GtS:
                            stack.Push((long)stack.Pop() > (long)stack.Pop() ? 1 : 0);
                            break;
                        case OpCode.I64GtU:
                            stack.Push((ulong)stack.Pop() > (ulong)stack.Pop() ? 1 : 0);
                            break;
                        case OpCode.I64LeS:
                            stack.Push((long)stack.Pop() <= (long)stack.Pop() ? 1 : 0);
                            break;
                        case OpCode.I64LeU:
                            stack.Push((ulong)stack.Pop() <= (ulong)stack.Pop() ? 1 : 0);
                            break;
                        case OpCode.I64GeS:
                            stack.Push((long)stack.Pop() >= (long)stack.Pop() ? 1 : 0);
                            break;
                        case OpCode.I64GeU:
                            stack.Push((ulong)stack.Pop() >= (ulong)stack.Pop() ? 1 : 0);
                            break;
                        case OpCode.F32Eq:
                            stack.Push((float)stack.Pop() == (float)stack.Pop() ? 1 : 0);
                            break;
                        case OpCode.F32Ne:
                            stack.Push((float)stack.Pop() != (float)stack.Pop() ? 1 : 0);
                            break;
                        case OpCode.F32Lt:
                            stack.Push((float)stack.Pop() < (float)stack.Pop() ? 1 : 0);
                            break;
                        case OpCode.F32Gt:
                            stack.Push((float)stack.Pop() > (float)stack.Pop() ? 1 : 0);
                            break;
                        case OpCode.F32Le:
                            stack.Push((float)stack.Pop() <= (float)stack.Pop() ? 1 : 0);
                            break;
                        case OpCode.F32Ge:
                            stack.Push((float)stack.Pop() >= (float)stack.Pop() ? 1 : 0);
                            break;
                        case OpCode.F64Eq:
                            stack.Push((double)stack.Pop() == (double)stack.Pop() ? 1 : 0);
                            break;
                        case OpCode.F64Ne:
                            stack.Push((double)stack.Pop() != (double)stack.Pop() ? 1 : 0);
                            break;
                        case OpCode.F64Lt:
                            stack.Push((double)stack.Pop() < (double)stack.Pop() ? 1 : 0);
                            break;
                        case OpCode.F64Gt:
                            stack.Push((double)stack.Pop() > (double)stack.Pop() ? 1 : 0);
                            break;
                        case OpCode.F64Le:
                            stack.Push((double)stack.Pop() <= (double)stack.Pop() ? 1 : 0);
                            break;
                        case OpCode.F64Ge:
                            stack.Push((double)stack.Pop() >= (double)stack.Pop() ? 1 : 0);
                            break;
                        case OpCode.I32Clz:
                            throw new NotImplementedException();
                        case OpCode.I32Ctz:
                            throw new NotImplementedException();
                        case OpCode.I32Popcnt:
                            throw new NotImplementedException();
                        case OpCode.I32Add:
                            stack.Push((uint)stack.Pop() + (uint)stack.Pop());
                            break;
                        case OpCode.I32Sub:
                            stack.Push((uint)stack.Pop() - (uint)stack.Pop());
                            break;
                        case OpCode.I32Mul:
                            stack.Push((uint)stack.Pop() * (uint)stack.Pop());
                            break;
                        case OpCode.I32DivS:
                            stack.Push((int)stack.Pop() / (int)stack.Pop());
                            break;
                        case OpCode.I32DivU:
                            stack.Push((uint)stack.Pop() / (uint)stack.Pop());
                            break;
                        case OpCode.I32RemS:
                            stack.Push((int)stack.Pop() % (int)stack.Pop());
                            break;
                        case OpCode.I32RemU:
                            stack.Push((uint)stack.Pop() % (uint)stack.Pop());
                            break;
                        case OpCode.I32And:
                            throw new NotImplementedException();
                        case OpCode.I32Or:
                            throw new NotImplementedException();
                        case OpCode.I32Xor:
                            throw new NotImplementedException();
                        case OpCode.I32Shl:
                            throw new NotImplementedException();
                        case OpCode.I32ShrS:
                            throw new NotImplementedException();
                        case OpCode.I32ShrU:
                            throw new NotImplementedException();
                        case OpCode.I32Rotl:
                            throw new NotImplementedException();
                        case OpCode.I32Rotr:
                            throw new NotImplementedException();
                        case OpCode.I64Clz:
                            throw new NotImplementedException();
                        case OpCode.I64Ctz:
                            throw new NotImplementedException();
                        case OpCode.I64Popcnt:
                            throw new NotImplementedException();
                        case OpCode.I64Add:
                            throw new NotImplementedException();
                        case OpCode.I64Sub:
                            throw new NotImplementedException();
                        case OpCode.I64Mul:
                            throw new NotImplementedException();
                        case OpCode.I64DivS:
                            throw new NotImplementedException();
                        case OpCode.I64DivU:
                            throw new NotImplementedException();
                        case OpCode.I64RemS:
                            throw new NotImplementedException();
                        case OpCode.I64RemU:
                            throw new NotImplementedException();
                        case OpCode.I64And:
                            throw new NotImplementedException();
                        case OpCode.I64Or:
                            throw new NotImplementedException();
                        case OpCode.I64Xor:
                            throw new NotImplementedException();
                        case OpCode.I64Shl:
                            throw new NotImplementedException();
                        case OpCode.I64ShrS:
                            throw new NotImplementedException();
                        case OpCode.I64ShrU:
                            throw new NotImplementedException();
                        case OpCode.I64Rotl:
                            throw new NotImplementedException();
                        case OpCode.I64Rotr:
                            throw new NotImplementedException();
                        case OpCode.F32Abs:
                            throw new NotImplementedException();
                        case OpCode.F32Neg:
                            throw new NotImplementedException();
                        case OpCode.F32Ceil:
                            throw new NotImplementedException();
                        case OpCode.F32Floor:
                            throw new NotImplementedException();
                        case OpCode.F32Trunc:
                            throw new NotImplementedException();
                        case OpCode.F32Nearest:
                            throw new NotImplementedException();
                        case OpCode.F32Sqrt:
                            throw new NotImplementedException();
                        case OpCode.F32Add:
                            throw new NotImplementedException();
                        case OpCode.F32Sub:
                            throw new NotImplementedException();
                        case OpCode.F32Mul:
                            throw new NotImplementedException();
                        case OpCode.F32Div:
                            throw new NotImplementedException();
                        case OpCode.F32Min:
                            throw new NotImplementedException();
                        case OpCode.F32Max:
                            throw new NotImplementedException();
                        case OpCode.F32Copysign:
                            throw new NotImplementedException();
                        case OpCode.F64Abs:
                            throw new NotImplementedException();
                        case OpCode.F64Neg:
                            throw new NotImplementedException();
                        case OpCode.F64Ceil:
                            throw new NotImplementedException();
                        case OpCode.F64Floor:
                            throw new NotImplementedException();
                        case OpCode.F64Trunc:
                            throw new NotImplementedException();
                        case OpCode.F64Nearest:
                            throw new NotImplementedException();
                        case OpCode.F64Sqrt:
                            throw new NotImplementedException();
                        case OpCode.F64Add:
                            throw new NotImplementedException();
                        case OpCode.F64Sub:
                            throw new NotImplementedException();
                        case OpCode.F64Mul:
                            throw new NotImplementedException();
                        case OpCode.F64Div:
                            throw new NotImplementedException();
                        case OpCode.F64Min:
                            throw new NotImplementedException();
                        case OpCode.F64Max:
                            throw new NotImplementedException();
                        case OpCode.F64Copysign:
                            throw new NotImplementedException();
                        case OpCode.I32WrapI64:
                            throw new NotImplementedException();
                        case OpCode.I32TruncSf32:
                            throw new NotImplementedException();
                        case OpCode.I32TruncUf32:
                            throw new NotImplementedException();
                        case OpCode.I32TruncSf64:
                            throw new NotImplementedException();
                        case OpCode.I32TruncUf64:
                            throw new NotImplementedException();
                        case OpCode.I64ExtendSi32:
                            throw new NotImplementedException();
                        case OpCode.I64ExtendUi32:
                            throw new NotImplementedException();
                        case OpCode.I64TruncSf32:
                            throw new NotImplementedException();
                        case OpCode.I64TruncUf32:
                            throw new NotImplementedException();
                        case OpCode.I64TruncSf64:
                            throw new NotImplementedException();
                        case OpCode.I64TruncUf64:
                            throw new NotImplementedException();
                        case OpCode.F32ConvertSi32:
                            throw new NotImplementedException();
                        case OpCode.F32ConvertUi32:
                            throw new NotImplementedException();
                        case OpCode.F32ConvertSi64:
                            throw new NotImplementedException();
                        case OpCode.F32ConvertUi64:
                            throw new NotImplementedException();
                        case OpCode.F32DemoteF64:
                            throw new NotImplementedException();
                        case OpCode.F64ConvertSi32:
                            throw new NotImplementedException();
                        case OpCode.F64ConvertUi32:
                            throw new NotImplementedException();
                        case OpCode.F64ConvertSi64:
                            throw new NotImplementedException();
                        case OpCode.F64ConvertUi64:
                            throw new NotImplementedException();
                        case OpCode.F64PromoteF32:
                            throw new NotImplementedException();
                        case OpCode.I32ReinterpretF32:
                            throw new NotImplementedException();
                        case OpCode.I64ReinterpretF64:
                            throw new NotImplementedException();
                        case OpCode.F32ReinterpretI32:
                            throw new NotImplementedException();
                        case OpCode.F64ReinterpretI64:
                            throw new NotImplementedException();
                        default:
                            throw new NotImplementedException();
                    }
                }
            }
        }
    }
}