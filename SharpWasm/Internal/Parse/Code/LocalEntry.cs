﻿using System;
using System.IO;

namespace SharpWasm.Internal.Parse.Code
{
    internal class LocalEntry: IEquatable<LocalEntry>
    {
        public readonly uint Count;
        public readonly ValueType Type;

        public readonly uint Length;

        public LocalEntry(uint count, ValueType type)
        {
            Count = count;
            Type = type;
        }

        public LocalEntry(BinaryReader reader)
        {
            var count = new VarIntUnsigned(reader);
            Count = count.UInt;
            Length += count.Count;

            var type = new VarIntSigned(reader);
            Type = type.ValueType;
            Length += type.Count;
        }

        public bool Equals(LocalEntry other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return Count == other.Count && Type.Equals(other.Type);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((LocalEntry) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((int) Count * 397) ^ Type.GetHashCode();
            }
        }

        public static bool operator ==(LocalEntry left, LocalEntry right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(LocalEntry left, LocalEntry right)
        {
            return !Equals(left, right);
        }
    }
}
