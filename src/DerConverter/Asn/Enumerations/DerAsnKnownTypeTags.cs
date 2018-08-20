namespace DerConverter.Asn
{
    public static class DerAsnKnownTypeTags
    {
        public static class Primitive
        {
            public static readonly long Boolean = 0x01;
            public static readonly long Integer = 0x02;
            public static readonly long BitString = 0x03;
            public static readonly long OctetString = 0x04;
            public static readonly long Null = 0x05;
            public static readonly long ObjectIdentifier = 0x06;
            public static readonly long Utf8String = 0x0C;
            public static readonly long PrintableString = 0x13;
            public static readonly long TeletexString = 0x14;
            public static readonly long Ia5tring = 0x16;
            public static readonly long UtcTime = 0x17;
            public static readonly long BmpString = 0x1E;
        }

        public static class Constructed
        {
            public static readonly long Sequence = 0x10;
            public static readonly long Set = 0x11;
        }
    }
}
