using System.Collections.Generic;
using System.Linq;

namespace DerConverter.Asn.KnownTypes
{
    public class DerAsnOctetString : DerAsnType<byte[]>
    {
        internal DerAsnOctetString(IDerAsnDecoder decoder, DerAsnIdentifier identifier, Queue<byte> rawData)
            : base(decoder, identifier, rawData)
        {
        }

        public DerAsnOctetString(DerAsnIdentifier identifier, byte[] value)
            : base(identifier, value)
        {
        }

        public DerAsnOctetString(byte[] value)
            : this(DerAsnIdentifiers.Primitive.OctetString, value)
        {
        }

        protected override byte[] DecodeValue(IDerAsnDecoder decoder, Queue<byte> rawData)
        {
            return rawData.DequeueAll().ToArray();
        }

        protected override IEnumerable<byte> EncodeValue(IDerAsnEncoder encoder, byte[] value)
        {
            return value;
        }
    }
}
