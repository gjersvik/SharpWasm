using NUnit.Framework;
using SharpWasm.Internal.Parse.Sections;
using SharpWasm.Tests.Helpers;

namespace SharpWasm.Tests.Internal.Parse.Sections
{
    [TestFixture]
    public class ElementSegmentTests
    {
        [Test]
        public void Properties()
        {
            var elementSegment = new ElementSegment(TestValues.InitExpr, new uint[]{1,2,42});
            Assert.Multiple(() =>
            {
                Assert.That(elementSegment.Index, Is.EqualTo(0), "Index");
                Assert.That(elementSegment.Offset, Is.EqualTo(TestValues.InitExpr), "Offset");
                Assert.That(elementSegment.NumElem, Is.EqualTo(3), "NumElem");
                Assert.That(elementSegment.Elements, Is.EqualTo(new uint[]{1,2,42}), "Elements");
            });
        }

        [Test]
        public void Binary()
        {
            const string hex = "00"+ TestValues.InitExprHex + "0301022A";
            ElementSegment elementSegment;
            using (var reader = BinaryTools.HexToReader(hex))
            {
                elementSegment = new ElementSegment(reader);
            }

            Assert.Multiple(() =>
            {
                Assert.That(elementSegment.Index, Is.EqualTo(0), "Index");
                Assert.That(elementSegment.Offset, Is.EqualTo(TestValues.InitExpr).AsCollection, "Offset");
                Assert.That(elementSegment.NumElem, Is.EqualTo(3), "NumElem");
                Assert.That(elementSegment.Elements, Is.EqualTo(new uint[] { 1, 2, 42 }), "Elements");
            });
        }

        [Test]
        public void Equals()
        {
            var a = new ElementSegment(TestValues.InitExpr, new uint[] { 1, 2, 42 });
            var b = new ElementSegment(TestValues.InitExpr, new uint[] { 1, 2, 42 });
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
