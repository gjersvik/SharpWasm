using System;
using NUnit.Framework;
using SharpWasm.Core.Runtime;
using ValueType = SharpWasm.Core.Types.ValueType;

namespace SharpWasm.Tests.Core.Runtime
{
    [TestFixture]
    public class ValueTests
    {
        [Test]
        public void ConstructorInt()
        {
            var value = new Value(-42);
            Assert.Multiple(() =>
            {
                Assert.That(value.Type, Is.EqualTo(ValueType.I32));
                Assert.That((int)value, Is.EqualTo(-42).And.TypeOf<int>());
            });
        }

        [Test]
        public void ConstructorUInt()
        {
            var value = new Value(42U);
            Assert.Multiple(() =>
            {
                Assert.That(value.Type, Is.EqualTo(ValueType.I32));
                Assert.That((uint)value, Is.EqualTo(42).And.TypeOf<uint>());
            });
        }

        [Test]
        public void ConstructorLong()
        {
            var value = new Value(-42L);
            Assert.Multiple(() =>
            {
                Assert.That(value.Type, Is.EqualTo(ValueType.I64));
                Assert.That((long)value, Is.EqualTo(-42).And.TypeOf<long>());
            });
        }

        [Test]
        public void ConstructorULong()
        {
            var value = new Value(42UL);
            Assert.Multiple(() =>
            {
                Assert.That(value.Type, Is.EqualTo(ValueType.I64));
                Assert.That((ulong)value, Is.EqualTo(42).And.TypeOf<ulong>());
            });
        }

        [Test]
        public void ConstructorFloat()
        {
            var value = new Value(42.0F);
            Assert.Multiple(() =>
            {
                Assert.That(value.Type, Is.EqualTo(ValueType.F32));
                Assert.That((float)value, Is.EqualTo(42).And.TypeOf<float>());
            });
        }

        [Test]
        public void ConstructorDouble()
        {
            var value = new Value(42.0D);
            Assert.Multiple(() =>
            {
                Assert.That(value.Type, Is.EqualTo(ValueType.F64));
                Assert.That((double)value, Is.EqualTo(42).And.TypeOf<double>());
            });
        }

        [Test]
        public void WrongCastUInt()
        {
            Value value = 1.0;
            Assert.That(() => (uint)value, Throws.TypeOf<InvalidCastException>());
        }

        [Test]
        public void WrongCastULong()
        {
            Value value = 1.0;
            Assert.That(() => (ulong)value, Throws.TypeOf<InvalidCastException>());
        }

        [Test]
        public void CastUInt()
        {
            Value value = 43U;
            Assert.That(value.Type, Is.EqualTo(ValueType.I32));
        }

        [Test]
        public void CastULong()
        {
            Value value = 43UL;
            Assert.That(value.Type, Is.EqualTo(ValueType.I64));
        }
    }
}
