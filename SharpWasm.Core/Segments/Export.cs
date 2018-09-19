using System;
using JetBrains.Annotations;
using SharpWasm.Core.Types;

namespace SharpWasm.Core.Segments
{
    internal class Export: IEquatable<Export>
    {
        [NotNull]public readonly string Name;
        public readonly ExternalKind Type;
        public readonly uint Index;

        public Export(string name, ExternalKind type, uint index)
        {
            Name = name;
            Type = type;
            Index = index;
        }

        public bool Equals(Export other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Name, other.Name) && Type == other.Type && Index == other.Index;
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Export) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Name.GetHashCode();
                hashCode = (hashCode * 397) ^ (int) Type;
                hashCode = (hashCode * 397) ^ (int) Index;
                return hashCode;
            }
        }

        public static bool operator ==(Export left, Export right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Export left, Export right)
        {
            return !Equals(left, right);
        }
    }
}