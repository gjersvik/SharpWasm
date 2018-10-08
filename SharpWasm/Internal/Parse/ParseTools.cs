using System.IO;
using SharpWasm.Core.Parser;
using SharpWasm.Internal.Parse.Sections;

namespace SharpWasm.Internal.Parse
{
    internal static class ParseTools
    {
        public static BinaryReader FromBytes(byte[] bytes)
        {
            return new BinaryReader(new MemoryStream(bytes));
        }

        public static BinaryReader ToReader(BinaryReader reader, uint length)
        {
            return FromBytes(reader.ReadBytes((int) length));
        }

        public static SectionCode ToSectionCode(BinaryReader reader) => (SectionCode)Values.ToByte(reader);
    }
}