using System;

namespace SharpWasm.Core.Types
{
    public class GlobalType:IEquatable<GlobalType>
    {
        public readonly ValueType ValueType;
        public readonly bool Mutable;

        public GlobalType(ValueType valueType, bool mutable)
        {
            ValueType = valueType;
            Mutable = mutable;
        }

        public bool Equals(GlobalType other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return ValueType == other.ValueType && Mutable == other.Mutable;
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((GlobalType) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((int) ValueType * 397) ^ Mutable.GetHashCode();
            }
        }

        public static bool operator ==(GlobalType left, GlobalType right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(GlobalType left, GlobalType right)
        {
            return !Equals(left, right);
        }
    }
}