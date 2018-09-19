using System.Collections.Immutable;
using NUnit.Framework;
using SharpWasm.Core.Code;
using SharpWasm.Core.Segments;

namespace SharpWasm.Tests.Core.Segments
{
    [TestFixture]
    public class ElementTests
    {
        [Test]
        public void Constructor()
        {
            var global = new Element(0, new IInstruction[] { Instruction.I32Const(3), Instruction.End }, new uint[] { 1, 2, 3 });
            Assert.Multiple(() =>
            {
                Assert.That(global.Table, Is.EqualTo(0).AsCollection, "Table");
                Assert.That(global.Offset, Is.EqualTo(new IInstruction[] { Instruction.I32Const(3), Instruction.End }).AsCollection, "Offset");
                Assert.That(global.Init, Is.EqualTo(new uint[] { 1, 2, 3 }).AsCollection, "Init");
            });
        }

        [Test]
        public void Equals()
        {
            var a = new Element(0,ImmutableArray<IInstruction>.Empty, new uint[]{1,2,3});
            var b = new Element(0, ImmutableArray<IInstruction>.Empty, new uint[] { 1, 2, 3 });
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
