using System.IO;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("SharpWasm.Tests")]

namespace SharpWasm.Internal
{
    internal class Module
    {
        public static Module Parse(byte[] wasm)
        {
            return Parse(new MemoryStream(wasm));
        }

        public static Module Parse(Stream wasm)
        {
            var header = Header.ParseHeader(wasm);
            return new Module(header);
        }

        public readonly Header Header;

        public Module(Header header)
        {
            Header = header;
        }

        public bool IsValid()
        {
            return Header.IsValid();
        }
    }
}
