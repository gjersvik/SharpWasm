using System;
using System.Collections.Generic;
using SharpWasm.Core.Types;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable IdentifierTypo

namespace SharpWasm.Core.Code
{
    internal interface IInstruction
    {
        OpCode OpCode { get; }
        bool HaveImmediate { get; }
    }

    internal class Instruction : IInstruction, IEquatable<Instruction>
    {
        //Control flow operators
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

        //Call operators
        public static Instruction<uint> Call(uint value) => new Instruction<uint>(OpCode.Call, value);

        public static Instruction<CallIndirect> CallIndirect(CallIndirect value) =>
            new Instruction<CallIndirect>(OpCode.CallIndirect, value);

        //Parametric operators
        public static readonly Instruction Drop = new Instruction(OpCode.Drop);
        public static readonly Instruction Select = new Instruction(OpCode.Select);

        //Variable access
        public static Instruction<uint> GetLocal(uint value) => new Instruction<uint>(OpCode.GetLocal, value);
        public static Instruction<uint> SetLocal(uint value) => new Instruction<uint>(OpCode.SetLocal, value);
        public static Instruction<uint> TeeLocal(uint value) => new Instruction<uint>(OpCode.TeeLocal, value);
        public static Instruction<uint> GetGlobal(uint value) => new Instruction<uint>(OpCode.GetGlobal, value);
        public static Instruction<uint> SetGlobal(uint value) => new Instruction<uint>(OpCode.SetGlobal, value);

        //Memory-related operators
        public static Instruction<MemoryImmediate> I32Load(MemoryImmediate value) =>
            new Instruction<MemoryImmediate>(OpCode.I32Load, value);

        public static Instruction<MemoryImmediate> I64Load(MemoryImmediate value) =>
            new Instruction<MemoryImmediate>(OpCode.I64Load, value);

        public static Instruction<MemoryImmediate> F32Load(MemoryImmediate value) =>
            new Instruction<MemoryImmediate>(OpCode.F32Load, value);

        public static Instruction<MemoryImmediate> F64Load(MemoryImmediate value) =>
            new Instruction<MemoryImmediate>(OpCode.F64Load, value);

        public static Instruction<MemoryImmediate> I32Load8S(MemoryImmediate value) =>
            new Instruction<MemoryImmediate>(OpCode.I32Load8S, value);

        public static Instruction<MemoryImmediate> I32Load8U(MemoryImmediate value) =>
            new Instruction<MemoryImmediate>(OpCode.I32Load8U, value);

        public static Instruction<MemoryImmediate> I32Load16S(MemoryImmediate value) =>
            new Instruction<MemoryImmediate>(OpCode.I32Load16S, value);

        public static Instruction<MemoryImmediate> I32Load16U(MemoryImmediate value) =>
            new Instruction<MemoryImmediate>(OpCode.I32Load16U, value);

        public static Instruction<MemoryImmediate> I64Load8S(MemoryImmediate value) =>
            new Instruction<MemoryImmediate>(OpCode.I64Load8S, value);

        public static Instruction<MemoryImmediate> I64Load8U(MemoryImmediate value) =>
            new Instruction<MemoryImmediate>(OpCode.I64Load8U, value);

        public static Instruction<MemoryImmediate> I64Load16S(MemoryImmediate value) =>
            new Instruction<MemoryImmediate>(OpCode.I64Load16S, value);

        public static Instruction<MemoryImmediate> I64Load16U(MemoryImmediate value) =>
            new Instruction<MemoryImmediate>(OpCode.I64Load16U, value);

        public static Instruction<MemoryImmediate> I64Load32S(MemoryImmediate value) =>
            new Instruction<MemoryImmediate>(OpCode.I64Load32S, value);

        public static Instruction<MemoryImmediate> I64Load32U(MemoryImmediate value) =>
            new Instruction<MemoryImmediate>(OpCode.I64Load32U, value);

        public static Instruction<MemoryImmediate> I32Store(MemoryImmediate value) =>
            new Instruction<MemoryImmediate>(OpCode.I32Store, value);

        public static Instruction<MemoryImmediate> I64Store(MemoryImmediate value) =>
            new Instruction<MemoryImmediate>(OpCode.I64Store, value);

        public static Instruction<MemoryImmediate> F32Store(MemoryImmediate value) =>
            new Instruction<MemoryImmediate>(OpCode.F32Store, value);

