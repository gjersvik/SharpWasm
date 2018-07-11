using System.Collections.Generic;
using System.Collections.Immutable;
using SharpWasm.Internal.Parse.Sections;

namespace SharpWasm.Internal
{
    internal class Element:ISection
    {
        public static readonly Element Empty = new Element(ImmutableArray<ElementSegment>.Empty);

        public SectionCode Id { get; } = SectionCode.Element;
        public readonly ImmutableArray<ElementSegment> ElementSegments;

        public Element(byte[] payload) : this(FromPayload(payload))
        {
        }

        private Element(IEnumerable<ElementSegment> list)
        {
            ElementSegments = list.ToImmutableArray();
        }

        private static IEnumerable<ElementSegment> FromPayload(byte[] payload)
        {
            using (var reader = new WasmReader(payload))
            {
                return reader.ReadElementSegments();
            }
        }
    }

    internal class ElementSegment
    {
        public readonly int Offset;
        public readonly uint[] Elems;

        public ElementSegment(int offset, uint[] elems)
        {
            Offset = offset;
            Elems = elems;
        }
    }
}
