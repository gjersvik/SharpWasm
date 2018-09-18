using NUnit.Framework;
using SharpWasm.Core.Code;

namespace SharpWasm.Tests.Core.Code
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
        public void End()
        {
            Assert.That(Instruction.End, Is.EqualTo(new Instruction(OpCode.End)));
        }

        [Test]
        public void GetGlobal()
        {
            Assert.That(Instruction.GetGlobal(42), Is.EqualTo(new Instruction<uint>(OpCode.GetGlobal, 42)));
        }

        [Test]
        public void I32Const()
        {
            Assert.That(Instruction.I32Const(42), Is.EqualTo(new Instruction<int>(OpCode.I32Const, 42)));
        }

        [Test]
        public void I64Const()
        {
            Assert.That(Instruction.I64Const(42), Is.EqualTo(new Instruction<long>(OpCode.I64Const, 42)));
        }

        [Test]
        public void F32Const()
        {
            Assert.That(Instruction.F32Const(42), Is.EqualTo(new Instruction<float>(OpCode.F32Const, 42)));
        }

        [Test]
        public void F64Const()
        {
            Assert.That(Instruction.F64Const(42), Is.EqualTo(new Instruction<double>(OpCode.F64Const, 42)));
        }

        [Test]
        public void Equals()
        {
            var a = new Instruction(OpCode.End);
            var b = new Instruction(OpCode.End);
            Assert.That(a.Equals(a), Is.True);
            Assert.That(a.Equals(b), Is.True);
            Assert.That(a.Equals(null), Is.False);
            Assert.That(a.Equals((object)a), Is.True);
            Assert.That(a.Equals((object)b), Is.True);
            Assert.That(a.Equals((object)null), Is.False);
            Assert.That(a.GetHashCode(), Is.EqualTo(b.GetHashCode()));
            Assert.That(a == b, Is.True);
            Assert.That(a != b, Is.False);
        }

        [Test]
        public void EqualsTyped()
        {
            var a = Instruction.I32Const(42);
            var b = Instruction.I32Const(42);
            Assert.That(a.Equals(a), Is.True);
            Assert.That(a.Equals(b), Is.True);
            Assert.That(a.Equals(null), Is.False);
            Assert.That(a.Equals((object)a), Is.True);
            Assert.That(a.Equals((object)b), Is.True);
            Assert.That(a.Equals((object)null), Is.False);
            Assert.That(a.GetHashCode(), Is.EqualTo(b.GetHashCode()));
            Assert.That(a == b, Is.True);
            Assert.That(a != b, Is.False);
        }
    }
}
