using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DerConverter.Asn.KnownTypes
{
    public class DerAsnBitString : DerAsnType<BitArray>
    {
        internal DerAsnBitString(IDerAsnDecoder decoder, DerAsnIdentifier identifier, Queue<byte> rawData)
            : base(decoder, identifier, rawData)
        {
        }

        public DerAsnBitString(DerAsnIdentifier identifier, BitArray value)
            : base(identifier, value)
        {
        }

        public DerAsnBitString(BitArray value)
            : this(DerAsnIdentifiers.Primitive.BitString, value)
        {
        }

        protected override BitArray DecodeValue(IDerAsnDecoder decoder, Queue<byte> rawData)
        {
            var unusedLowerBitsInLastByte = rawData.Dequeue();
            var data = rawData.DequeueAll().ToArray();

            var result = new BitArray(data.Length * 8 - unusedLowerBitsInLastByte);
            for (int i = 0; i < result.Length; i++)
                result[i] = (data[i / 8] << (i % 8) & 0x80) != 0;

            return result;
        }

        protected override IEnumerable<byte> EncodeValue(IDerAsnEncoder encoder, BitArray value)
        {
            var unusedLowerBitsInLastByte = (byte)(7 - ((value.Length + 7) % 8));
            yield return unusedLowerBitsInLastByte;
            byte result = 0;
            for (int i = 0; i < value.Length; i++)
            {
                if (value[i]) result |= (byte)(0x80 >> (i % 8));
                if (i % 8 == 7)
                {
                    yield return result;
                    result = 0;
                }
            }

            if (unusedLowerBitsInLastByte > 0)
            {
                yield return result;
            }
        }
    }
}
