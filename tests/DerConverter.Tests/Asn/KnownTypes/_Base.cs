using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DerConverter.Asn;
using DerConverter.Asn.KnownTypes;
using Moq;
using NUnit.Framework;

namespace DerConverter.Tests.Asn.KnownTypes
{
    [TestFixture]
    public abstract class Base<TDerAsnType, TValue>
        where TDerAsnType : DerAsnType<TValue>
    {
        private readonly ConstructorInfo _valueConstructor;
        private readonly ConstructorInfo _decodeConstructor;

        protected readonly DerAsnIdentifier Identifier;
        protected readonly Mock<IDerAsnDecoder> DecoderMock;
        protected readonly Mock<IDerAsnEncoder> EncoderMock;

        protected Base(DerAsnIdentifier identifier)
        {
            Identifier = identifier;
            DecoderMock = new Mock<IDerAsnDecoder>();
            EncoderMock = new Mock<IDerAsnEncoder>();

            _valueConstructor = typeof(TDerAsnType) == typeof(DerAsnNull)
                ? typeof(TDerAsnType).GetConstructor(Array.Empty<Type>())
                : typeof(TDerAsnType).GetConstructor(new[] { typeof(TValue) });

            _decodeConstructor = typeof(TDerAsnType).GetConstructor(
                types: new[] { typeof(IDerAsnDecoder), typeof(DerAsnIdentifier), typeof(Queue<byte>) },
                bindingAttr: BindingFlags.NonPublic | BindingFlags.Instance,
                modifiers: null,
                binder: null);
        }

        [SetUp]
        public virtual void SetUp()
        {
            DecoderMock.Reset();
            EncoderMock.Reset();
        }

        [Test]
        public void ValueConstructor_ShouldSetIdentifier()
        {
            Assert.That(_valueConstructor, Is.Not.Null, $"No value constructor found for {typeof(TDerAsnType).Name}");
            var type = typeof(TDerAsnType) == typeof(DerAsnNull)
                ? (TDerAsnType)_valueConstructor.Invoke(Array.Empty<object>())
                : (TDerAsnType)_valueConstructor.Invoke(new object[] { default(TValue) });
            Assert.That(type.Identifier, Is.EqualTo(Identifier));
        }

        public virtual void DecodeConstructor_ShouldDecodeCorrectly(TValue expectedValue, params int[] rawData)
        {
            Assert.That(_decodeConstructor, Is.Not.Null, $"No decode constructor found for {typeof(TDerAsnType).Name}");
            var type = (TDerAsnType)_decodeConstructor.Invoke(new object[] { DecoderMock.Object, Identifier, new Queue<byte>(rawData.Select(x => (byte)x)) });
            Assert.That(type.Value, Is.EqualTo(expectedValue));
        }

        public virtual void Encode_ShouldEncodeCorrectly(TValue value, params int[] expectedRawData)
        {
            Assert.That(_valueConstructor, Is.Not.Null, $"No value constructor found for {typeof(TDerAsnType).Name}");
            var type = typeof(TDerAsnType) == typeof(DerAsnNull)
                ? (TDerAsnType)_valueConstructor.Invoke(Array.Empty<object>())
                : (TDerAsnType)_valueConstructor.Invoke(new object[] { value });
            Assert.That(type.Encode(EncoderMock.Object), Is.EqualTo(expectedRawData.Select(x => (byte)x)));
        }
    }
}
