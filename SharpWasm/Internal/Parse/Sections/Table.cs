using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using SharpWasm.Core.Parser;
using SharpWasm.Internal.Parse.Types;

namespace SharpWasm.Internal.Parse.Sections
{
    internal class Table: ISection
    {
        public static readonly Table Empty = new Table(ImmutableArray<TableType>.Empty);

        public SectionCode Id { get; } = SectionCode.Table;
        public readonly uint Count;
        public readonly ImmutableArray<TableType> Entries;

        public Table(IEnumerable<TableType> entries)
        {
            Entries = entries.ToImmutableArray();
            Count = (uint)Entries.Length;
        }

        public Table(BinaryReader reader)
        {
            Count = Values.ToUInt(reader);
            Entries = ParseTools.ToArray(reader, Count, r => new TableType(r));
        }
    }
}
