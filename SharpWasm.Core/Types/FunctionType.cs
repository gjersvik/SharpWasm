using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace SharpWasm.Core.Types
{
    public class FunctionType: IEquatable<FunctionType>
    {
        public readonly ImmutableArray<ValueType> Parameters;
        public readonly ImmutableArray<ValueType> Returns;

        public FunctionType() : this(ImmutableArray<ValueType>.Empty)
        {

        }

        public FunctionType(IEnumerable<ValueType> parameters, IEnumerable<ValueType> returns = null)
        {
            Parameters = parameters.ToImmutableArray();
            Returns = returns?.ToImmutableArray() ?? ImmutableArray<ValueType>.Empty;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = Parameters.Length.GetHashCode();
                hash = Parameters.Take(5).Aggregate(hash, (current, valueType) => (current * 397) ^ valueType.GetHashCode());

                if (!Returns.IsEmpty)
                {
                    hash = (hash * 397) ^ Returns[0].GetHashCode();
                }

                return hash;
            }
        }

        public bool Equals(FunctionType other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Parameters.SequenceEqual(other.Parameters) && Returns.SequenceEqual(other.Returns);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((FunctionType) obj);
        }

        public static bool operator ==(FunctionType left, FunctionType right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(FunctionType left, FunctionType right)
        {
            return !Equals(left, right);
        }
    }
}