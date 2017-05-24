using System;
using System.Collections.Generic;

namespace DerConverter.Asn
{
    public class DerAsnBitString : DerAsnType
    {
        private readonly List<byte> _bytes = new List<byte>();
        private readonly int _unusedLowerBitsInLastByte;

        internal DerAsnBitString(Queue<byte> rawData)
            : base(DerAsnTypeTag.BitString)
        {
            if (rawData == null) throw new ArgumentNullException(nameof(rawData));
            _unusedLowerBitsInLastByte = rawData.Dequeue();
            _bytes.AddRange(rawData.DequeueAll());
        }

        public DerAsnBitString(byte[] bytes, int unusedLowerBitsInLastByte = 0)
            : base(DerAsnTypeTag.BitString)
        {
            if (bytes == null) throw new ArgumentNullException(nameof(bytes));
            if (unusedLowerBitsInLastByte < 0 || unusedLowerBitsInLastByte > 7) throw new ArgumentOutOfRangeException(nameof(unusedLowerBitsInLastByte));
            _bytes.AddRange(bytes);
            _unusedLowerBitsInLastByte = unusedLowerBitsInLastByte;
        }

        public override object Value => _bytes.ToArray();

        public int UnusedLowerBitsInLastByte => _unusedLowerBitsInLastByte;

        protected override byte[] InternalGetBytes()
        {
            var result = new List<byte>();
            result.Add((byte)_unusedLowerBitsInLastByte);
            result.AddRange(_bytes);
            return result.ToArray();
        }
    }
}
