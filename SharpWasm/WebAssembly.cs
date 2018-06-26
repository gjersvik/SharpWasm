using System;

namespace SharpWasm
{
    public static class WebAssembly
    {
        public static WebAssemblyModule Compile(byte[] wasm)
        {
            throw new NotImplementedException();
        }
        public static WebAssemblyInstance Instantiate(byte[] wasm, WebAssemblyImports importObject = null)
        {
            return Instantiate(Compile(wasm), importObject);
        }
        public static WebAssemblyInstance Instantiate(WebAssemblyModule wasm, WebAssemblyImports importObject = null)
        {
            throw new NotImplementedException();
        }
        public static bool Validate(byte[] wasm)
        {
            throw new NotImplementedException();
        }
    }
}
