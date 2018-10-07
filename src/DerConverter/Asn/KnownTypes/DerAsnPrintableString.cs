using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DerConverter.Asn.KnownTypes
{
    public class DerAsnPrintableString : DerAsnType<string>
    {
        internal DerAsnPrintableString(IDerAsnDecoder decoder, DerAsnIdentifier identifier, Queue<byte> rawData)
            : base(decoder, identifier, rawData)
        {
        }

        public DerAsnPrintableString(DerAsnIdentifier identifier, string value)
            : base(identifier, value)
        {
        }

        public DerAsnPrintableString(string value)
            : this(DerAsnIdentifiers.Primitive.PrintableString, value)
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
