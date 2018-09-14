using NUnit.Framework;
using SharpWasm.Core.Types;

namespace SharpWasm.Tests.Core.Types
{
    [TestFixture]
    public class ValueTypeTests
    {
        [Test]
        public void I32()
        {
            const ValueType value = (ValueType)(-0x01);
            Assert.That(value, Is.EqualTo(ValueType.I32));
        }
        [Test]
        public void I64()
        {
            const ValueType value = (ValueType)(-0x02);
            Assert.That(value, Is.EqualTo(ValueType.I64));
        }
        [Test]
        public void F32()
        {
            const ValueType value = (ValueType)(-0x03);
            Assert.That(value, Is.EqualTo(ValueType.F32));
        }
        [Test]
        public void F64()
        {
            const ValueType value = (ValueType)(-0x04);
            Assert.That(value, Is.EqualTo(ValueType.F64));
        }
    }
}
