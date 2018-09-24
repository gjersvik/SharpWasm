using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using SharpWasm.Core.Types;
using SharpWasm.Internal.Parse;
using SharpWasm.Internal.Parse.Sections;
using FunctionSection = SharpWasm.Internal.Parse.Sections.Function;

namespace SharpWasm.Internal
{
    internal class Module
    {
        private readonly ImmutableArray<FunctionType> _type;
        public readonly Import Import;
        private readonly FunctionSection _function;
        public readonly Table Table;
        public readonly Export Export;
        public readonly Element Element;
        private readonly CodeSection _code;
        public readonly Data Data;
        private readonly ImmutableDictionary<string, ImmutableArray<byte>> _custom;

        public Module(ParseModule parsed)
        {
            _type = parsed.Types;
            Import = parsed.Imports.FirstOrDefault() ?? Import.Empty;
            _function = parsed.Functions.FirstOrDefault() ?? FunctionSection.Empty;
            Table = parsed.Tables.FirstOrDefault() ?? Table.Empty;
            Export = parsed.Exports.FirstOrDefault() ?? Export.Empty;
            Element = parsed.Elements.FirstOrDefault() ?? Element.Empty;
            _code = parsed.Code.FirstOrDefault() ?? CodeSection.Empty;
            Data = parsed.Data.FirstOrDefault() ?? Data.Empty;
            _custom = parsed.Customs;
        }

        public ImmutableArray<byte> ByName(string name)
        {
            return _custom.ContainsKey(name) ? _custom[name] : ImmutableArray<byte>.Empty;
        }

        public AFunction GetFunction(uint id)
        {
            if (id < Import.Functions.Length)
            {
                var import = Import.Functions[(int) id];
                return new ImportFunction(id, _type[(int) import.Type], import.ModuleStr, import.FieldStr,
                    import.Type);
            }

            var baseId = (int) (id - Import.Functions.Length);
            return new Function(id, _code.Bodies[baseId].Code, _type[(int) _function.Types[baseId]],
                _function.Types[baseId]);
        }

        [ExcludeFromCodeCoverage]
        public Function GetFunction(string name)
        {
            var possibleIndex = Export.Func(name);
            if (possibleIndex is uint index) return GetFunction(index) as Function;
            return null;
        }
    }
}