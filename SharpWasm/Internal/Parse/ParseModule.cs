using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using SharpWasm.Core.Parser;
using SharpWasm.Core.Segments;
using SharpWasm.Core.Types;
using SharpWasm.Internal.Parse.Sections;
using Data = SharpWasm.Internal.Parse.Sections.Data;

namespace SharpWasm.Internal.Parse
{
    internal class ParseModule
    {
        public readonly uint MagicNumber;
        public readonly uint Version;
        public readonly ImmutableArray<ISection> ClassicSections;

        public readonly ImmutableDictionary<string, ImmutableArray<byte>> Customs;
        public readonly ImmutableArray<FunctionType> Types;
        public readonly ImmutableArray<Import> Imports;
        public readonly ImmutableArray<uint> Functions;
        public readonly ImmutableArray<TableType> Tables;
        public readonly ImmutableArray<MemoryType> Memories;
        public readonly ImmutableArray<Global> Globals;
        public readonly ImmutableArray<Export> Exports;
        public readonly uint? Starts;
        public readonly ImmutableArray<Element> Elements;
        public readonly ImmutableArray<CodeSection> Code;
        public readonly ImmutableArray<Data> Data;


        public ParseModule(IEnumerable<ISection> sections) : this(0x6d736100, 0x1, new Tuple<ImmutableArray<ISection>, Core.Parser.Sections>(sections.ToImmutableArray(),new Core.Parser.Sections()))
        {
        }

        public ParseModule(BinaryReader reader) : this(reader.ReadUInt32(), reader.ReadUInt32(), ParseSelections(reader))
        {
        }

        private ParseModule(uint magicNumber, uint version, Tuple<ImmutableArray<ISection>, Core.Parser.Sections> sections)
        {
            MagicNumber = magicNumber;
            Version = version;
            ClassicSections = sections.Item1;
            var newSections = sections.Item2;

            Customs = newSections.Custom;
            Types = newSections.Type;
            Imports = newSections.Import;
            Functions = newSections.Function;
            Tables = newSections.Table;
            Memories = newSections.Memory;
            Globals = newSections.Global;
            Exports = newSections.Export;
            Starts = newSections.Start;
            Elements = newSections.Element;
            // ReSharper disable ImpureMethodCallOnReadonlyValueField
            Code = ClassicSections.OfType<CodeSection>().ToImmutableArray();
            Data = ClassicSections.OfType<Data>().ToImmutableArray();
            // ReSharper enable ImpureMethodCallOnReadonlyValueField
        }

        [ExcludeFromCodeCoverage]
        private static Tuple<ImmutableArray<ISection>,Core.Parser.Sections> ParseSelections(BinaryReader reader)
        {
            var sections = ImmutableArray.CreateBuilder<ISection>();
            var newSections = new Core.Parser.Sections();
            while (reader.BaseStream.Position != reader.BaseStream.Length)
            {
                var id = ParseTools.ToSectionCode(reader);
                var len = Values.ToUInt(reader);
                using (var subReader = ParseTools.ToReader(reader, len))
                {
                    switch (id)
                    {
                        case SectionCode.Custom:
                            newSections.ParseCustom(subReader);
                            break;
                        case SectionCode.Type:
                            newSections.ParseType(subReader);
                            break;
                        case SectionCode.Import:
                            newSections.ParseImport(subReader);
                            break;
                        case SectionCode.Function:
                            newSections.ParseFunction(subReader);
                            break;
                        case SectionCode.Table:
                            newSections.ParseTable(subReader);
                            break;
                        case SectionCode.Memory:
                            newSections.ParseMemory(subReader);
                            break;
                        case SectionCode.Global:
                            newSections.ParseGlobal(subReader);
                            break;
                        case SectionCode.Export:
                            newSections.ParseExport(subReader);
                            break;
                        case SectionCode.Start:
                            newSections.ParseStart(subReader);
                            break;
                        case SectionCode.Element:
                            newSections.ParseElement(subReader);
                            break;
                        case SectionCode.Code:
                            sections.Add(new CodeSection(subReader));
                            break;
                        case SectionCode.Data:
                            sections.Add(new Data(subReader));
                            break;
                        default:
                            throw new NotImplementedException();
                    }
                }
            }

            sections.Capacity = sections.Count;
            return new Tuple<ImmutableArray<ISection>, Core.Parser.Sections>(sections.MoveToImmutable(), newSections);
        }
    }
}
