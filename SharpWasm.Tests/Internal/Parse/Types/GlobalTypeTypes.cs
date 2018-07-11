using NUnit.Framework;
using SharpWasm.Internal.Parse.Types;
using SharpWasm.Tests.Helpers;

namespace SharpWasm.Tests.Internal.Parse.Types
{
    [TestFixture]
    public class GlobalTypeTypes
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
    }
}