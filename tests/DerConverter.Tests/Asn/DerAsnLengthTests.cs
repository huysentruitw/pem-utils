using System;
using System.Collections.Generic;
using DerConverter.Asn;
using NUnit.Framework;

namespace DerConverter.Tests.Asn
{
    [TestFixture]
    public class DerAsnLengthTests
    {
        [Test]
        public void Constructor_ShouldSetProperties()
        {
            var length = new DerAsnLength(666);
            Assert.That(length.Length, Is.EqualTo(666));
        }

        [Test]
        public void Equals_EqualPropertyValues_ShouldReturnTrue()
        {
            var a = new DerAsnLength(543);
            var b = new DerAsnLength(543);
            Assert.That(a.Equals(b), Is.True);
        }

        [Test]
        public void Equals_DifferentPropertyValues_ShouldReturnFalse()
        {
            var a = new DerAsnLength(543);
            var b = new DerAsnLength(544);
            Assert.That(a.Equals(b), Is.False);
        }

        [Test]
        public void Encode_ShortForm_ShouldEncodeCorrectly()
        {
            var length = new DerAsnLength(20);
            var result = length.Encode();
            Assert.That(result, Is.EqualTo(new byte[] { 0x14 }));

            length = new DerAsnLength(127);
            result = length.Encode();
            Assert.That(result, Is.EqualTo(new byte[] { 0x7F }));
        }

        [Test]
        public void Encode_LongForm_ShouldEncodeCorrectly()
        {
            var length = new DerAsnLength(128);
            var result = length.Encode();
            Assert.That(result, Is.EqualTo(new byte[] { 0x81, 0x80 }));

            length = new DerAsnLength(201);
            result = length.Encode();
            Assert.That(result, Is.EqualTo(new byte[] { 0x81, 0xC9 }));

            length = new DerAsnLength(123456);
            result = length.Encode();
            Assert.That(result, Is.EqualTo(new byte[] { 0x83, 0x01, 0xE2, 0x40 }));
        }

        [Test]
        public void Decode_ShortForm_ShouldDecodeCorrectly()
        {
            var data = new Queue<byte>();

            data.Enqueue(0x14);
            var length = DerAsnLength.Decode(data);
            Assert.That(data, Is.Empty);
            Assert.That(length.Length, Is.EqualTo(20));

            data.Enqueue(0x7F);
            length = DerAsnLength.Decode(data);
            Assert.That(data, Is.Empty);
            Assert.That(length.Length, Is.EqualTo(127));
        }

        [Test]
        public void Decode_LongForm_ShouldDecodeCorrectly()
        {
            var data = new Queue<byte>();

            data.Enqueue(0x81); data.Enqueue(0x80);
            var length = DerAsnLength.Decode(data);
            Assert.That(data, Is.Empty);
            Assert.That(length.Length, Is.EqualTo(128));

            data.Enqueue(0x81); data.Enqueue(0xC9);
            length = DerAsnLength.Decode(data);
            Assert.That(data, Is.Empty);
            Assert.That(length.Length, Is.EqualTo(201));

            data.Enqueue(0x83); data.Enqueue(0x01); data.Enqueue(0xE2); data.Enqueue(0x40);
            length = DerAsnLength.Decode(data);
            Assert.That(data, Is.Empty);
            Assert.That(length.Length, Is.EqualTo(123456));
        }

        [Test]
        public void Decode_NotEnoughBytes_ShouldThrowInvalidOperationException()
        {
            var data = new Queue<byte>(new byte[] { 0x83, 0x81, 0xE2 });
            Assert.Throws<InvalidOperationException>(() => DerAsnLength.Decode(data));
        }
    }
}
