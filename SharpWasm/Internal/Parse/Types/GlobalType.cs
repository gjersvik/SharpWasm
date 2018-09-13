using System;
using System.IO;
using ValueType = SharpWasm.Core.Types.ValueType;

namespace SharpWasm.Internal.Parse.Types
{
    internal class GlobalType:IEquatable<GlobalType>
    {
        public readonly ValueType ContentType;
        public readonly bool Mutability;

        public GlobalType(BinaryReader reader)
        {
            ContentType = VarIntSigned.ToValueType(reader);
            Mutability = VarIntUnsigned.ToBool(reader);
        }

        public GlobalType(ValueType contentType, bool mutability)
        {
            ContentType = contentType;
            Mutability = mutability;
        }

        public bool Equals(GlobalType other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return ContentType == other.ContentType && Mutability == other.Mutability;
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((GlobalType) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((int) ContentType * 397) ^ Mutability.GetHashCode();
            }
        }

        public static bool operator ==(GlobalType left, GlobalType right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(GlobalType left, GlobalType right)
        {
            return !Equals(left, right);
        }
    }
}