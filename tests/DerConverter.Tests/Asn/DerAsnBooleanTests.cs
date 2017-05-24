using DerConverter.Asn;
using NUnit.Framework;

namespace DerConverter.Tests.Asn
{
    [TestFixture]
    class DerAsnBooleanTests
    {
        [Test]
        public void DerAsnBoolean_Parse_ShouldDecodeCorrectly()
        {
            var dataFalse = new byte[] { 0x01, 0x01, 0x00 };
            var dataTrue = new byte[] { 0x01, 0x01, 0x07 };

            var type = DerAsnType.Parse(dataFalse);
            Assert.That(type is DerAsnBoolean, Is.True);
            Assert.That((bool)type.Value, Is.False);

            type = DerAsnType.Parse(dataTrue);
            Assert.That(type is DerAsnBoolean, Is.True);
            Assert.That((bool)type.Value, Is.True);
        }

        [Test]
        public void DerAsnBoolean_GetBytes_ShouldEncodeCorrectly()
        {
            var type = new DerAsnBoolean(false);
            var data = type.GetBytes();

            Assert.That(data.Length, Is.EqualTo(3));
            Assert.That(data[0], Is.EqualTo(0x01));
            Assert.That(data[1], Is.EqualTo(0x01));
            Assert.That(data[2], Is.EqualTo(0x00));

            type = new DerAsnBoolean(true);
            data = type.GetBytes();

            Assert.That(data.Length, Is.EqualTo(3));
            Assert.That(data[0], Is.EqualTo(0x01));
            Assert.That(data[1], Is.EqualTo(0x01));
            Assert.That(data[2], Is.EqualTo(0xFF));
        }
    }
}
