﻿using NUnit.Framework;
using SharpWasm.Internal.Parse.Code;
using SharpWasm.Tests.Helpers;

namespace SharpWasm.Tests.Internal.Parse.Code
{
    [TestFixture]
    public class FunctionBodyTests
    {
        [Test]
        public void Properties()
        {
            var functionBody = new FunctionBody(new[] {TestValues.LocalEntry, TestValues.LocalEntry},
                TestValues.InitExprOps);
            Assert.Multiple(() =>
            {
                Assert.That(functionBody.LocalCount, Is.EqualTo(2), "LocalCount");
                Assert.That(functionBody.Locals, Is.EqualTo(new[] {TestValues.LocalEntry, TestValues.LocalEntry}));
                Assert.That(functionBody.Code, Is.EqualTo(TestValues.InitExprOps), "Code");
            });
        }

        [Test]
        public void Binary()
        {
            const string hex = "0802" + TestValues.LocalEntryHex + TestValues.LocalEntryHex + TestValues.InitExprHex;
            FunctionBody functionBody;
            using (var reader = BinaryTools.HexToReader(hex))
            {
                functionBody = new FunctionBody(reader);
            }

            Assert.Multiple(() =>
            {
                Assert.That(functionBody.BodySize, Is.EqualTo(8), "BodySize");
                Assert.That(functionBody.LocalCount, Is.EqualTo(2), "LocalCount");
                Assert.That(functionBody.Locals, Is.EqualTo(new[] {TestValues.LocalEntry, TestValues.LocalEntry}));
                Assert.That(functionBody.Code, Is.EqualTo(TestValues.InitExprOps).AsCollection, "Code");
            });
        }

        [Test]
        public void Equals()
        {
            var a = new FunctionBody(new[] {TestValues.LocalEntry, TestValues.LocalEntry},
                TestValues.InitExprOps);
            var b = new FunctionBody(new[] {TestValues.LocalEntry, TestValues.LocalEntry},
                TestValues.InitExprOps);
            Assert.That(a.Equals(a), Is.True);
            Assert.That(a.Equals(b), Is.True);
            Assert.That(a.Equals(null), Is.False);
            Assert.That(a.Equals((object) a), Is.True);
            Assert.That(a.Equals((object) b), Is.True);
            Assert.That(a.Equals((object) null), Is.False);
            Assert.That(a.GetHashCode(), Is.EqualTo(b.GetHashCode()));
            Assert.That(a == b, Is.True);
            Assert.That(a != b, Is.False);
        }
    }
}