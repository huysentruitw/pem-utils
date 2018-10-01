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

            data = encoder.Encode(new DerAsnOctetString(new byte[] { 0x01, 0x23, 0x45, 0x67, 0x89, 0xAB, 0xCD, 0xEF }));
            Assert.That(data, Is.EqualTo(new byte[] { 0x04, 0x08, 0x01, 0x23, 0x45, 0x67, 0x89, 0xAB, 0xCD, 0xEF }));

            data = encoder.Encode(new DerAsnNull());
            Assert.That(data, Is.EqualTo(new byte[] { 0x05, 0x00 }));
        }
    }
}
