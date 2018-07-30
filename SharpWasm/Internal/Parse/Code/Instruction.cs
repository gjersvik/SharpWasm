using System;
using System.Collections.Generic;
using System.IO;
using SharpWasm.Internal.Parse.Types;

namespace SharpWasm.Internal.Parse.Code
{
    internal interface IInstruction
    {
        OpCode OpCode { get; }
        bool HaveImmediate { get; }
    }

    internal class Instruction: IInstruction, IEquatable<Instruction>
    {
        public static readonly Instruction Unreachable = new Instruction(OpCode.Unreachable);
        public static readonly Instruction Nop = new Instruction(OpCode.Nop);
        public static Instruction<BlockType> Block(BlockType value) => new Instruction<BlockType>(OpCode.Block, value);
        public static Instruction<BlockType> Loop(BlockType value) => new Instruction<BlockType>(OpCode.Loop, value);
        public static Instruction<BlockType> If(BlockType value) => new Instruction<BlockType>(OpCode.If, value);
        public static readonly Instruction Else = new Instruction(OpCode.Else);
        public static readonly Instruction End = new Instruction(OpCode.End);
        public static Instruction<uint> Br(uint value) => new Instruction<uint>(OpCode.Br, value);
        public static Instruction<uint> BrIf(uint value) => new Instruction<uint>(OpCode.BrIf, value);
        public static Instruction<BrTable> BrTable(BrTable value) => new Instruction<BrTable>(OpCode.BrTable, value);
        public static readonly Instruction Return = new Instruction(OpCode.Return);

        public static Instruction<uint> GetGlobal(uint value) => new Instruction<uint>(OpCode.GetGlobal, value);
        public static Instruction<int> I32Const(int value) => new Instruction<int>(OpCode.I32Const, value);
        public static Instruction<long> I64Const(long value) => new Instruction<long>(OpCode.I64Const, value);
        public static Instruction<float> F32Const(float value) => new Instruction<float>(OpCode.F32Const, value);
        public static Instruction<double> F64Const(double value) => new Instruction<double>(OpCode.F64Const, value);

