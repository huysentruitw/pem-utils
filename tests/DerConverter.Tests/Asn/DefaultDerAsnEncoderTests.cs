using System.Collections;
using System.Linq;
using DerConverter.Asn;
using DerConverter.Asn.KnownTypes;
using NUnit.Framework;

namespace DerConverter.Tests.Asn
{
    [TestFixture]
    public class DefaultDerAsnEncoderTests
    {
        [Test]
        public void Encode_DerAsnBoolean_ShouldEncodeKnownDefaultType()
        {
            var encoder = new DefaultDerAsnEncoder();
            var data = encoder.Encode(new DerAsnBoolean(true));
            Assert.That(data, Is.EqualTo(new byte[] { 0x01, 0x01, 0xFF }));
        }

        [Test]
        public void Encode_DerAsnInteger_ShouldEncodeKnownDefaultType()
        {
            var encoder = new DefaultDerAsnEncoder();
            var data = encoder.Encode(new DerAsnInteger(0x10));
            Assert.That(data, Is.EqualTo(new byte[] { 0x02, 0x01, 0x10 }));
        }

        [Test]
        public void Encode_DerAsnBitString_ShouldEncodeKnownDefaultType()
        {
            var encoder = new DefaultDerAsnEncoder();
            var bits = new[] { false, true, false, true, true, true, false, false, false, true };
            var data = encoder.Encode(new DerAsnBitString(new BitArray(bits.Reverse().ToArray())));
            Assert.That(data, Is.EqualTo(new byte[] { 0x03, 0x03, 0x06, 0x5C, 0x40 }));
        }

        [Test]
        public void Encode_DerAsnOctetString_ShouldEncodeKnownDefaultType()
        {
            var encoder = new DefaultDerAsnEncoder();
            var data = encoder.Encode(new DerAsnOctetString(new byte[] { 0x01, 0x23, 0x45, 0x67, 0x89, 0xAB, 0xCD, 0xEF }));
            Assert.That(data, Is.EqualTo(new byte[] { 0x04, 0x08, 0x01, 0x23, 0x45, 0x67, 0x89, 0xAB, 0xCD, 0xEF }));
        }

        [Test]
        public void Encode_DerAsnNull_ShouldEncodeKnownDefaultType()
        {
            var encoder = new DefaultDerAsnEncoder();
            var data = encoder.Encode(new DerAsnNull());
            Assert.That(data, Is.EqualTo(new byte[] { 0x05, 0x00 }));
        }

        [Test]
        public void Encode_DerAsnObjectIdentifier_ShouldEncodeKnownDefaultType()
        {
            var encoder = new DefaultDerAsnEncoder();
            var data = encoder.Encode(new DerAsnObjectIdentifier(new int[] { 1, 2, 840, 113549 }));
            Assert.That(data, Is.EqualTo(new byte[] { 0x06, 0x06, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D }));
        }

        [Test]
        public void Encode_DerAsnUtf8String_ShouldEncodeKnownDefaultType()
        {
            var encoder = new DefaultDerAsnEncoder();
            var data = encoder.Encode(new DerAsnUtf8String("test"));
            Assert.That(data, Is.EqualTo(new byte[] { 0x0C, 0x04, 0x74, 0x65, 0x73, 0x74 }));
        }

        [Test]
        public void Encode_DerAsnPrintableString_ShouldEncodeKnownDefaultType()
        {
            var encoder = new DefaultDerAsnEncoder();
            var data = encoder.Encode(new DerAsnPrintableString("test"));
            Assert.That(data, Is.EqualTo(new byte[] { 0x13, 0x04, 0x74, 0x65, 0x73, 0x74 }));
        }

        [Test]
        public void Encode_DerAsnIa5String_ShouldEncodeKnownDefaultType()
        {
            var encoder = new DefaultDerAsnEncoder();
            var data = encoder.Encode(new DerAsnIa5String("test"));
            Assert.That(data, Is.EqualTo(new byte[] { 0x16, 0x04, 0x74, 0x65, 0x73, 0x74 }));
        }

        [Test]
        public void Encode_DerAsnSequence_ShouldEncodeKnownDefaultType()
        {
            var encoder = new DefaultDerAsnEncoder();
            var data = encoder.Encode(new DerAsnSequence(new DerAsnType[]
            {
                new DerAsnNull(),
                new DerAsnObjectIdentifier(1, 2, 840, 113549, 1, 1, 1),
                new DerAsnNull()
            }));
            Assert.That(data, Is.EqualTo(new byte[] { 0x30, 0x0F, 0x05, 0x00, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x01, 0x01, 0x05, 0x00 }));
        }

        [Test]
        public void Encode_DerAsnSet_ShouldEncodeKnownDefaultType()
        {
            var encoder = new DefaultDerAsnEncoder();
            var data = encoder.Encode(new DerAsnSet(new DerAsnType[]
            {
                new DerAsnNull(),
                new DerAsnObjectIdentifier(1, 2, 840, 113549, 1, 1, 1),
                new DerAsnNull()
            }));
            Assert.That(data, Is.EqualTo(new byte[] { 0x31, 0x0F, 0x05, 0x00, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x01, 0x01, 0x05, 0x00 }));
        }
    }
}
