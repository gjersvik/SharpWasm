using System.Collections.Generic;
using System.Linq;
using SharpWasm.Internal;

namespace SharpWasm
{
    public class WebAssemblyModule
    {
        public byte[] CustomSections(string name)
        {
            return _module.ByName(name).ToArray();
        }

        public IEnumerable<ModuleExportDescriptor> Exports()
        {
            return _module.Export.Entries.Select(e => new ModuleExportDescriptor(e));
        }

        private readonly Module _module;

        internal WebAssemblyModule(Module module)
        {
            _module = module;
        }

        public WebAssemblyInstance Instantiate(WebAssemblyImports imports = null)
        {
            return new WebAssemblyInstance(_module, imports);
        }
    }
}