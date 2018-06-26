using System.IO;

namespace SharpWasm.Internal
{
    internal class Header
    {
        public static Header ParseHeader(Stream wasm)
        {
            var reader = new BinaryReader(wasm);
            var mn = reader.ReadUInt32();
            var v = reader.ReadUInt32();
            return new Header(mn, v);
        }

        public readonly uint MagicNumber;
        public readonly uint Version;

        public Header(uint magicNumber, uint version)
        {
            MagicNumber = magicNumber;
            Version = version;
        }

        public bool IsValid()
        {
            if (MagicNumber != 0x6d736100) return false;
            return Version == 1;
        }
    }
}
