using NUnit.Framework;
using SharpWasm.Core.Segments;

namespace SharpWasm.Tests.Core.Segments
{
    [TestFixture]
    public class FunctionTests
    {
        [Test]
        public void Constructor()
        {
            Assert.That(() => new Function(), Throws.Nothing);
        }
    }
}
