using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using SharpWasm.Core.Parser;
using SharpWasm.Internal.Parse.Sections;
using Type = SharpWasm.Internal.Parse.Sections.Type;

namespace SharpWasm.Internal.Parse
{
    internal class ParseModule
    {
        public readonly uint MagicNumber;
        public readonly uint Version;
        public readonly ImmutableArray<ISection> Sections;

        public readonly ImmutableArray<Custom> Customs;
        public readonly ImmutableArray<Type> Types;
        public readonly ImmutableArray<Import> Imports;
        public readonly ImmutableArray<Sections.Function> Functions;
        public readonly ImmutableArray<Table> Tables;
        public readonly ImmutableArray<Memory> Memories;
        public readonly ImmutableArray<Global> Globals;
        public readonly ImmutableArray<Export> Exports;
        public readonly ImmutableArray<Start> Starts;
        public readonly ImmutableArray<Element> Elements;
        public readonly ImmutableArray<CodeSection> Code;
        public readonly ImmutableArray<Data> Data;

        public ParseModule(IEnumerable<ISection> sections) : this(0x6d736100, 0x1, sections.ToImmutableArray())
        {
        }

        public ParseModule(BinaryReader reader) : this(reader.ReadUInt32(), reader.ReadUInt32(), ParseSelections(reader))
        {
        }

        private ParseModule(uint magicNumber, uint version, ImmutableArray<ISection> sections)
        {
            MagicNumber = magicNumber;
            Version = version;
            Sections = sections;

            Customs = sections.OfType<Custom>().ToImmutableArray();
            Types = sections.OfType<Type>().ToImmutableArray();
            Imports = sections.OfType<Import>().ToImmutableArray();
            Functions = sections.OfType<Sections.Function>().ToImmutableArray();
            Tables = sections.OfType<Table>().ToImmutableArray();
            Memories = sections.OfType<Memory>().ToImmutableArray();
            Globals = sections.OfType<Global>().ToImmutableArray();
            Exports = sections.OfType<Export>().ToImmutableArray();
            Starts = sections.OfType<Start>().ToImmutableArray();
            Elements = sections.OfType<Element>().ToImmutableArray();
            Code = sections.OfType<CodeSection>().ToImmutableArray();
            Data = sections.OfType<Data>().ToImmutableArray();
        }

        [ExcludeFromCodeCoverage]
        private static ImmutableArray<ISection> ParseSelections(BinaryReader reader)
        {
            var sections = ImmutableArray.CreateBuilder<ISection>();
            while (reader.BaseStream.Position != reader.BaseStream.Length)
            {
                var id = ParseTools.ToSectionCode(reader);
                var len = Values.ToUInt(reader);
                using (var subReader = ParseTools.ToReader(reader, len))
                {
                    switch (id)
                    {
                        case SectionCode.Custom:
                            sections.Add(new Custom(subReader));
                            break;
                        case SectionCode.Type:
                            sections.Add(new Type(subReader));
                            break;
                        case SectionCode.Import:
                            sections.Add(new Import(subReader));
                            break;
                        case SectionCode.Function:
                            sections.Add(new Sections.Function(subReader));
                            break;
                        case SectionCode.Table:
                            sections.Add(new Table(subReader));
                            break;
                        case SectionCode.Memory:
                            sections.Add(new Memory(subReader));
                            break;
                        case SectionCode.Global:
                            sections.Add(new Global(subReader));
                            break;
                        case SectionCode.Export:
                            sections.Add(new Export(subReader));
                            break;
                        case SectionCode.Start:
                            sections.Add(new Start(subReader));
                            break;
                        case SectionCode.Element:
                            sections.Add(new Element(subReader));
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
            return sections.MoveToImmutable();
        }
    }
}
