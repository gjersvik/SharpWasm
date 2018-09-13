using SharpWasm.Core.Runtime;
// ReSharper disable  MemberCanBeMadeStatic.Global
// ReSharper disable UnusedParameter.Global

namespace SharpWasm.Core
{
    public class ModuleInstance
    {
        public ExternalValue GetExport(string name)
        {
            return new ExternalValue();
        }
    }
}