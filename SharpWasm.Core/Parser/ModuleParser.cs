using System;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using SharpWasm.Core.Segments;

namespace SharpWasm.Core.Parser
{
    internal static class ModuleParser
    {
        public static Module ToModule(byte[] bytes)
        {
            using (var reader = Tools.FromBytes(bytes))
            {
                if (reader.ReadUInt32() != 0x6d736100) throw new Exception("Wrong magic number.");
                if (reader.ReadUInt32() != 1) throw new Exception("Only support version 1");

                var sections = ToSections(reader);
                var function = sections.Function
                    .Zip(sections.Code, (index, code) => new Function(index, code.Locals, code.Code))
                    .ToImmutableArray();

                return new Module(
                    sections.Custom,
                    sections.Type,
                    function,
                    sections.Table,
                    sections.Memory,
                    sections.Global,
                    sections.Element,
                    sections.Data,
                    sections.Start,
                    sections.Import,
                    sections.Export
                );
            }
        }

        public static Sections ToSections(BinaryReader reader)
        {
            var newSections = new Sections();
            while (reader.BaseStream.Position != reader.BaseStream.Length)
            {
                var id = ToSectionCode(reader);
                var len = Values.ToUInt(reader);
                using (var subReader = Tools.ToReader(reader, len))
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
                            newSections.ParseCode(subReader);
                            break;
                        case SectionCode.Data:
                            newSections.ParseData(subReader);
                            break;
                        default:
                            throw new NotImplementedException();
                    }
                }
            }

            return newSections;
        }

        private static SectionCode ToSectionCode(BinaryReader reader)
        {
            return (SectionCode) Values.ToByte(reader);
        }
    }

    internal enum SectionCode : byte
    {
        Custom = 0,
        Type,
        Import,
        Function,
        Table,
        Memory,
        Global,
        Export,
        Start,
        Element,
        Code,
        Data
    }
}