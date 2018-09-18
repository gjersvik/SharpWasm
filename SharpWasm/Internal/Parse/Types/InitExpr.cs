using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using SharpWasm.Core.Code;
using SharpWasm.Core.Parser;

namespace SharpWasm.Internal.Parse.Types
{
    internal class InitExpr : IEquatable<InitExpr>
    {
        public readonly ImmutableArray<IInstruction> Instructions;

        public InitExpr(IEnumerable<IInstruction> instructions)
        {
            Instructions = instructions.ToImmutableArray();
        }

        public InitExpr(BinaryReader reader)
        {
            var builder = ImmutableArray.CreateBuilder<IInstruction>(2);
            do
            {
                builder.Add(CodeParser.ToInstruction(reader));
            } while (builder.Last().OpCode != OpCode.End);

            builder.Capacity = builder.Count;

            Instructions = builder.MoveToImmutable();
        }

        public bool Equals(InitExpr other)
        {
            if (other is null) return false;
            return ReferenceEquals(this, other) || Instructions.SequenceEqual(other.Instructions);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((InitExpr) obj);
        }

        public override int GetHashCode()
        {
            return Instructions.Length;
        }

        public static bool operator ==(InitExpr left, InitExpr right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(InitExpr left, InitExpr right)
        {
            return !Equals(left, right);
        }
    }
}