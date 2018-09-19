using NUnit.Framework;
using SharpWasm.Core.Segments;
using SharpWasm.Core.Types;

namespace SharpWasm.Tests.Core.Segments
{
    [TestFixture]
    public class ImportTests
    {
        [Test]
        public void Function()
        {
            var import = new Import("module", "name",0);
            Assert.Multiple(() =>
            {
                Assert.That(import.Module, Is.EqualTo("module"));
                Assert.That(import.Name, Is.EqualTo("name"));
                Assert.That(import.Type, Is.EqualTo(ExternalKind.Function));
                Assert.That(import.Function, Is.EqualTo(0));
            });
        }

        [Test]
        public void TableType()
        {
            var import = new Import("module", "name", new TableType(0));
            Assert.Multiple(() =>
            {
                Assert.That(import.Module, Is.EqualTo("module"));
                Assert.That(import.Name, Is.EqualTo("name"));
                Assert.That(import.Type, Is.EqualTo(ExternalKind.Table));
                Assert.That(import.Table, Is.EqualTo(new TableType(0)));
            });
        }

        [Test]
        public void MemoryType()
        {
            var import = new Import("module", "name", new MemoryType(0));
            Assert.Multiple(() =>
            {
                Assert.That(import.Module, Is.EqualTo("module"));
                Assert.That(import.Name, Is.EqualTo("name"));
                Assert.That(import.Type, Is.EqualTo(ExternalKind.Memory));
                Assert.That(import.Memory, Is.EqualTo(new MemoryType(0)));
            });
        }

        [Test]
        public void GlobalType()
        {
            var import = new Import("module", "name", new GlobalType(ValueType.I32, false));
            Assert.Multiple(() =>
            {
                Assert.That(import.Module, Is.EqualTo("module"));
                Assert.That(import.Name, Is.EqualTo("name"));
                Assert.That(import.Type, Is.EqualTo(ExternalKind.Global));
                Assert.That(import.Global, Is.EqualTo(new GlobalType(ValueType.I32, false)));
            });
        }

        [Test]
        public void FunctionTypeEquals() =>
            Equals(new Import("module", "name", 0), new Import("module", "name", 0));

        [Test]
        public void TableTypeEquals() =>
            Equals(new Import("module", "name", new TableType(0)), new Import("module", "name", new TableType(0)));

        [Test]
        public void MemoryTypeEquals() =>
            Equals(new Import("module", "name", new MemoryType(0)), new Import("module", "name", new MemoryType(0)));

        [Test]
        public void GlobalTypeEquals() =>
            Equals(new Import("module", "name", new GlobalType(ValueType.I32, false)), new Import("module", "name", new GlobalType(ValueType.I32, false)));

        [Test]
        public void DifferentModuleNotEqual()
        {
            var a = new Import("module", "name", 0);
            var b = new Import("otherModule", "name", 0);
            Assert.That(a.Equals(b), Is.False);
        }

        [Test]
        public void DifferentNameNotEqual()
        {
            var a = new Import("module", "name", 0);
            var b = new Import("module", "otherName", 0);
            Assert.That(a.Equals(b), Is.False);
        }

        [Test]
        public void DifferentTypeNotEqual()
        {
            var a = new Import("module", "name", 0);
            var b = new Import("module", "name", new TableType(0));
            Assert.That(a.Equals(b), Is.False);
        }

        private static void Equals(Import a, Import b)
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
