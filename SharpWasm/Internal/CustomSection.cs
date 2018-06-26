namespace SharpWasm.Internal
{
    internal class CustomSection: ISection
    {
        public SectionId Id { get; } = SectionId.Custom;
        public byte[] Payload { get; }

        public readonly string Name;

        public CustomSection(string name, byte[] payload)
        {
            Name = name;
            Payload = payload;
        }
    }
}
