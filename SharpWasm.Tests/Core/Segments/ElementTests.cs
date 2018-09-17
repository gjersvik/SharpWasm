using NUnit.Framework;
using SharpWasm.Core.Segments;

namespace SharpWasm.Tests.Core.Segments
{
    [TestFixture]
    public class ElementTests
    {
        [Test]
        public void Constructor()
        {
            Assert.That(() => new Element(), Throws.Nothing);
        }
    }
}
