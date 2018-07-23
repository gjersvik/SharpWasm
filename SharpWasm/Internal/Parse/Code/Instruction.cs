using System.IO;

namespace SharpWasm.Internal.Parse.Code
{
    internal interface IInstruction
    {
        OpCode OpCode { get; }
        bool HaveImmediate { get; }
    }

    internal class Instruction: IInstruction
    {
        public static IInstruction Parse(BinaryReader reader)
        {
            var opCode = (OpCode)reader.ReadByte();
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (opCode)
            {
                case OpCode.GetGlobal:
                    return new Instruction<uint>(opCode, VarIntUnsigned.ToUInt(reader));
                case OpCode.I32Const:
                    return new Instruction<int>(opCode, VarIntSigned.ToInt(reader));
                case OpCode.I64Const:
                    return new Instruction<long>(opCode, VarIntSigned.ToLong(reader));
                case OpCode.F32Const:
                    return new Instruction<float>(opCode, reader.ReadSingle());
                case OpCode.F64Const:
                    return new Instruction<double>(opCode, reader.ReadDouble());
                default:
                    return new Instruction(opCode);
            }
        }

        public Instruction(OpCode opCode)
        {
            OpCode = opCode;
        }

        public OpCode OpCode { get; }
        public bool HaveImmediate { get; } = false;
    }
    internal class Instruction<T>: IInstruction
    {
        public OpCode OpCode { get; }
        public bool HaveImmediate { get; } = true;

        public readonly T Immediate;

        public Instruction(OpCode opCode, T immediate)
        {
            OpCode = opCode;
            Immediate = immediate;
        }
    }
}
