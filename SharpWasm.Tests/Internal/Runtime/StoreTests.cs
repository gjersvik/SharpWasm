using NUnit.Framework;
using SharpWasm.Internal.Runtime;

namespace SharpWasm.Tests.Internal.Runtime
{
    [TestFixture]
    public class StoreTests
    {
        [Test]
        public void Function()
        {
            var store = new Store();
            var instance = new FunctionInstance();
            var index = store.AddFunction(instance);
            Assert.That(store.Function(index), Is.SameAs(instance));
        }
        [Test]
        public void Table()
        {
            var store = new Store();
            var instance = new TableInstance();
            var index = store.AddTable(instance);
            Assert.That(store.Table(index), Is.SameAs(instance));
        }
        [Test]
        public void Memory()
        {
            var store = new Store();
            var instance = new MemoryInstance();
            var index = store.AddMemory(instance);
            Assert.That(store.Memory(index), Is.SameAs(instance));
        }
        [Test]
        public void Global()
        {
            var store = new Store();
            var instance = new GlobalInstance();
            var index = store.AddGlobal(instance);
            Assert.That(store.Global(index), Is.SameAs(instance));
        }
    }
}
