using System;
using DerConverter.Asn;
using Moq;
using NUnit.Framework;

namespace DerConverter.Tests
{
    [TestFixture]
    public class DerConvertTests
    {
        #region Decode

        [Test]
        public void Decode_NullAsData_ShouldThrowArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => DerConvert.Decode(null));
            Assert.That(ex.ParamName, Is.EqualTo("data"));
        }

        [Test]
        public void Decode_EmptyDataArray_ShouldThrowArgumentException()
        {
            var ex = Assert.Throws<ArgumentException>(() => DerConvert.Decode(new byte[] { }));
            Assert.That(ex.Message, Does.StartWith("Data cannot be empty"));
            Assert.That(ex.ParamName, Is.EqualTo("data"));
        }

        [Test]
        public void Decode_NoDefaultDecoder_ShouldThrowArgumentNullException()
        {
            DerConvert.DefaultDecoder = null;
            var ex = Assert.Throws<ArgumentNullException>(() => DerConvert.Decode(new byte[] { 0x00 }));
            Assert.That(ex.ParamName, Is.EqualTo("DefaultDecoder"));
        }

        [Test]
        public void Decode_ShouldForwardCallToDecoder_ShouldDisposeDecoder()
        {
            var data = new byte[] { 1, 2, 3 };
            var decoderMock = new Mock<IDerAsnDecoder>();
            DerConvert.DefaultDecoder = () => decoderMock.Object;
            DerConvert.Decode(data);
            decoderMock.Verify(x => x.Decode(data), Times.Once);
            decoderMock.Verify(x => x.Dispose(), Times.Once);
        }

        #endregion

        #region Encode

        [Test]
        public void Encode_NullAsData_ShouldThrowArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => DerConvert.Encode(null));
            Assert.That(ex.ParamName, Is.EqualTo("data"));
        }

        [Test]
        public void Encode_NoDefaultEncoder_ShouldThrowArgumentNullException()
        {
            var typeMock = new Mock<DerAsnType>(DerAsnIdentifiers.Universal.Boolean);
            DerConvert.DefaultEncoder = null;
            var ex = Assert.Throws<ArgumentNullException>(() => DerConvert.Encode(typeMock.Object));
            Assert.That(ex.ParamName, Is.EqualTo("DefaultEncoder"));
        }

        [Test]
        public void Encode_ShouldForwardCallToEncoder_ShouldDisposeEncoder()
        {
            var typeMock = new Mock<DerAsnType>(DerAsnIdentifiers.Universal.Boolean);
            var encoderMock = new Mock<IDerAsnEncoder>();
            DerConvert.DefaultEncoder = () => encoderMock.Object;
            DerConvert.Encode(typeMock.Object);
            encoderMock.Verify(x => x.Encode(typeMock.Object), Times.Once);
            encoderMock.Verify(x => x.Dispose(), Times.Once);
        }

        #endregion
    }
}
