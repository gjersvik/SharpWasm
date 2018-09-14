using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using JetBrains.Annotations;
using SharpWasm.Core.Parser;

namespace SharpWasm.Internal.Parse.Sections
{
    internal class ElementSegment: IEquatable<ElementSegment>
    {
        public readonly uint Index;
        [NotNull] public readonly Types.InitExpr Offset;
        public readonly uint NumElem;
        public readonly ImmutableArray<uint> Elements;

        public ElementSegment(Types.InitExpr offset, IEnumerable<uint> elements)
        {
            Index = 0;
            Offset = offset;
            Elements = elements.ToImmutableArray();
            NumElem = (uint) Elements.Length;
        }

        public ElementSegment(BinaryReader reader)
        {
            Index = Values.ToUInt(reader);
            Offset = new Types.InitExpr(reader);
            NumElem = Values.ToUInt(reader);
            Elements = ParseTools.ToArray(reader, NumElem, Values.ToUInt);
        }

        public bool Equals(ElementSegment other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return Index == other.Index && Offset.Equals(other.Offset) && NumElem == other.NumElem && Elements.SequenceEqual(other.Elements);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((ElementSegment) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (int) Index;
                hashCode = (hashCode * 397) ^ Offset.GetHashCode();
                hashCode = (hashCode * 397) ^ (int) NumElem;
                return hashCode;
            }
        }

        public static bool operator ==(ElementSegment left, ElementSegment right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ElementSegment left, ElementSegment right)
        {
            return !Equals(left, right);
        }
    }
}
