using DerConverter.Asn;
using DerConverter.Asn.KnownTypes;
using NUnit.Framework;

namespace DerConverter.Tests.Asn
{
    [TestFixture]
    public class DefaultDerAsnEncoderTests
    {
        [Test]
        public void Encode_ShouldEncodeAllKnownDefaultTypes()
        {
            var encoder = new DefaultDerAsnEncoder();

            var data = encoder.Encode(new DerAsnBoolean(true));
            Assert.That(data, Is.EqualTo(new byte[] { 0x01, 0x01, 0xFF }));
        }
    }
}
