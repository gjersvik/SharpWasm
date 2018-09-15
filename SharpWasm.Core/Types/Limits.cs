using System;

namespace SharpWasm.Core.Types
{
    public class Limits: IEquatable<Limits>
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
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return Max == other.Max && Min == other.Min;
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Limits) obj);
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
            return Equals(left, right);
        }

        public static bool operator !=(Limits left, Limits right)
        {
            return !Equals(left, right);
        }
    }
}
