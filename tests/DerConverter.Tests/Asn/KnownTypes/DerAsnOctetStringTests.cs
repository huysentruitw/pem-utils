using DerConverter.Asn;
using DerConverter.Asn.KnownTypes;
using NUnit.Framework;

namespace DerConverter.Tests.Asn.KnownTypes
{
    [TestFixture]
    public class DerAsnOctetStringTests : Base<DerAsnOctetString, byte[]>
    {
        public DerAsnOctetStringTests() : base(DerAsnIdentifiers.Primitive.OctetString) { }

        [TestCase(new byte[] { })]
        [TestCase(new byte[] { 0x12, 0x34 }, 0x12, 0x34)]
        public override void DecodeConstructor_ShouldDecodeCorrectly(byte[] expectedValue, params int[] rawData)
        {
            base.DecodeConstructor_ShouldDecodeCorrectly(expectedValue, rawData);
        }

        [TestCase(new byte[] { })]
        [TestCase(new byte[] { 0x12, 0x34 }, 0x12, 0x34)]
        public override void Encode_ShouldEncodeCorrectly(byte[] value, params int[] expectedRawData)
        {
            base.Encode_ShouldEncodeCorrectly(value, expectedRawData);
        }
    }
}
