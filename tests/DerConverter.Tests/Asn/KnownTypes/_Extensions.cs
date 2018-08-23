using System.Collections.Generic;

namespace DerConverter.Tests.Asn.KnownTypes
{
    internal static class ObjectExtensions
    {
        public static Queue<byte> Q(this object _, params byte[] data)
            => new Queue<byte>(data);
    }
}
