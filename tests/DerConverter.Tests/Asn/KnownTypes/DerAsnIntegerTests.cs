using System.Numerics;
using DerConverter.Asn;
using DerConverter.Asn.KnownTypes;
using NUnit.Framework;

namespace DerConverter.Tests.Asn.KnownTypes
{
    public class DerAsnIntegerTests : Base<DerAsnInteger, BigInteger>
    {
        public DerAsnIntegerTests() : base(DerAsnIdentifiers.Primitive.Integer) { }

        [TestCase(0, 0x00)]
        [TestCase(-40, 0xD8)]
        [TestCase(8898, 0x22, 0xC2)]
        [TestCase(-23870, 0xA2, 0xC2)]
        [TestCase(-536870974, 0xDF, 0xFF, 0xFF, 0xC2)]
        public void DecodeConstructor_ShouldDecodeCorrectly(long expectedValue, params int[] rawData)
        {
            base.DecodeConstructor_ShouldDecodeCorrectly(expectedValue, rawData);
        }

        [TestCase(0, 0x00)]
        [TestCase(-40, 0xD8)]
        [TestCase(8898, 0x22, 0xC2)]
        [TestCase(-23870, 0xA2, 0xC2)]
        [TestCase(-536870974, 0xDF, 0xFF, 0xFF, 0xC2)]
        [TestCase(-128, 0x80)]
        [TestCase(-2104048, 0xDF, 0xE5, 0x10)]
        [TestCase(-10485761, 0xFF, 0x5F, 0xFF, 0xFF)]
        public void Encode_ShouldEncodeCorrectly(long value, params int[] expectedRawData)
        {
            base.Encode_ShouldEncodeCorrectly(value, expectedRawData);
        }
    }
}
