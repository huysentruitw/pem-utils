using System.Collections.Generic;
using System.Linq;

namespace DerConverter.Asn.KnownTypes
{
    public class DerAsnNull : DerAsnType<object>
    {
        internal DerAsnNull(IDerAsnDecoder decoder, DerAsnIdentifier identifier, Queue<byte> rawData)
            : base(decoder, identifier, rawData)
        {
        }

        public DerAsnNull(DerAsnIdentifier identifier)
            : base(identifier, null)
        {
        }

        public DerAsnNull()
            : this(DerAsnIdentifiers.Primitive.Null)
        {
        }

        protected override object DecodeValue(IDerAsnDecoder decoder, Queue<byte> rawData)
        {
            return null;
        }

        protected override IEnumerable<byte> EncodeValue(IDerAsnEncoder encoder, object value)
        {
            return Enumerable.Empty<byte>();
        }
    }
}
