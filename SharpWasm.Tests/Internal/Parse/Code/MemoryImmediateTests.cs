using NUnit.Framework;
using SharpWasm.Internal.Parse.Code;
using SharpWasm.Tests.Helpers;

namespace SharpWasm.Tests.Internal.Parse.Code
{
    [TestFixture]
    public class MemoryImmediateTests
    {
        [Test]
        public void Properties()
        {
            var memoryImmediate = new MemoryImmediate(1,2);
            Assert.Multiple(() =>
            {
                Assert.That(memoryImmediate.Flags, Is.EqualTo(1), "Flags");
                Assert.That(memoryImmediate.Offset, Is.EqualTo(2), "Offset");
            });
        }

        [Test]
        public void Binary()
        {
            const string hex = "0102";
            MemoryImmediate memoryImmediate;
            using (var reader = BinaryTools.HexToReader(hex))
            {
                memoryImmediate = new MemoryImmediate(reader);
            }

            Assert.Multiple(() =>
            {
                Assert.That(memoryImmediate.Flags, Is.EqualTo(1), "Flags");
                Assert.That(memoryImmediate.Offset, Is.EqualTo(2), "Offset");
            });
        }

        [Test]
        public void Equals()
        {
            var a = new MemoryImmediate(1, 2);
            var b = new MemoryImmediate(1, 2);
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
