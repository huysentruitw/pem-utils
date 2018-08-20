using System;

namespace DerConverter.Asn
{
    public class DefaultDerAsnEncoder : IDerAsnEncoder
    {
        public virtual void Dispose() { }

        public virtual byte[] Encode(DerAsnType data)
        {
            throw new NotImplementedException();
        }
    }
}
