using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using SharpWasm.Core.Code;
using SharpWasm.Core.Parser;

namespace SharpWasm.Internal.Parse.Sections
{
    internal class DataSegment: IEquatable<DataSegment>
    {
        public readonly uint Index;
        public readonly ImmutableArray<IInstruction> Offset;
        public readonly uint Size;
        public readonly ImmutableArray<byte> Data;

        public DataSegment(ImmutableArray<IInstruction> offset, IEnumerable<byte> data)
        {
            Index = 0;
            Offset = offset;
            Data = data.ToImmutableArray();
            Size = (uint)Data.Length;
        }

        public DataSegment(BinaryReader reader)
        {
            Index = Values.ToUInt(reader);
            Offset = CodeParser.ToInitExpr(reader);
            Size = Values.ToUInt(reader);
            Data = ParseTools.ToBytes(reader,Size);
        }

        public bool Equals(DataSegment other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return Index == other.Index && Offset.SequenceEqual(other.Offset) && Size == other.Size && Data.SequenceEqual(other.Data);
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
                hashCode = (hashCode * 397) ^ Offset.Length;
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
