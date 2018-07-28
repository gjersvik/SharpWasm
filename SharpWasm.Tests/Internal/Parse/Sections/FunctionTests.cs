using NUnit.Framework;
using SharpWasm.Internal.Parse.Sections;

namespace SharpWasm.Tests.Internal.Parse.Sections
{
    [TestFixture]
    internal class FunctionTests
    {
        [Test]
        public void Empty()
        {
            Assert.Multiple(() =>
            {
                Assert.That(Function.Empty.Id, Is.EqualTo(SectionCode.Function), "Id");
                Assert.That(Function.Empty.Count, Is.EqualTo(0), "Count");
                Assert.That(Function.Empty.Types, Is.Empty, "Types");
            });
        }

        [Test]
        public void Properties()
        {
            var func = new Function(new uint[]{1,2,3,4});
            Assert.Multiple(() =>
            {
                Assert.That(func.Count, Is.EqualTo(4), "Count");
                Assert.That(func.Types, Is.EqualTo(new uint[] { 1, 2, 3, 4 }).AsCollection, "Types");
            });
        }
    }
}
