using NUnit.Framework;
using SharpWasm.Core.Types;

namespace SharpWasm.Tests.Core.Types
{
    [TestFixture]
    public class ExternalTypeTests
    {

        [Test]
        public void Constructor()
        {
            Assert.That(() => new ExternalType(), Throws.Nothing);
        }
    }
}
