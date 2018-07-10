﻿using NUnit.Framework;
using SharpWasm.Internal.Parse;
using SharpWasm.Tests.Helpers;

namespace SharpWasm.Tests.Internal.Parse
{
    [TestFixture]
    class VarIntUnsignedTests
    {
        [Test]
        public void WikipediaExample()
        {
            const string hex = "E58E26";
            using (var reader = BinaryTools.HexToReader(hex))
            {
                var number = new VarIntUnsigned(reader);
                Assert.That(number.UInt, Is.EqualTo(624485));
                Assert.That(number.Count, Is.EqualTo(3));
            }
        }
        [Test]
        public void One()
        {
            const string hex = "01";
            using (var reader = BinaryTools.HexToReader(hex))
            {
                var number = new VarIntUnsigned(reader);
                Assert.That(number.Byte, Is.EqualTo(1));
                Assert.That(number.Bool, Is.True);
                Assert.That(number.Count, Is.EqualTo(1));
            }
        }
        [Test]
        public void Zero()
        {
            const string hex = "00";
            using (var reader = BinaryTools.HexToReader(hex))
            {
                var number = new VarIntUnsigned(reader);
                Assert.That(number.Byte, Is.EqualTo(0));
                Assert.That(number.Bool, Is.False);
                Assert.That(number.Count, Is.EqualTo(1));
            }
        }
    }
}
