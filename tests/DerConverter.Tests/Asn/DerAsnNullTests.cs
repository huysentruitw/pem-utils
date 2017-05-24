using DerConverter.Asn;
using NUnit.Framework;

namespace DerConverter.Tests.Asn
{
    [TestFixture]
    public class DerAsnNullTests
    {
        [Test]
        public void DerAsnNull_Parse_ShouldDecodeCorrectly()
        {
            var type = DerAsnType.Parse(new byte[] { 0x05, 0x00 });
            Assert.That(type is DerAsnNull, Is.True);
        }

        [Test]
        public void DerAsnNull_GetBytes_ShouldEncodeCorrectly()
        {
            var type = new DerAsnNull();
            Assert.That(type.GetBytes(), Is.EqualTo(new byte[] { 0x05, 0x00 }));
        }
    }
}
