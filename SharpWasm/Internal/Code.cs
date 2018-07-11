using System.Collections.Immutable;
using SharpWasm.Internal.Parse.Sections;

namespace SharpWasm.Internal
{
    internal class Code: ISection
    {
        public static readonly Code Empty = new Code();

        public SectionCode Id { get; } = SectionCode.Code;
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
