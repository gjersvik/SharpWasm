using System;

namespace SharpWasm.Core.Types
{
    internal struct Limits: IEquatable<Limits>
    {
        public readonly uint? Max;
        public readonly uint Min;

        public Limits(uint min, uint? max = null)
        {
            Min = min;
            Max = max;
        }

        public bool Equals(Limits other)
        {
            return Max == other.Max && Min == other.Min;
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            return obj is Limits limits && Equals(limits);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Max.GetHashCode() * 397) ^ (int) Min;
            }
        }

        public static bool operator ==(Limits left, Limits right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Limits left, Limits right)
        {
            return !left.Equals(right);
        }
    }
}
