using NUnit.Framework;
using SharpWasm.Core.Types;
using SharpWasm.Internal;
using SharpWasm.Internal.Parse.Code;
using SharpWasm.Internal.Parse.Types;

namespace SharpWasm.Tests.Internal
{
    [TestFixture]
    public class AFunctionTests
    {
        [Test]
        public void Function()
        {
            var function = new Function(1, new IInstruction[] {Instruction.Nop, Instruction.End}, new FuncType(new[] {ValueType.I32}, ValueType.I64), 5);
            Assert.Multiple(() =>
            {
                Assert.That(function.Id, Is.EqualTo(1));
                Assert.That(function.TypeId, Is.EqualTo(5));
                Assert.That(function.Param, Is.EqualTo(new[] {ValueType.I32}));
                Assert.That(function.Return, Is.EqualTo(ValueType.I64));
                Assert.That(function.Import, Is.False);
                Assert.That(function.Body, Is.EqualTo(new IInstruction[] { Instruction.Nop, Instruction.End }));
            });
        }

        [Test]
        public void ImportFunction()
        {
            var function = new ImportFunction(1, new FuncType(new[] {ValueType.I32}, ValueType.I64), "module", "test",
                5);
            Assert.Multiple(() =>
            {
                Assert.That(function.Id, Is.EqualTo(1));
                Assert.That(function.TypeId, Is.EqualTo(5));
                Assert.That(function.Param, Is.EqualTo(new[] {ValueType.I32}));
                Assert.That(function.Return, Is.EqualTo(ValueType.I64));
                Assert.That(function.Import, Is.True);
                Assert.That(function.Module, Is.EqualTo("module"));
                Assert.That(function.Field, Is.EqualTo("test"));
            });
        }
    }
}