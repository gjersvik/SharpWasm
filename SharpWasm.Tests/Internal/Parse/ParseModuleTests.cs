using System.Collections.Immutable;
using NUnit.Framework;
using SharpWasm.Internal.Parse;
using SharpWasm.Internal.Parse.Sections;

namespace SharpWasm.Tests.Internal.Parse
{
    [TestFixture]
    public class ParseModuleTests
    {
        [Test]
        public void Empty()
        {
            var module = new ParseModule(ImmutableArray<ISection>.Empty);
            Assert.Multiple(() =>
            {
                Assert.That(module.MagicNumber, Is.EqualTo(0x6d736100));
                Assert.That(module.Version, Is.EqualTo(1));
                Assert.That(module.ClassicSections, Is.Empty, "ClassicSections");
                Assert.That(module.Customs, Is.Empty, "Customs");
                Assert.That(module.Types, Is.Empty, "Types");
                Assert.That(module.Imports, Is.Empty, "Imports");
                Assert.That(module.Functions, Is.Empty, "Functions");
                Assert.That(module.Tables, Is.Empty, "Tables");
                Assert.That(module.Memories, Is.Empty, "Memories");
                Assert.That(module.Globals, Is.Empty, "Globals");
                Assert.That(module.Exports, Is.Empty, "Exports");
                Assert.That(module.Starts, Is.Empty, "Starts");
                Assert.That(module.Elements, Is.Empty, "Elements");
                Assert.That(module.Code, Is.Empty, "Code");
                Assert.That(module.Data, Is.Empty, "Data");
            });
        }
    }
}
