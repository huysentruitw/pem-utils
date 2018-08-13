﻿using DerConverter.Asn;
using NUnit.Framework;
using System.Linq;

namespace DerConverter.Tests.Asn
{
    [TestFixture]
    public class DerAsnUtf8StringTests
    {
        [Test]
        public void DerAsnUtf8String_Parse_ShouldDecodeCorrectly()
        {
            var data = new byte[]
            {
                0x0C, 0x0A, 0x41, 0x70, 0x70, 0x6C, 0x65, 0x20, 0x57, 0x6F, 0x72, 0x6C
            };

            var type = DerAsnUtf8String.Parse(data);
            Assert.That(type is DerAsnUtf8String, Is.True);

            Assert.That(type.Value, Is.EqualTo(data.Skip(2).ToArray()));
        }

        [Test]
        public void DerAsnPrintable_GetBytes_ShouldEncodeCorrectly()
        {
            var type = new DerAsnUtf8String(new byte[] { 0x41, 0x70, 0x70, 0x6C, 0x65, 0x20, 0x49, 0x6E, 0x63, 0x2E });

            var data = type.GetBytes();

            Assert.That((DerAsnTypeTag)data[0], Is.EqualTo(DerAsnTypeTag.Utf8String));
            Assert.That(data[1], Is.EqualTo(0x0A));
            Assert.That(data.Skip(2).ToArray(), Is.EqualTo(new byte[] { 0x41, 0x70, 0x70, 0x6C, 0x65, 0x20, 0x49, 0x6E, 0x63, 0x2E }));
        }
    }
}
