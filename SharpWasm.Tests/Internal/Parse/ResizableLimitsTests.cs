using NUnit.Framework;
using SharpWasm.Internal.Parse;
using SharpWasm.Tests.Helpers;

namespace SharpWasm.Tests.Internal.Parse
{
    [TestFixture]
    public class ResizableLimitsTests
    {
        [Test]
        public void Flags()
        {
            var limits = new ResizableLimits(1, 2);
            Assert.That(limits.Flags, Is.True);
        }

        [Test]
        public void Initial()
        {
            var limits = new ResizableLimits(1, 2);
            Assert.That(limits.Initial, Is.EqualTo(1));
        }

        [Test]
        public void Maximum()
        {
            var limits = new ResizableLimits(1, 2);
            Assert.That(limits.Maximum, Is.EqualTo(2));
        }

        [Test]
        public void Parse()
        {
            const string hex = "010102";
            using (var reader = BinaryTools.HexToReader(hex))
            {
                var limits = new ResizableLimits(reader);
                Assert.That(limits.Flags, Is.True);
                Assert.That(limits.Initial, Is.EqualTo(1));
                Assert.That(limits.Maximum, Is.EqualTo(2));
            }
        }
    }
}
