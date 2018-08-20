using System;
using System.Linq;
using DerConverter.Asn;

namespace DerConverter
{
    public static class DerConvert
    {
        public static Func<IDerAsnDecoder> DefaultDecoder { get; set; } = () => new DefaultDerAsnDecoder();

        public static Func<IDerAsnEncoder> DefaultEncoder { get; set; } = () => new DefaultDerAsnEncoder();

        public static DerAsnType Decode(byte[] data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            if (!data.Any()) throw new ArgumentException("Data cannot be empty", nameof(data));
            if (DefaultDecoder == null) throw new ArgumentNullException(nameof(DefaultDecoder));

            using (var decoder = DefaultDecoder())
            {
                return decoder.Decode(data);
            }
        }

        public static byte[] Encode(DerAsnType data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            if (DefaultEncoder == null) throw new ArgumentNullException(nameof(DefaultEncoder));

            using (var encoder = DefaultEncoder())
            {
                return encoder.Encode(data);
            }
        }
    }
}
