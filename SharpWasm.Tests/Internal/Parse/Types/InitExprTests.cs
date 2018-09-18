using NUnit.Framework;
using SharpWasm.Core.Code;
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
            var initExpr = new InitExpr(new []{Instruction.End});
            Assert.That(initExpr.Instructions.Length, Is.EqualTo(1));
        }

        [Test]
        public void Equals()
        {
            var a = new InitExpr(new[] { Instruction.End });
            var b = new InitExpr(new[] { Instruction.End });
            Assert.That(a.Equals(a), Is.True);
            Assert.That(a.Equals(b), Is.True);
            Assert.That(a.Equals(null), Is.False);
            Assert.That(a.Equals((object)a), Is.True);
            Assert.That(a.Equals((object)b), Is.True);
            Assert.That(a.Equals((object)null), Is.False);
            Assert.That(a.GetHashCode(), Is.EqualTo(b.GetHashCode()));
            Assert.That(a == b, Is.True);
            Assert.That(a != b, Is.False);
        }
    }
}