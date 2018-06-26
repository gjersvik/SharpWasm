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
            return _module.Sections.Where(s => s.Id == SectionId.Custom).Cast<CustomSection>()
                .Where(cs => cs.Name == name).Select(cs => cs.Payload);
        }

        public IEnumerable<ModuleExportDescriptor> Exports()
        {
            throw new NotImplementedException();
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

        public WebAssemblyInstance Instantiate(WebAssemblyImports importObject = null)
        {
            throw new NotImplementedException();
        }
    }
}