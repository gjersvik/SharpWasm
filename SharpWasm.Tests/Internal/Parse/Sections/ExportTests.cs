using NUnit.Framework;
using SharpWasm.Internal.Parse.Sections;
using SharpWasm.Tests.Helpers;

namespace SharpWasm.Tests.Internal.Parse.Sections
{
    [TestFixture]
    public class ExportTests
    {
        [Test]
        public void Empty()
        {
            Assert.Multiple(() =>
            {
                Assert.That(Export.Empty.Id, Is.EqualTo(SectionCode.Export), "Id");
                Assert.That(Export.Empty.Count, Is.EqualTo(0), "Count");
                Assert.That(Export.Empty.Entries, Is.Empty, "Entries");
            });
        }

        [Test]
        public void Properties()
        {
            var export = new Export(new[] { TestValues.ExportEntry, TestValues.ExportEntry });
            Assert.Multiple(() =>
            {
                Assert.That(export.Count, Is.EqualTo(2), "Count");
                Assert.That(export.Entries,
                    Is.EqualTo(new[] { TestValues.ExportEntry, TestValues.ExportEntry }).AsCollection, "Entries");
            });
        }

        [Test]
        public void Binary()
        {
            const string hex = "02" + TestValues.ExportEntryHex + TestValues.ExportEntryHex;
            Export export;
            using (var reader = BinaryTools.HexToReader(hex))
            {
                export = new Export(reader);
            }

            Assert.Multiple(() =>
            {
                Assert.That(export.Count, Is.EqualTo(2), "Count");
                Assert.That(export.Entries,
                    Is.EqualTo(new[] { TestValues.ExportEntry, TestValues.ExportEntry }).AsCollection, "Entries");
            });
        }
    }
}
