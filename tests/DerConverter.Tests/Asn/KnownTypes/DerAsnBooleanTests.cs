using DerConverter.Asn;
using DerConverter.Asn.KnownTypes;
using NUnit.Framework;

namespace DerConverter.Tests.Asn.KnownTypes
{
    [TestFixture]
    public class DerAsnBooleanTests : Base<DerAsnBoolean, bool>
    {
        public DerAsnBooleanTests() : base(DerAsnIdentifiers.Primitive.Boolean) { }

        [TestCase(false, 0x00)]
        [TestCase(true, 0xFF)]
        public override void DecodeConstructor_ShouldDecodeCorrectly(bool expectedValue, params int[] rawData)
        {
            base.DecodeConstructor_ShouldDecodeCorrectly(expectedValue, rawData);
        }

        [TestCase(false, 0x00)]
        [TestCase(true, 0xFF)]
        public override void Encode_ShouldEncodeCorrectly(bool value, params int[] expectedRawData)
        {
            base.Encode_ShouldEncodeCorrectly(value, expectedRawData);
        }
    }
}
