using System;
using System.Collections.Immutable;
using System.Linq;

namespace SharpWasm.Core.Code
{
    internal class BrTable: IEquatable<BrTable>
    {
        public readonly ImmutableArray<uint> TargetTable;
        public readonly uint DefaultTarget;

        public BrTable(ImmutableArray<uint> targetTable, uint defaultTarget)
        {
            TargetTable = targetTable;
            DefaultTarget = defaultTarget;
        }

        public bool Equals(BrTable other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return TargetTable.SequenceEqual(other.TargetTable) && DefaultTarget == other.DefaultTarget;
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((BrTable) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = TargetTable.Length;
                hashCode = (hashCode * 397) ^ (int) DefaultTarget;
                return hashCode;
            }
        }

        public static bool operator ==(BrTable left, BrTable right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(BrTable left, BrTable right)
        {
            return !Equals(left, right);
        }
    }
}