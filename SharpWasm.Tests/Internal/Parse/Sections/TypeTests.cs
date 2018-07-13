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
        public void Count()
        {
            var type = new Type(new FuncType[]{});
            Assert.That(type.Entries, Is.EqualTo(ImmutableArray<FuncType>.Empty));
            Assert.That(type.Count, Is.EqualTo(0));
        }
    }
}
