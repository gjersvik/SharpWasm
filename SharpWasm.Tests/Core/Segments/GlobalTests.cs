using System.Collections.Immutable;
using NUnit.Framework;
using SharpWasm.Core.Code;
using SharpWasm.Core.Segments;
using SharpWasm.Core.Types;

namespace SharpWasm.Tests.Core.Segments
{
    [TestFixture]
    public class GlobalTests
    {
        [Test]
        public void Constructor()
        {
            var global = new Global(new GlobalType(ValueType.F32, false), new IInstruction[] { Instruction.I32Const(0), Instruction.End });
            Assert.Multiple(() =>
            {
                Assert.That(global.Type, Is.EqualTo(new GlobalType(ValueType.F32, false)).AsCollection, "Type");
                Assert.That(global.Init, Is.EqualTo(new IInstruction[] { Instruction.I32Const(0), Instruction.End }).AsCollection, "Init");
            });
        }

        [Test]
        public void Equals()
        {
            var a = new Global(new GlobalType(ValueType.F32, false), ImmutableArray<IInstruction>.Empty);
            var b = new Global(new GlobalType(ValueType.F32, false), ImmutableArray<IInstruction>.Empty);
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
