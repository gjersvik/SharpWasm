using System.Collections.Immutable;
using NUnit.Framework;
using SharpWasm.Internal.Parse.Sections;
using SharpWasm.Internal.Parse.Types;

namespace SharpWasm.Tests.Internal.Parse.Sections
{
    [TestFixture]
    public class TypeTests
    {
        [Test]
        public void Empty()
        {
            Assert.Multiple(() =>
            {
                Assert.That(Type.Empty.Id, Is.EqualTo(SectionCode.Type), "Id");
                Assert.That(Type.Empty.Count, Is.EqualTo(0), "Count");
                Assert.That(Type.Empty.Entries, Is.Empty, "Entries");
            });
        }

        [Test]
        public void Count()
        {
            var type = new Type(new FuncType[]{});
            Assert.That(type.Entries, Is.EqualTo(ImmutableArray<FuncType>.Empty));
            Assert.That(type.Count, Is.EqualTo(0));
        }
    }
}
