using NUnit.Framework;
using SharpWasm.Internal;

namespace SharpWasm.Tests.Internal
{
    [TestFixture]
    public class HeaderTests
    {
        [Test]
        public void Valid()
        {
            var header = new Header(0x6d736100, 1);
            Assert.That(header.IsValid(), Is.True);
        }

        [Test]
        public void InValidMagic()
        {
            var header = new Header(42, 1);
            Assert.That(header.IsValid(), Is.False);
        }

        [Test]
        public void InValidVersion()
        {
            var header = new Header(0x6d736100, 2);
            Assert.That(header.IsValid(), Is.False);
        }
    }
}
