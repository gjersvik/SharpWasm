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
        public readonly Exports Exports;
        public readonly Code Code;
        public readonly ImmutableList<CustomSection> Custom;

        public Module(Header header, IEnumerable<ISection> sections)
        {
            Header = header;
            Sections = sections.ToImmutableList();
            Types = Sections.Find(s => s.Id == SectionId.Type) as Types ?? Types.Empty;
            Functions = Sections.Find(s => s.Id == SectionId.Function) as FunctionSelection ?? FunctionSelection.Empty;
            Exports = Sections.Find(s => s.Id == SectionId.Export) as Exports ?? Exports.Empty;
            Code = Sections.Find(s => s.Id == SectionId.Code) as Code ?? Code.Empty;
            Custom = Sections.Where(s => s.Id == SectionId.Custom).Cast<CustomSection>().ToImmutableList();
        }

        public IEnumerable<CustomSection> ByName(string name)
        {
            return Custom.Where(cs => cs.Name == name);
        }

        public Function GetFunction(uint id)
        {
            return new Function(id, Code.Bodies[(int)id].Code, Types.TypeList[(int)Functions.FunctionList[(int)id]]);
        }
        public Function GetFunction(string name)
        {
            return GetFunction(Exports.Func(name));
        }
    }
}
