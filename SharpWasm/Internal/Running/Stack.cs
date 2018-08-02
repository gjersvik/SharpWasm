using System;
using System.Collections.Generic;

namespace SharpWasm.Internal.Running
{
    internal class Stack
    {
        public Stack(int stackSize = 10000)
        {
            _stack = new Stack<IStackValue>(stackSize);
            _maxStack = stackSize;
        }

        public void Push(int value)
        {
            Push(new StackI32(value));
        }

        public void Push(long value)
        {
            Push(new StackI64(value));
        }

        public void Push(float value)
        {
            Push(new StackF32(value));
        }

        public void Push(double value)
        {
            Push(new StackF64(value));
        }

        public IStackValue Pop()
        {
            return _stack.Pop();
        }

        public void Push(IStackValue stackValue)
        {
            if (_stack.Count >= _maxStack) throw new WebAssemblyStackOverflowException();
            _stack.Push(stackValue);
        }

        public int PopInt()
        {
            var raw = Pop();
            if (raw is StackI32 value) return value.Value;
            throw new WebAssemblyRuntimeException($"Wrong type on top off stack. Expected I32 found {raw.Type}.");
        }

        public long PopLong()
        {
            var raw = Pop();
            if (raw is StackI64 value) return value.Value;
            throw new WebAssemblyRuntimeException($"Wrong type on top off stack. Expected I64 found {raw.Type}.");
        }

        public float PopFloat()
        {
            var raw = Pop();
            if (raw is StackF32 value) return value.Value;
            throw new WebAssemblyRuntimeException($"Wrong type on top off stack. Expected F32 found {raw.Type}.");
        }

        public double PopDouble()
        {
            var raw = Pop();
            if (raw is StackF64 value) return value.Value;
            throw new WebAssemblyRuntimeException($"Wrong type on top off stack. Expected F64 found {raw.Type}.");
        }

        public int Count => _stack.Count;

        private readonly Stack<IStackValue> _stack;
        private readonly int _maxStack;
    }

    public interface IStackValue
    {
        ValueType Type { get; }
    }

    public class StackI32 : IStackValue
    {
        public ValueType Type { get; } = Parse.Types.ValueType.I32;
        public readonly int Value;

        public StackI32(int value)
        {
            Value = value;
        }
    }

    public class StackI64 : IStackValue
    {
        public ValueType Type { get; } = Parse.Types.ValueType.I64;
        public readonly long Value;

        public StackI64(long value)
        {
            Value = value;
        }
    }

    public class StackF32 : IStackValue
    {
        public ValueType Type { get; } = Parse.Types.ValueType.F32;
        public readonly float Value;

        public StackF32(float value)
        {
            Value = value;
        }
    }

    public class StackF64 : IStackValue
    {
        public ValueType Type { get; } = Parse.Types.ValueType.F64;
        public readonly double Value;

        public StackF64(double value)
        {
            Value = value;
        }
    }
}
