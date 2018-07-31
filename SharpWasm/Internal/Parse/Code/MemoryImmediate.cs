using System;
using System.IO;

namespace SharpWasm.Internal.Parse.Code
{
    internal class MemoryImmediate: IEquatable<MemoryImmediate>
    {
        public readonly uint Flags;
        public readonly uint Offset;

        public MemoryImmediate(uint flags, uint offset)
        {
            Flags = flags;
            Offset = offset;
        }

        public MemoryImmediate(BinaryReader reader)
        {
            Flags = VarIntUnsigned.ToUInt(reader);
            Offset = VarIntUnsigned.ToUInt(reader);
        }

        public bool Equals(MemoryImmediate other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Flags == other.Flags && Offset == other.Offset;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((MemoryImmediate) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((int) Flags * 397) ^ (int) Offset;
            }
        }

        public static bool operator ==(MemoryImmediate left, MemoryImmediate right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(MemoryImmediate left, MemoryImmediate right)
        {
            return !Equals(left, right);
        }
    }
}
