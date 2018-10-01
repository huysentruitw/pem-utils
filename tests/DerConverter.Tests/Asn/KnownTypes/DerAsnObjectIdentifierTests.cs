using DerConverter.Asn;
using DerConverter.Asn.KnownTypes;
using NUnit.Framework;

namespace DerConverter.Tests.Asn.KnownTypes
{
    [TestFixture]
    public class DerAsnObjectIdentifierTests : Base<DerAsnObjectIdentifier, int[]>
    {
        public DerAsnObjectIdentifierTests() : base(DerAsnIdentifiers.Primitive.ObjectIdentifier) { }

        [TestCase(new int[] { 1, 2, 840, 113549 }, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D)]
        [TestCase(new int[] { 1, 3, 6, 1, 4, 1, 311, 21, 20 }, 0x2B, 0x06, 0x01, 0x04, 0x01, 0x82, 0x37, 0x15, 0x14)]
        public override void DecodeConstructor_ShouldDecodeCorrectly(int[] expectedValue, params int[] rawData)
        {
            base.DecodeConstructor_ShouldDecodeCorrectly(expectedValue, rawData);
        }

        [TestCase(new int[] { 1, 2, 840, 113549 }, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D)]
        [TestCase(new int[] { 1, 2, 840, 113549, 1, 1, 1 }, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x01, 0x01)]
        public override void Encode_ShouldEncodeCorrectly(int[] value, params int[] expectedRawData)
        {
            base.Encode_ShouldEncodeCorrectly(value, expectedRawData);
        }
    }
}
