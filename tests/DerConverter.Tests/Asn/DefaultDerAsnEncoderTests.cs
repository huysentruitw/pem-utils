using System.Collections;
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

            data = encoder.Encode(new DerAsnInteger(0x10));
            Assert.That(data, Is.EqualTo(new byte[] { 0x02, 0x01, 0x10 }));

            data = encoder.Encode(new DerAsnBitString(new BitArray(new[] { false, true, false, true, true, true, false, false, false, true })));
            Assert.That(data, Is.EqualTo(new byte[] { 0x03, 0x03, 0x06, 0x5C, 0x40 }));
        }
    }
}
