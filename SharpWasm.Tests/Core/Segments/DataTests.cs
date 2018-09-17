using NUnit.Framework;
using SharpWasm.Core.Segments;

namespace SharpWasm.Tests.Core.Segments
{
    [TestFixture]
    public class DataTests
    {
        [Test]
        public void Constructor()
        {
            Assert.That(() => new Data(), Throws.Nothing);
        }
    }
}
