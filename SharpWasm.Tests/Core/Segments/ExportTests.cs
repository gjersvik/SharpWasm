using NUnit.Framework;
using SharpWasm.Core.Segments;
using SharpWasm.Core.Types;

namespace SharpWasm.Tests.Core.Segments
{
    [TestFixture]
    public class ExportTests
    {
        [Test]
        public void Constructor()
        {
            var export = new Export("test", ExternalKind.Function, 2);
            Assert.Multiple(() =>
            {
                Assert.That(export.Name, Is.EqualTo("test").AsCollection, "Name");
                Assert.That(export.Type, Is.EqualTo(ExternalKind.Function).AsCollection, "Type");
                Assert.That(export.Index, Is.EqualTo(2).AsCollection, "Index");
            });
        }

        [Test]
        public void Equals()
        {
            var a = new Export("test", ExternalKind.Function, 2);
            var b = new Export("test", ExternalKind.Function, 2);
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
