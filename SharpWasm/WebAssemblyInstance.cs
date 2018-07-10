using SharpWasm.Internal;

namespace SharpWasm
{
    public class WebAssemblyInstance
    {
        internal readonly Module Module;
        private readonly VirtualMachine _vm;
        public readonly WebAssemblyImports Imports;

        public readonly WebAssemblyMemory Memory;

        internal WebAssemblyInstance(Module module, WebAssemblyImports imports)
        {
            Module = module;
            Imports = imports;
            _vm = new VirtualMachine(this);

            var memoryImport = Module.Imports.Memory;
            if (memoryImport == null) return;
            Memory = Imports.GetMemory(memoryImport);
            foreach (var segment in module.Data.DataSegments)
            {
                Memory.Write(segment);
            }
        }

        public int Run(string name, params int[] args)
        {
            var function = Module.GetFunction(name);
            return _vm.Run(function, args);
        }
    }
}