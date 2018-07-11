using System;
using System.IO;
using JetBrains.Annotations;

namespace SharpWasm.Internal.Parse
{
    internal class ResizableLimits: IEquatable<ResizableLimits>
    {
        public readonly bool Flags;
        public readonly uint Initial;
        [CanBeNull]public readonly uint? Maximum;

        public ResizableLimits(BinaryReader reader)
        {
            Flags = VarIntUnsigned.ToBool(reader);
            Initial = VarIntUnsigned.ToUInt(reader);
            if (Flags) Maximum = VarIntUnsigned.ToUInt(reader);
        }

        public ResizableLimits(uint initial, uint? maximum = null)
        {
            Initial = initial;
            Maximum = maximum;
            Flags = Maximum != null;
        }

        public bool Equals(ResizableLimits other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return Initial == other.Initial && Maximum == other.Maximum;
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((ResizableLimits) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((int) Initial * 397) ^ Maximum.GetHashCode();
            }
        }
    }
}