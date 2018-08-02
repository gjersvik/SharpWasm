using NUnit.Framework;
using SharpWasm.Internal.Running;

namespace SharpWasm.Tests.Internal.Running
{
    [TestFixture]
    public class StackTest
    {
        [Test]
        public void PushAndPopAll()
        {
            var stack = new Stack();
            stack.Push(1);
            stack.Push(2L);
            stack.Push(3F);
            stack.Push(4.0);
            Assert.That(stack.PopDouble(),Is.EqualTo(4) );
            Assert.That(stack.PopFloat(), Is.EqualTo(3));
            Assert.That(stack.PopLong(), Is.EqualTo(2));
            Assert.That(stack.PopInt(), Is.EqualTo(1));
        }

        [Test]
        public void PopIntWrongType()
        {
            var stack = new Stack();
            stack.Push(2L);
            Assert.That(() => stack.PopInt(), Throws.Exception.Message.Contain("I64"));
        }

        [Test]
        public void PopLongWrongType()
        {
            var stack = new Stack();
            stack.Push(3F);
            Assert.That(() => stack.PopLong(), Throws.Exception.Message.Contain("F32"));
        }

        [Test]
        public void PopFloatWrongType()
        {
            var stack = new Stack();
            stack.Push(4.0);
            Assert.That(() => stack.PopFloat(), Throws.Exception.Message.Contain("F64"));
        }

        [Test]
        public void PopDoubleWrongType()
        {
            var stack = new Stack();
            stack.Push(1);
            Assert.That(() => stack.PopDouble(), Throws.Exception.Message.Contain("I32"));
        }

        [Test]
        public void StackOverflow()
        {
            var stack = new Stack(2);
            stack.Push(1);
            stack.Push(1);
            Assert.That(() => stack.Push(1), Throws.InstanceOf<WebAssemblyStackOverflowException>());
        }
    }
}
