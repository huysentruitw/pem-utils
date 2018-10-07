using DerConverter.Asn;
using DerConverter.Asn.KnownTypes;
using NUnit.Framework;

namespace DerConverter.Tests.Asn.KnownTypes
{
    [TestFixture]
    public class DerAsnUtf8StringTests : Base<DerAsnUtf8String, string>
    {
        public DerAsnUtf8StringTests() : base(DerAsnIdentifiers.Primitive.Utf8String) { }

        [TestCase("test", 0x74, 0x65, 0x73, 0x74)]
        [TestCase(@"¯\_(ツ)_/¯", 0xC2, 0xAF, 0x5C, 0x5F, 0x28, 0xE3, 0x83, 0x84, 0x29, 0x5F, 0x2F, 0xC2, 0xAF)]
        public override void DecodeConstructor_ShouldDecodeCorrectly(string expectedValue, params int[] rawData)
        {
            var d = System.Text.Encoding.UTF8.GetBytes(@"¯\_(ツ)_/¯");
            base.DecodeConstructor_ShouldDecodeCorrectly(expectedValue, rawData);
        }

        [TestCase("test", 0x74, 0x65, 0x73, 0x74)]
        [TestCase(@"¯\_(ツ)_/¯", 0xC2, 0xAF, 0x5C, 0x5F, 0x28, 0xE3, 0x83, 0x84, 0x29, 0x5F, 0x2F, 0xC2, 0xAF)]
        public override void Encode_ShouldEncodeCorrectly(string value, params int[] expectedRawData)
        {
            base.Encode_ShouldEncodeCorrectly(value, expectedRawData);
        }
    }
}
