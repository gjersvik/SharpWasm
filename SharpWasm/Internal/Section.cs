namespace SharpWasm.Internal
{
    internal class Section: ISection
    {
        public Section(SectionId id)
        {
            Id = id;
        }

        public SectionId Id { get; }
    }
}
