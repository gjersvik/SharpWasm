using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using SharpWasm.Core.Parser;

namespace SharpWasm.Internal.Parse.Sections
{
    internal class Function: ISection
    {
        public static readonly Function Empty = new Function(ImmutableArray<uint>.Empty);

        public SectionCode Id { get; } = SectionCode.Function;
        public readonly uint Count;
        public readonly ImmutableArray<uint> Types;

        public Function(IEnumerable<uint> types)
        {
            Types = types.ToImmutableArray();
            Count = (uint)Types.Length;
        }

        public Function(BinaryReader reader)
        {
            Count = Values.ToUInt(reader);
            Types = ParseTools.ToArray(reader, Count, Values.ToUInt);
        }
    }
}
