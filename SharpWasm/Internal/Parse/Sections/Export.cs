using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using SharpWasm.Internal.Parse.Types;

namespace SharpWasm.Internal.Parse.Sections
{
    internal class Export: ISection
    {
        public static readonly Export Empty = new Export(ImmutableArray<ExportEntry>.Empty);

        public SectionCode Id { get; } = SectionCode.Export;
        public readonly uint Count;
        public readonly ImmutableArray<ExportEntry> Entries;

        public Export(IEnumerable<ExportEntry> entries)
        {
            Entries = entries.ToImmutableArray();
            Count = (uint)Entries.Length;
        }

        public Export(BinaryReader reader)
        {
            Count = VarIntUnsigned.ToUInt(reader);
            Entries = ParseTools.ToArray(reader, Count, r => new ExportEntry(r));
        }

        public uint? Func(string name)
        {
            return Entries.Where(e => e.ExternalKind == ExternalKind.Function).FirstOrDefault(e => e.FieldStr == name)?.Index;
        }
    }
}