        public static Instruction<MemoryImmediate> F64Store(MemoryImmediate value) =>
            new Instruction<MemoryImmediate>(OpCode.F64Store, value);

        public static Instruction<MemoryImmediate> I32Store8(MemoryImmediate value) =>
            new Instruction<MemoryImmediate>(OpCode.I32Store8, value);

        public static Instruction<MemoryImmediate> I32Store16(MemoryImmediate value) =>
            new Instruction<MemoryImmediate>(OpCode.I32Store16, value);

        public static Instruction<MemoryImmediate> I64Store8(MemoryImmediate value) =>
            new Instruction<MemoryImmediate>(OpCode.I64Store8, value);

        public static Instruction<MemoryImmediate> I64Store16(MemoryImmediate value) =>
            new Instruction<MemoryImmediate>(OpCode.I64Store16, value);

        public static Instruction<MemoryImmediate> I64Store32(MemoryImmediate value) =>
            new Instruction<MemoryImmediate>(OpCode.I64Store32, value);

        public static Instruction<bool> CurrentMemory(bool value) => new Instruction<bool>(OpCode.CurrentMemory, value);
        public static Instruction<bool> GrowMemory(bool value) => new Instruction<bool>(OpCode.GrowMemory, value);

        //Constants
        public static Instruction<int> I32Const(int value) => new Instruction<int>(OpCode.I32Const, value);
        public static Instruction<long> I64Const(long value) => new Instruction<long>(OpCode.I64Const, value);
        public static Instruction<float> F32Const(float value) => new Instruction<float>(OpCode.F32Const, value);
        public static Instruction<double> F64Const(double value) => new Instruction<double>(OpCode.F64Const, value);

        //Comparison operators
        public static readonly Instruction I32Eqz = new Instruction(OpCode.I32Eqz);
        public static readonly Instruction I32Eq = new Instruction(OpCode.I32Eq);
        public static readonly Instruction I32Ne = new Instruction(OpCode.I32Ne);
        public static readonly Instruction I32LtS = new Instruction(OpCode.I32LtS);
        public static readonly Instruction I32LtU = new Instruction(OpCode.I32LtU);
        public static readonly Instruction I32GtS = new Instruction(OpCode.I32GtS);
        public static readonly Instruction I32GtU = new Instruction(OpCode.I32GtU);
        public static readonly Instruction I32LeS = new Instruction(OpCode.I32LeS);
        public static readonly Instruction I32LeU = new Instruction(OpCode.I32LeU);
        public static readonly Instruction I32GeS = new Instruction(OpCode.I32GeS);
        public static readonly Instruction I32GeU = new Instruction(OpCode.I32GeU);
        public static readonly Instruction I64Eqz = new Instruction(OpCode.I64Eqz);
        public static readonly Instruction I64Eq = new Instruction(OpCode.I64Eq);
        public static readonly Instruction I64Ne = new Instruction(OpCode.I64Ne);
        public static readonly Instruction I64LtS = new Instruction(OpCode.I64LtS);
        public static readonly Instruction I64LtU = new Instruction(OpCode.I64LtU);
        public static readonly Instruction I64GtS = new Instruction(OpCode.I64GtS);
        public static readonly Instruction I64GtU = new Instruction(OpCode.I64GtU);
        public static readonly Instruction I64LeS = new Instruction(OpCode.I64LeS);
        public static readonly Instruction I64LeU = new Instruction(OpCode.I64LeU);
        public static readonly Instruction I64GeS = new Instruction(OpCode.I64GeS);
        public static readonly Instruction I64GeU = new Instruction(OpCode.I64GeU);
        public static readonly Instruction F32Eq = new Instruction(OpCode.F32Eq);
        public static readonly Instruction F32Ne = new Instruction(OpCode.F32Ne);
        public static readonly Instruction F32Lt = new Instruction(OpCode.F32Lt);
        public static readonly Instruction F32Gt = new Instruction(OpCode.F32Gt);
        public static readonly Instruction F32Le = new Instruction(OpCode.F32Le);
        public static readonly Instruction F32Ge = new Instruction(OpCode.F32Ge);
        public static readonly Instruction F64Eq = new Instruction(OpCode.F64Eq);
        public static readonly Instruction F64Ne = new Instruction(OpCode.F64Ne);
        public static readonly Instruction F64Lt = new Instruction(OpCode.F64Lt);
        public static readonly Instruction F64Gt = new Instruction(OpCode.F64Gt);
        public static readonly Instruction F64Le = new Instruction(OpCode.F64Le);
        public static readonly Instruction F64Ge = new Instruction(OpCode.F64Ge);

