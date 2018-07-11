using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;

namespace SharpWasm.Internal.Parse.Sections
{
    internal class Custom: ISection
    {
        public SectionCode Id { get; } = SectionCode.Custom;
        public readonly uint NameLen;
        public readonly string Name;
        public readonly ImmutableArray<byte> PayloadData;

        public Custom(string name, IEnumerable<byte> payloadData)
        {
            Name = name;
            PayloadData = payloadData.ToImmutableArray();
            NameLen = (uint)Name.Length;
        }

        public Custom(BinaryReader reader)
        {
            NameLen = VarIntUnsigned.ToUInt(reader);
            Name = ParseTools.ToUtf8(reader, NameLen);
            PayloadData = ParseTools.ToBytes(reader);
        }
    }
}
