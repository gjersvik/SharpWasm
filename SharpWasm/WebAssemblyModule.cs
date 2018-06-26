using System;
using System.Collections.Generic;

namespace SharpWasm
{
    public partial class WebAssemblyModule
    {
        public static byte[] CustomSections(WebAssemblyModule module, string name)
        {
            throw new NotImplementedException();
        }
        public static IEnumerable<ModuleExportDescriptor> Exports(WebAssemblyModule module)
        {
            throw new NotImplementedException();
        }
        public static IEnumerable<ModuleImportDescriptor> Import(WebAssemblyModule module)
        {
            throw new NotImplementedException();
        }
    }
}