        //Numeric operators

        public static readonly Instruction I32Clz = new Instruction(OpCode.I32Clz);
        public static readonly Instruction I32Ctz = new Instruction(OpCode.I32Ctz);
        public static readonly Instruction I32Popcnt = new Instruction(OpCode.I32Popcnt);
        public static readonly Instruction I32Add = new Instruction(OpCode.I32Add);
        public static readonly Instruction I32Sub = new Instruction(OpCode.I32Sub);
        public static readonly Instruction I32Mul = new Instruction(OpCode.I32Mul);
        public static readonly Instruction I32DivS = new Instruction(OpCode.I32DivS);
        public static readonly Instruction I32DivU = new Instruction(OpCode.I32DivU);
        public static readonly Instruction I32RemS = new Instruction(OpCode.I32RemS);
        public static readonly Instruction I32RemU = new Instruction(OpCode.I32RemU);
        public static readonly Instruction I32And = new Instruction(OpCode.I32And);
        public static readonly Instruction I32Or = new Instruction(OpCode.I32Or);
        public static readonly Instruction I32Xor = new Instruction(OpCode.I32Xor);
        public static readonly Instruction I32Shl = new Instruction(OpCode.I32Shl);
        public static readonly Instruction I32ShrS = new Instruction(OpCode.I32ShrS);
        public static readonly Instruction I32ShrU = new Instruction(OpCode.I32ShrU);
        public static readonly Instruction I32Rotl = new Instruction(OpCode.I32Rotl);
        public static readonly Instruction I32Rotr = new Instruction(OpCode.I32Rotr);
        public static readonly Instruction I64Clz = new Instruction(OpCode.I64Clz);
        public static readonly Instruction I64Ctz = new Instruction(OpCode.I64Ctz);
        public static readonly Instruction I64Popcnt = new Instruction(OpCode.I64Popcnt);
        public static readonly Instruction I64Add = new Instruction(OpCode.I64Add);
        public static readonly Instruction I64Sub = new Instruction(OpCode.I64Sub);
        public static readonly Instruction I64Mul = new Instruction(OpCode.I64Mul);
        public static readonly Instruction I64DivS = new Instruction(OpCode.I64DivS);
        public static readonly Instruction I64DivU = new Instruction(OpCode.I64DivU);
        public static readonly Instruction I64RemS = new Instruction(OpCode.I64RemS);
        public static readonly Instruction I64RemU = new Instruction(OpCode.I64RemU);
        public static readonly Instruction I64And = new Instruction(OpCode.I64And);
        public static readonly Instruction I64Or = new Instruction(OpCode.I64Or);
        public static readonly Instruction I64Xor = new Instruction(OpCode.I64Xor);
        public static readonly Instruction I64Shl = new Instruction(OpCode.I64Shl);
        public static readonly Instruction I64ShrS = new Instruction(OpCode.I64ShrS);
        public static readonly Instruction I64ShrU = new Instruction(OpCode.I64ShrU);
        public static readonly Instruction I64Rotl = new Instruction(OpCode.I64Rotl);
        public static readonly Instruction I64Rotr = new Instruction(OpCode.I64Rotr);
        public static readonly Instruction F32Abs = new Instruction(OpCode.F32Abs);
        public static readonly Instruction F32Neg = new Instruction(OpCode.F32Neg);
        public static readonly Instruction F32Ceil = new Instruction(OpCode.F32Ceil);
        public static readonly Instruction F32Floor = new Instruction(OpCode.F32Floor);
        public static readonly Instruction F32Trunc = new Instruction(OpCode.F32Trunc);
        public static readonly Instruction F32Nearest = new Instruction(OpCode.F32Nearest);
        public static readonly Instruction F32Sqrt = new Instruction(OpCode.F32Sqrt);
        public static readonly Instruction F32Add = new Instruction(OpCode.F32Add);
        public static readonly Instruction F32Sub = new Instruction(OpCode.F32Sub);
        public static readonly Instruction F32Mul = new Instruction(OpCode.F32Mul);
        public static readonly Instruction F32Div = new Instruction(OpCode.F32Div);
        public static readonly Instruction F32Min = new Instruction(OpCode.F32Min);
        public static readonly Instruction F32Max = new Instruction(OpCode.F32Max);
        public static readonly Instruction F32Copysign = new Instruction(OpCode.F32Copysign);
        public static readonly Instruction F64Abs = new Instruction(OpCode.F64Abs);
        public static readonly Instruction F64Neg = new Instruction(OpCode.F64Neg);
        public static readonly Instruction F64Ceil = new Instruction(OpCode.F64Ceil);
        public static readonly Instruction F64Floor = new Instruction(OpCode.F64Floor);
        public static readonly Instruction F64Trunc = new Instruction(OpCode.F64Trunc);
        public static readonly Instruction F64Nearest = new Instruction(OpCode.F64Nearest);
        public static readonly Instruction F64Sqrt = new Instruction(OpCode.F64Sqrt);
        public static readonly Instruction F64Add = new Instruction(OpCode.F64Add);
        public static readonly Instruction F64Sub = new Instruction(OpCode.F64Sub);
        public static readonly Instruction F64Mul = new Instruction(OpCode.F64Mul);
        public static readonly Instruction F64Div = new Instruction(OpCode.F64Div);
        public static readonly Instruction F64Min = new Instruction(OpCode.F64Min);
        public static readonly Instruction F64Max = new Instruction(OpCode.F64Max);
        public static readonly Instruction F64Copysign = new Instruction(OpCode.F64Copysign);

