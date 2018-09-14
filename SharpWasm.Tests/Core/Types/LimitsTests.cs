using NUnit.Framework;
using SharpWasm.Core.Types;

namespace SharpWasm.Tests.Core.Types
{
    [TestFixture]
    public class LimitsTests
    {
        [Test]
        public void Min()
        {
            var limits = new Limits(1);
            Assert.That(limits.Min, Is.EqualTo(1));
        }

        [Test]
        public void Max()
        {
            var limits = new Limits(1, 2);
            Assert.That(limits.Max, Is.EqualTo(2));
        }

        [Test]
        public void Equals()
        {
            var a = new Limits(1);
            var b = new Limits(1);
            Assert.That(a.Equals(a), Is.True);
            Assert.That(a.Equals(b), Is.True);
            Assert.That(a.Equals(null), Is.False);
            Assert.That(a.Equals((object)a), Is.True);
            Assert.That(a.Equals((object)b), Is.True);
            Assert.That(a.GetHashCode(), Is.EqualTo(b.GetHashCode()));
            Assert.That(a == b, Is.True);
            Assert.That(a != b, Is.False);
        }
    }
}