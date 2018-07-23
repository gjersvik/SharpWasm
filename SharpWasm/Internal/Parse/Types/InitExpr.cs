using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using SharpWasm.Internal.Parse.Code;

namespace SharpWasm.Internal.Parse.Types
{
    internal class InitExpr
    {
        public readonly ImmutableArray<IInstruction> Instructions;

        public InitExpr(IEnumerable<IInstruction> instructions)
        {
            Instructions = instructions.ToImmutableArray();
        }

        public InitExpr(BinaryReader reader)
        {
            var builder = ImmutableArray.CreateBuilder<IInstruction>(2);
            do
            {
                builder.Add(Instruction.Parse(reader));
            } while (builder.Last().OpCode != OpCode.End);

            builder.Capacity = builder.Count;

            Instructions = builder.MoveToImmutable();
        }
    }
}
