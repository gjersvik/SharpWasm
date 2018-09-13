using NUnit.Framework;
using SharpWasm.Core;

namespace SharpWasm.Tests.Core
{
    [TestFixture]
    public class ErrorTests
    {
        [Test]
        public void Constructor()
        {
            Assert.That(() => new Error(), Throws.Nothing);
        }
    }
}
