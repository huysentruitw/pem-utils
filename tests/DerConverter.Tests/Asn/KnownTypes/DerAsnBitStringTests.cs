using System.Collections;
using DerConverter.Asn;
using DerConverter.Asn.KnownTypes;
using NUnit.Framework;

namespace DerConverter.Tests.Asn.KnownTypes
{
    [TestFixture]
    public class DerAsnBitStringTests : Base<DerAsnBitString, BitArray>
    {
        public DerAsnBitStringTests() : base(DerAsnIdentifiers.Primitive.BitString) { }

        [TestCase(new bool[0], 0x00)]
        [TestCase(new[] { false, true, false, true, true, false, true, false }, 0x00, 0x5A)]
        [TestCase(new[] { true, false, true, false, false, false, true, true, false, true }, 0x06, 0xA3, 0x40)]
        [TestCase(new[] { true, false, true, false, false, false, true, true, false, false, false }, 0x05, 0xA3, 0x00)]
        public void DecodeConstructor_ShouldDecodeCorrectly(bool[] expectedValue, params int[] rawData)
        {
            base.DecodeConstructor_ShouldDecodeCorrectly(new BitArray(expectedValue), rawData);
        }

        [TestCase(new bool[0], 0x00)]
        [TestCase(new[] { false, true, false, true, true, false, true, false }, 0x00, 0x5A)]
        [TestCase(new[] { true, false, true, false, false, false, true, true, false, true }, 0x06, 0xA3, 0x40)]
        [TestCase(new[] { true, false, true, false, false, false, true, true, false, false, false }, 0x05, 0xA3, 0x00)]
        public void Encode_ShouldEncodeCorrectly(bool[] value, params int[] expectedRawData)
        {
            base.Encode_ShouldEncodeCorrectly(new BitArray(value), expectedRawData);
        }
    }
}
