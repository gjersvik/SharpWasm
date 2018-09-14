using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using SharpWasm.Core.Types;
using SharpWasm.Internal.Parse.Types;

namespace SharpWasm.Internal.Parse.Sections
{
    internal class Type: ISection
    {
        public static readonly Type Empty = new Type(ImmutableArray<FunctionType>.Empty);

        public SectionCode Id { get; } = SectionCode.Type;
        public readonly uint Count;
        public readonly ImmutableArray<FunctionType> Entries;

        public Type(IEnumerable<FunctionType> entries)
        {
            Entries = entries.ToImmutableArray();
            Count = (uint)Entries.Length;
        }

        public Type(BinaryReader reader)
        {
            Count = VarIntUnsigned.ToUInt(reader);
            Entries = ParseTools.ToArray(reader, Count, FuncType.Parse);
        }
    }
}
