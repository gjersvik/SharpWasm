﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;

namespace SharpWasm.Internal.Parse.Code
{
    internal class FunctionBody: IEquatable<FunctionBody>
    {
        public readonly uint BodySize;
        public readonly uint LocalCount;
        public readonly ImmutableArray<LocalEntry> Locals;
        public readonly ImmutableArray<byte> Code;

        public FunctionBody(BinaryReader reader)
        {
            BodySize = VarIntUnsigned.ToUInt(reader);
            var codeLength = BodySize;
            var localCount = new VarIntUnsigned(reader);
            LocalCount = localCount.UInt;
            codeLength -= localCount.Count;
            Locals = ParseTools.ToArray(reader, LocalCount, r => new LocalEntry(r));
            codeLength -= Locals.Select(l => l.Length).Aggregate(0U, (a, b) => a + b);

            Code = ParseTools.ToBytes(reader, codeLength);
        }

        public FunctionBody(IEnumerable<LocalEntry> locals, IEnumerable<byte> code)
        {
            Locals = locals.ToImmutableArray();
            LocalCount = (uint) Locals.Length;
            Code = code.ToImmutableArray();
            BodySize = (uint) Code.Length;
        }

        public bool Equals(FunctionBody other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return BodySize == other.BodySize && LocalCount == other.LocalCount && Locals.SequenceEqual(other.Locals) && Code.SequenceEqual(other.Code);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((FunctionBody) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (int) BodySize;
                hashCode = (hashCode * 397) ^ (int) LocalCount;
                return hashCode;
            }
        }

        public static bool operator ==(FunctionBody left, FunctionBody right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(FunctionBody left, FunctionBody right)
        {
            return !Equals(left, right);
        }
    }
}