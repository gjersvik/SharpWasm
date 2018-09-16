﻿using System;
using System.IO;
using JetBrains.Annotations;
using SharpWasm.Core.Parser;
using SharpWasm.Core.Types;
using SharpWasm.Internal.Parse.Types;

namespace SharpWasm.Internal.Parse.Sections
{
    internal class GlobalEntry: IEquatable<GlobalEntry>
    {
        [NotNull] public readonly GlobalType Type;
        [NotNull] public readonly InitExpr InitExpr;

        public GlobalEntry(GlobalType type, InitExpr initExpr)
        {
            Type = type;
            InitExpr = initExpr;
        }

        public GlobalEntry(BinaryReader reader)
        {
            Type = TypeParser.ToGlobalType(reader);
            InitExpr = new InitExpr(reader);
        }

        public bool Equals(GlobalEntry other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return Type.Equals(other.Type) && InitExpr.Equals(other.InitExpr);
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
                return (Type.GetHashCode() * 397) ^ InitExpr.GetHashCode();
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
