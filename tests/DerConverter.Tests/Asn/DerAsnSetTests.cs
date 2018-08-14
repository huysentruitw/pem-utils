using System.Linq;
using DerConverter.Asn;
using NUnit.Framework;

namespace DerConverter.Tests.Asn
{
    [TestFixture]
    public class DerAsnSetTests
    {
        [Test]
        public void DerAsnSet_Parse_ShouldDecodeCorrectly()
        {
            var data = new byte[]
            {
                0x31, 0x0D,
                0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x01, 0x01,
                0x05, 0x00
            };

            var type = DerAsnType.Parse(data);
            Assert.That(type is DerAsnSet, Is.True);

            var items = type.Value as DerAsnType[];
            Assert.That(items, Is.Not.Null);
            Assert.That(items[0] is DerAsnObjectIdentifier, Is.True);
            Assert.That(items[0].Value, Is.EqualTo("1.2.840.113549.1.1.1"));
            Assert.That(items[1] is DerAsnNull, Is.True);
        }

        [Test]
        public void DerAsnSet_GetBytes_ShouldEncodeCorrectly()
        {
            var type = new DerAsnSet(new DerAsnType[]
            {
                new DerAsnNull(),
                new DerAsnObjectIdentifier("1.2.840.113549.1.1.1"),
                new DerAsnNull()
            });

            var data = type.GetBytes();
            Assert.That((DerAsnTypeTag)data[0], Is.EqualTo(DerAsnTypeTag.Set));
            Assert.That(data[1], Is.EqualTo(0x0F));
            Assert.That(data.Skip(2).ToArray(), Is.EqualTo(new byte[]
            {
                0x05, 0x00,
                0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x01, 0x01,
                0x05, 0x00
            }));
        }

        [Test]
        public void DerAsnSet_GetBytesIndefiniteLength_ShouldEncodeCorrectly()
        {
            var type = new DerAsnSet(new DerAsnType[]
            {
                new DerAsnNull(),
                new DerAsnObjectIdentifier("1.2.840.113549.1.1.1"),
                new DerAsnNull()
            });

            type.UseIndefiniteLengthEncoding = true;

            var data = type.GetBytes();
            Assert.That((DerAsnTypeTag)data[0], Is.EqualTo(DerAsnTypeTag.Set));
            Assert.That(data[1], Is.EqualTo(0x80)); // 0x80 indicates indefinite length encoding
            Assert.That(data.Skip(2).ToArray(), Is.EqualTo(new byte[]
            {
                0x05, 0x00,
                0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x01, 0x01,
                0x05, 0x00,
                0x00, 0x00 // Termination marker for indefinite length encoding
            }));
        }
    }
}
