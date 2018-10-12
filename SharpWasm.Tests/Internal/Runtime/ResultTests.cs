using NUnit.Framework;
using SharpWasm.Core.Runtime;
using SharpWasm.Internal.Runtime;

namespace SharpWasm.Tests.Internal.Runtime
{
    [TestFixture]
    public class ResultTests
    {
        [Test]
        public void ConstructorEmpty()
        {
            var result = new Result();
            Assert.Multiple(() =>
            {
                Assert.That(result.Value, Is.Null);
                Assert.That(result.Trap, Is.Null);
            });
        }

        [Test]
        public void ConstructorValue()
        {
            var result = new Result(42);
            Assert.Multiple(() =>
            {
                Assert.That(result.Value, Is.Not.Null);
                Assert.That(result.Trap, Is.Null);
            });
        }

        [Test]
        public void ConstructorTrap()
        {
            var result = new Result(new Trap());
            Assert.Multiple(() =>
            {
                Assert.That(result.Value, Is.Null);
                Assert.That(result.Trap, Is.Not.Null);
            });
        }

        [Test]
        public void CastValue()
        {
            Result result = (Value)42;
            var value = result.Value ?? 0;
            Assert.That((int)value, Is.EqualTo(42));
        }

        [Test]
        public void CastTrap()
        {
            Result result = new Trap();
            Assert.That(result.Trap, Is.Not.Null);
        }
    }
}
