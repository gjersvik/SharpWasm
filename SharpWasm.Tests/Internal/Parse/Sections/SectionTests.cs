using NUnit.Framework;
using SharpWasm.Internal.Parse.Sections;

namespace SharpWasm.Tests.Internal.Parse.Sections
{
    [TestFixture]
    public class SectionTests
    {
        [Test]
        public void Id()
        {
            var section = (ISection)new Start(0);
            Assert.That(section.Id, Is.EqualTo(SectionCode.Start));
        }
    }
}
