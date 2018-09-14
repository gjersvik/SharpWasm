using System;
using System.IO;
using SharpWasm.Core.Types;
using SharpWasm.Core.Parser;

namespace SharpWasm.Internal.Parse.Types
{
    internal class MemoryType: IEquatable<MemoryType>
    {
        public readonly Limits Limits;

        public MemoryType(BinaryReader reader)
        {
            Limits = TypeParser.ToLimits(reader);
        }

        public MemoryType(Limits limits)
        {
            Limits = limits;
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
