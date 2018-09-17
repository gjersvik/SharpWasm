using NUnit.Framework;
using SharpWasm.Core.Segments;

namespace SharpWasm.Tests.Core.Segments
{
    [TestFixture]
    public class GlobalTests
    {
        [Test]
        public void Constructor()
        {
            Assert.That(() => new Global(), Throws.Nothing);
        }
    }
}