        public static IInstruction Parse(BinaryReader reader)
        {
            var opCode = (OpCode)reader.ReadByte();
            switch (opCode)
            {
                case OpCode.Unreachable:
                    return Unreachable;
                case OpCode.Nop:
                    return Nop;
                case OpCode.Block:
                    return Block(VarIntSigned.ToBlockType(reader));
                case OpCode.Loop:
                    return Loop(VarIntSigned.ToBlockType(reader));
                case OpCode.If:
                    return If(VarIntSigned.ToBlockType(reader));
                case OpCode.Else:
                    return Else;
                case OpCode.End:
                    return End;
                case OpCode.Br:
                    return Br(VarIntUnsigned.ToUInt(reader));
                case OpCode.BrIf:
                    return BrIf(VarIntUnsigned.ToUInt(reader));
                case OpCode.BrTable:
                    return BrTable(new BrTable(reader));
                case OpCode.Return:
                    return Return;
                case OpCode.Call:
                    break;
                case OpCode.CallIndirect:
                    break;
                case OpCode.Drop:
                    break;
                case OpCode.Select:
                    break;
                case OpCode.GetLocal:
                    break;
                case OpCode.SetLocal:
                    break;
                case OpCode.TeeLocal:
                    break;
                case OpCode.GetGlobal:
                    return GetGlobal(VarIntUnsigned.ToUInt(reader));
                case OpCode.SetGlobal:
                    break;
                case OpCode.I32Load:
                    break;
                case OpCode.I64Load:
                    break;
                case OpCode.F32Load:
                    break;
                case OpCode.F64Load:
                    break;
                case OpCode.I32Load8S:
                    break;
                case OpCode.I32Load8U:
                    break;
                case OpCode.I32Load16S:
                    break;
                case OpCode.I32Load16U:
                    break;
                case OpCode.I64Load8S:
                    break;
                case OpCode.I64Load8U:
                    break;
                case OpCode.I64Load16S:
                    break;
                case OpCode.I64Load16U:
                    break;
                case OpCode.I64Load32S:
                    break;
                case OpCode.I64Load32U:
                    break;
                case OpCode.I32Store:
                    break;
                case OpCode.I64Store:
                    break;
                case OpCode.F32Store:
                    break;
                case OpCode.F64Store:
                    break;
                case OpCode.I32Store8:
                    break;
                case OpCode.I32Store16:
                    break;
                case OpCode.I64Store8:
                    break;
                case OpCode.I64Store16:
                    break;
                case OpCode.I64Store32:
                    break;
                case OpCode.CurrentMemory:
                    break;
                case OpCode.GrowMemory:
                    break;
                case OpCode.I32Const:
                    return I32Const(VarIntSigned.ToInt(reader));
                case OpCode.I64Const:
                    return I64Const(VarIntSigned.ToLong(reader));
                case OpCode.F32Const:
                    return F32Const(reader.ReadSingle());
                case OpCode.F64Const:
                    return F64Const(reader.ReadDouble());
                case OpCode.I32Eqz:
                    break;
                case OpCode.I32Eq:
                    break;
                case OpCode.I32Ne:
                    break;
                case OpCode.I32LtS:
                    break;
                case OpCode.I32LtU:
                    break;
                case OpCode.I32GtS:
                    break;
                case OpCode.I32GtU:
                    break;
                case OpCode.I32LeS:
                    break;
                case OpCode.I32LeU:
                    break;
                case OpCode.I32GeS:
                    break;
                case OpCode.I32GeU:
                    break;
                case OpCode.I64Eqz:
                    break;
                case OpCode.I64Eq:
                    break;
                case OpCode.I64Ne:
                    break;
                case OpCode.I64LtS:
                    break;
                case OpCode.I64LtU:
                    break;
                case OpCode.I64GtS:
                    break;
                case OpCode.I64GtU:
                    break;
                case OpCode.I64LeS:
                    break;
                case OpCode.I64LeU:
                    break;
                case OpCode.I64GeS:
                    break;
                case OpCode.I64GeU:
                    break;
                case OpCode.F32Eq:
                    break;
                case OpCode.F32Ne:
                    break;
                case OpCode.F32Lt:
                    break;
                case OpCode.F32Gt:
                    break;
                case OpCode.F32Le:
                    break;
                case OpCode.F32Ge:
                    break;
                case OpCode.F64Eq:
                    break;
                case OpCode.F64Ne:
                    break;
                case OpCode.F64Lt:
                    break;
                case OpCode.F64Gt:
                    break;
                case OpCode.F64Le:
                    break;
                case OpCode.F64Ge:
                    break;
                case OpCode.I32Clz:
                    break;
                case OpCode.I32Ctz:
                    break;
                case OpCode.I32Popcnt:
                    break;
                case OpCode.I32Add:
                    break;
                case OpCode.I32Sub:
                    break;
                case OpCode.I32Mul:
                    break;
                case OpCode.I32DivS:
                    break;
                case OpCode.I32DivU:
                    break;
                case OpCode.I32RemS:
                    break;
                case OpCode.I32RemU:
                    break;
                case OpCode.I32And:
                    break;
                case OpCode.I32Or:
                    break;
                case OpCode.I32Xor:
                    break;
                case OpCode.I32Shl:
                    break;
                case OpCode.I32ShrS:
                    break;
                case OpCode.I32ShrU:
                    break;
                case OpCode.I32Rotl:
                    break;
                case OpCode.I32Rotr:
                    break;
                case OpCode.I64Clz:
                    break;
                case OpCode.I64Ctz:
                    break;
                case OpCode.I64Popcnt:
                    break;
                case OpCode.I64Add:
                    break;
                case OpCode.I64Sub:
                    break;
                case OpCode.I64Mul:
                    break;
                case OpCode.I64DivS:
                    break;
                case OpCode.I64DivU:
                    break;
                case OpCode.I64RemS:
                    break;
                case OpCode.I64RemU:
                    break;
                case OpCode.I64And:
                    break;
                case OpCode.I64Or:
                    break;
                case OpCode.I64Xor:
                    break;
                case OpCode.I64Shl:
                    break;
                case OpCode.I64ShrS:
                    break;
                case OpCode.I64ShrU:
                    break;
                case OpCode.I64Rotl:
                    break;
                case OpCode.I64Rotr:
                    break;
                case OpCode.F32Abs:
                    break;
                case OpCode.F32Neg:
                    break;
                case OpCode.F32Ceil:
                    break;
                case OpCode.F32Floor:
                    break;
                case OpCode.F32Trunc:
                    break;
                case OpCode.F32Nearest:
                    break;
                case OpCode.F32Sqrt:
                    break;
                case OpCode.F32Add:
                    break;
                case OpCode.F32Sub:
                    break;
                case OpCode.F32Mul:
                    break;
                case OpCode.F32Div:
                    break;
                case OpCode.F32Min:
                    break;
                case OpCode.F32Max:
                    break;
                case OpCode.F32Copysign:
                    break;
                case OpCode.F64Abs:
                    break;
                case OpCode.F64Neg:
                    break;
                case OpCode.F64Ceil:
                    break;
                case OpCode.F64Floor:
                    break;
                case OpCode.F64Trunc:
                    break;
                case OpCode.F64Nearest:
                    break;
                case OpCode.F64Sqrt:
                    break;
                case OpCode.F64Add:
                    break;
                case OpCode.F64Sub:
                    break;
                case OpCode.F64Mul:
                    break;
                case OpCode.F64Div:
                    break;
                case OpCode.F64Min:
                    break;
                case OpCode.F64Max:
                    break;
                case OpCode.F64Copysign:
                    break;
                case OpCode.I32WrapI64:
                    break;
                case OpCode.I32TruncSF32:
                    break;
                case OpCode.I32TruncUF32:
                    break;
                case OpCode.I32TruncSF64:
                    break;
                case OpCode.I32TruncUF64:
                    break;
                case OpCode.I64ExtendSI32:
                    break;
                case OpCode.I64ExtendUI32:
                    break;
                case OpCode.I64TruncSF32:
                    break;
                case OpCode.I64TruncUF32:
                    break;
                case OpCode.I64TruncSF64:
                    break;
                case OpCode.I64TruncUF64:
                    break;
                case OpCode.F32ConvertSI32:
                    break;
                case OpCode.F32ConvertUI32:
                    break;
                case OpCode.F32ConvertSI64:
                    break;
                case OpCode.F32ConvertUI64:
                    break;
                case OpCode.F32DemoteF64:
                    break;
                case OpCode.F64ConvertSI32:
                    break;
                case OpCode.F64ConvertUI32:
                    break;
                case OpCode.F64ConvertSI64:
                    break;
                case OpCode.F64ConvertUI64:
                    break;
                case OpCode.F64PromoteF32:
                    break;
                case OpCode.I32ReinterpretF32:
                    break;
                case OpCode.I64ReinterpretF64:
                    break;
                case OpCode.F32ReinterpretI32:
                    break;
                case OpCode.F64ReinterpretI64:
                    break;
            }
            throw new NotImplementedException();
        }


