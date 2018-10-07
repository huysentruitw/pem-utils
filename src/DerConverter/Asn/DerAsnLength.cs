using System.Collections.Generic;

namespace DerConverter.Asn
{
    public class DerAsnLength
    {
        public long Length { get; private set; }

        public DerAsnLength(long length)
        {
            Length = length;
        }

        public override string ToString()
        {
            return Length.ToString();
        }

        public override bool Equals(object obj)
            => obj is DerAsnLength length && Length == length.Length;

        public override int GetHashCode()
            => Length.GetHashCode();

        public static implicit operator DerAsnLength(long value)
            => new DerAsnLength(value);

        public static implicit operator long(DerAsnLength value)
            => value.Length;

        internal byte[] Encode()
        {
            var length = Length;
            if (length < 0x80) return new[] { (byte)length };
            var result = new List<byte>();
            while (length > 0)
            {
                result.Add((byte)(length & 0xFF));
                length >>= 8;
            }
            result.Add((byte)(0x80 | result.Count));
            result.Reverse();
            return result.ToArray();
        }

        internal static DerAsnLength Decode(Queue<byte> data)
        {
            var count = data.Dequeue();
            if (count < 0x80) return count;
            count -= 0x80;
            long result = 0;
            for (int i = 0; i < count; i++)
            {
                result <<= 8;
                result += data.Dequeue();
            }
            return result;
        }
    }
}
