using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace DerConverter.Asn.KnownTypes
{
    public class DerAsnInteger : DerAsnType<BigInteger>
    {
        internal DerAsnInteger(IDerAsnDecoder decoder, DerAsnIdentifier identifier, Queue<byte> rawData)
            : base(decoder, identifier, rawData)
        {
        }

        public DerAsnInteger(DerAsnIdentifier identifier, BigInteger value)
            : base(identifier, value)
        {
        }

        public DerAsnInteger(BigInteger value)
            : this(DerAsnIdentifiers.Primitive.Integer, value)
        {
        }

        protected override BigInteger DecodeValue(IDerAsnDecoder decoder, Queue<byte> rawData)
        {
            if (rawData.Count < 1) throw new ArgumentException("Integer-type must contain at least one data byte", nameof(rawData));
            var littleEndianData = rawData.DequeueAll().Reverse().ToArray();
            return new BigInteger(littleEndianData);
        }

        protected override IEnumerable<byte> EncodeValue(IDerAsnEncoder encoder, BigInteger value)
        {
            var littleEndianData = value.ToByteArray();
            return littleEndianData.Reverse();
        }
    }
}
