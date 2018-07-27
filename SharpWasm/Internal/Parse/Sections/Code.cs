using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using SharpWasm.Internal.Parse.Code;

namespace SharpWasm.Internal.Parse.Sections
{
    internal class Code: ISection
    {
        public static readonly Code Empty = new Code(ImmutableArray<FunctionBody>.Empty);

        public SectionCode Id { get; } = SectionCode.Code;
        public readonly uint Count;
        public readonly ImmutableArray<FunctionBody> Bodies;

        public Code(IEnumerable<FunctionBody> entries)
        {
            Bodies = entries.ToImmutableArray();
            Count = (uint)Bodies.Length;
        }

        public Code(BinaryReader reader)
        {
            Count = VarIntUnsigned.ToUInt(reader);
            Bodies = ParseTools.ToArray(reader, Count, r => new FunctionBody(r));
        }
    }
}
