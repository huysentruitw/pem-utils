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

        public abstract byte[] Encode();
    }

    public abstract class DerAsnType<TValue> : DerAsnType
    {
        protected DerAsnType(DerAsnIdentifier identifier, Queue<byte> rawData)
            : base(identifier)
        {
            Value = DecodeValue(rawData);
        }

        protected DerAsnType(DerAsnIdentifier identifier, TValue value)
            : base(identifier)
        {
            Value = value;
        }

        public override byte[] Encode() => EncodeValue((TValue)Value);

        protected abstract TValue DecodeValue(Queue<byte> rawData);

        protected abstract byte[] EncodeValue(TValue value);
    }
}
