﻿using System;

namespace SharpWasm.Core.Code
{
    internal class CallIndirect: IEquatable<CallIndirect>
    {
        public readonly uint TypeIndex;
        public readonly bool Reserved;

        public CallIndirect(uint typeIndex, bool reserved = false)
        {
            TypeIndex = typeIndex;
            Reserved = reserved;
        }

        public bool Equals(CallIndirect other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return TypeIndex == other.TypeIndex && Reserved == other.Reserved;
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((CallIndirect) obj);
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