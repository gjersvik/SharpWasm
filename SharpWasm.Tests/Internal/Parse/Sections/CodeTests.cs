using NUnit.Framework;
using SharpWasm.Internal.Parse.Sections;
using SharpWasm.Tests.Helpers;

namespace SharpWasm.Tests.Internal.Parse.Sections
{
    [TestFixture]
    public class CodeTests
    {
        [Test]
        public void Empty()
        {
            Assert.Multiple(() =>
            {
                Assert.That(CodeSection.Empty.Id, Is.EqualTo(SectionCode.Code), "Id");
                Assert.That(CodeSection.Empty.Count, Is.EqualTo(0), "Count");
                Assert.That(CodeSection.Empty.Bodies, Is.Empty, "Bodies");
            });
        }

        [Test]
        public void Properties()
        {
            var code = new CodeSection(new[] { TestValues.FunctionBody, TestValues.FunctionBody });
            Assert.Multiple(() =>
            {
                Assert.That(code.Count, Is.EqualTo(2), "Count");
                Assert.That(code.Bodies,
                    Is.EqualTo(new[] { TestValues.FunctionBody, TestValues.FunctionBody }).AsCollection, "Entries");
            });
        }
    }
}
