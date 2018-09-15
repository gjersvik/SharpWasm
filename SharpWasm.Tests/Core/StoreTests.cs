using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using SharpWasm.Core;
using SharpWasm.Core.Runtime;
using SharpWasm.Core.Types;

namespace SharpWasm.Tests.Core
{
    [TestFixture]
    public class StoreTests
    {
        [Test]
        public void InstantiateModule()
        {
            var store = Store.Init();
            Assert.That(store.InstantiateModule(new Module(), new ExternalValue[0]), Is.InstanceOf<ModuleInstance>());
        }

        [Test]
        public void AllocFunction()
        {
            var store = Store.Init();
            Assert.That(store.AllocFunction(new FunctionType(), NoopHost), Is.EqualTo(-1));
        }

        [Test]
        public void FunctionType()
        {
            var store = Store.Init();
            Assert.That(store.FunctionType(0), Is.EqualTo(new FunctionType()));
        }

        [Test]
        public void InvokeFunction()
        {
            var store = Store.Init();
            Assert.That(store.InvokeFunction(0), Is.EqualTo(new Value()));
        }

        [Test]
        public void AllocTable()
        {
            var store = Store.Init();
            Assert.That(store.AllocTable(new TableType()), Is.EqualTo(-1));
        }

        [Test]
        public void TableType()
        {
            var store = Store.Init();
            Assert.That(store.TableType(0), Is.InstanceOf<TableType>());
        }

        [Test]
        public void ReadTable()
        {
            var store = Store.Init();
            Assert.That(store.ReadTable(0,0), Is.EqualTo(-1));
        }

        [Test]
        public void WriteTable()
        {
            var store = Store.Init();
            store.WriteTable(0, 0, 0);
        }

        [Test]
        public void SizeTable()
        {
            var store = Store.Init();
            Assert.That(store.SizeTable(0), Is.EqualTo(0));
        }

        [Test]
        public void GrowTable()
        {
            var store = Store.Init();
            store.GrowTable(0,0);
        }

        [Test]
        public void AllocMemory()
        {
            var store = Store.Init();
            Assert.That(store.AllocMemory(new MemoryType()), Is.EqualTo(-1));
        }

        [Test]
        public void MemoryType()
        {
            var store = Store.Init();
            Assert.That(store.MemoryType(0), Is.InstanceOf<MemoryType>());
        }

        [Test]
        public void ReadMemory()
        {
            var store = Store.Init();
            Assert.That(store.ReadMemory(0, 0), Is.EqualTo(0));
        }

        [Test]
        public void WriteMemory()
        {
            var store = Store.Init();
            store.WriteMemory(0, 0, 0);
        }

        [Test]
        public void SizeMemory()
        {
            var store = Store.Init();
            Assert.That(store.SizeMemory(0), Is.EqualTo(0));
        }

        [Test]
        public void GrowMemory()
        {
            var store = Store.Init();
            store.GrowMemory(0, 0);
        }

        [Test]
        public void AllocGlobal()
        {
            var store = Store.Init();
            Assert.That(store.AllocGlobal(new GlobalType(), new Value()), Is.EqualTo(-1));
        }

        [Test]
        public void GlobalType()
        {
            var store = Store.Init();
            Assert.That(store.GlobalType(0), Is.InstanceOf<GlobalType>());
        }

        [Test]
        public void ReadGlobal()
        {
            var store = Store.Init();
            Assert.That(store.ReadGlobal(0, 0), Is.EqualTo(new Value()));
        }

        [Test]
        public void WriteGlobal()
        {
            var store = Store.Init();
            store.WriteGlobal(0, 0, new Value());
        }

        [ExcludeFromCodeCoverage]
        private static Value? NoopHost(Value[] args)
        {
            return null;
        }
    }
}
