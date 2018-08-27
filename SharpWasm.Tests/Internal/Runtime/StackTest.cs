using NUnit.Framework;
using SharpWasm.Internal.Runtime;

namespace SharpWasm.Tests.Internal.Runtime
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
            Assert.That((double)stack.Pop(),Is.EqualTo(4) );
            Assert.That((float)stack.Pop(), Is.EqualTo(3));
            Assert.That((long)stack.Pop(), Is.EqualTo(2));
            Assert.That((int)stack.Pop(), Is.EqualTo(1));
        }

        [Test]
        public void PopIntWrongType()
        {
            var stack = new Stack();
            stack.Push(2L);
            Assert.That(() => (int)stack.Pop(), Throws.Exception.Message.Contain("I64"));
        }

        [Test]
        public void PopLongWrongType()
        {
            var stack = new Stack();
            stack.Push(3F);
            Assert.That(() => (long)stack.Pop(), Throws.Exception.Message.Contain("F32"));
        }

        [Test]
        public void PopFloatWrongType()
        {
            var stack = new Stack();
            stack.Push(4.0);
            Assert.That(() => (float)stack.Pop(), Throws.Exception.Message.Contain("F64"));
        }

        [Test]
        public void PopDoubleWrongType()
        {
            var stack = new Stack();
            stack.Push(1);
            Assert.That(() => (double)stack.Pop(), Throws.Exception.Message.Contain("I32"));
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
