using System;
using System.Collections.Generic;
using System.IO;

namespace SharpWasm.Internal.Parse.Code
{
    internal interface IInstruction
    {
        OpCode OpCode { get; }
        bool HaveImmediate { get; }
    }

    internal class Instruction: IInstruction, IEquatable<Instruction>
    {
        public static IInstruction Parse(BinaryReader reader)
        {
            var opCode = (OpCode)reader.ReadByte();
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (opCode)
            {
                case OpCode.GetGlobal:
                    return GetGlobal(VarIntUnsigned.ToUInt(reader));
                case OpCode.I32Const:
                    return I32Const(VarIntSigned.ToInt(reader));
                case OpCode.I64Const:
                    return I64Const(VarIntSigned.ToLong(reader));
                case OpCode.F32Const:
                    return F32Const(reader.ReadSingle());
                case OpCode.F64Const:
                    return F64Const(reader.ReadDouble());
                default:
                    return new Instruction(opCode);
            }
        }
        public static readonly Instruction End = new Instruction(OpCode.End);

        public static Instruction<uint> GetGlobal(uint value) => new Instruction<uint>(OpCode.GetGlobal, value);
        public static Instruction<int> I32Const(int value) => new Instruction<int>(OpCode.I32Const, value);
        public static Instruction<long> I64Const(long value) => new Instruction<long>(OpCode.I64Const, value);
        public static Instruction<float> F32Const(float value) => new Instruction<float>(OpCode.F32Const, value);
        public static Instruction<double> F64Const(double value) => new Instruction<double>(OpCode.F64Const, value);
        

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
