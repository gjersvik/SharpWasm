using NUnit.Framework;
using SharpWasm.Core.Segments;

namespace SharpWasm.Tests.Core.Segments
{
    [TestFixture]
    public class ImportTests
    {
        [Test]
        public void Constructor()
        {
            Assert.That(() => new Import(), Throws.Nothing);
        }
    }
}
