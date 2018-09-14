using System;
using System.IO;
using SharpWasm.Core.Parser;
using SharpWasm.Core.Types;

namespace SharpWasm.Internal.Parse.Types
{
    internal class TableType: IEquatable<TableType>
    {
        public readonly ElemType ElementType;
        public readonly Limits Limits;

        public TableType(BinaryReader reader)
        {
            ElementType = ParseTools.ToElemType(reader);
            Limits = TypeParser.ToLimits(reader);
        }

        public TableType(Limits limits)
        {
            ElementType = ElemType.AnyFunc;
            Limits = limits;
        }

        public bool Equals(TableType other)
        {
            if (other is null) return false;
            return ReferenceEquals(this, other) || Limits.Equals(other.Limits);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((TableType) obj);
        }

        public override int GetHashCode()
        {
            return Limits.GetHashCode();
        }

        public static bool operator ==(TableType left, TableType right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(TableType left, TableType right)
        {
            return !Equals(left, right);
        }
    }
}