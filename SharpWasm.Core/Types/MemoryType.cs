using System;
using JetBrains.Annotations;

namespace SharpWasm.Core.Types
{
    public class MemoryType: IEquatable<MemoryType>
    {
        [NotNull] public readonly Limits Limits;

        public MemoryType(Limits limits)
        {
            Limits = limits;
        }

        public MemoryType(uint min, uint? max = null)
        {
            Limits = new Limits(min, max);
        }

        public bool Equals(MemoryType other)
        {
            if (other is null) return false;
            return ReferenceEquals(this, other) || Limits.Equals(other.Limits);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((MemoryType) obj);
        }

        public override int GetHashCode()
        {
            return Limits.GetHashCode();
        }

        public static bool operator ==(MemoryType left, MemoryType right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(MemoryType left, MemoryType right)
        {
            return !Equals(left, right);
        }
    }
}