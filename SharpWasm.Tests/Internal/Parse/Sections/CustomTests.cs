using NUnit.Framework;
using SharpWasm.Internal.Parse.Sections;

namespace SharpWasm.Tests.Internal.Parse.Sections
{
    [TestFixture]
    public class CustomTests
    {
        [Test]
        public void Name()
        {
            var custom = new Custom("name", new byte[] { });
            Assert.That(custom.Id, Is.EqualTo(SectionCode.Custom));
            Assert.That(custom.Name, Is.EqualTo("name"));
            Assert.That(custom.NameLen, Is.EqualTo(4));
        }
    }
}
