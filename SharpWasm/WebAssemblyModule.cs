using System;
using System.Collections.Generic;
using System.Linq;
using SharpWasm.Internal;

namespace SharpWasm
{
    public class WebAssemblyModule
    {
        public IEnumerable<byte[]> CustomSections(string name)
        {
            return _module.ByName(name).Select(cs => cs.PayloadData.ToArray());
        }

        public IEnumerable<ModuleExportDescriptor> Exports()
        {
            return _module.Exports.ExportList.Select(e => new ModuleExportDescriptor(e));
        }

        public IEnumerable<ModuleImportDescriptor> Import()
        {
            throw new NotImplementedException();
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