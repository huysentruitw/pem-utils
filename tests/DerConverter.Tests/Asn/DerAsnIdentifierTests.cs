using System;
using System.Collections.Generic;
using DerConverter.Asn;
using NUnit.Framework;

namespace DerConverter.Tests.Asn
{
    [TestFixture]
    public class DerAsnIdentifierTests
    {
        [Test]
        public void Constructor_ShouldSetProperties()
        {
            var identifier = new DerAsnIdentifier(DerAsnTagClass.ContextSpecific, DerAsnEncodingType.Primitive, 1234);
            Assert.That(identifier.TagClass, Is.EqualTo(DerAsnTagClass.ContextSpecific));
            Assert.That(identifier.EncodingType, Is.EqualTo(DerAsnEncodingType.Primitive));
            Assert.That(identifier.Tag, Is.EqualTo(1234));

            identifier = new DerAsnIdentifier(DerAsnTagClass.Universal, DerAsnEncodingType.Constructed, 5432);
            Assert.That(identifier.TagClass, Is.EqualTo(DerAsnTagClass.Universal));
            Assert.That(identifier.EncodingType, Is.EqualTo(DerAsnEncodingType.Constructed));
            Assert.That(identifier.Tag, Is.EqualTo(5432));
        }

        [Test]
        public void Equals_EqualPropertyValues_ShouldReturnTrue()
        {
            var a = new DerAsnIdentifier(DerAsnTagClass.Private, DerAsnEncodingType.Constructed, 1234);
            var b = new DerAsnIdentifier(DerAsnTagClass.Private, DerAsnEncodingType.Constructed, 1234);
            Assert.That(a.Equals(b), Is.True);
        }

        [Test]
        public void Equals_DifferentPropertyValues_ShouldReturnFalse()
        {
            var a = new DerAsnIdentifier(DerAsnTagClass.Private, DerAsnEncodingType.Constructed, 1234);
            var b = new DerAsnIdentifier(DerAsnTagClass.ContextSpecific, DerAsnEncodingType.Constructed, 1234);
            Assert.That(a.Equals(b), Is.False);

            b = new DerAsnIdentifier(DerAsnTagClass.Private, DerAsnEncodingType.Primitive, 1234);
            Assert.That(a.Equals(b), Is.False);

            b = new DerAsnIdentifier(DerAsnTagClass.Private, DerAsnEncodingType.Constructed, 123);
            Assert.That(a.Equals(b), Is.False);
        }

        [Test]
        public void Encode_LowTagNumberForm_ShouldEncodeCorrectly()
        {
            var identifier = new DerAsnIdentifier(DerAsnTagClass.ContextSpecific, DerAsnEncodingType.Primitive, 5);
            Assert.That(identifier.Encode(), Is.EqualTo(new byte[] { 0x85 }));

            identifier = new DerAsnIdentifier(DerAsnTagClass.Universal, DerAsnEncodingType.Constructed, 7);
            Assert.That(identifier.Encode(), Is.EqualTo(new byte[] { 0x27 }));

            identifier = new DerAsnIdentifier(DerAsnTagClass.Application, DerAsnEncodingType.Constructed, 30);
            Assert.That(identifier.Encode(), Is.EqualTo(new byte[] { 0x7E }));
        }

        [Test]
        public void Encode_HighTagNumberForm_ShouldEncodeCorrectly()
        {
            var identifier = new DerAsnIdentifier(DerAsnTagClass.ContextSpecific, DerAsnEncodingType.Constructed, 1097914);
            Assert.That(identifier.Encode(), Is.EqualTo(new byte[] { 0xBF, 0xC3, 0x81, 0x3A }));

            identifier = new DerAsnIdentifier(DerAsnTagClass.ContextSpecific, DerAsnEncodingType.Constructed, 31);
            Assert.That(identifier.Encode(), Is.EqualTo(new byte[] { 0xBF, 0x1F }));
        }

        [Test]
        public void Decode_LowTagNumberForm_ShouldDecodeCorrectly()
        {
            var data = new Queue<byte>();

            data.Enqueue(0x85);
            var identifier = DerAsnIdentifier.Decode(data);
            Assert.That(data, Is.Empty);
            Assert.That(identifier.TagClass, Is.EqualTo(DerAsnTagClass.ContextSpecific));
            Assert.That(identifier.EncodingType, Is.EqualTo(DerAsnEncodingType.Primitive));
            Assert.That(identifier.Tag, Is.EqualTo(5));

            data.Enqueue(0x27);
            identifier = DerAsnIdentifier.Decode(data);
            Assert.That(data, Is.Empty);
            Assert.That(identifier.TagClass, Is.EqualTo(DerAsnTagClass.Universal));
            Assert.That(identifier.EncodingType, Is.EqualTo(DerAsnEncodingType.Constructed));
            Assert.That(identifier.Tag, Is.EqualTo(7));

            data.Enqueue(0x7E);
            identifier = DerAsnIdentifier.Decode(data);
            Assert.That(data, Is.Empty);
            Assert.That(identifier.TagClass, Is.EqualTo(DerAsnTagClass.Application));
            Assert.That(identifier.EncodingType, Is.EqualTo(DerAsnEncodingType.Constructed));
            Assert.That(identifier.Tag, Is.EqualTo(30));
        }

        [Test]
        public void Decode_HighTagNumberForm_ShouldDecodeCorrectly()
        {
            // 0xBF => [10][1][1 1111]
            // 0xC3 0x81 0x3A => [1]100 0011 [1]000 0001 [0]011 1010
            var data = new Queue<byte>(new byte[] { 0xBF, 0xC3, 0x81, 0x3A });
            var identifier = DerAsnIdentifier.Decode(data);
            Assert.That(data, Is.Empty);
            Assert.That(identifier.TagClass, Is.EqualTo(DerAsnTagClass.ContextSpecific));
            Assert.That(identifier.EncodingType, Is.EqualTo(DerAsnEncodingType.Constructed));
            Assert.That(identifier.Tag, Is.EqualTo(1097914));
        }

        [Test]
        public void Decode_NotEnoughBytes_ShouldThrowInvalidOperationException()
        {
            var data = new byte[] { 0xBF, 0xC3 };
            var ex = Assert.Throws<InvalidOperationException>(() => DerAsnIdentifier.Decode(new Queue<byte>(data)));
            Assert.That(ex.Message, Is.EqualTo("Unexpected end of queue"));
        }

        [Test]
        public void Decode_TagTooLargeToFitInLong_ShouldThrowNotSupportedException()
        {
            var data = new byte[] { 0xBF, 0xC1, 0xC2, 0xC3, 0xC4, 0xC5, 0xC6, 0xC7, 0xC8, 0xC9, 0x00 };
            var ex = Assert.Throws<NotSupportedException>(() => DerAsnIdentifier.Decode(new Queue<byte>(data)));
            Assert.That(ex.Message, Is.EqualTo("Tag number too large (more than 9 octets)"));
        }
    }
}
