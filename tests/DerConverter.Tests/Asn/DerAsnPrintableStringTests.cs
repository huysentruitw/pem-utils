using DerConverter.Asn;
using NUnit.Framework;
using System.Linq;

namespace DerConverter.Tests.Asn
{
    [TestFixture]
    public class DerAsnPrintableStringTests
    {
        [Test]
        public void DerAsnPrintable_Parse_ShouldDecodeCorrectly()
        {
            var data = new byte[]
            {
                0x13, 0x02, 0x55, 0x53
            };

            var type = DerAsnPrintableString.Parse(data);
            Assert.That(type is DerAsnPrintableString, Is.True);

            Assert.That(type.Value, Is.EqualTo(data.Skip(2).ToArray()));
        }

        [Test]
        public void DerAsnPrintable_GetBytes_ShouldEncodeCorrectly()
        {
            var type = new DerAsnPrintableString(new byte[] { 0x55, 0x53 });

            var data = type.GetBytes();

            Assert.That((DerAsnTypeTag)data[0], Is.EqualTo(DerAsnTypeTag.PrintableString));
            Assert.That(data[1], Is.EqualTo(0x02));
            Assert.That(data.Skip(2).ToArray(), Is.EqualTo(new byte[] { 0x55, 0x53 }));
        }
    }
}
