using NUnit.Framework;
using SharpWasm.Internal.Parse.Sections;
using SharpWasm.Internal.Parse.Types;
using SharpWasm.Tests.Helpers;

namespace SharpWasm.Tests.Internal.Parse.Sections
{
    [TestFixture]
    public class ExportEntryTests
    {
        [Test]
        public void Properties()
        {
            var exportEntry = new ExportEntry("test", ExternalKind.Function,2);
            Assert.Multiple(() =>
            {
                Assert.That(exportEntry.FieldLen, Is.EqualTo(4), "FieldLen");
                Assert.That(exportEntry.FieldStr, Is.EqualTo("test"), "FieldStr");
                Assert.That(exportEntry.ExternalKind, Is.EqualTo(ExternalKind.Function), "ExternalKind");
                Assert.That(exportEntry.Index, Is.EqualTo(2), "Index");
            });
        }

        [Test]
        public void Binary()
        {
            const string hex = TestValues.TestStringHex + "0002";
            ExportEntry exportEntry;
            using (var reader = BinaryTools.HexToReader(hex))
            {
                exportEntry = new ExportEntry(reader);
            }

            Assert.Multiple(() =>
            {
                Assert.That(exportEntry.FieldLen, Is.EqualTo(4), "FieldLen");
                Assert.That(exportEntry.FieldStr, Is.EqualTo("test"), "FieldStr");
                Assert.That(exportEntry.ExternalKind, Is.EqualTo(ExternalKind.Function), "ExternalKind");
                Assert.That(exportEntry.Index, Is.EqualTo(2), "Index");
            });
        }

        [Test]
        public void Equals()
        {
            var a = new ExportEntry("test", ExternalKind.Function, 2);
            var b = new ExportEntry("test", ExternalKind.Function, 2);
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
