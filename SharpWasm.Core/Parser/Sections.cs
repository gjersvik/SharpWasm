using System.Collections.Immutable;
using System.IO;
using SharpWasm.Core.Segments;
using SharpWasm.Core.Types;

namespace SharpWasm.Core.Parser
{
    internal class Sections
    {
        public ImmutableDictionary<string, ImmutableArray<byte>> Custom { get; private set; } =
            ImmutableDictionary<string, ImmutableArray<byte>>.Empty;

        public ImmutableArray<FunctionType> Type { get; private set; } = ImmutableArray<FunctionType>.Empty;
        public ImmutableArray<Import> Import { get; private set; } = ImmutableArray<Import>.Empty;
        public ImmutableArray<uint> Function { get; private set; } = ImmutableArray<uint>.Empty;
        public ImmutableArray<TableType> Table { get; private set; } = ImmutableArray<TableType>.Empty;
        public ImmutableArray<MemoryType> Memory { get; private set; } = ImmutableArray<MemoryType>.Empty;
        public ImmutableArray<Global> Global { get; private set; } = ImmutableArray<Global>.Empty;
        public ImmutableArray<Export> Export { get; private set; } = ImmutableArray<Export>.Empty;
        public uint? Start { get; private set; }
        public ImmutableArray<Element> Element { get; private set; } = ImmutableArray<Element>.Empty;
        public ImmutableArray<CodeSection> Code { get; private set; } = ImmutableArray<CodeSection>.Empty;
        public ImmutableArray<Data> Data { get; private set; } = ImmutableArray<Data>.Empty;

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

        public void ParseFunction(BinaryReader reader)
        {
            Function = Values.ToVector(reader, Values.ToUInt);
        }

        public void ParseTable(BinaryReader reader)
        {
            Table = Values.ToVector(reader, TypeParser.ToTableType);
        }

        public void ParseMemory(BinaryReader reader)
        {
            Memory = Values.ToVector(reader, TypeParser.ToMemoryType);
        }

        public void ParseGlobal(BinaryReader reader)
        {
            Global = Values.ToVector(reader, SegmentsParser.ToGlobal);
        }

        public void ParseExport(BinaryReader reader)
        {
            Export = Values.ToVector(reader, SegmentsParser.ToExport);
        }

        public void ParseStart(BinaryReader reader)
        {
            Start = Values.ToUInt(reader);
        }

        public void ParseElement(BinaryReader reader)
        {
            Element = Values.ToVector(reader, SegmentsParser.ToElement);
        }

        public void ParseCode(BinaryReader reader)
        {
            Code = Values.ToVector(reader, SegmentsParser.ToCodeSection);
        }

        public void ParseData(BinaryReader reader)
        {
            Data = Values.ToVector(reader, SegmentsParser.ToData);
        }
    }
}
