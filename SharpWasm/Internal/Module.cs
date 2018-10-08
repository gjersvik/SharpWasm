using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using SharpWasm.Core.Parser;
using SharpWasm.Core.Segments;
using SharpWasm.Core.Types;
using SharpWasm.Internal.Parse;
using Data = SharpWasm.Internal.Parse.Sections.Data;

namespace SharpWasm.Internal
{
    internal class Module
    {
        private readonly ImmutableArray<FunctionType> _type;
        public readonly ImmutableArray<Import> Import;
        private readonly ImmutableArray<uint> _function;
        public readonly ImmutableArray<TableType> Table;
        public readonly ImmutableArray<Export> Export;
        public readonly ImmutableArray<Element> Element;
        private readonly ImmutableArray<CodeSection> _code;
        public readonly Data Data;
        private readonly ImmutableDictionary<string, ImmutableArray<byte>> _custom;

        public Module(ParseModule parsed)
        {
            _type = parsed.Types;
            Import = parsed.Imports;
            _function = parsed.Functions;
            Table = parsed.Tables;
            Export = parsed.Exports;
            Element = parsed.Elements;
            _code = parsed.Code;
            Data = parsed.Data.FirstOrDefault() ?? Data.Empty;
            _custom = parsed.Customs;
        }

        public ImmutableArray<byte> ByName(string name)
        {
            return _custom.ContainsKey(name) ? _custom[name] : ImmutableArray<byte>.Empty;
        }

        public AFunction GetFunction(uint id)
        {
            var imports = Import.Where(i => i.Type == ExternalKind.Function).ToImmutableArray();
            var importFunctions = Import.Where(i => i.Function != null).Select(i => i.Function).Cast<uint>()
                .ToImmutableArray();
            if (id < imports.Length)
            {
                var import = imports[(int) id];
                return new ImportFunction(id, _type[(int) importFunctions[(int) id]], import.Module, import.Name,
                    importFunctions[(int) id]);
            }

            var baseId = (int) (id - Import.Count(i => i.Type == ExternalKind.Function));
            return new Function(id, _code[baseId].Code, _type[(int) _function[baseId]],
                _function[baseId]);
        }

        [ExcludeFromCodeCoverage]
        public Function GetFunction(string name)
        {
            var possibleIndex = Export.Where(e => e.Type == ExternalKind.Function).SingleOrDefault(e => e.Name == name)
                ?.Index;
            if (possibleIndex is uint index) return GetFunction(index) as Function;
            return null;
        }
    }
}