using NUnit.Framework;
using SharpWasm.Internal.Parse.Code;
using SharpWasm.Tests.Helpers;

namespace SharpWasm.Tests.Internal.Parse.Code
{
    [TestFixture]
    public class InstructionTests
    {
        [Test]
        public void InstructionBasic()
        {
            var inst = new Instruction(OpCode.Nop);
            Assert.That(inst.HaveImmediate, Is.False);
            Assert.That(inst.OpCode, Is.EqualTo(OpCode.Nop));
        }

        [Test]
        public void InstructionTyped()
        {
            var inst = new Instruction<int>(OpCode.I32Const, 4);
            Assert.That(inst.HaveImmediate, Is.True);
            Assert.That(inst.OpCode, Is.EqualTo(OpCode.I32Const));
            Assert.That(inst.Immediate, Is.EqualTo(4));
        }

        [Test]
        public void ParseSimple() 
        {
            IInstruction inst;
            using (var reader = BinaryTools.HexToReader("00"))
            {
                inst = Instruction.Parse(reader);
            }
            Assert.That(inst.OpCode, Is.EqualTo(OpCode.Unreachable));
            Assert.That(inst.HaveImmediate, Is.False);
        }

        [Test]
        public void ParseGetGlobal() => Parse("232A", OpCode.GetGlobal, 42U);
        [Test]
        public void ParseI32Const() => Parse("412A", OpCode.I32Const, 42);
        [Test]
        public void ParseI64Const() => Parse("422A", OpCode.I64Const, 42L);
        [Test]
        public void ParseF32Const() => Parse("4300002842", OpCode.F32Const, 42.0F);
        [Test]
        public void ParseF64Const() => Parse("440000000000004540", OpCode.F64Const, 42.0);

        private static void Parse<T>(string hex, OpCode opCode, T immediate)
        {
            Instruction<T> inst;
            using (var reader = BinaryTools.HexToReader(hex))
            {
                inst = (Instruction<T>)Instruction.Parse(reader);
            }
            Assert.That(inst.OpCode, Is.EqualTo(opCode));
            Assert.That(inst.HaveImmediate, Is.True);
            Assert.That(inst.Immediate, Is.EqualTo(immediate));
        }
    }
}
