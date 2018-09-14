using System;
using System.IO;
using JetBrains.Annotations;
using SharpWasm.Core.Parser;

namespace SharpWasm.Internal.Parse.Types
{
    internal class ResizableLimits: IEquatable<ResizableLimits>
    {
        public readonly bool Flags;
        public readonly uint Initial;
        [CanBeNull]public readonly uint? Maximum;

        public ResizableLimits(BinaryReader reader)
        {
            Flags = Values.ToBool(reader);
            Initial = Values.ToUInt(reader);
            if (Flags) Maximum = Values.ToUInt(reader);
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