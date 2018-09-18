using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using JetBrains.Annotations;
using SharpWasm.Core.Code;
using SharpWasm.Core.Types;

namespace SharpWasm.Core.Segments
{
    internal class Global: IEquatable<Global>
    {
        [NotNull]public readonly GlobalType Type;
        public readonly ImmutableArray<IInstruction> Init;

        public Global(GlobalType type, IEnumerable<IInstruction> init)
        {
            Type = type;
            Init = init.ToImmutableArray();
        }

        public bool Equals(Global other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return Type.Equals(other.Type) && Init.SequenceEqual(other.Init);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Global) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Type.GetHashCode() * 397) ^ Init.FirstOrDefault()?.GetHashCode() ?? 0;
            }
        }

        public static bool operator ==(Global left, Global right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Global left, Global right)
        {
            return !Equals(left, right);
        }
    }
}