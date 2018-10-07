using System;
using System.Collections.Generic;

namespace DerConverter.Asn
{
    public interface IDerAsnEncoder : IDisposable
    {
        IEnumerable<byte> Encode(DerAsnType asnType);
    }
}
