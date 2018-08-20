using System.Collections.Generic;

namespace DerConverter.Asn
{
    public abstract class DerAsnType
    {
        public DerAsnIdentifier Identifier { get; private set; }

        public object Value { get; protected set; }

        protected DerAsnType(DerAsnIdentifier identifier)
        {
            Identifier = identifier;
        }
    }

    public abstract class DerAsnType<TValue> : DerAsnType
    {
        protected DerAsnType(DerAsnIdentifier identifier, Queue<byte> data)
            : base(identifier)
        {
            Value = DecodeValue(data);
        }

        protected DerAsnType(DerAsnIdentifier identifier, TValue value)
            : base(identifier)
        {
            Value = value;
        }

        protected abstract TValue DecodeValue(Queue<byte> data);

        protected abstract byte[] EncodeValue(TValue value);
    }
}
