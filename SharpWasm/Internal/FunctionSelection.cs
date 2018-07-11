using System.Collections.Immutable;
using SharpWasm.Internal.Parse.Sections;

namespace SharpWasm.Internal
{
    internal class FunctionSelection : ISection
    {
        public static FunctionSelection Empty { get; } = new FunctionSelection();

        public SectionCode Id { get; } = SectionCode.Function;

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
