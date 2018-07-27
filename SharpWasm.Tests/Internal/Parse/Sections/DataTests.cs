using NUnit.Framework;
using SharpWasm.Internal.Parse.Sections;
using SharpWasm.Tests.Helpers;

namespace SharpWasm.Tests.Internal.Parse.Sections
{
    [TestFixture]
    public class DataTests
    {
        [Test]
        public void Empty()
        {
            Assert.Multiple(() =>
            {
                Assert.That(Data.Empty.Id, Is.EqualTo(SectionCode.Data), "Id");
                Assert.That(Data.Empty.Count, Is.EqualTo(0), "Count");
                Assert.That(Data.Empty.Entries, Is.Empty, "Entries");
            });
        }

        [Test]
        public void Properties()
        {
            var data = new Data(new[] { TestValues.DataSegment, TestValues.DataSegment });
            Assert.Multiple(() =>
            {
                Assert.That(data.Count, Is.EqualTo(2), "Count");
                Assert.That(data.Entries,
                    Is.EqualTo(new[] { TestValues.DataSegment, TestValues.DataSegment }).AsCollection, "Entries");
            });
        }

        [Test]
        public void Binary()
        {
            const string hex = "02" + TestValues.DataSegmentHex + TestValues.DataSegmentHex;
            Data data;
            using (var reader = BinaryTools.HexToReader(hex))
            {
                data = new Data(reader);
            }

            Assert.Multiple(() =>
            {
                Assert.That(data.Count, Is.EqualTo(2), "Count");
                Assert.That(data.Entries,
                    Is.EqualTo(new[] { TestValues.DataSegment, TestValues.DataSegment }).AsCollection, "Entries");
            });
        }

    }
}
