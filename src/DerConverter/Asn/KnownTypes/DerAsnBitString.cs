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

            if (unusedLowerBitsInLastByte > 0)
            {
                byte rest = 0;
                for (int i = 0; i < data.Length; i++)
                {
                    var @byte = data[i];
                    data[i] = (byte)((@byte >> unusedLowerBitsInLastByte) | rest);
                    rest = (byte)(@byte << (8 - unusedLowerBitsInLastByte));
                }
            }

            return new BitArray(data.Reverse().ToArray())
            {
                Length = data.Length * 8 - unusedLowerBitsInLastByte
            };
        }

        protected override IEnumerable<byte> EncodeValue(IDerAsnEncoder encoder, BitArray value)
        {
            var unusedLowerBitsInLastByte = (byte)(7 - ((value.Length + 7) % 8));
            yield return unusedLowerBitsInLastByte;

            var data = new byte[(value.Length + 7) / 8];
            value.CopyTo(data, 0);

            if (unusedLowerBitsInLastByte > 0)
            {
                byte rest = 0;
                for (int i = 0; i < data.Length; i++)
                {
                    var @byte = data[i];
                    data[i] = (byte)(@byte << unusedLowerBitsInLastByte | rest);
                    rest = (byte)(@byte >> (8 - unusedLowerBitsInLastByte));
                }
            }

            foreach (var @byte in data.Reverse())
                yield return @byte;
        }
    }
}
