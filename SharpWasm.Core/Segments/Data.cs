using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using SharpWasm.Core.Code;

namespace SharpWasm.Core.Segments
{
    internal class Data: IEquatable<Data>
    {
        public readonly uint Memory;
        public readonly ImmutableArray<IInstruction> Offset;
        public readonly ImmutableArray<byte> Init;

        public Data(uint memory, IEnumerable<IInstruction> offset, IEnumerable<byte> init)
        {
            Memory = memory;
            Offset = offset.ToImmutableArray();
            Init = init.ToImmutableArray();
        }

        public bool Equals(Data other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return Memory == other.Memory && Offset.SequenceEqual(other.Offset) && Init.SequenceEqual(other.Init);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Data) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (int) Memory;
                hashCode = (hashCode * 397) ^ Offset.FirstOrDefault()?.GetHashCode() ?? 0;
                hashCode = (hashCode * 397) ^ Init.Length.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(Data left, Data right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Data left, Data right)
        {
            return !Equals(left, right);
        }
    }
}