        public Instruction(OpCode opCode)
        {
            OpCode = opCode;
        }

        public OpCode OpCode { get; }
        public bool HaveImmediate { get; } = false;

        public bool Equals(Instruction other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return OpCode == other.OpCode;
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Instruction) obj);
        }

        public override int GetHashCode()
        {
            return (int) OpCode;
        }

        public static bool operator ==(Instruction left, Instruction right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Instruction left, Instruction right)
        {
            return !Equals(left, right);
        }
    }
    internal class Instruction<T>: IInstruction, IEquatable<Instruction<T>>
    {
        public OpCode OpCode { get; }
        public bool HaveImmediate { get; } = true;

        public readonly T Immediate;

        public Instruction(OpCode opCode, T immediate)
        {
            OpCode = opCode;
            Immediate = immediate;
        }

        public bool Equals(Instruction<T> other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return EqualityComparer<T>.Default.Equals(Immediate, other.Immediate) && OpCode == other.OpCode;
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Instruction<T>) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (EqualityComparer<T>.Default.GetHashCode(Immediate) * 397) ^ (int) OpCode;
            }
        }

        public static bool operator ==(Instruction<T> left, Instruction<T> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Instruction<T> left, Instruction<T> right)
        {
            return !Equals(left, right);
        }
    }
}
