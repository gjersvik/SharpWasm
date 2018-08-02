using NUnit.Framework;
using SharpWasm.Internal.Running;

namespace SharpWasm.Tests.Internal.Running
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
