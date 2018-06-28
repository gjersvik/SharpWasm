using SharpWasm.Internal;

namespace SharpWasm
{
    public class WebAssemblyInstance
    {
        private readonly Module _module;
        private readonly VirtualMachine _vm = new VirtualMachine();

        internal WebAssemblyInstance(Module module)
        {
            _module = module;
        }

        public int Run(string name)
        {
            var function = _module.Exports.Func(name);
            return _vm.Run(_module.Code.Bodies[function]);
        }
    }
}