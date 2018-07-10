using System.Collections.Immutable;

namespace SharpWasm.Internal
{
    internal class FunctionSelection : ISection
    {
        public static FunctionSelection Empty { get; } = new FunctionSelection();

        public SectionId Id { get; } = SectionId.Function;

        public ImmutableArray<uint> FunctionList;
        public FunctionSelection(byte[] payload)
        {
            using (var reader = new WasmReader(payload))
            {
                FunctionList = reader.ReadFunction().ToImmutableArray();
            }
        }

        private FunctionSelection()
        {
            FunctionList = ImmutableArray<uint>.Empty;
        }

    }
}
