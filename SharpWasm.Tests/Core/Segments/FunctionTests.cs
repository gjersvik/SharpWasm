using System.Collections.Immutable;
using NUnit.Framework;
using SharpWasm.Core.Code;
using SharpWasm.Core.Segments;
using SharpWasm.Core.Types;

namespace SharpWasm.Tests.Core.Segments
{
    [TestFixture]
    public class FunctionTests
    {
        [Test]
        public void Constructor()
        {
            var function = new Function(0, new[] {ValueType.I32}, new IInstruction[] {Instruction.I32Const(0), Instruction.End});
            Assert.Multiple(() =>
            {
                Assert.That(function.TypeIndex, Is.EqualTo(0), "TypeIndex");
                Assert.That(function.Locals, Is.EqualTo(new[] { ValueType.I32 }).AsCollection, "Locals");
                Assert.That(function.Body, Is.EqualTo(new IInstruction[] { Instruction.I32Const(0), Instruction.End }).AsCollection, "Body");
            });
        }

        [Test]
        public void Equals()
        {
            var a = new Function(0, ImmutableArray<ValueType>.Empty, ImmutableArray<IInstruction>.Empty);
            var b = new Function(0, ImmutableArray<ValueType>.Empty, ImmutableArray<IInstruction>.Empty);
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
