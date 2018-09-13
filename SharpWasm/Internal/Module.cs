using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using SharpWasm.Internal.Parse;
using SharpWasm.Internal.Parse.Sections;
using FunctionSection = SharpWasm.Internal.Parse.Sections.Function;

namespace SharpWasm.Internal
{
    internal class Module
    {
        private readonly Type _type;
        public readonly Import Import;
        private readonly FunctionSection _function;
        public readonly Table Table;
        public readonly Export Export;
        public readonly Element Element;
        private readonly CodeSection _code;
        public readonly Data Data;
        private readonly ImmutableArray<Custom> _custom;

        public Module(ParseModule parsed)
        {
            _type = parsed.Types.FirstOrDefault() ?? Type.Empty;
            Import = parsed.Imports.FirstOrDefault() ?? Import.Empty;
            _function = parsed.Functions.FirstOrDefault() ?? FunctionSection.Empty;
            Table = parsed.Tables.FirstOrDefault() ?? Table.Empty;
            Export = parsed.Exports.FirstOrDefault() ?? Export.Empty;
            Element = parsed.Elements.FirstOrDefault() ?? Element.Empty;
            _code = parsed.Code.FirstOrDefault() ?? CodeSection.Empty;
            Data = parsed.Data.FirstOrDefault() ?? Data.Empty;
            _custom = parsed.Customs;
        }

        public IEnumerable<Custom> ByName(string name)
        {
            return _custom.Where(cs => cs.Name == name);
        }

        public AFunction GetFunction(uint id)
        {
            if (id < Import.Functions.Length)
            {
                var import = Import.Functions[(int) id];
                return new ImportFunction(id, _type.Entries[(int) import.Type], import.ModuleStr, import.FieldStr,
                    import.Type);
            }

            var baseId = (int) (id - Import.Functions.Length);
            return new Function(id, _code.Bodies[baseId].Code, _type.Entries[(int) _function.Types[baseId]],
                _function.Types[baseId]);
        }

        public Function GetFunction(string name)
        {
            var possibleIndex = Export.Func(name);
            if (possibleIndex is uint index) return GetFunction(index) as Function;
            return null;
        }
    }
}