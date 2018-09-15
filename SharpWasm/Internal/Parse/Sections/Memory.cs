﻿using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using SharpWasm.Core.Parser;
using SharpWasm.Core.Types;

namespace SharpWasm.Internal.Parse.Sections
{
    internal class Memory: ISection
    {
        public static readonly Memory Empty = new Memory(ImmutableArray<MemoryType>.Empty);

        public SectionCode Id { get; } = SectionCode.Memory;
        public readonly uint Count;
        public readonly ImmutableArray<MemoryType> Entries;

        public Memory(IEnumerable<MemoryType> entries)
        {
            Entries = entries.ToImmutableArray();
            Count = (uint)Entries.Length;
        }

        public Memory(BinaryReader reader)
        {
            Count = Values.ToUInt(reader);
            Entries = ParseTools.ToArray(reader, Count, TypeParser.ToMemoryType);
        }
    }
}
