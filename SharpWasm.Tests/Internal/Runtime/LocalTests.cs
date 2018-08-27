using NUnit.Framework;
using SharpWasm.Internal.Runtime;

namespace SharpWasm.Tests.Internal.Runtime
{
    [TestFixture]
    public class LocalTests
    {
        [Test]
        public void Constructor()
        {
            var stack = new Stack();
            var local = new Local(stack);
            Assert.That(local.Stack, Is.EqualTo(stack));
        }
    }
}
