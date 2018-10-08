using System.IO;

namespace SharpWasm.Core.Parser
{
    internal static class Tools
    {
        private static BinaryReader FromBytes(byte[] bytes)
        {
            return new BinaryReader(new MemoryStream(bytes));
        }

        public static BinaryReader ToReader(BinaryReader reader, uint length)
        {
            return FromBytes(reader.ReadBytes((int)length));
        }
    }
}
