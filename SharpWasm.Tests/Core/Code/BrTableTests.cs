using System.Collections.Immutable;
using NUnit.Framework;
using SharpWasm.Core.Code;

namespace SharpWasm.Tests.Core.Code
{
    [TestFixture]
    public class BrTableTests
    {
        [Test]
        public void Properties()
        {
            var brTable = new BrTable(ImmutableArray.Create<uint>(1,2,3,4), 5);
            Assert.Multiple(() =>
            {
                Assert.That(brTable.TargetTable, Is.EqualTo(new []{1,2,3,4}), "TargetTable");
                Assert.That(brTable.DefaultTarget, Is.EqualTo(5), "DefaultTarget");
            });
        }

        [Test]
        public void Equals()
        {
            var a = new BrTable(ImmutableArray.Create<uint>(1, 2, 3, 4), 5);
            var b = new BrTable(ImmutableArray.Create<uint>(1, 2, 3, 4), 5);
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
