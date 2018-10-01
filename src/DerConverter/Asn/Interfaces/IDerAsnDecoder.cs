using System;
using System.Collections.Generic;

namespace DerConverter.Asn
{
    public interface IDerAsnDecoder : IDisposable
    {
        DerAsnType Decode(byte[] rawData);

        DerAsnType Decode(Queue<byte> rawData);
    }
}
