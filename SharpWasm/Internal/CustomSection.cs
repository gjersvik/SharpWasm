using SharpWasm.Internal.Parse.Sections;

namespace SharpWasm.Internal
{
    internal class CustomSection: ISection
    {
        public SectionCode Id { get; } = SectionCode.Custom;
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
