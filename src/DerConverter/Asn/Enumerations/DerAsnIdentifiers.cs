namespace DerConverter.Asn
{
    public static class DerAsnIdentifiers
    {
        public static class Universal
        {
            public static readonly DerAsnIdentifier Boolean = new DerAsnIdentifier(DerAsnTagClass.Universal, DerAsnEncodingType.Primitive, 0x01);
            public static readonly DerAsnIdentifier Integer = new DerAsnIdentifier(DerAsnTagClass.Universal, DerAsnEncodingType.Primitive, 0x02);
            public static readonly DerAsnIdentifier BitString = new DerAsnIdentifier(DerAsnTagClass.Universal, DerAsnEncodingType.Primitive, 0x03);
            public static readonly DerAsnIdentifier OctetString = new DerAsnIdentifier(DerAsnTagClass.Universal, DerAsnEncodingType.Primitive, 0x04);
            public static readonly DerAsnIdentifier Null = new DerAsnIdentifier(DerAsnTagClass.Universal, DerAsnEncodingType.Primitive, 0x05);
            public static readonly DerAsnIdentifier ObjectIdentifier = new DerAsnIdentifier(DerAsnTagClass.Universal, DerAsnEncodingType.Primitive, 0x06);
            public static readonly DerAsnIdentifier Utf8String = new DerAsnIdentifier(DerAsnTagClass.Universal, DerAsnEncodingType.Primitive, 0x0C);
            public static readonly DerAsnIdentifier Sequence = new DerAsnIdentifier(DerAsnTagClass.Universal, DerAsnEncodingType.Constructed, 0x10);
            public static readonly DerAsnIdentifier Set = new DerAsnIdentifier(DerAsnTagClass.Universal, DerAsnEncodingType.Constructed, 0x11);
            public static readonly DerAsnIdentifier PrintableString = new DerAsnIdentifier(DerAsnTagClass.Universal, DerAsnEncodingType.Primitive, 0x13);
            public static readonly DerAsnIdentifier TeletexString = new DerAsnIdentifier(DerAsnTagClass.Universal, DerAsnEncodingType.Primitive, 0x14);
            public static readonly DerAsnIdentifier Ia5tring = new DerAsnIdentifier(DerAsnTagClass.Universal, DerAsnEncodingType.Primitive, 0x16);
            public static readonly DerAsnIdentifier UtcTime = new DerAsnIdentifier(DerAsnTagClass.Universal, DerAsnEncodingType.Primitive, 0x17);
            public static readonly DerAsnIdentifier BmpString = new DerAsnIdentifier(DerAsnTagClass.Universal, DerAsnEncodingType.Primitive, 0x1E);
        }
    }
}
