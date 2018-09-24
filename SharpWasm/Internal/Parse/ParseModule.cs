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
        public readonly ImmutableArray<ISection> ClassicSections;

        public readonly ImmutableDictionary<string, ImmutableArray<byte>> Customs;
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
            // ReSharper disable ImpureMethodCallOnReadonlyValueField
            Types = ClassicSections.OfType<Type>().ToImmutableArray();
            Imports = ClassicSections.OfType<Import>().ToImmutableArray();
            Functions = ClassicSections.OfType<Sections.Function>().ToImmutableArray();
            Tables = ClassicSections.OfType<Table>().ToImmutableArray();
            Memories = ClassicSections.OfType<Memory>().ToImmutableArray();
            Globals = ClassicSections.OfType<Global>().ToImmutableArray();
            Exports = ClassicSections.OfType<Export>().ToImmutableArray();
            Starts = ClassicSections.OfType<Start>().ToImmutableArray();
            Elements = ClassicSections.OfType<Element>().ToImmutableArray();
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
            return new Tuple<ImmutableArray<ISection>, Core.Parser.Sections>(sections.MoveToImmutable(), newSections);
        }
    }
}
