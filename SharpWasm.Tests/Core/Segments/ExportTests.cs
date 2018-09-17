using NUnit.Framework;
using SharpWasm.Core.Segments;

namespace SharpWasm.Tests.Core.Segments
{
    [TestFixture]
    public class ExportTests
    {
        [Test]
        public void Constructor()
        {
            Assert.That(() => new Export(), Throws.Nothing);
        }
    }
}
