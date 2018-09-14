using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using SharpWasm.Core.Parser;
using SharpWasm.Internal.Parse.Code;

namespace SharpWasm.Internal.Parse.Sections
{
    internal class CodeSection: ISection
    {
        public static readonly CodeSection Empty = new CodeSection(ImmutableArray<FunctionBody>.Empty);

        public SectionCode Id { get; } = SectionCode.Code;
        public readonly uint Count;
        public readonly ImmutableArray<FunctionBody> Bodies;

        public CodeSection(IEnumerable<FunctionBody> entries)
        {
            Bodies = entries.ToImmutableArray();
            Count = (uint)Bodies.Length;
        }

        public CodeSection(BinaryReader reader)
        {
            Count = Values.ToUInt(reader);
            Bodies = ParseTools.ToArray(reader, Count, r => new FunctionBody(r));
        }
    }
}
