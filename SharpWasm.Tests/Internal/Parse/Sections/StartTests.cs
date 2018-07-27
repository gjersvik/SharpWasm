using NUnit.Framework;
using SharpWasm.Internal.Parse.Sections;
using SharpWasm.Tests.Helpers;

namespace SharpWasm.Tests.Internal.Parse.Sections
{
    internal class StartTests
    {
        [Test]
        public void Properties()
        {
            var start = new Start(42);
            Assert.Multiple(() =>
            {
                Assert.That(start.Id, Is.EqualTo(SectionCode.Start));
                Assert.That(start.Index, Is.EqualTo(42), "Index");
            });
        }

        [Test]
        public void Binary()
        {
            const string hex = "2A";
            Start start;
            using (var reader = BinaryTools.HexToReader(hex))
            {
                start = new Start(reader);
            }

            Assert.Multiple(() =>
            {
                Assert.That(start.Id, Is.EqualTo(SectionCode.Start));
                Assert.That(start.Index, Is.EqualTo(42), "Index");
            });
        }
    }
}
