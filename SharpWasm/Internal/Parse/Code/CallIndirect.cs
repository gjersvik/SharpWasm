using System;
using System.IO;

namespace SharpWasm.Internal.Parse.Code
{
    internal class CallIndirect: IEquatable<CallIndirect>
    {
        public readonly uint TypeIndex;
        public readonly bool Reserved;

        public CallIndirect(uint typeIndex, bool reserved)
        {
            TypeIndex = typeIndex;
            Reserved = reserved;
        }
        public CallIndirect(BinaryReader reader)
        {
            TypeIndex = VarIntUnsigned.ToUInt(reader);
            Reserved = VarIntUnsigned.ToBool(reader);
        }

        public bool Equals(CallIndirect other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return TypeIndex == other.TypeIndex && Reserved == other.Reserved;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((CallIndirect) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((int) TypeIndex * 397) ^ Reserved.GetHashCode();
            }
        }

        public static bool operator ==(CallIndirect left, CallIndirect right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(CallIndirect left, CallIndirect right)
        {
            return !Equals(left, right);
        }
    }
}