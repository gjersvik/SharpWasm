using System;
using System.Collections.Generic;
using System.IO;
using SharpWasm.Core.Code;
using SharpWasm.Core.Parser;
using SharpWasm.Core.Types;
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable IdentifierTypo

namespace SharpWasm.Internal.Parse.Code
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

        public static IInstruction Parse(BinaryReader reader)
        {
            var opCode = (OpCode) reader.ReadByte();
            switch (opCode)
            {
                case OpCode.Unreachable:
                    return Unreachable;
                case OpCode.Nop:
                    return Nop;
                case OpCode.Block:
                    return Block(TypeParser.ToBlockType(reader));
                case OpCode.Loop:
                    return Loop(TypeParser.ToBlockType(reader));
                case OpCode.If:
                    return If(TypeParser.ToBlockType(reader));
                case OpCode.Else:
                    return Else;
                case OpCode.End:
                    return End;
                case OpCode.Br:
                    return Br(Values.ToUInt(reader));
                case OpCode.BrIf:
                    return BrIf(Values.ToUInt(reader));
                case OpCode.BrTable:
                    return BrTable(CodeParser.ToBrTable(reader));
                case OpCode.Return:
                    return Return;
                case OpCode.Call:
                    return Call(Values.ToUInt(reader));
                case OpCode.CallIndirect:
                    return CallIndirect(CodeParser.ToCallIndirect(reader));
                case OpCode.Drop:
                    return Drop;
                case OpCode.Select:
                    return Select;
                case OpCode.GetLocal:
                    return GetLocal(Values.ToUInt(reader));
                case OpCode.SetLocal:
                    return SetLocal(Values.ToUInt(reader));
                case OpCode.TeeLocal:
                    return TeeLocal(Values.ToUInt(reader));
                case OpCode.GetGlobal:
                    return GetGlobal(Values.ToUInt(reader));
                case OpCode.SetGlobal:
                    return SetGlobal(Values.ToUInt(reader));
                case OpCode.I32Load:
                    return I32Load(new MemoryImmediate(reader));
                case OpCode.I64Load:
                    return I64Load(new MemoryImmediate(reader));
                case OpCode.F32Load:
                    return F32Load(new MemoryImmediate(reader));
                case OpCode.F64Load:
                    return F64Load(new MemoryImmediate(reader));
                case OpCode.I32Load8S:
                    return I32Load8S(new MemoryImmediate(reader));
                case OpCode.I32Load8U:
                    return I32Load8U(new MemoryImmediate(reader));
                case OpCode.I32Load16S:
                    return I32Load16S(new MemoryImmediate(reader));
                case OpCode.I32Load16U:
                    return I32Load16U(new MemoryImmediate(reader));
                case OpCode.I64Load8S:
                    return I64Load8S(new MemoryImmediate(reader));
                case OpCode.I64Load8U:
                    return I64Load8U(new MemoryImmediate(reader));
                case OpCode.I64Load16S:
                    return I64Load16S(new MemoryImmediate(reader));
                case OpCode.I64Load16U:
                    return I64Load16U(new MemoryImmediate(reader));
                case OpCode.I64Load32S:
                    return I64Load32S(new MemoryImmediate(reader));
                case OpCode.I64Load32U:
                    return I64Load32U(new MemoryImmediate(reader));
                case OpCode.I32Store:
                    return I32Store(new MemoryImmediate(reader));
                case OpCode.I64Store:
                    return I64Store(new MemoryImmediate(reader));
                case OpCode.F32Store:
                    return F32Store(new MemoryImmediate(reader));
                case OpCode.F64Store:
                    return F64Store(new MemoryImmediate(reader));
                case OpCode.I32Store8:
                    return I32Store8(new MemoryImmediate(reader));
                case OpCode.I32Store16:
                    return I32Store16(new MemoryImmediate(reader));
                case OpCode.I64Store8:
                    return I64Store8(new MemoryImmediate(reader));
                case OpCode.I64Store16:
                    return I64Store16(new MemoryImmediate(reader));
                case OpCode.I64Store32:
                    return I64Store32(new MemoryImmediate(reader));
                case OpCode.CurrentMemory:
                    return CurrentMemory(Values.ToBool(reader));
                case OpCode.GrowMemory:
                    return GrowMemory(Values.ToBool(reader));
                case OpCode.I32Const:
                    return I32Const(Values.ToInt(reader));
                case OpCode.I64Const:
                    return I64Const(Values.ToLong(reader));
                case OpCode.F32Const:
                    return F32Const(reader.ReadSingle());
                case OpCode.F64Const:
                    return F64Const(reader.ReadDouble());
                case OpCode.I32Eqz:
                    return I32Eqz;
                case OpCode.I32Eq:
                    return I32Eq;
                case OpCode.I32Ne:
                    return I32Ne;
                case OpCode.I32LtS:
                    return I32LtS;
                case OpCode.I32LtU:
                    return I32LtU;
                case OpCode.I32GtS:
                    return I32GtS;
                case OpCode.I32GtU:
                    return I32GtU;
                case OpCode.I32LeS:
                    return I32LeS;
                case OpCode.I32LeU:
                    return I32LeU;
                case OpCode.I32GeS:
                    return I32GeS;
                case OpCode.I32GeU:
                    return I32GeU;
                case OpCode.I64Eqz:
                    return I64Eqz;
                case OpCode.I64Eq:
                    return I64Eq;
                case OpCode.I64Ne:
                    return I64Ne;
                case OpCode.I64LtS:
                    return I64LtS;
                case OpCode.I64LtU:
                    return I64LtU;
                case OpCode.I64GtS:
                    return I64GtS;
                case OpCode.I64GtU:
                    return I64GtU;
                case OpCode.I64LeS:
                    return I64LeS;
                case OpCode.I64LeU:
                    return I64LeU;
                case OpCode.I64GeS:
                    return I64GeS;
                case OpCode.I64GeU:
                    return I64GeU;
                case OpCode.F32Eq:
                    return F32Eq;
                case OpCode.F32Ne:
                    return F32Ne;
                case OpCode.F32Lt:
                    return F32Lt;
                case OpCode.F32Gt:
                    return F32Gt;
                case OpCode.F32Le:
                    return F32Le;
                case OpCode.F32Ge:
                    return F32Ge;
                case OpCode.F64Eq:
                    return F64Eq;
                case OpCode.F64Ne:
                    return F64Ne;
                case OpCode.F64Lt:
                    return F64Lt;
                case OpCode.F64Gt:
                    return F64Gt;
                case OpCode.F64Le:
                    return F64Le;
                case OpCode.F64Ge:
                    return F64Ge;
                case OpCode.I32Clz:
                    return I32Clz;
                case OpCode.I32Ctz:
                    return I32Ctz;
                case OpCode.I32Popcnt:
                    return I32Popcnt;
                case OpCode.I32Add:
                    return I32Add;
                case OpCode.I32Sub:
                    return I32Sub;
                case OpCode.I32Mul:
                    return I32Mul;
                case OpCode.I32DivS:
                    return I32DivS;
                case OpCode.I32DivU:
                    return I32DivU;
                case OpCode.I32RemS:
                    return I32RemS;
                case OpCode.I32RemU:
                    return I32RemU;
                case OpCode.I32And:
                    return I32And;
                case OpCode.I32Or:
                    return I32Or;
                case OpCode.I32Xor:
                    return I32Xor;
                case OpCode.I32Shl:
                    return I32Shl;
                case OpCode.I32ShrS:
                    return I32ShrS;
                case OpCode.I32ShrU:
                    return I32ShrU;
                case OpCode.I32Rotl:
                    return I32Rotl;
                case OpCode.I32Rotr:
                    return I32Rotr;
                case OpCode.I64Clz:
                    return I64Clz;
                case OpCode.I64Ctz:
                    return I64Ctz;
                case OpCode.I64Popcnt:
                    return I64Popcnt;
                case OpCode.I64Add:
                    return I64Add;
                case OpCode.I64Sub:
                    return I64Sub;
                case OpCode.I64Mul:
                    return I64Mul;
                case OpCode.I64DivS:
                    return I64DivS;
                case OpCode.I64DivU:
                    return I64DivU;
                case OpCode.I64RemS:
                    return I64RemS;
                case OpCode.I64RemU:
                    return I64RemU;
                case OpCode.I64And:
                    return I64And;
                case OpCode.I64Or:
                    return I64Or;
                case OpCode.I64Xor:
                    return I64Xor;
                case OpCode.I64Shl:
                    return I64Shl;
                case OpCode.I64ShrS:
                    return I64ShrS;
                case OpCode.I64ShrU:
                    return I64ShrU;
                case OpCode.I64Rotl:
                    return I64Rotl;
                case OpCode.I64Rotr:
                    return I64Rotr;
                case OpCode.F32Abs:
                    return F32Abs;
                case OpCode.F32Neg:
                    return F32Neg;
                case OpCode.F32Ceil:
                    return F32Ceil;
                case OpCode.F32Floor:
                    return F32Floor;
                case OpCode.F32Trunc:
                    return F32Trunc;
                case OpCode.F32Nearest:
                    return F32Nearest;
                case OpCode.F32Sqrt:
                    return F32Sqrt;
                case OpCode.F32Add:
                    return F32Add;
                case OpCode.F32Sub:
                    return F32Sub;
                case OpCode.F32Mul:
                    return F32Mul;
                case OpCode.F32Div:
                    return F32Div;
                case OpCode.F32Min:
                    return F32Min;
                case OpCode.F32Max:
                    return F32Max;
                case OpCode.F32Copysign:
                    return F32Copysign;
                case OpCode.F64Abs:
                    return F64Abs;
                case OpCode.F64Neg:
                    return F64Neg;
                case OpCode.F64Ceil:
                    return F64Ceil;
                case OpCode.F64Floor:
                    return F64Floor;
                case OpCode.F64Trunc:
                    return F64Trunc;
                case OpCode.F64Nearest:
                    return F64Nearest;
                case OpCode.F64Sqrt:
                    return F64Sqrt;
                case OpCode.F64Add:
                    return F64Add;
                case OpCode.F64Sub:
                    return F64Sub;
                case OpCode.F64Mul:
                    return F64Mul;
                case OpCode.F64Div:
                    return F64Div;
                case OpCode.F64Min:
                    return F64Min;
                case OpCode.F64Max:
                    return F64Max;
                case OpCode.F64Copysign:
                    return F64Copysign;
                case OpCode.I32WrapI64:
                    return I32WrapI64;
                case OpCode.I32TruncSf32:
                    return I32TruncSf32;
                case OpCode.I32TruncUf32:
                    return I32TruncUf32;
                case OpCode.I32TruncSf64:
                    return I32TruncSf64;
                case OpCode.I32TruncUf64:
                    return I32TruncUf64;
                case OpCode.I64ExtendSi32:
                    return I64ExtendSi32;
                case OpCode.I64ExtendUi32:
                    return I64ExtendUi32;
                case OpCode.I64TruncSf32:
                    return I64TruncSf32;
                case OpCode.I64TruncUf32:
                    return I64TruncUf32;
                case OpCode.I64TruncSf64:
                    return I64TruncSf64;
                case OpCode.I64TruncUf64:
                    return I64TruncUf64;
                case OpCode.F32ConvertSi32:
                    return F32ConvertSi32;
                case OpCode.F32ConvertUi32:
                    return F32ConvertUi32;
                case OpCode.F32ConvertSi64:
                    return F32ConvertSi64;
                case OpCode.F32ConvertUi64:
                    return F32ConvertUi64;
                case OpCode.F32DemoteF64:
                    return F32DemoteF64;
                case OpCode.F64ConvertSi32:
                    return F64ConvertSi32;
                case OpCode.F64ConvertUi32:
                    return F64ConvertUi32;
                case OpCode.F64ConvertSi64:
                    return F64ConvertSi64;
                case OpCode.F64ConvertUi64:
                    return F64ConvertUi64;
                case OpCode.F64PromoteF32:
                    return F64PromoteF32;
                case OpCode.I32ReinterpretF32:
                    return I32ReinterpretF32;
                case OpCode.I64ReinterpretF64:
                    return I64ReinterpretF64;
                case OpCode.F32ReinterpretI32:
                    return F32ReinterpretI32;
                case OpCode.F64ReinterpretI64:
                    return F64ReinterpretI64;
                default:
                    throw new NotImplementedException();
            }
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