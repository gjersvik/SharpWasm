using NUnit.Framework;
using SharpWasm.Core.Types;

namespace SharpWasm.Tests.Core.Types
{
    [TestFixture]
    public class ExternalTypeTests
    {
        [Test]
        public void FunctionType()
        {
            var externalType = new ExternalType(new FunctionType());
            Assert.Multiple(() =>
            {
                Assert.That(externalType.Type, Is.EqualTo(ExternalKind.Function));
                Assert.That(externalType.FunctionType, Is.EqualTo(new FunctionType()));
            });
        }

        [Test]
        public void TableType()
        {
            var externalType = new ExternalType(new TableType(0));
            Assert.Multiple(() =>
            {
                Assert.That(externalType.Type, Is.EqualTo(ExternalKind.Table));
                Assert.That(externalType.TableType, Is.EqualTo(new TableType(0)));
            });
        }

        [Test]
        public void MemoryType()
        {
            var externalType = new ExternalType(new MemoryType(0));
            Assert.Multiple(() =>
            {
                Assert.That(externalType.Type, Is.EqualTo(ExternalKind.Memory));
                Assert.That(externalType.MemoryType, Is.EqualTo(new MemoryType(0)));
            });
        }

        [Test]
        public void GlobalType()
        {
            var externalType = new ExternalType(new GlobalType(ValueType.I32,false));
            Assert.Multiple(() =>
            {
                Assert.That(externalType.Type, Is.EqualTo(ExternalKind.Global));
                Assert.That(externalType.GlobalType, Is.EqualTo(new GlobalType(ValueType.I32,false)));
            });
        }

        [Test]
        public void FunctionTypeEquals() =>
            Equals(new ExternalType(new FunctionType()), new ExternalType(new FunctionType()));

        [Test]
        public void TableTypeEquals() =>
            Equals(new ExternalType(new TableType(0)), new ExternalType(new TableType(0)));

        [Test]
        public void MemoryTypeEquals() =>
            Equals(new ExternalType(new MemoryType(0)), new ExternalType(new MemoryType(0)));

        [Test]
        public void GlobalTypeEquals() =>
            Equals(new ExternalType(new GlobalType(ValueType.I32, false)), new ExternalType(new GlobalType(ValueType.I32, false)));

        [Test]
        public void DifferentTypeNotEqual()
        {
            var a = new ExternalType(new FunctionType());
            var b = new ExternalType(new TableType(0));
            Assert.That(a.Equals(b), Is.False);
        }

        private static void Equals(ExternalType a, ExternalType b)
        {
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
