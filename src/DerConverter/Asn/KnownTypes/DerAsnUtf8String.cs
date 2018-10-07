using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DerConverter.Asn.KnownTypes
{
    public class DerAsnUtf8String : DerAsnType<string>
    {
        internal DerAsnUtf8String(IDerAsnDecoder decoder, DerAsnIdentifier identifier, Queue<byte> rawData)
            : base(decoder, identifier, rawData)
        {
        }

        public DerAsnUtf8String(DerAsnIdentifier identifier, string value)
            : base(identifier, value)
        {
        }

        public DerAsnUtf8String(string value)
            : this(DerAsnIdentifiers.Primitive.Utf8String, value)
        {
        }

        protected override string DecodeValue(IDerAsnDecoder decoder, Queue<byte> rawData)
        {
            return Encoding.UTF8.GetString(rawData.DequeueAll().ToArray());
        }

        protected override IEnumerable<byte> EncodeValue(IDerAsnEncoder encoder, string value)
        {
            return Encoding.UTF8.GetBytes(value);
        }
    }
}
