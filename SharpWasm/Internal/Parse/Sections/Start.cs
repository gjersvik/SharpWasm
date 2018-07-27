using System.IO;

namespace SharpWasm.Internal.Parse.Sections
{
    internal class Start: ISection
    {
        public SectionCode Id { get; } = SectionCode.Start;
        public readonly uint Index;

        public Start(uint index)
        {
            Index = index;
        }

        public Start(BinaryReader reader)
        {
            Index = VarIntUnsigned.ToUInt(reader);
        }
    }
}
