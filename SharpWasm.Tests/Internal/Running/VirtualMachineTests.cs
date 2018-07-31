using System.Collections.Immutable;
using NUnit.Framework;
using SharpWasm.Internal.Parse.Code;
using SharpWasm.Internal.Running;

namespace SharpWasm.Tests.Internal.Running
{
    [TestFixture]
    public class VirtualMachineTests
    {
        [Test]
        public void Unreachable()
        {
            var code = ImmutableArray.Create<IInstruction>(Instruction.Unreachable);
            Assert.That(() => VirtualMachine.ExecuteCode(code), Throws.Exception.Message.Contain("Unreachable"));
        }

        [Test]
        public void Nop()
        {
            var code = ImmutableArray.Create<IInstruction>(Instruction.Nop);
            Assert.That(() => VirtualMachine.ExecuteCode(code), Throws.Nothing);
        }
    }
}
