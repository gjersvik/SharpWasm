using NUnit.Framework;
using SharpWasm.Internal.Parse.Code;
using SharpWasm.Internal.Parse.Types;
using SharpWasm.Tests.Helpers;

namespace SharpWasm.Tests.Internal.Parse.Types
{
    [TestFixture]
    public class InitExprTests
    {
        [Test]
        public void Parse()
        {
            const string hex = "412A0B";
            using (var reader = BinaryTools.HexToReader(hex))
            {
                var initExpr = new InitExpr(reader);
                Assert.That(initExpr.Instructions.Length, Is.EqualTo(2));
            }
        }
        [Test]
        public void Constructor()
        {
            var initExpr = new InitExpr(new []{new Instruction(OpCode.End)});
            Assert.That(initExpr.Instructions.Length, Is.EqualTo(1));
        }
    }
}