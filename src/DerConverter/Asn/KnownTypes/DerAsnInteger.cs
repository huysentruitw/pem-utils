using System;
using System.Collections.Generic;
using System.Linq;

namespace DerConverter.Asn.KnownTypes
{
    public class DerAsnInteger : DerAsnType<long>
    {
        internal DerAsnInteger(IDerAsnDecoder decoder, DerAsnIdentifier identifier, Queue<byte> rawData)
            : base(decoder, identifier, rawData)
        {
        }

        public DerAsnInteger(DerAsnIdentifier identifier, long value)
            : base(identifier, value)
        {
        }

        public DerAsnInteger(long value)
            : this(DerAsnIdentifiers.Primitive.Integer, value)
        {
        }

        protected override long DecodeValue(IDerAsnDecoder decoder, Queue<byte> rawData)
        {
            if (rawData.Count < 1) throw new ArgumentException("Integer-type must contain at least one data byte", nameof(rawData));

            var firstByte = rawData.Dequeue();
            long value = firstByte & 0x7F;
            long weight = firstByte & 0x80;
            foreach (var data in rawData.DequeueAll())
            {
                value <<= 8;
                value += data;
                weight <<= 8;
            }

            return value - weight;
        }

        protected override IEnumerable<byte> EncodeValue(IDerAsnEncoder encoder, long value)
        {
            ulong bits = (ulong)value;

            var data = new Stack<byte>();
            var allOnes = ulong.MaxValue;
            do
            {
                data.Push((byte)bits);
                bits >>= 8;
                allOnes >>= 8;
            } while (bits != 0 && bits != allOnes);

            // When encoding a negative integer but bit 8 of first MSB in stack is 0,
            // put 0xFF in front of it so we don't lose the sign
            if (bits == allOnes && data.Peek() < 0x80)
                data.Push(0xFF);

            while (data.Any())
                yield return data.Pop();
        }
    }
}
