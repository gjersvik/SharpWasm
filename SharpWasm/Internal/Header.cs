namespace SharpWasm.Internal
{
    internal class Header
    {
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
