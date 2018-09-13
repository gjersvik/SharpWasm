using NUnit.Framework;
using SharpWasm.Core.Types;
using SharpWasm.Tests.Helpers;
using GlobalType = SharpWasm.Internal.Parse.Types.GlobalType;

namespace SharpWasm.Tests.Internal.Parse.Types
{
    [TestFixture]
    public class GlobalTypeTests
    {
        [Test]
        public void ContentType()
        {
            var globalType = new GlobalType(ValueType.I32, false);
            Assert.That(globalType.ContentType, Is.EqualTo(ValueType.I32));
        }

        [Test]
        public void Mutability()
        {
            var globalType = new GlobalType(ValueType.I32, false);
            Assert.That(globalType.Mutability, Is.False);
        }

        [Test]
        public void Parse()
        {
            const string hex = "7F00";
            using (var reader = BinaryTools.HexToReader(hex))
            {
                var globalType = new GlobalType(reader);
                Assert.That(globalType.ContentType, Is.EqualTo(ValueType.I32));
                Assert.That(globalType.Mutability, Is.False);
            }
        }

        [Test]
        public void Equals()
        {
            var a = new GlobalType(ValueType.I32, false);
            var b = new GlobalType(ValueType.I32, false);
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