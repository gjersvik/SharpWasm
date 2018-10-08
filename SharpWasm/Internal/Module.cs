using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using SharpWasm.Core.Segments;
using SharpWasm.Core.Types;

namespace SharpWasm.Internal
{
    internal class Module
    {
        private readonly Core.Module _coreModule;

       
        public ImmutableArray<Import> Import => _coreModule.ImportsArray;
        public ImmutableArray<TableType> Table => _coreModule.Tables;
        public ImmutableArray<Export> Export => _coreModule.ExportsArray;
        public ImmutableArray<Element> Element => _coreModule.Elem;
        public ImmutableArray<Data> Data => _coreModule.Data;

        public Module(Core.Module module)
        {
            _coreModule = module;
        }

        public ImmutableArray<byte> ByName(string name)
        {
            return _coreModule.Custom.ContainsKey(name) ? _coreModule.Custom[name] : ImmutableArray<byte>.Empty;
        }

        public AFunction GetFunction(uint id)
        {
            var imports = Import.Where(i => i.Type == ExternalKind.Function).ToImmutableArray();
            var importFunctions = Import.Where(i => i.Function != null).Select(i => i.Function).Cast<uint>()
                .ToImmutableArray();
            if (id < imports.Length)
            {
                var import = imports[(int) id];
                return new ImportFunction(id, _coreModule.Types[(int) importFunctions[(int) id]], import.Module, import.Name,
                    importFunctions[(int) id]);
            }

            var baseId = (int) (id - Import.Count(i => i.Type == ExternalKind.Function));
            return new Function(id, _coreModule.Funcs[baseId].Body, _coreModule.Types[(int)_coreModule.Funcs[baseId].TypeIndex],
                _coreModule.Funcs[baseId].TypeIndex);
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