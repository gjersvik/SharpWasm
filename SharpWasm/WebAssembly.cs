using System;
using SharpWasm.Internal;

namespace SharpWasm
{
    public static class WebAssembly
    {
        public static WebAssemblyModule Compile(byte[] wasm)
        {
            using (var reader = new WasmReader(wasm))
            {
                return new WebAssemblyModule(reader.ReadModule());
            }
        }
        public static WebAssemblyInstance Instantiate(byte[] wasm, WebAssemblyImports importObject = null)
        {
            return Instantiate(Compile(wasm), importObject);
        }
        public static WebAssemblyInstance Instantiate(WebAssemblyModule module, WebAssemblyImports importObject = null)
        {
            return module.Instantiate(importObject);
        }
        public static bool Validate(byte[] wasm)
        {
            using (var reader = new WasmReader(wasm))
            {
                try
                {
                    reader.ReadModule();
                }
                catch (Exception)
                {
                    return false;
                }

                return true;
            }
        }
    }
}
