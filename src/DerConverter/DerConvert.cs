using System;
using System.Linq;
using DerConverter.Asn;

namespace DerConverter
{
    public static class DerConvert
    {
        public static Func<IDerAsnDecoder> DefaultDecoder { get; set; } = () => new DefaultDerAsnDecoder();

        public static Func<IDerAsnEncoder> DefaultEncoder { get; set; } = () => new DefaultDerAsnEncoder();

        public static DerAsnType Decode(byte[] rawData)
        {
            if (rawData == null) throw new ArgumentNullException(nameof(rawData));
            if (!rawData.Any()) throw new ArgumentException("Data cannot be empty", nameof(rawData));
            if (DefaultDecoder == null) throw new ArgumentNullException(nameof(DefaultDecoder));

            using (var decoder = DefaultDecoder())
            {
                return decoder.Decode(rawData);
            }
        }

        public static byte[] Encode(DerAsnType asnType)
        {
            if (asnType == null) throw new ArgumentNullException(nameof(asnType));
            if (DefaultEncoder == null) throw new ArgumentNullException(nameof(DefaultEncoder));

            using (var encoder = DefaultEncoder())
            {
                return encoder.Encode(asnType);
            }
        }
    }
}
