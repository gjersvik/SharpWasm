﻿
using System;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using SharpWasm.Core.Code;

namespace SharpWasm.Core.Parser
{
    internal static class CodeParser
    {
        public static BrTable ToBrTable(BinaryReader reader)
        {
            var targetTable = Values.ToVector(reader, Values.ToUInt);
            var defaultTarget = Values.ToUInt(reader);
            return new BrTable(targetTable, defaultTarget);
        }

        public static CallIndirect ToCallIndirect(BinaryReader reader)
        {
            var typeIndex = Values.ToUInt(reader);
            var reserved = Values.ToBool(reader);
            return new CallIndirect(typeIndex,reserved);
        }

        public static MemoryImmediate ToMemoryImmediate(BinaryReader reader)
        {
            var flags = Values.ToUInt(reader);
            var offset = Values.ToUInt(reader);
            return new MemoryImmediate(flags, offset);
        }

        public static ImmutableArray<IInstruction> ToInitExpr(BinaryReader reader)
        {
            var builder = ImmutableArray.CreateBuilder<IInstruction>(2);
            do
            {
                builder.Add(ToInstruction(reader));
            } while (builder.Last().OpCode != OpCode.End);

            builder.Capacity = builder.Count;

            return builder.MoveToImmutable();
        }

        public static IInstruction ToInstruction(BinaryReader reader)
        {
            var opCode = (OpCode)reader.ReadByte();
            switch (opCode)
            {
                case OpCode.Unreachable:
                    return Instruction.Unreachable;
                case OpCode.Nop:
                    return Instruction.Nop;
                case OpCode.Block:
                    return Instruction.Block(TypeParser.ToBlockType(reader));
                case OpCode.Loop:
                    return Instruction.Loop(TypeParser.ToBlockType(reader));
                case OpCode.If:
                    return Instruction.If(TypeParser.ToBlockType(reader));
                case OpCode.Else:
                    return Instruction.Else;
                case OpCode.End:
                    return Instruction.End;
                case OpCode.Br:
                    return Instruction.Br(Values.ToUInt(reader));
                case OpCode.BrIf:
                    return Instruction.BrIf(Values.ToUInt(reader));
                case OpCode.BrTable:
                    return Instruction.BrTable(ToBrTable(reader));
                case OpCode.Return:
                    return Instruction.Return;
                case OpCode.Call:
                    return Instruction.Call(Values.ToUInt(reader));
                case OpCode.CallIndirect:
                    return Instruction.CallIndirect(ToCallIndirect(reader));
                case OpCode.Drop:
                    return Instruction.Drop;
                case OpCode.Select:
                    return Instruction.Select;
                case OpCode.GetLocal:
                    return Instruction.GetLocal(Values.ToUInt(reader));
                case OpCode.SetLocal:
                    return Instruction.SetLocal(Values.ToUInt(reader));
                case OpCode.TeeLocal:
                    return Instruction.TeeLocal(Values.ToUInt(reader));
                case OpCode.GetGlobal:
                    return Instruction.GetGlobal(Values.ToUInt(reader));
                case OpCode.SetGlobal:
                    return Instruction.SetGlobal(Values.ToUInt(reader));
                case OpCode.I32Load:
                    return Instruction.I32Load(ToMemoryImmediate(reader));
                case OpCode.I64Load:
                    return Instruction.I64Load(ToMemoryImmediate(reader));
                case OpCode.F32Load:
                    return Instruction.F32Load(ToMemoryImmediate(reader));
                case OpCode.F64Load:
                    return Instruction.F64Load(ToMemoryImmediate(reader));
                case OpCode.I32Load8S:
                    return Instruction.I32Load8S(ToMemoryImmediate(reader));
                case OpCode.I32Load8U:
                    return Instruction.I32Load8U(ToMemoryImmediate(reader));
                case OpCode.I32Load16S:
                    return Instruction.I32Load16S(ToMemoryImmediate(reader));
                case OpCode.I32Load16U:
                    return Instruction.I32Load16U(ToMemoryImmediate(reader));
                case OpCode.I64Load8S:
                    return Instruction.I64Load8S(ToMemoryImmediate(reader));
                case OpCode.I64Load8U:
                    return Instruction.I64Load8U(ToMemoryImmediate(reader));
                case OpCode.I64Load16S:
                    return Instruction.I64Load16S(ToMemoryImmediate(reader));
                case OpCode.I64Load16U:
                    return Instruction.I64Load16U(ToMemoryImmediate(reader));
                case OpCode.I64Load32S:
                    return Instruction.I64Load32S(ToMemoryImmediate(reader));
                case OpCode.I64Load32U:
                    return Instruction.I64Load32U(ToMemoryImmediate(reader));
                case OpCode.I32Store:
                    return Instruction.I32Store(ToMemoryImmediate(reader));
                case OpCode.I64Store:
                    return Instruction.I64Store(ToMemoryImmediate(reader));
                case OpCode.F32Store:
                    return Instruction.F32Store(ToMemoryImmediate(reader));
                case OpCode.F64Store:
                    return Instruction.F64Store(ToMemoryImmediate(reader));
                case OpCode.I32Store8:
                    return Instruction.I32Store8(ToMemoryImmediate(reader));
                case OpCode.I32Store16:
                    return Instruction.I32Store16(ToMemoryImmediate(reader));
                case OpCode.I64Store8:
                    return Instruction.I64Store8(ToMemoryImmediate(reader));
                case OpCode.I64Store16:
                    return Instruction.I64Store16(ToMemoryImmediate(reader));
                case OpCode.I64Store32:
                    return Instruction.I64Store32(ToMemoryImmediate(reader));
                case OpCode.CurrentMemory:
                    return Instruction.CurrentMemory(Values.ToBool(reader));
                case OpCode.GrowMemory:
                    return Instruction.GrowMemory(Values.ToBool(reader));
                case OpCode.I32Const:
                    return Instruction.I32Const(Values.ToInt(reader));
                case OpCode.I64Const:
                    return Instruction.I64Const(Values.ToLong(reader));
                case OpCode.F32Const:
                    return Instruction.F32Const(reader.ReadSingle());
                case OpCode.F64Const:
                    return Instruction.F64Const(reader.ReadDouble());
                case OpCode.I32Eqz:
                    return Instruction.I32Eqz;
                case OpCode.I32Eq:
                    return Instruction.I32Eq;
                case OpCode.I32Ne:
                    return Instruction.I32Ne;
                case OpCode.I32LtS:
                    return Instruction.I32LtS;
                case OpCode.I32LtU:
                    return Instruction.I32LtU;
                case OpCode.I32GtS:
                    return Instruction.I32GtS;
                case OpCode.I32GtU:
                    return Instruction.I32GtU;
                case OpCode.I32LeS:
                    return Instruction.I32LeS;
                case OpCode.I32LeU:
                    return Instruction.I32LeU;
                case OpCode.I32GeS:
                    return Instruction.I32GeS;
                case OpCode.I32GeU:
                    return Instruction.I32GeU;
                case OpCode.I64Eqz:
                    return Instruction.I64Eqz;
                case OpCode.I64Eq:
                    return Instruction.I64Eq;
                case OpCode.I64Ne:
                    return Instruction.I64Ne;
                case OpCode.I64LtS:
                    return Instruction.I64LtS;
                case OpCode.I64LtU:
                    return Instruction.I64LtU;
                case OpCode.I64GtS:
                    return Instruction.I64GtS;
                case OpCode.I64GtU:
                    return Instruction.I64GtU;
                case OpCode.I64LeS:
                    return Instruction.I64LeS;
                case OpCode.I64LeU:
                    return Instruction.I64LeU;
                case OpCode.I64GeS:
                    return Instruction.I64GeS;
                case OpCode.I64GeU:
                    return Instruction.I64GeU;
                case OpCode.F32Eq:
                    return Instruction.F32Eq;
                case OpCode.F32Ne:
                    return Instruction.F32Ne;
                case OpCode.F32Lt:
                    return Instruction.F32Lt;
                case OpCode.F32Gt:
                    return Instruction.F32Gt;
                case OpCode.F32Le:
                    return Instruction.F32Le;
                case OpCode.F32Ge:
                    return Instruction.F32Ge;
                case OpCode.F64Eq:
                    return Instruction.F64Eq;
                case OpCode.F64Ne:
                    return Instruction.F64Ne;
                case OpCode.F64Lt:
                    return Instruction.F64Lt;
                case OpCode.F64Gt:
                    return Instruction.F64Gt;
                case OpCode.F64Le:
                    return Instruction.F64Le;
                case OpCode.F64Ge:
                    return Instruction.F64Ge;
                case OpCode.I32Clz:
                    return Instruction.I32Clz;
                case OpCode.I32Ctz:
                    return Instruction.I32Ctz;
                case OpCode.I32Popcnt:
                    return Instruction.I32Popcnt;
                case OpCode.I32Add:
                    return Instruction.I32Add;
                case OpCode.I32Sub:
                    return Instruction.I32Sub;
                case OpCode.I32Mul:
                    return Instruction.I32Mul;
                case OpCode.I32DivS:
                    return Instruction.I32DivS;
                case OpCode.I32DivU:
                    return Instruction.I32DivU;
                case OpCode.I32RemS:
                    return Instruction.I32RemS;
                case OpCode.I32RemU:
                    return Instruction.I32RemU;
                case OpCode.I32And:
                    return Instruction.I32And;
                case OpCode.I32Or:
                    return Instruction.I32Or;
                case OpCode.I32Xor:
                    return Instruction.I32Xor;
                case OpCode.I32Shl:
                    return Instruction.I32Shl;
                case OpCode.I32ShrS:
                    return Instruction.I32ShrS;
                case OpCode.I32ShrU:
                    return Instruction.I32ShrU;
                case OpCode.I32Rotl:
                    return Instruction.I32Rotl;
                case OpCode.I32Rotr:
                    return Instruction.I32Rotr;
                case OpCode.I64Clz:
                    return Instruction.I64Clz;
                case OpCode.I64Ctz:
                    return Instruction.I64Ctz;
                case OpCode.I64Popcnt:
                    return Instruction.I64Popcnt;
                case OpCode.I64Add:
                    return Instruction.I64Add;
                case OpCode.I64Sub:
                    return Instruction.I64Sub;
                case OpCode.I64Mul:
                    return Instruction.I64Mul;
                case OpCode.I64DivS:
                    return Instruction.I64DivS;
                case OpCode.I64DivU:
                    return Instruction.I64DivU;
                case OpCode.I64RemS:
                    return Instruction.I64RemS;
                case OpCode.I64RemU:
                    return Instruction.I64RemU;
                case OpCode.I64And:
                    return Instruction.I64And;
                case OpCode.I64Or:
                    return Instruction.I64Or;
                case OpCode.I64Xor:
                    return Instruction.I64Xor;
                case OpCode.I64Shl:
                    return Instruction.I64Shl;
                case OpCode.I64ShrS:
                    return Instruction.I64ShrS;
                case OpCode.I64ShrU:
                    return Instruction.I64ShrU;
                case OpCode.I64Rotl:
                    return Instruction.I64Rotl;
                case OpCode.I64Rotr:
                    return Instruction.I64Rotr;
                case OpCode.F32Abs:
                    return Instruction.F32Abs;
                case OpCode.F32Neg:
                    return Instruction.F32Neg;
                case OpCode.F32Ceil:
                    return Instruction.F32Ceil;
                case OpCode.F32Floor:
                    return Instruction.F32Floor;
                case OpCode.F32Trunc:
                    return Instruction.F32Trunc;
                case OpCode.F32Nearest:
                    return Instruction.F32Nearest;
                case OpCode.F32Sqrt:
                    return Instruction.F32Sqrt;
                case OpCode.F32Add:
                    return Instruction.F32Add;
                case OpCode.F32Sub:
                    return Instruction.F32Sub;
                case OpCode.F32Mul:
                    return Instruction.F32Mul;
                case OpCode.F32Div:
                    return Instruction.F32Div;
                case OpCode.F32Min:
                    return Instruction.F32Min;
                case OpCode.F32Max:
                    return Instruction.F32Max;
                case OpCode.F32Copysign:
                    return Instruction.F32Copysign;
                case OpCode.F64Abs:
                    return Instruction.F64Abs;
                case OpCode.F64Neg:
                    return Instruction.F64Neg;
                case OpCode.F64Ceil:
                    return Instruction.F64Ceil;
                case OpCode.F64Floor:
                    return Instruction.F64Floor;
                case OpCode.F64Trunc:
                    return Instruction.F64Trunc;
                case OpCode.F64Nearest:
                    return Instruction.F64Nearest;
                case OpCode.F64Sqrt:
                    return Instruction.F64Sqrt;
                case OpCode.F64Add:
                    return Instruction.F64Add;
                case OpCode.F64Sub:
                    return Instruction.F64Sub;
                case OpCode.F64Mul:
                    return Instruction.F64Mul;
                case OpCode.F64Div:
                    return Instruction.F64Div;
                case OpCode.F64Min:
                    return Instruction.F64Min;
                case OpCode.F64Max:
                    return Instruction.F64Max;
                case OpCode.F64Copysign:
                    return Instruction.F64Copysign;
                case OpCode.I32WrapI64:
                    return Instruction.I32WrapI64;
                case OpCode.I32TruncSf32:
                    return Instruction.I32TruncSf32;
                case OpCode.I32TruncUf32:
                    return Instruction.I32TruncUf32;
                case OpCode.I32TruncSf64:
                    return Instruction.I32TruncSf64;
                case OpCode.I32TruncUf64:
                    return Instruction.I32TruncUf64;
                case OpCode.I64ExtendSi32:
                    return Instruction.I64ExtendSi32;
                case OpCode.I64ExtendUi32:
                    return Instruction.I64ExtendUi32;
                case OpCode.I64TruncSf32:
                    return Instruction.I64TruncSf32;
                case OpCode.I64TruncUf32:
                    return Instruction.I64TruncUf32;
                case OpCode.I64TruncSf64:
                    return Instruction.I64TruncSf64;
                case OpCode.I64TruncUf64:
                    return Instruction.I64TruncUf64;
                case OpCode.F32ConvertSi32:
                    return Instruction.F32ConvertSi32;
                case OpCode.F32ConvertUi32:
                    return Instruction.F32ConvertUi32;
                case OpCode.F32ConvertSi64:
                    return Instruction.F32ConvertSi64;
                case OpCode.F32ConvertUi64:
                    return Instruction.F32ConvertUi64;
                case OpCode.F32DemoteF64:
                    return Instruction.F32DemoteF64;
                case OpCode.F64ConvertSi32:
                    return Instruction.F64ConvertSi32;
                case OpCode.F64ConvertUi32:
                    return Instruction.F64ConvertUi32;
                case OpCode.F64ConvertSi64:
                    return Instruction.F64ConvertSi64;
                case OpCode.F64ConvertUi64:
                    return Instruction.F64ConvertUi64;
                case OpCode.F64PromoteF32:
                    return Instruction.F64PromoteF32;
                case OpCode.I32ReinterpretF32:
                    return Instruction.I32ReinterpretF32;
                case OpCode.I64ReinterpretF64:
                    return Instruction.I64ReinterpretF64;
                case OpCode.F32ReinterpretI32:
                    return Instruction.F32ReinterpretI32;
                case OpCode.F64ReinterpretI64:
                    return Instruction.F64ReinterpretI64;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
