using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace SharpWasm.Core.Types
{
    public struct FunctionType: IEquatable<FunctionType>
    {
        public readonly ImmutableArray<ValueType> Parameters;
        public readonly ImmutableArray<ValueType> Returns;

        public FunctionType(IEnumerable<ValueType> parameters, IEnumerable<ValueType> returns = null)
        {
            Parameters = parameters.ToImmutableArray();
            Returns = returns?.ToImmutableArray() ?? ImmutableArray<ValueType>.Empty;
        }

        public bool Equals(FunctionType other)
        {
            return Parameters.SequenceEqual(other.Parameters) && Returns.SequenceEqual(other.Returns);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            return obj is FunctionType && Equals((FunctionType) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = Parameters.Length.GetHashCode();
                foreach (var valueType in Parameters.Take(5))
                {
                    hash = (hash * 397) ^ valueType.GetHashCode();
                }

                if (!Returns.IsEmpty)
                {
                    hash = (hash * 397) ^ Returns[0].GetHashCode();
                }

                return hash;
            }
        }

        public static bool operator ==(FunctionType left, FunctionType right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(FunctionType left, FunctionType right)
        {
            return !left.Equals(right);
        }
    }
}