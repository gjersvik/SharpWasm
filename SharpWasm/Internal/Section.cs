using SharpWasm.Internal.Parse.Sections;

namespace SharpWasm.Internal
{
    internal class Section: ISection
    {
        public Section(SectionCode id)
        {
            Id = id;
        }

        public SectionCode Id { get; }
    }
}
