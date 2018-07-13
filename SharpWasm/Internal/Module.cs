using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using SharpWasm.Internal.Parse.Sections;

[assembly: InternalsVisibleTo("SharpWasm.Tests")]

namespace SharpWasm.Internal
{
    internal class Module
    {
        public readonly Header Header;
        public readonly ImmutableList<ISection> Sections;

        public readonly Type Type;
        public readonly Import Import;
        public readonly FunctionSelection Functions;
        public readonly Table Table;
        public readonly Exports Exports;
        public readonly Element Element;
        public readonly Code Code;
        public readonly Data Data;
        public readonly ImmutableList<Custom> Custom;

        public Module(Header header, IEnumerable<ISection> sections)
        {
            Header = header;
            Sections = sections.ToImmutableList();
            Type = Sections.Find(s => s.Id == SectionCode.Type) as Type ?? Type.Empty;
            Import = Sections.Find(s => s.Id == SectionCode.Import) as Import ?? Import.Empty;
            Functions = Sections.Find(s => s.Id == SectionCode.Function) as FunctionSelection ?? FunctionSelection.Empty;
            Table = Sections.Find(s => s.Id == SectionCode.Table) as Table;
            Exports = Sections.Find(s => s.Id == SectionCode.Export) as Exports ?? Exports.Empty;
            Element = Sections.Find(s => s.Id == SectionCode.Element) as Element ?? Element.Empty;
            Code = Sections.Find(s => s.Id == SectionCode.Code) as Code ?? Code.Empty;
            Data = Sections.Find(s => s.Id == SectionCode.Data) as Data ?? Data.Empty;
            Custom = Sections.Where(s => s.Id == SectionCode.Custom).Cast<Custom>().ToImmutableList();
        }

        public IEnumerable<Custom> ByName(string name)
        {
            return Custom.Where(cs => cs.Name == name);
        }

        public AFunction GetFunction(uint id)
        {
            if (id < Import.Functions.Length)
            {
                var import = Import.Functions[(int)id];
                return new ImportFunction(id, Type.Entries[(int)import.Type], import.ModuleStr, import.FieldStr, import.Type);
            }
            var baseId = (int) (id - Import.Functions.Length);
            return new Function(id, Code.Bodies[baseId].Code, Type.Entries[(int)Functions.FunctionList[baseId]], Functions.FunctionList[baseId]);
        }
        public Function GetFunction(string name)
        {
            return GetFunction(Exports.Func(name)) as Function;
        }
    }
}
