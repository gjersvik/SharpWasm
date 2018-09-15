using NUnit.Framework;
using SharpWasm.Core.Types;

namespace SharpWasm.Tests.Core.Types
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
