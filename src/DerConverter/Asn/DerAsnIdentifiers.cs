namespace DerConverter.Asn
{
    public static class DerAsnIdentifiers
    {
        public static class Universal
        {
            public static DerAsnIdentifier Boolean { get; } = new DerAsnIdentifier(DerAsnTagClass.Universal, DerAsnEncodingType.Primitive, 0x01);
            public static DerAsnIdentifier Integer { get; } = new DerAsnIdentifier(DerAsnTagClass.Universal, DerAsnEncodingType.Primitive, 0x02);
            public static DerAsnIdentifier BitString { get; } = new DerAsnIdentifier(DerAsnTagClass.Universal, DerAsnEncodingType.Primitive, 0x03);
            public static DerAsnIdentifier OctetString { get; } = new DerAsnIdentifier(DerAsnTagClass.Universal, DerAsnEncodingType.Primitive, 0x04);
            public static DerAsnIdentifier Null { get; } = new DerAsnIdentifier(DerAsnTagClass.Universal, DerAsnEncodingType.Primitive, 0x05);
            public static DerAsnIdentifier ObjectIdentifier { get; } = new DerAsnIdentifier(DerAsnTagClass.Universal, DerAsnEncodingType.Primitive, 0x06);
            public static DerAsnIdentifier Utf8String { get; } = new DerAsnIdentifier(DerAsnTagClass.Universal, DerAsnEncodingType.Primitive, 0x0C);
            public static DerAsnIdentifier Sequence { get; } = new DerAsnIdentifier(DerAsnTagClass.Universal, DerAsnEncodingType.Constructed, 0x10);
            public static DerAsnIdentifier Set { get; } = new DerAsnIdentifier(DerAsnTagClass.Universal, DerAsnEncodingType.Constructed, 0x11);
            public static DerAsnIdentifier PrintableString { get; } = new DerAsnIdentifier(DerAsnTagClass.Universal, DerAsnEncodingType.Primitive, 0x13);
            public static DerAsnIdentifier TeletexString { get; } = new DerAsnIdentifier(DerAsnTagClass.Universal, DerAsnEncodingType.Primitive, 0x14);
            public static DerAsnIdentifier Ia5tring { get; } = new DerAsnIdentifier(DerAsnTagClass.Universal, DerAsnEncodingType.Primitive, 0x16);
            public static DerAsnIdentifier UtcTime { get; } = new DerAsnIdentifier(DerAsnTagClass.Universal, DerAsnEncodingType.Primitive, 0x17);
            public static DerAsnIdentifier BmpString { get; } = new DerAsnIdentifier(DerAsnTagClass.Universal, DerAsnEncodingType.Primitive, 0x1E);
        }
    }
}
