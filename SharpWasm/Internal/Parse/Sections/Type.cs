using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using SharpWasm.Internal.Parse.Types;

namespace SharpWasm.Internal.Parse.Sections
{
    internal class Type: ISection
    {
        public SectionCode Id { get; } = SectionCode.Type;
        public readonly uint Count;
        public readonly ImmutableArray<FuncType> Entries;

        public Type(IEnumerable<FuncType> entries)
        {
            Entries = entries.ToImmutableArray();
            Count = (uint)Entries.Length;
        }

        public Type(BinaryReader reader)
        {
            Count = VarIntUnsigned.ToUInt(reader);
            Entries = ParseTools.ToArray(reader, Count, r => new FuncType(r));
        }
    }
}
