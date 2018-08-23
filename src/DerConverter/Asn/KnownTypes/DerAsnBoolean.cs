using System;
using System.Collections.Generic;

namespace DerConverter.Asn.KnownTypes
{
    public class DerAsnBoolean : DerAsnType<bool>
    {
        internal DerAsnBoolean(IDerAsnDecoder decoder, DerAsnIdentifier identifier, Queue<byte> rawData)
            : base(decoder, identifier, rawData)
        {
        }

        public DerAsnBoolean(DerAsnIdentifier identifier, bool value)
            : base(identifier, value)
        {
        }

        public DerAsnBoolean(bool value)
            : this(DerAsnIdentifiers.Primitive.Boolean, value)
        {
        }

        protected override bool DecodeValue(IDerAsnDecoder decoder, Queue<byte> rawData)
        {
            if (rawData.Count != 1) throw new ArgumentException("Boolean-type must contain one data byte", nameof(rawData));
            return rawData.Dequeue() != 0;
        }

        protected override IEnumerable<byte> EncodeValue(IDerAsnEncoder encoder, bool value)
        {
            yield return value ? (byte)0xFF : (byte)0x00;
        }
    }
}
