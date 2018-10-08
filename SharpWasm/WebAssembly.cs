using System;
using System.Diagnostics.CodeAnalysis;
using SharpWasm.Internal;

namespace SharpWasm
{
    public static class WebAssembly
    {
        public static WebAssemblyModule Compile(byte[] wasm)
        {
            return new WebAssemblyModule(new Module(Core.Module.Decode(wasm)));
        }

        [ExcludeFromCodeCoverage]
        public static bool Validate(byte[] wasm)
        {
            try
            {
                return Core.Module.Decode(wasm).Validate() is null;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
