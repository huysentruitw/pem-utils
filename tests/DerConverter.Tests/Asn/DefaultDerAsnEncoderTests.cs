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

            data = encoder.Encode(new DerAsnObjectIdentifier(new int[] { 1, 2, 840, 113549 }));
            Assert.That(data, Is.EqualTo(new byte[] { 0x06, 0x06, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D }));

            data = encoder.Encode(new DerAsnSequence(new DerAsnType[]
            {
                new DerAsnNull(),
                new DerAsnObjectIdentifier(1, 2, 840, 113549, 1, 1, 1),
                new DerAsnNull()
            }));
            Assert.That(data, Is.EqualTo(new byte[] { 0x30, 0x0F, 0x05, 0x00, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x01, 0x01, 0x05, 0x00 }));
        }
    }
}
