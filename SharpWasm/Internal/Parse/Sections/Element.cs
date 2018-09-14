using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using SharpWasm.Core.Parser;

namespace SharpWasm.Internal.Parse.Sections
{
    internal class Element: ISection
    {
        public static readonly Element Empty = new Element(ImmutableArray<ElementSegment>.Empty);

        public SectionCode Id { get; } = SectionCode.Element;
        public readonly uint Count;
        public readonly ImmutableArray<ElementSegment> Entries;

        public Element(IEnumerable<ElementSegment> entries)
        {
            Entries = entries.ToImmutableArray();
            Count = (uint)Entries.Length;
        }

        public Element(BinaryReader reader)
        {
            Count = Values.ToUInt(reader);
            Entries = ParseTools.ToArray(reader, Count, r => new ElementSegment(r));
        }
    }
}
