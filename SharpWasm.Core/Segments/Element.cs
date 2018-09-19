using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using SharpWasm.Core.Code;

namespace SharpWasm.Core.Segments
{
    internal class Element: IEquatable<Element>
    {
        public readonly uint Table;
        public readonly ImmutableArray<IInstruction> Offset;
        public readonly ImmutableArray<uint> Init;

        public Element(uint table, IEnumerable<IInstruction> offset, IEnumerable<uint> init)
        {
            Table = table;
            Offset = offset.ToImmutableArray();
            Init = init.ToImmutableArray();
        }

        public bool Equals(Element other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return Table == other.Table && Offset.SequenceEqual(other.Offset) && Init.SequenceEqual(other.Init);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Element) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (int) Table;
                hashCode = (hashCode * 397) ^ Offset.FirstOrDefault()?.GetHashCode() ?? 0;
                hashCode = (hashCode * 397) ^ Init.Length.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(Element left, Element right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Element left, Element right)
        {
            return !Equals(left, right);
        }
    }
}