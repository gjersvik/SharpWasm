namespace SharpWasm.Internal
{
    internal class Section: ISection
    {
        public Section(SectionId id, byte[] payload)
        {
            Id = id;
            Payload = payload;
        }

        public SectionId Id { get; }
        public byte[] Payload { get; }
    }
}
