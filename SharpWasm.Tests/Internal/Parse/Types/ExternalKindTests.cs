using NUnit.Framework;
using SharpWasm.Internal.Parse;
using SharpWasm.Internal.Parse.Types;
using SharpWasm.Tests.Helpers;

namespace SharpWasm.Tests.Internal.Parse.Types
{
    public class ExternalKindTests
    {
        [Test]
        public void Parse()
        {
            const string hex = "00010203";
            using (var reader = BinaryTools.HexToReader(hex))
            {
                var kinds = ParseTools.ToArray(reader,4,ParseTools.ToExternalKind);
                Assert.That(kinds, Is.EqualTo(new [] {ExternalKind.Function, ExternalKind.Table, ExternalKind.Memory, ExternalKind.Global}));
            }
        }
    }
}
