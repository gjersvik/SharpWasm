using NUnit.Framework;
using SharpWasm.Internal.Parse.Sections;
using SharpWasm.Tests.Helpers;

namespace SharpWasm.Tests.Internal.Parse.Sections
{
    [TestFixture]
    public class DataSegmentTests
    {
        [Test]
        public void Properties()
        {
            var dataSegment = new DataSegment(TestValues.InitExpr, new byte[] { 1, 2, 42 });
            Assert.Multiple(() =>
            {
                Assert.That(dataSegment.Index, Is.EqualTo(0), "Index");
                Assert.That(dataSegment.Offset, Is.EqualTo(TestValues.InitExpr), "Offset");
                Assert.That(dataSegment.Size, Is.EqualTo(3), "Size");
                Assert.That(dataSegment.Data, Is.EqualTo(new byte[] { 1, 2, 42 }), "Data");
            });
        }

        [Test]
        public void Binary()
        {
            const string hex = "00" + TestValues.InitExprHex + "0301022A";
            DataSegment dataSegment;
            using (var reader = BinaryTools.HexToReader(hex))
            {
                dataSegment = new DataSegment(reader);
            }

            Assert.Multiple(() =>
            {
                Assert.That(dataSegment.Index, Is.EqualTo(0), "Index");
                Assert.That(dataSegment.Offset, Is.EqualTo(TestValues.InitExpr), "Offset");
                Assert.That(dataSegment.Size, Is.EqualTo(3), "Size");
                Assert.That(dataSegment.Data, Is.EqualTo(new byte[] { 1, 2, 42 }), "Data");
            });
        }

        [Test]
        public void Equals()
        {
            var a = new DataSegment(TestValues.InitExpr, new byte[] { 1, 2, 42 });
            var b = new DataSegment(TestValues.InitExpr, new byte[] { 1, 2, 42 });
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
