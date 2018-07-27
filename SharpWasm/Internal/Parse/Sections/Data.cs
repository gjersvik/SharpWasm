using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;

namespace SharpWasm.Internal.Parse.Sections
{
    internal class Data : ISection
    {
        public static readonly Data Empty = new Data(ImmutableArray<DataSegment>.Empty);

        public SectionCode Id { get; } = SectionCode.Data;
        public readonly uint Count;
        public readonly ImmutableArray<DataSegment> Entries;

        public Data(IEnumerable<DataSegment> entries)
        {
            Entries = entries.ToImmutableArray();
            Count = (uint)Entries.Length;
        }

        public Data(BinaryReader reader)
        {
            Count = VarIntUnsigned.ToUInt(reader);
            Entries = ParseTools.ToArray(reader, Count, r => new DataSegment(r));
        }
    }
}
