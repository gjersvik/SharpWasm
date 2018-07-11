using NUnit.Framework;
using SharpWasm.Internal.Parse;
using SharpWasm.Tests.Helpers;

namespace SharpWasm.Tests.Internal.Parse
{
    [TestFixture]
    public class TableTypeTests
    {
        [Test]
        public void ElementType()
        {
            var tableType = new TableType(new ResizableLimits(1));
            Assert.That(tableType.ElementType, Is.EqualTo(ElemType.AnyFunc));
        }

        [Test]
        public void Limits()
        {
            var tableType = new TableType(new ResizableLimits(1));
            Assert.That(tableType.Limits, Is.EqualTo(new ResizableLimits(1)));
        }

        [Test]
        public void Parse()
        {
            const string hex = "700001";
            using (var reader = BinaryTools.HexToReader(hex))
            {
                var tableType = new TableType(reader);
                Assert.That(tableType.ElementType, Is.EqualTo(ElemType.AnyFunc));
                Assert.That(tableType.Limits, Is.EqualTo(new ResizableLimits(1)));
            }
        }
    }
}