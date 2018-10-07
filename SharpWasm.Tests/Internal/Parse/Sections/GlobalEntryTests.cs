using NUnit.Framework;
using SharpWasm.Internal.Parse.Sections;
using SharpWasm.Tests.Helpers;

namespace SharpWasm.Tests.Internal.Parse.Sections
{
    [TestFixture]
    public class GlobalEntryTests
    {
        [Test]
        public void Properties()
        {
            var globalEntry = new GlobalEntry(TestValues.GlobalType, TestValues.InitExpr);
            Assert.Multiple(() =>
            {
                Assert.That(globalEntry.Type, Is.EqualTo(TestValues.GlobalType), "Type");
                Assert.That(globalEntry.InitExpr, Is.EqualTo(TestValues.InitExpr), "InitExpr");
            });
        }
        
        [Test]
        public void Binary()
        {
            const string hex = TestValues.GlobalTypeHex + TestValues.InitExprHex;
            GlobalEntry globalEntry;
            using (var reader = BinaryTools.HexToReader(hex))
            {
                globalEntry = new GlobalEntry(reader);
            }

            Assert.Multiple(() =>
            {
                Assert.That(globalEntry.Type, Is.EqualTo(TestValues.GlobalType), "Type");
                Assert.That(globalEntry.InitExpr, Is.EqualTo(TestValues.InitExpr).AsCollection, "InitExpr");
            });
        }

        [Test]
        public void Equals()
        {
            var a = new GlobalEntry(TestValues.GlobalType, TestValues.InitExpr);
            var b = new GlobalEntry(TestValues.GlobalType, TestValues.InitExpr);
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
