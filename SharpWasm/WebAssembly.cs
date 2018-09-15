using System;
using System.Diagnostics.CodeAnalysis;
using SharpWasm.Internal;
using SharpWasm.Internal.Parse;

namespace SharpWasm
{
    public static class WebAssembly
    {
        public static WebAssemblyModule Compile(byte[] wasm)
        {
            using (var reader = ParseTools.FromBytes(wasm))
            {
                return new WebAssemblyModule(new Module(new ParseModule(reader)));
            }
        }

        [ExcludeFromCodeCoverage]
        public static bool Validate(byte[] wasm)
        {
            using (var reader = ParseTools.FromBytes(wasm))
            {
                try
                {
                    // ReSharper disable once ObjectCreationAsStatement
                    new ParseModule(reader);
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
