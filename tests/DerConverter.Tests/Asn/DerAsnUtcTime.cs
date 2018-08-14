using DerConverter.Asn;
using NUnit.Framework;
using System.Linq;

namespace DerConverter.Tests.Asn
{
    [TestFixture]
    public class DerAsnUtcTimeTests
    {
        [Test]
        public void DerAsnUtcTime_Parse_ShouldDecodeCorrectly()
        {
            var data = new byte[]
            {
                0x17, 0x0D,
                0x31, 0x33, 0x30, 0x32, 0x30, 0x37, 0x32, 0x31, 0x34, 0x38, 0x34, 0x37, 0x5A
            };

            var type = DerAsnType.Parse(data);
            Assert.That(type is DerAsnUtcTime, Is.True);

            Assert.That(type.Value, Is.EqualTo(data.Skip(2).ToArray()));
        }

        [Test]
        public void DerAsnUtcTime_GetBytes_ShouldEncodeCorrectly()
        {
            var type = new DerAsnUtcTime(new byte[] { 0x31, 0x33, 0x30, 0x32, 0x30, 0x37, 0x32, 0x31, 0x34, 0x38, 0x34, 0x37, 0x5A });

            var data = type.GetBytes();

            Assert.That((DerAsnTypeTag)data[0], Is.EqualTo(DerAsnTypeTag.UtcTime));
            Assert.That(data[1], Is.EqualTo(0xD));
            Assert.That(data.Skip(2).ToArray(), Is.EqualTo(new byte[] { 0x31, 0x33, 0x30, 0x32, 0x30, 0x37, 0x32, 0x31, 0x34, 0x38, 0x34, 0x37, 0x5A }));
        }
    }
}
