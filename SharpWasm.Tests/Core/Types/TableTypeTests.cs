﻿using NUnit.Framework;
using SharpWasm.Core.Types;
using SharpWasm.Tests.Helpers;

namespace SharpWasm.Tests.Core.Types
{
    [TestFixture]
    public class TableTypeTests
    {
        [Test]
        public void ElementType()
        {
            var tableType = new TableType(new Limits(1));
            Assert.That(tableType.ElemType, Is.EqualTo(ElemType.AnyFunc));
        }

        [Test]
        public void Limits()
        {
            var tableType = new TableType(new Limits(1));
            Assert.That(tableType.Limits, Is.EqualTo(new Limits(1)));
        }

        [Test]
        public void Equals()
        {
            var a = new TableType(TestValues.Limits);
            var b = new TableType(TestValues.Limits);
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