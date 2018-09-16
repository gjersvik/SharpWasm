using NUnit.Framework;
using SharpWasm.Core.Types;

namespace SharpWasm.Tests
{
    [TestFixture]
    public class ModuleExportDescriptorTests
    {
        [Test]
        public void Equals()
        {
            var a = new ModuleExportDescriptor(ExternalKind.Function, "test");
            var b = new ModuleExportDescriptor(ExternalKind.Function, "test");
            Assert.That(a.Equals(a), Is.True);
            Assert.That(a.Equals(b), Is.True);
            Assert.That(a.Equals(null), Is.False);
            Assert.That(a.Equals((object)a), Is.True);
            Assert.That(a.Equals((object)b), Is.True);
            Assert.That(a.Equals((object)null), Is.False);
            Assert.That(a.GetHashCode(), Is.EqualTo(b.GetHashCode()));
            Assert.That(a == b, Is.True);
            Assert.That(a != b, Is.False);
        }
    }
}
