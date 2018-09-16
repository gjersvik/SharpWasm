using System;

namespace SharpWasm.Core.Types
{
    public class TableType: IEquatable<TableType>
    {
        public readonly ElemType ElemType;
        public readonly Limits Limits;

        public TableType(Limits limits, ElemType elemType = ElemType.AnyFunc)
        {
            ElemType = elemType;
            Limits = limits;
        }
        public TableType(uint min, uint? max = null): this(new Limits(min,max))
        {
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