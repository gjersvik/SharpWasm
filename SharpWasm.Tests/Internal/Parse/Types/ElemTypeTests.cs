using NUnit.Framework;
using SharpWasm.Internal.Parse.Types;

namespace SharpWasm.Tests.Internal.Parse.Types
{
    [TestFixture]
    public class ElemTypeTests
    {
        [Test]
        public void AnyFunc()
        {
            const ElemType elem = (ElemType)(-0x10);
            Assert.That(elem, Is.EqualTo(ElemType.AnyFunc));
        }
    }
}
