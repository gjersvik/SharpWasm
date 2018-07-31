using System;
using System.Collections.Generic;

namespace SharpWasm.Internal.Running
{
    internal class Stack
    {
        public Stack(int stackSize = 10000)
        {
            _stack = new Stack<IValue>(stackSize);
            _maxStack = stackSize;
        }

        public void Push(int value)
        {
            Push(new I32(value));
        }

        public void Push(long value)
        {
            Push(new I64(value));
        }

        public void Push(float value)
        {
            Push(new F32(value));
        }

        public void Push(double value)
        {
            Push(new F64(value));
        }

        public int PopInt()
        {
            var raw = Pop();
            if (raw is I32 value) return value.Value;
            throw new WebAssemblyRuntimeException($"Wrong type on top off stack. Expected I32 found {raw.Type}.");
        }

        public long PopLong()
        {
            var raw = Pop();
            if (raw is I64 value) return value.Value;
            throw new WebAssemblyRuntimeException($"Wrong type on top off stack. Expected I32 found {raw.Type}.");
        }

        public float PopFloat()
        {
            var raw = Pop();
            if (raw is F32 value) return value.Value;
            throw new WebAssemblyRuntimeException($"Wrong type on top off stack. Expected I32 found {raw.Type}.");
        }

        public double PopDouble()
        {
            var raw = Pop();
            if (raw is F64 value) return value.Value;
            throw new WebAssemblyRuntimeException($"Wrong type on top off stack. Expected I32 found {raw.Type}.");
        }

        private readonly Stack<IValue> _stack;
        private readonly int _maxStack;

        private void Push(IValue value)
        {
            if (_stack.Count >= _maxStack) throw new WebAssemblyStackOverflowException();
            _stack.Push(value);
        }
        private IValue Pop()
        {
            return _stack.Pop();
        }

        private interface IValue
        {
            ValueType Type { get; }
        }

        private class I32: IValue
        {
            public ValueType Type { get; } = Parse.Types.ValueType.I32;
            public readonly int Value;

            public I32(int value)
            {
                Value = value;
            }
        }

        private class I64 : IValue
        {
            public ValueType Type { get; } = Parse.Types.ValueType.I64;
            public readonly long Value;

            public I64(long value)
            {
                Value = value;
            }
        }

        private class F32 : IValue
        {
            public ValueType Type { get; } = Parse.Types.ValueType.F32;
            public readonly float Value;

            public F32(float value)
            {
                Value = value;
            }
        }

        private class F64 : IValue
        {
            public ValueType Type { get; } = Parse.Types.ValueType.F64;
            public readonly double Value;

            public F64(double value)
            {
                Value = value;
            }
        }
    }
}
