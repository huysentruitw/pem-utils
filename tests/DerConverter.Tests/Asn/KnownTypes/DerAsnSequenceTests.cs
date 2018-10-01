using System;
using System.Collections.Generic;
using DerConverter.Asn;
using DerConverter.Asn.KnownTypes;
using Moq;
using NUnit.Framework;

namespace DerConverter.Tests.Asn.KnownTypes
{
    [TestFixture]
    public class DerAsnSequenceTests
    {
        [Test]
        public void ValueConstructor_ShouldSetIdentifier()
        {
            var sequence = new DerAsnSequence(Array.Empty<DerAsnType>());
            Assert.That(sequence.Identifier, Is.EqualTo(DerAsnIdentifiers.Constructed.Sequence));
        }

        [Test]
        public void DecodeConstructor_ShouldDecodeCorrectly()
        {
            var data = new Queue<byte>(new byte[] { 0xA0, 0x05, 0x80 });
            var decoderMock = new Mock<IDerAsnDecoder>();
            decoderMock.Setup(x => x.Decode(It.IsAny<Queue<byte>>())).Callback<Queue<byte>>(x => x.Dequeue());

            new DerAsnSequence(decoderMock.Object, DerAsnIdentifiers.Constructed.Sequence, data);

            Assert.That(data, Has.Count.EqualTo(0));
            decoderMock.Verify(x => x.Decode(It.IsAny<Queue<byte>>()), Times.Exactly(3));
        }

        [Test]
        public void Encode_ShouldEncodeCorrectly()
        {
            var encoderMock = new Mock<IDerAsnEncoder>();
            var sequence = new DerAsnSequence(new DerAsnType[]
                {
                    new DerAsnNull(),
                    new DerAsnObjectIdentifier(1, 2, 840, 113549, 1, 1, 1),
                    new DerAsnNull()
                });

            sequence.Encode(encoderMock.Object);

            encoderMock.Verify(x => x.Encode(It.IsAny<DerAsnType>()), Times.Exactly(3));
            encoderMock.Verify(x => x.Encode(It.IsAny<DerAsnNull>()), Times.Exactly(2));
            encoderMock.Verify(x => x.Encode(It.IsAny<DerAsnObjectIdentifier>()), Times.Once);
        }
    }
}
