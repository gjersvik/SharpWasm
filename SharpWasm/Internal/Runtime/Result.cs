using JetBrains.Annotations;
using SharpWasm.Core.Runtime;

namespace SharpWasm.Internal.Runtime
{
    internal class Result
    {
        public readonly Value? Value;
        [CanBeNull] public readonly Trap Trap;

        public Result() : this(null, null)
        {

        }

        public Result(Value trap) : this(trap, null)
        {

        }

        public Result(Trap trap) : this(null, trap)
        {

        }

        private Result(Value? value, Trap trap)
        {
            Value = value;
            Trap = trap;
        }

        public static implicit operator Result(Value value) => new Result(value);
        public static implicit operator Result(Trap trap) => new Result(trap);
    }
}
