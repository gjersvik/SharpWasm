﻿using System.Collections.Immutable;
using System.IO;
using SharpWasm.Core.Segments;
using SharpWasm.Core.Types;

namespace SharpWasm.Core.Parser
{
    internal class Sections
    {
        public ImmutableDictionary<string, ImmutableArray<byte>> Custom { get; private set; } = ImmutableDictionary<string, ImmutableArray<byte>>.Empty;
        public ImmutableArray<FunctionType> Type { get; private set; } = ImmutableArray<FunctionType>.Empty;
        public ImmutableArray<Import> Import { get; private set; } = ImmutableArray<Import>.Empty;

        public void ParseCustom(BinaryReader reader)
        {
            var name = Values.ToName(reader);
            using (var ms = new MemoryStream())
            {
                reader.BaseStream.CopyTo(ms);
                Custom = Custom.Add(name, ms.ToArray().ToImmutableArray());
            }
        }

        public void ParseType(BinaryReader reader)
        {
            Type = Values.ToVector(reader, TypeParser.ToFunctionType);
        }

        public void ParseImport(BinaryReader reader)
        {
            Import = Values.ToVector(reader, SegmentsParser.ToImport);
        }
    }
}
