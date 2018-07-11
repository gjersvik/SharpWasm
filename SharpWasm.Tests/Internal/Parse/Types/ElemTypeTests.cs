using NUnit.Framework;
using SharpWasm.Internal.Parse;
using SharpWasm.Internal.Parse.Types;

namespace SharpWasm.Tests.Internal.Parse.Types
{
    [TestFixture]
    public class ElemTypeTests
    {
        [Test]
        public void AnyFunc()
        {
            var elem = new VarIntSigned(-0x10).ElemType;
            Assert.That(elem, Is.EqualTo(ElemType.AnyFunc));
        }
    }
}
