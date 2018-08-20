//using DerConverter.Asn;
//using NUnit.Framework;

//namespace DerConverter.Tests.Asn
//{
//    [TestFixture]
//    public class DerAsnObjectIdentifierTests
//    {
//        [Test]
//        public void DerAsnObjectIdentifier_Parse_ShouldDecodeCorrectly()
//        {
//            var data = new byte[]
//            {
//                0x06, 0x09,
//                0x2B, 0x06, 0x01, 0x04, 0x01, 0x82, 0x37, 0x15, 0x14
//            };

//            var type = DerAsnType.Parse(data);
//            Assert.That(type is DerAsnObjectIdentifier, Is.True);

//            var objectIdentifier = type as DerAsnObjectIdentifier;
//            Assert.That(objectIdentifier.Value, Is.EqualTo("1.3.6.1.4.1.311.21.20"));
//        }

//        [Test]
//        public void DerAsnObjectIdentifier_GetBytes_ShouldEncodeCorrectly()
//        {
//            var type = new DerAsnObjectIdentifier("1.2.840.113549.1.1.1");

//            var data = type.GetBytes();

//            Assert.That(data, Is.EqualTo(new byte[] { 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x01, 0x01 }));
//        }
//    }
//}