        // Conversions
        public static readonly Instruction I32WrapI64 = new Instruction(OpCode.I32WrapI64);
        public static readonly Instruction I32TruncSf32 = new Instruction(OpCode.I32TruncSf32);
        public static readonly Instruction I32TruncUf32 = new Instruction(OpCode.I32TruncUf32);
        public static readonly Instruction I32TruncSf64 = new Instruction(OpCode.I32TruncSf64);
        public static readonly Instruction I32TruncUf64 = new Instruction(OpCode.I32TruncUf64);
        public static readonly Instruction I64ExtendSi32 = new Instruction(OpCode.I64ExtendSi32);
        public static readonly Instruction I64ExtendUi32 = new Instruction(OpCode.I64ExtendUi32);
        public static readonly Instruction I64TruncSf32 = new Instruction(OpCode.I64TruncSf32);
        public static readonly Instruction I64TruncUf32 = new Instruction(OpCode.I64TruncUf32);
        public static readonly Instruction I64TruncSf64 = new Instruction(OpCode.I64TruncSf64);
        public static readonly Instruction I64TruncUf64 = new Instruction(OpCode.I64TruncUf64);
        public static readonly Instruction F32ConvertSi32 = new Instruction(OpCode.F32ConvertSi32);
        public static readonly Instruction F32ConvertUi32 = new Instruction(OpCode.F32ConvertUi32);
        public static readonly Instruction F32ConvertSi64 = new Instruction(OpCode.F32ConvertSi64);
        public static readonly Instruction F32ConvertUi64 = new Instruction(OpCode.F32ConvertUi64);
        public static readonly Instruction F32DemoteF64 = new Instruction(OpCode.F32DemoteF64);
        public static readonly Instruction F64ConvertSi32 = new Instruction(OpCode.F64ConvertSi32);
        public static readonly Instruction F64ConvertUi32 = new Instruction(OpCode.F64ConvertUi32);
        public static readonly Instruction F64ConvertSi64 = new Instruction(OpCode.F64ConvertSi64);
        public static readonly Instruction F64ConvertUi64 = new Instruction(OpCode.F64ConvertUi64);
        public static readonly Instruction F64PromoteF32 = new Instruction(OpCode.F64PromoteF32);

        //Reinterpretations
        public static readonly Instruction I32ReinterpretF32 = new Instruction(OpCode.I32ReinterpretF32);
        public static readonly Instruction I64ReinterpretF64 = new Instruction(OpCode.I64ReinterpretF64);
        public static readonly Instruction F32ReinterpretI32 = new Instruction(OpCode.F32ReinterpretI32);
        public static readonly Instruction F64ReinterpretI64 = new Instruction(OpCode.F64ReinterpretI64);

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

    internal class Instruction<T> : IInstruction, IEquatable<Instruction<T>>
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