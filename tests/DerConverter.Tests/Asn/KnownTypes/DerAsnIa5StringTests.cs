using DerConverter.Asn;
using DerConverter.Asn.KnownTypes;
using NUnit.Framework;

namespace DerConverter.Tests.Asn.KnownTypes
{
    [TestFixture]
    public class DerAsnIa5StringTests : Base<DerAsnIa5String, string>
    {
        public DerAsnIa5StringTests() : base(DerAsnIdentifiers.Primitive.Ia5String) { }

        [TestCase("Test", 0x54, 0x65, 0x73, 0x74)]
        public override void DecodeConstructor_ShouldDecodeCorrectly(string expectedValue, params int[] rawData)
        {
            base.DecodeConstructor_ShouldDecodeCorrectly(expectedValue, rawData);
        }

        [TestCase("Test", 0x54, 0x65, 0x73, 0x74)]
        public override void Encode_ShouldEncodeCorrectly(string value, params int[] expectedRawData)
        {
            base.Encode_ShouldEncodeCorrectly(value, expectedRawData);
        }
    }
}
