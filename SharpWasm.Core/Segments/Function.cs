using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using SharpWasm.Core.Code;
using ValueType = SharpWasm.Core.Types.ValueType;

namespace SharpWasm.Core.Segments
{
    internal class Function:IEquatable<Function>
    {
        public readonly uint TypeIndex;
        public readonly ImmutableArray<ValueType> Locals;
        public readonly ImmutableArray<IInstruction> Body;

        public Function(uint typeIndex, IEnumerable<ValueType> locals, IEnumerable<IInstruction> body)
        {
            TypeIndex = typeIndex;
            Locals = locals.ToImmutableArray();
            Body = body.ToImmutableArray();
        }

        public bool Equals(Function other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return TypeIndex == other.TypeIndex && Locals.SequenceEqual(other.Locals) && Body.SequenceEqual(other.Body);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Function) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (int) TypeIndex;
                hashCode = (hashCode * 397) ^ Locals.Length.GetHashCode();
                hashCode = (hashCode * 397) ^ Body.Length.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(Function left, Function right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Function left, Function right)
        {
            return !Equals(left, right);
        }
    }
}