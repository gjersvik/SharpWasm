using NUnit.Framework;
using SharpWasm.Core.Types;
using SharpWasm.Internal.Parse.Code;
using SharpWasm.Tests.Helpers;

namespace SharpWasm.Tests.Internal.Parse.Code
{
    [TestFixture]
    public class LocalEntryTests
    {
        [Test]
        public void Properties()
        {
            var localEntry = new LocalEntry(10,ValueType.I32);
            Assert.Multiple(() =>
            {
                Assert.That(localEntry.Count, Is.EqualTo(10), "Count");
                Assert.That(localEntry.Type, Is.EqualTo(ValueType.I32), "Type");
            });
        }

        [Test]
        public void Binary()
        {
            const string hex = "0A7F";
            LocalEntry localEntry;
            using (var reader = BinaryTools.HexToReader(hex))
            {
                localEntry = new LocalEntry(reader);
            }
            Assert.Multiple(() =>
            {
                Assert.That(localEntry.Count, Is.EqualTo(10), "Count");
                Assert.That(localEntry.Type, Is.EqualTo(ValueType.I32), "Type");
                Assert.That(localEntry.Length, Is.EqualTo(2), "Length");
            });
        }

        [Test]
        public void Equals()
        {
            var a = new LocalEntry(10, ValueType.I32);
            var b = new LocalEntry(10, ValueType.I32);
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
