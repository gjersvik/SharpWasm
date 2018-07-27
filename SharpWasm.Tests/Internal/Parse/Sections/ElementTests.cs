using NUnit.Framework;
using SharpWasm.Internal.Parse.Sections;
using SharpWasm.Tests.Helpers;

namespace SharpWasm.Tests.Internal.Parse.Sections
{
    [TestFixture]
    public class ElementTests
    {
        [Test]
        public void Empty()
        {
            Assert.Multiple(() =>
            {
                Assert.That(Element.Empty.Id, Is.EqualTo(SectionCode.Element), "Id");
                Assert.That(Element.Empty.Count, Is.EqualTo(0), "Count");
                Assert.That(Element.Empty.Entries, Is.Empty, "Entries");
            });
        }

        [Test]
        public void Properties()
        {
            var element = new Element(new[] { TestValues.ElementSegment, TestValues.ElementSegment });
            Assert.Multiple(() =>
            {
                Assert.That(element.Count, Is.EqualTo(2), "Count");
                Assert.That(element.Entries,
                    Is.EqualTo(new[] { TestValues.ElementSegment, TestValues.ElementSegment }).AsCollection, "Entries");
            });
        }

        [Test]
        public void Binary()
        {
            const string hex = "02" + TestValues.ElementSegmentHex + TestValues.ElementSegmentHex;
            Element element;
            using (var reader = BinaryTools.HexToReader(hex))
            {
                element = new Element(reader);
            }

            Assert.Multiple(() =>
            {
                Assert.That(element.Count, Is.EqualTo(2), "Count");
                Assert.That(element.Entries,
                    Is.EqualTo(new[] { TestValues.ElementSegment, TestValues.ElementSegment }).AsCollection, "Entries");
            });
        }
    }
}
