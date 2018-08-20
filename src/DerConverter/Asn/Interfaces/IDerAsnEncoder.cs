using System;

namespace DerConverter.Asn
{
    public interface IDerAsnEncoder : IDisposable
    {
        byte[] Encode(DerAsnType data);
    }
}
