using NUnit.Framework;
using SharpWasm.Core.Code;
using SharpWasm.Core.Parser;
using SharpWasm.Tests.Helpers;

namespace SharpWasm.Tests.Core.Parser
{
    public class CodeParserTests
    {
        [Test]
        public void ToBrTable()
        {
            const string hex = "040102030405";
            BrTable brTable;
            using (var reader = BinaryTools.HexToReader(hex))
            {
                brTable = CodeParser.ToBrTable(reader);
            }

            Assert.Multiple(() =>
            {
                Assert.That(brTable.TargetTable, Is.EqualTo(new[] { 1, 2, 3, 4 }), "TargetTable");
                Assert.That(brTable.DefaultTarget, Is.EqualTo(5), "DefaultTarget");
            });
        }

        [Test]
        public void ToCallIndirect()
        {
            const string hex = "0401";
            CallIndirect callIndirect;
            using (var reader = BinaryTools.HexToReader(hex))
            {
                callIndirect = CodeParser.ToCallIndirect(reader);
            }

            Assert.Multiple(() =>
            {
                Assert.That(callIndirect.TypeIndex, Is.EqualTo(4), "TypeIndex");
                Assert.That(callIndirect.Reserved, Is.True, "Reserved");
            });
        }
    }
}
