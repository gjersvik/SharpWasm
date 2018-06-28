using System.Collections.Immutable;

namespace SharpWasm.Internal
{
    internal class Code: ISection
    {
        public static readonly Code Empty = new Code();

        public SectionId Id { get; } = SectionId.Code;
        public ImmutableArray<FunctionBody> Bodies;

        public Code(byte[] payload)
        {
            using (var reader = new WasmReader(payload))
            {
                Bodies = reader.ReadFunctionBodies().ToImmutableArray();
            }
        }

        private Code()
        {
            Bodies = ImmutableArray<FunctionBody>.Empty;
        }
    }

    internal class FunctionBody
    {
        public readonly byte[] Code;

        public FunctionBody(byte[] code)
        {
            Code = code;
        }
    }
}
