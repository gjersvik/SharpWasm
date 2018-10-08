using System.Collections.Immutable;
using SharpWasm.Core.Code;
using SharpWasm.Core.Types;

namespace SharpWasm.Core.Parser
{
    internal class CodeSection
    {
        public readonly ImmutableArray<ValueType> Locals;
        public readonly ImmutableArray<IInstruction> Code;

        public CodeSection(ImmutableArray<ValueType> locals, ImmutableArray<IInstruction> code)
        {
            Locals = locals;
            Code = code;
        }
    }
}
