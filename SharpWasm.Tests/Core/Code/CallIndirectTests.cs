using NUnit.Framework;
using SharpWasm.Core.Code;

namespace SharpWasm.Tests.Core.Code
{
    [TestFixture]
    public class CallIndirectTests
    {
        [Test]
        public void Properties()
        {
            var callIndirect = new CallIndirect(5, true);
            Assert.Multiple(() =>
            {
                Assert.That(callIndirect.TypeIndex, Is.EqualTo(5), "TypeIndex");
                Assert.That(callIndirect.Reserved, Is.True, "Reserved");
            });
        }

        [Test]
        public void Equals()
        {
            var a = new CallIndirect(5, true);
            var b = new CallIndirect(5, true);
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
