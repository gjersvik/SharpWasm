using System;
using ValueType = SharpWasm.Core.Types.ValueType;

namespace SharpWasm.Internal.Runtime
{
    internal struct Value
    {
        public readonly ValueType Type;
        private readonly byte[] _value;

        public Value(int value) : this(BitConverter.GetBytes(value), ValueType.I32) { }
        public Value(uint value) : this(BitConverter.GetBytes(value), ValueType.I32) { }
        public Value(long value) : this(BitConverter.GetBytes(value), ValueType.I64) { }
        public Value(ulong value) : this(BitConverter.GetBytes(value), ValueType.I64) { }
        public Value(float value) : this(BitConverter.GetBytes(value), ValueType.F32) { }
        public Value(double value) : this(BitConverter.GetBytes(value), ValueType.F64) { }

        private Value(byte[] value, ValueType type)
        {
            Type = type;
            _value = value;
        }

        public static implicit operator Value(int value) => new Value(value);
        public static implicit operator Value(uint value) => new Value(value);
        public static implicit operator Value(long value) => new Value(value);
        public static implicit operator Value(ulong value) => new Value(value);
        public static implicit operator Value(float value) => new Value(value);
        public static implicit operator Value(double value) => new Value(value);

        public static explicit operator int(Value value)
        {
            if (value.Type != ValueType.I32) throw new InvalidCastException($"Expected I32 found {value.Type}");
            return BitConverter.ToInt32(value._value,0);
        }
        public static explicit operator uint(Value value)
        {
            if (value.Type != ValueType.I32) throw new InvalidCastException($"Expected I32 found {value.Type}");
            return BitConverter.ToUInt32(value._value, 0);
        }
        public static explicit operator long(Value value)
        {
            if (value.Type != ValueType.I64) throw new InvalidCastException($"Expected I64 found {value.Type}");
            return BitConverter.ToInt64(value._value, 0);
        }
        public static explicit operator ulong(Value value)
        {
            if (value.Type != ValueType.I64) throw new InvalidCastException($"Expected I64 found {value.Type}");
            return BitConverter.ToUInt64(value._value, 0);
        }
        public static explicit operator float(Value value)
        {
            if (value.Type != ValueType.F32) throw new InvalidCastException($"Expected F32 found {value.Type}");
            return BitConverter.ToSingle(value._value, 0);
        }
        public static explicit operator double(Value value)
        {
            if (value.Type != ValueType.F64) throw new InvalidCastException($"Expected F64 found {value.Type}");
            return BitConverter.ToDouble(value._value, 0);
        }
    }
}
