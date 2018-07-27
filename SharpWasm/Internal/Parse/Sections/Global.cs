using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;

namespace SharpWasm.Internal.Parse.Sections
{
    internal class Global : ISection
    {
        public static readonly Global Empty = new Global(ImmutableArray<GlobalEntry>.Empty);

        public SectionCode Id { get; } = SectionCode.Global;
        public readonly uint Count;
        public readonly ImmutableArray<GlobalEntry> Entries;

        public Global(IEnumerable<GlobalEntry> entries)
        {
            Entries = entries.ToImmutableArray();
            Count = (uint) Entries.Length;
        }

        public Global(BinaryReader reader)
        {
            Count = VarIntUnsigned.ToUInt(reader);
            Entries = ParseTools.ToArray(reader, Count, r => new GlobalEntry(r));
        }
    }
}
