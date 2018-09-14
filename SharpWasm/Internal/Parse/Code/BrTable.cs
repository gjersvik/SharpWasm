using System;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using SharpWasm.Core.Parser;

namespace SharpWasm.Internal.Parse.Code
{
    public class BrTable: IEquatable<BrTable>
    {
        public readonly uint TargetCount;
        public readonly ImmutableArray<uint> TargetTable;
        public readonly uint DefaultTarget;

        public BrTable(ImmutableArray<uint> targetTable, uint defaultTarget)
        {
            TargetTable = targetTable;
            DefaultTarget = defaultTarget;
            TargetCount = (uint) TargetTable.Length;
        }

        public BrTable(BinaryReader reader)
        {
            TargetCount = Values.ToUInt(reader);
            TargetTable = ParseTools.ToArray(reader, TargetCount,  Values.ToUInt);
            DefaultTarget = Values.ToUInt(reader);
        }

        public bool Equals(BrTable other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return TargetCount == other.TargetCount && TargetTable.SequenceEqual(other.TargetTable) && DefaultTarget == other.DefaultTarget;
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
                var hashCode = (int) TargetCount;
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