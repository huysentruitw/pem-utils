using DerConverter.Asn;
using DerConverter.Asn.KnownTypes;
using NUnit.Framework;

namespace DerConverter.Tests.Asn.KnownTypes
{
    [TestFixture]
    public class DerAsnNullTests : Base<DerAsnNull, object>
    {
        public DerAsnNullTests() : base(DerAsnIdentifiers.Primitive.Null) { }

        [TestCase(null)]
        public override void DecodeConstructor_ShouldDecodeCorrectly(object expectedValue, params int[] rawData)
        {
            base.DecodeConstructor_ShouldDecodeCorrectly(expectedValue, rawData);
        }

        [TestCase(null)]
        public override void Encode_ShouldEncodeCorrectly(object value, params int[] expectedRawData)
        {
            base.Encode_ShouldEncodeCorrectly(value, expectedRawData);
        }
    }
}
