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
