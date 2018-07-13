using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using SharpWasm.Internal.Parse.Types;

namespace SharpWasm.Internal.Parse.Sections
{
    internal class Import : ISection
    {
        public static readonly Import Empty = new Import(ImmutableArray<ImportEntry>.Empty);

        public SectionCode Id { get; } = SectionCode.Import;

        public readonly uint Count;
        public readonly ImmutableArray<ImportEntry> Entries;
        public readonly ImmutableArray<ImportEntryFunction> Functions;
        public readonly ImportEntryTable Table;
        public readonly ImportEntryMemory Memory;
        public readonly ImmutableArray<ImportEntryGlobal> Globals;

        public Import(IEnumerable<ImportEntry> entries)
        {
            Entries = entries.ToImmutableArray();
            Count = (uint) Entries.Length;

            Functions = Entries.Where(i => i.Kind == ExternalKind.Function).Cast<ImportEntryFunction>()
                .ToImmutableArray();
            Table = Entries.FirstOrDefault(i => i.Kind == ExternalKind.Table) as ImportEntryTable;
            Memory = Entries.FirstOrDefault(i => i.Kind == ExternalKind.Memory) as ImportEntryMemory;
            Globals = Entries.Where(i => i.Kind == ExternalKind.Global).Cast<ImportEntryGlobal>().ToImmutableArray();
        }

        public Import(BinaryReader reader):this(Parse(reader))
        {
        }

        private static IEnumerable<ImportEntry> Parse(BinaryReader reader)
        {
            var count = VarIntUnsigned.ToUInt(reader);
            return ParseTools.ToArray(reader, count, ImportEntry.Parse);
        }
    }
}