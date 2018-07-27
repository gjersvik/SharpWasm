using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using JetBrains.Annotations;

namespace SharpWasm.Internal.Parse.Sections
{
    internal class DataSegment: IEquatable<DataSegment>
    {
        public readonly uint Index;
        [NotNull] public readonly Types.InitExpr Offset;
        public readonly uint Size;
        public readonly ImmutableArray<byte> Data;

        public DataSegment(Types.InitExpr offset, IEnumerable<byte> data)
        {
            Index = 0;
            Offset = offset;
            Data = data.ToImmutableArray();
            Size = (uint)Data.Length;
        }

        public DataSegment(BinaryReader reader)
        {
            Index = VarIntUnsigned.ToUInt(reader);
            Offset = new Types.InitExpr(reader);
            Size = VarIntUnsigned.ToUInt(reader);
            Data = ParseTools.ToBytes(reader,Size);
        }

        public bool Equals(DataSegment other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return Index == other.Index && Offset.Equals(other.Offset) && Size == other.Size && Data.SequenceEqual(other.Data);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((DataSegment) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (int) Index;
                hashCode = (hashCode * 397) ^ Offset.GetHashCode();
                hashCode = (hashCode * 397) ^ (int) Size;
                return hashCode;
            }
        }

        public static bool operator ==(DataSegment left, DataSegment right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(DataSegment left, DataSegment right)
        {
            return !Equals(left, right);
        }
    }
}
