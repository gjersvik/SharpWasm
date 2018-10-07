using System;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using JetBrains.Annotations;
using SharpWasm.Core.Code;
using SharpWasm.Core.Parser;
using SharpWasm.Core.Types;

namespace SharpWasm.Internal.Parse.Sections
{
    internal class GlobalEntry: IEquatable<GlobalEntry>
    {
        [NotNull] public readonly GlobalType Type;
        public readonly ImmutableArray<IInstruction> InitExpr;

        public GlobalEntry(GlobalType type, ImmutableArray<IInstruction> initExpr)
        {
            Type = type;
            InitExpr = initExpr;
        }

        public GlobalEntry(BinaryReader reader)
        {
            Type = TypeParser.ToGlobalType(reader);
            InitExpr = CodeParser.ToInitExpr(reader);
        }

        public bool Equals(GlobalEntry other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return Type.Equals(other.Type) && InitExpr.SequenceEqual(other.InitExpr);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((GlobalEntry) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Type.GetHashCode() * 397) ^ InitExpr.Length;
            }
        }

        public static bool operator ==(GlobalEntry left, GlobalEntry right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(GlobalEntry left, GlobalEntry right)
        {
            return !Equals(left, right);
        }
    }
}
