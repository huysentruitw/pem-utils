using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DerConverter.Asn.KnownTypes
{
    public class DerAsnIa5String : DerAsnType<string>
    {
        internal DerAsnIa5String(IDerAsnDecoder decoder, DerAsnIdentifier identifier, Queue<byte> rawData)
            : base(decoder, identifier, rawData)
        {
        }

        public DerAsnIa5String(DerAsnIdentifier identifier, string value)
            : base(identifier, value)
        {
        }

        public DerAsnIa5String(string value)
            : this(DerAsnIdentifiers.Primitive.Ia5String, value)
        {
        }

        protected override string DecodeValue(IDerAsnDecoder decoder, Queue<byte> rawData)
        {
            return Encoding.ASCII.GetString(rawData.DequeueAll().ToArray());
        }

        protected override IEnumerable<byte> EncodeValue(IDerAsnEncoder encoder, string value)
        {
            return Encoding.ASCII.GetBytes(value);
        }
    }
}
