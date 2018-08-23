using DerConverter.Asn;
using DerConverter.Asn.KnownTypes;
using NUnit.Framework;

namespace DerConverter.Tests.Asn.KnownTypes
{
    [TestFixture]
    public class DerAsnBooleanTests
    {
        [Test]
        public void DerAsnBoolean_Constructor_ShouldDecodeCorrectly()
        {
            var boolean = new DerAsnBoolean(null, DerAsnIdentifiers.Primitive.Boolean, this.Q(0x00));
            Assert.That(boolean.Value, Is.False);

            boolean = new DerAsnBoolean(null, DerAsnIdentifiers.Primitive.Boolean, this.Q(0xFF));
            Assert.That(boolean.Value, Is.True);
        }

        [Test]
        public void DerAsnBoolean_Encode_ShouldEncodeCorrectly()
        {
            var boolean = new DerAsnBoolean(false);
            Assert.That(boolean.Encode(null), Is.EqualTo(new byte[] { 0x00 }));

            boolean = new DerAsnBoolean(true);
            Assert.That(boolean.Encode(null), Is.EqualTo(new byte[] { 0xFF }));
        }

        [Test]
        public void DerAsnBoolean_Constructor_ShouldSetUniversalIdentifier()
        {
            var boolean = new DerAsnBoolean(false);
            Assert.That(boolean.Identifier, Is.EqualTo(DerAsnIdentifiers.Primitive.Boolean));
        }
    }
}
