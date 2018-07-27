using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using SharpWasm.Internal.Parse;
using SharpWasm.Internal.Parse.Sections;
using FunctionSection = SharpWasm.Internal.Parse.Sections.Function;

[assembly: InternalsVisibleTo("SharpWasm.Tests")]

namespace SharpWasm.Internal
{
    internal class Module
    {
        public readonly Type Type;
        public readonly Import Import;
        public readonly FunctionSection Function;
        public readonly Table Table;
        public readonly Export Export;
        public readonly Element Element;
        public readonly CodeSection CodeSection;
        public readonly Data Data;
        public readonly ImmutableArray<Custom> Custom;

        public Module(ParseModule parsed)
        {
            Type = parsed.Types.FirstOrDefault() ?? Type.Empty;
            Import = parsed.Imports.FirstOrDefault() ?? Import.Empty;
            Function = parsed.Functions.FirstOrDefault() ?? FunctionSection.Empty;
            Table = parsed.Tables.FirstOrDefault() ?? Table.Empty;
            Export = parsed.Exports.FirstOrDefault() ?? Export.Empty;
            Element = parsed.Elements.FirstOrDefault() ?? Element.Empty;
            CodeSection = parsed.Code.FirstOrDefault() ?? CodeSection.Empty;
            Data = parsed.Data.FirstOrDefault() ?? Data.Empty;
            Custom = parsed.Customs;
        }

        public IEnumerable<Custom> ByName(string name)
        {
            return Custom.Where(cs => cs.Name == name);
        }

        public AFunction GetFunction(uint id)
        {
            if (id < Import.Functions.Length)
            {
                var import = Import.Functions[(int) id];
                return new ImportFunction(id, Type.Entries[(int) import.Type], import.ModuleStr, import.FieldStr,
                    import.Type);
            }

            var baseId = (int) (id - Import.Functions.Length);
            return new Function(id, CodeSection.Bodies[baseId].Code.ToArray(), Type.Entries[(int) Function.Types[baseId]],
                Function.Types[baseId]);
        }

        public Function GetFunction(string name)
        {
            var possibleIndex = Export.Func(name);
            if (possibleIndex is uint index) return GetFunction(index) as Function;
            return null;
        }
    }
}