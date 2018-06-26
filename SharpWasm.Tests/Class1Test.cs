using NUnit.Framework;

namespace SharpWasm.Tests
{
    [TestFixture]
    public class Class1Test
    {
        [Test]
        public void Test()
        {
            Assert.That(Class1.GetInt(), Is.EqualTo(42));
        }
    }
}
