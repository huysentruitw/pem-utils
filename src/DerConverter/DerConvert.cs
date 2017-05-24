using DerConverter.Asn;

namespace DerConverter
{
    public static class DerConvert
    {
        public static DerAsnType Decode(byte[] data) => DerAsnType.Parse(data);

        public static byte[] Encode(DerAsnType data) => data.GetBytes();
    }
}
