using System.Linq;
using DerConverter.Asn.KnownTypes;

namespace PemUtils
{
    internal static class DerAsnBitStringExtensions
    {
        public static byte[] ToByteArray(this DerAsnBitString bitString)
            => bitString.Encode(null).Skip(1).ToArray();
    }
}
