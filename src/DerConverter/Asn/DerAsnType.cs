using System.Collections.Generic;
using System.Linq;

namespace DerConverter.Asn
{
    public abstract class DerAsnType
    {
        public DerAsnIdentifier Identifier { get; private set; }

        public virtual object Value { get; protected set; }

        protected DerAsnType(DerAsnIdentifier identifier)
        {
            Identifier = identifier;
        }

        public abstract byte[] Encode(IDerAsnEncoder encoder);
    }

    public abstract class DerAsnType<TValue> : DerAsnType
    {
        protected DerAsnType(IDerAsnDecoder decoder, DerAsnIdentifier identifier, Queue<byte> rawData)
            : base(identifier)
        {
            Value = DecodeValue(decoder, rawData);
        }

        protected DerAsnType(DerAsnIdentifier identifier, TValue value)
            : base(identifier)
        {
            Value = value;
        }

        public new TValue Value
        {
            get { return (TValue)base.Value; }
            set { base.Value = value; }
        }

        public override byte[] Encode(IDerAsnEncoder encoder)
            => EncodeValue(encoder, Value).ToArray();

        protected abstract TValue DecodeValue(IDerAsnDecoder decoder, Queue<byte> rawData);

        protected abstract IEnumerable<byte> EncodeValue(IDerAsnEncoder encoder, TValue value);
    }
}
