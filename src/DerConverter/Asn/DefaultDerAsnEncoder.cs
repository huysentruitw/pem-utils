using System;
using System.Collections.Generic;
using System.Linq;

namespace DerConverter.Asn
{
    public class DefaultDerAsnEncoder : IDerAsnEncoder
    {
        public virtual void Dispose() { }

        public virtual IEnumerable<byte> Encode(DerAsnType asnType)
        {
            if (asnType == null) throw new ArgumentNullException(nameof(asnType));

            foreach (var data in asnType.Identifier.Encode())
                yield return data;

            var valueData = asnType.Encode(this).ToArray();

            foreach (var data in new DerAsnLength(valueData.Length).Encode())
                yield return data;

            foreach (var data in valueData)
                yield return data;
        }
    }
}
