using System;

namespace DerConverter.Asn
{
    public interface IDerAsnDecoder : IDisposable
    {
        DerAsnType Decode(byte[] rawData);
    }
}
