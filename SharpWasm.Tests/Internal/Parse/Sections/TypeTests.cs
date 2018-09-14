using System.Collections.Immutable;
using NUnit.Framework;
using SharpWasm.Core.Types;
using SharpWasm.Internal.Parse.Sections;

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
            var type = new Type(new FunctionType[0]);
            Assert.That(type.Entries, Is.EqualTo(ImmutableArray<FunctionType>.Empty));
            Assert.That(type.Count, Is.EqualTo(0));
        }
    }
}
