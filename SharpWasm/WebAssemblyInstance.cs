using SharpWasm.Internal;

namespace SharpWasm
{
    public class WebAssemblyInstance
    {
        internal readonly Module Module;
        private readonly VirtualMachine _vm;
        public readonly WebAssemblyImports Imports;

        public readonly WebAssemblyMemory Memory;

        public readonly WebAssemblyTable Table;

        internal WebAssemblyInstance(Module module, WebAssemblyImports imports)
        {
            Module = module;
            Imports = imports;
            _vm = new VirtualMachine(this);

            var memoryImport = Module.Import?.Memory;
            if (memoryImport != null)
            {
                Memory = Imports.GetMemory(memoryImport);
                foreach (var segment in module.Data.DataSegments)
                {
                    Memory.Write(segment);
                }
            }

            var table = Module.Table;
            if (table != null)
            {
                Table = new WebAssemblyTable(table.Initial);
                foreach (var segment in module.Element.ElementSegments)
                {
                    Table.Write(segment);
                }
            }
        }

        public int Run(string name, params int[] args)
        {
            var function = Module.GetFunction(name);
            return _vm.Run(function, args);
        }
    }
}