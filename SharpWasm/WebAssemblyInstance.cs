using SharpWasm.Internal;

namespace SharpWasm
{
    public class WebAssemblyInstance
    {
        private readonly Module _module;
        private readonly VirtualMachine _vm;

        internal WebAssemblyInstance(Module module, WebAssemblyImports imports)
        {
            _module = module;
            _vm = new VirtualMachine(_module);
        }

        public int Run(string name, params int[] args)
        {
            var function = _module.GetFunction(name);
            return _vm.Run(function, args);
        }
    }
}