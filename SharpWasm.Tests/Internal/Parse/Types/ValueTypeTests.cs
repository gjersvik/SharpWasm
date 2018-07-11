using NUnit.Framework;
using SharpWasm.Internal.Parse;
using SharpWasm.Internal.Parse.Types;

namespace SharpWasm.Tests.Internal.Parse.Types
{
    [TestFixture]
    public class ValueTypeTests
    {
        [Test]
        public void I32()
        {
            var value = new VarIntSigned(-0x01).ValueType;
            Assert.That(value, Is.EqualTo(ValueType.I32));
        }
        [Test]
        public void I64()
        {
            var value = new VarIntSigned(-0x02).ValueType;
            Assert.That(value, Is.EqualTo(ValueType.I64));
        }
        [Test]
        public void F32()
        {
            var value = new VarIntSigned(-0x03).ValueType;
            Assert.That(value, Is.EqualTo(ValueType.F32));
        }
        [Test]
        public void F64()
        {
            var value = new VarIntSigned(-0x04).ValueType;
            Assert.That(value, Is.EqualTo(ValueType.F64));
        }
    }
}
