using NUnit.Framework;
using SharpWasm.Core.Types;
using SharpWasm.Tests.Helpers;
using MemoryType = SharpWasm.Internal.Parse.Types.MemoryType;

namespace SharpWasm.Tests.Internal.Parse.Types
{
    [TestFixture]
    public class MemoryTypeTests
    {
        [Test]
        public void Limits()
        {
            var memoryType = new MemoryType(new Limits(1));
            Assert.That(memoryType.Limits, Is.EqualTo(new Limits(1)));
        }

        [Test]
        public void Parse()
        {
            const string hex = "0001";
            using (var reader = BinaryTools.HexToReader(hex))
            {
                var memoryType = new MemoryType(reader);
                Assert.That(memoryType.Limits, Is.EqualTo(new Limits(1)));
            }
        }

        [Test]
        public void Equals()
        {
            var a = new MemoryType(TestValues.Limits);
            var b = new MemoryType(TestValues.Limits);
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
