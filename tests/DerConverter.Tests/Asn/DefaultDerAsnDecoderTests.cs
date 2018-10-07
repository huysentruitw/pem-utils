using System;
using DerConverter.Asn;
using DerConverter.Asn.KnownTypes;
using NUnit.Framework;

namespace DerConverter.Tests.Asn
{
    [TestFixture]
    public class DefaultDerAsnDecoderTests
    {
        [TestCase(typeof(DerAsnBoolean), new byte[] { 0x01, 0x01, 0xFF })]
        [TestCase(typeof(DerAsnInteger), new byte[] { 0x02, 0x01, 0x10 })]
        [TestCase(typeof(DerAsnBitString), new byte[] { 0x03, 0x07, 0x04, 0x0A, 0x3B, 0x5F, 0x29, 0x1C, 0xD0 })]
        [TestCase(typeof(DerAsnOctetString), new byte[] { 0x04, 0x08, 0x01, 0x23, 0x45, 0x67, 0x89, 0xAB, 0xCD, 0xEF })]
        [TestCase(typeof(DerAsnNull), new byte[] { 0x05, 0x00 })]
        [TestCase(typeof(DerAsnNull), new byte[] { 0x05, 0x81, 0x00 })]
        [TestCase(typeof(DerAsnObjectIdentifier), new byte[] { 0x06, 0x06, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D })]
        [TestCase(typeof(DerAsnUtf8String), new byte[] { 0x0C, 0x04, 0x74, 0x65, 0x73, 0x74 })]
        [TestCase(typeof(DerAsnSequence), new byte[] { 0x30, 0x0D, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x01, 0x01, 0x05, 0x00 })]
        [TestCase(typeof(DerAsnSet), new byte[] { 0x31, 0x0D, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x01, 0x01, 0x05, 0x00 })]
        public void Decode_ShouldDecodeAllKnownDefaultTypes(Type expectedType, byte[] rawData)
        {
            var decoder = new DefaultDerAsnDecoder();
            Assert.That(decoder.Decode(rawData), Is.InstanceOf(expectedType));
        }

        [Test]
        public void RegisterGenericType_PassNullAsTypeConstructor_ShouldThrowArgumentNullException()
        {
            var decoder = new DefaultDerAsnDecoder();
            var ex = Assert.Throws<ArgumentNullException>(() => decoder.RegisterGenericType(DerAsnEncodingType.Constructed, 0, null));
            Assert.That(ex.ParamName, Is.EqualTo("typeConstructor"));
        }

        [Test]
        public void RegisterGenericType_RegisterSameTypeTwice_ReplaceSetToFalse_ShouldThrowInvalidOperationException()
        {
            var decoder = new DefaultDerAsnDecoder();
            decoder.RegisterGenericType(DerAsnEncodingType.Constructed, 4095, (_, __, ___) => null);
            var ex = Assert.Throws<InvalidOperationException>(() => decoder.RegisterGenericType(DerAsnEncodingType.Constructed, 0xFFF, (_, __, ___) => null));
            Assert.That(ex.Message, Is.EqualTo("Type with encoding type Constructed and tag number 4095 already registered"));
        }

        [Test]
        public void RegisterGenericType_RegisterSameTypeTwice_ReplaceSetToTrue_ShouldNotThrowException()
        {
            var decoder = new DefaultDerAsnDecoder();
            decoder.RegisterGenericType(DerAsnEncodingType.Constructed, 4095, (_, __, ___) => null);
            Assert.DoesNotThrow(() => decoder.RegisterGenericType(DerAsnEncodingType.Constructed, 4095, (_, __, ___) => null, true));
        }

        [Test]
        public void RegisterType_PassNullAsIdentifier_ShouldThrowArgumentNullException()
        {
            var decoder = new DefaultDerAsnDecoder();
            var ex = Assert.Throws<ArgumentNullException>(() => decoder.RegisterType(null, (_, __, ___) => null));
            Assert.That(ex.ParamName, Is.EqualTo("identifier"));
        }

        [Test]
        public void RegisterType_PassNullAsTypeConstructor_ShouldThrowArgumentNullException()
        {
            var identifier = DerAsnIdentifiers.Primitive.Boolean;
            var decoder = new DefaultDerAsnDecoder();
            var ex = Assert.Throws<ArgumentNullException>(() => decoder.RegisterType(identifier, null));
            Assert.That(ex.ParamName, Is.EqualTo("typeConstructor"));
        }

        [Test]
        public void RegisterType_RegisterSameTypeTwice_ReplaceSetToFalse_ShouldThrowInvalidOperationException()
        {
            var identifier = DerAsnIdentifiers.Primitive.Boolean;
            var decoder = new DefaultDerAsnDecoder();
            decoder.RegisterType(identifier, (_, __, ___) => null);
            var ex = Assert.Throws<InvalidOperationException>(() => decoder.RegisterType(identifier, (_, __, ___) => null));
            Assert.That(ex.Message, Is.EqualTo("Type with class Universal, encoding type Primitive and tag number 1 already registered"));
        }

        [Test]
        public void RegisterType_RegisterSameTypeTwice_ReplaceSetToTrue_ShouldNotThrowException()
        {
            var identifier = DerAsnIdentifiers.Primitive.Boolean;
            var decoder = new DefaultDerAsnDecoder();
            decoder.RegisterType(identifier, (_, __, ___) => null);
            Assert.DoesNotThrow(() => decoder.RegisterType(identifier, (_, __, ___) => null, true));
        }
    }
}
