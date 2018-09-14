using System.IO;
using SharpWasm.Core.Parser;

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
            Index = Values.ToUInt(reader);
        }
    }
}
