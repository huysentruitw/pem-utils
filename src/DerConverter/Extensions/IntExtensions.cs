using System.Collections.Generic;

namespace DerConverter
{
    internal static class IntExtensions
    {
        public static byte[] ToDerLengthBytes(this int length)
        {
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
    }
}
