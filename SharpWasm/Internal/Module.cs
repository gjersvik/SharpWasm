using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("SharpWasm.Tests")]

namespace SharpWasm.Internal
{
    internal class Module
    {
        public readonly Header Header;
        public readonly ImmutableList<ISection> Sections;

        public readonly Types Types;
        public readonly FunctionSelection Functions;
        public readonly Imports Imports;
        public readonly Exports Exports;
        public readonly Code Code;
        public readonly Data Data;
        public readonly ImmutableList<CustomSection> Custom;

        public Module(Header header, IEnumerable<ISection> sections)
        {
            Header = header;
            Sections = sections.ToImmutableList();
            Types = Sections.Find(s => s.Id == SectionId.Type) as Types ?? Types.Empty;
            Functions = Sections.Find(s => s.Id == SectionId.Function) as FunctionSelection ?? FunctionSelection.Empty;
            Imports = Sections.Find(s => s.Id == SectionId.Import) as Imports ?? Imports.Empty;
            Exports = Sections.Find(s => s.Id == SectionId.Export) as Exports ?? Exports.Empty;
            Code = Sections.Find(s => s.Id == SectionId.Code) as Code ?? Code.Empty;
            Data = Sections.Find(s => s.Id == SectionId.Data) as Data ?? Data.Empty;
            Custom = Sections.Where(s => s.Id == SectionId.Custom).Cast<CustomSection>().ToImmutableList();
        }

        public IEnumerable<CustomSection> ByName(string name)
        {
            return Custom.Where(cs => cs.Name == name);
        }

        public AFunction GetFunction(uint id)
        {
            if (id < Imports.FunctionCount)
            {
                var import = Imports.Functions[(int)id];
                return new ImportFunction(id, Types.TypeList[(int)import.TypeIndex], import.Module, import.Field);
            }
            var baseId = (int) (id - Imports.FunctionCount);
            return new Function(id, Code.Bodies[baseId].Code, Types.TypeList[(int)Functions.FunctionList[baseId]]);
        }
        public Function GetFunction(string name)
        {
            return GetFunction(Exports.Func(name)) as Function;
        }
    }
}
