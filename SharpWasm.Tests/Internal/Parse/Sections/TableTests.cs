using NUnit.Framework;
using SharpWasm.Internal.Parse.Sections;
using SharpWasm.Tests.Helpers;

namespace SharpWasm.Tests.Internal.Parse.Sections
{
    [TestFixture]
    public class TableTests
    {
        [Test]
        public void Properties()
        {
            var table = new Table(new [] { TestValues.TableType });
            Assert.Multiple(() =>
            {
                Assert.That(table.Count, Is.EqualTo(1), "Count");
                Assert.That(table.Entries, Is.EqualTo(new [] { TestValues.TableType }).AsCollection, "Entries");
            });
        }
    }
}
