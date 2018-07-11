using NUnit.Framework;
using SharpWasm.Internal.Parse;
using SharpWasm.Tests.Helpers;

namespace SharpWasm.Tests.Internal.Parse
{
    [TestFixture]
    public class MemoryTypeTests
    {
        [Test]
        public void Limits()
        {
            var memoryType = new MemoryType(new ResizableLimits(1));
            Assert.That(memoryType.Limits, Is.EqualTo(new ResizableLimits(1)));
        }

        [Test]
        public void Parse()
        {
            const string hex = "0001";
            using (var reader = BinaryTools.HexToReader(hex))
            {
                var memoryType = new MemoryType(reader);
                Assert.That(memoryType.Limits, Is.EqualTo(new ResizableLimits(1)));
            }
        }
    }
}
