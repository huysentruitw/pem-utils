using System.Linq;
using DerConverter.Asn;
using NUnit.Framework;

namespace DerConverter.Tests.Asn
{
    [TestFixture]
    public class DerAsnOctetStringTests
    {
        [Test]
        public void DerAsnOctetString_Parse_ShouldDecodeCorrectly()
        {
            var data = new byte[]
            {
                0x04, 0x0A,
                0x1E, 0x08, 0x00, 0x55, 0x00, 0x73, 0x00, 0x65, 0x00, 0x72
            };

            var type = DerAsnType.Parse(data);
            Assert.That(type is DerAsnOctetString, Is.True);

            Assert.That(type.Value, Is.EqualTo(data.Skip(2).ToArray()));
        }

        [Test]
        public void DerAsnOctetString_GetBytes_ShouldEncodeCorrectly()
        {
            var type = new DerAsnOctetString(new byte[] { 0x1E, 0x08, 0x00, 0x55, 0x00, 0x73, 0x00, 0x65, 0x00, 0x72 });

            var data = type.GetBytes();

            Assert.That((DerAsnTypeTag)data[0], Is.EqualTo(DerAsnTypeTag.OctetString));
            Assert.That(data[1], Is.EqualTo(0x0A));
            Assert.That(data.Skip(2).ToArray(), Is.EqualTo(new byte[] { 0x1E, 0x08, 0x00, 0x55, 0x00, 0x73, 0x00, 0x65, 0x00, 0x72 }));
        }
    }
}
