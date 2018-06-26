namespace SharpWasm.Internal
{
    internal class CustomSection: ISection
    {
        public SectionId Id { get; } = SectionId.Custom;
        public byte[] Payload { get; }

        public readonly string Name;

        public CustomSection(byte[] payload)
        {
            using (var reader = new WasmReader(payload))
            {
                Name = reader.ReadString();
                Payload = reader.ReadRest();
            }
        }
    }
}
