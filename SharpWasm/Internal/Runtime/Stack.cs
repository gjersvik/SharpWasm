using System.Collections.Generic;
using SharpWasm.Core.Runtime;

namespace SharpWasm.Internal.Runtime
{
    internal class Stack
    {
        public Stack(int stackSize = 10000)
        {
            _stack = new Stack<Value>(stackSize);
            _maxStack = stackSize;
        }


        public Value Pop()
        {
            return _stack.Pop();
        }

        public void Push(Value stackValue)
        {
            if (_stack.Count >= _maxStack) throw new WebAssemblyStackOverflowException();
            _stack.Push(stackValue);
        }

        public int Count => _stack.Count;

        private readonly Stack<Value> _stack;
        private readonly int _maxStack;
    }
}
