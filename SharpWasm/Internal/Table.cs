using SharpWasm.Internal.Parse;

namespace SharpWasm.Internal
{
    internal class Table: ISection
    {
        public SectionId Id { get; } = SectionId.Table;
        public readonly uint Initial;
        public readonly uint Maximum;

        public Table(byte[] payload) : this(FromPayload(payload))
        {
        }

        private Table(ResizableLimits limits)
        {
            Initial = limits.Initial;
            Maximum = limits.Maximum;
        }

        private static ResizableLimits FromPayload(byte[] payload)
        {
            using (var reader = new WasmReader(payload))
            {
                return reader.ReadResizableLimits();
            }
        }
    }
}
