namespace DerConverter.Asn
{
    public static class DerAsnIdentifiers
    {
        public static class Primitive
        {
            public static readonly DerAsnIdentifier Boolean = new DerAsnIdentifier(DerAsnTagClass.Universal, DerAsnEncodingType.Primitive, DerAsnKnownTypeTags.Primitive.Boolean);
            public static readonly DerAsnIdentifier Integer = new DerAsnIdentifier(DerAsnTagClass.Universal, DerAsnEncodingType.Primitive, DerAsnKnownTypeTags.Primitive.Integer);
            public static readonly DerAsnIdentifier BitString = new DerAsnIdentifier(DerAsnTagClass.Universal, DerAsnEncodingType.Primitive, DerAsnKnownTypeTags.Primitive.BitString);
            public static readonly DerAsnIdentifier OctetString = new DerAsnIdentifier(DerAsnTagClass.Universal, DerAsnEncodingType.Primitive, DerAsnKnownTypeTags.Primitive.OctetString);
            public static readonly DerAsnIdentifier Null = new DerAsnIdentifier(DerAsnTagClass.Universal, DerAsnEncodingType.Primitive, DerAsnKnownTypeTags.Primitive.Null);
            public static readonly DerAsnIdentifier ObjectIdentifier = new DerAsnIdentifier(DerAsnTagClass.Universal, DerAsnEncodingType.Primitive, DerAsnKnownTypeTags.Primitive.ObjectIdentifier);
            public static readonly DerAsnIdentifier Utf8String = new DerAsnIdentifier(DerAsnTagClass.Universal, DerAsnEncodingType.Primitive, DerAsnKnownTypeTags.Primitive.Utf8String);
            public static readonly DerAsnIdentifier PrintableString = new DerAsnIdentifier(DerAsnTagClass.Universal, DerAsnEncodingType.Primitive, DerAsnKnownTypeTags.Primitive.PrintableString);
            public static readonly DerAsnIdentifier TeletexString = new DerAsnIdentifier(DerAsnTagClass.Universal, DerAsnEncodingType.Primitive, DerAsnKnownTypeTags.Primitive.TeletexString);
            public static readonly DerAsnIdentifier Ia5String = new DerAsnIdentifier(DerAsnTagClass.Universal, DerAsnEncodingType.Primitive, DerAsnKnownTypeTags.Primitive.Ia5String);
            public static readonly DerAsnIdentifier UtcTime = new DerAsnIdentifier(DerAsnTagClass.Universal, DerAsnEncodingType.Primitive, DerAsnKnownTypeTags.Primitive.UtcTime);
            public static readonly DerAsnIdentifier BmpString = new DerAsnIdentifier(DerAsnTagClass.Universal, DerAsnEncodingType.Primitive, DerAsnKnownTypeTags.Primitive.BmpString);
        }

        public static class Constructed
        {
            public static readonly DerAsnIdentifier Sequence = new DerAsnIdentifier(DerAsnTagClass.Universal, DerAsnEncodingType.Constructed, DerAsnKnownTypeTags.Constructed.Sequence);
            public static readonly DerAsnIdentifier Set = new DerAsnIdentifier(DerAsnTagClass.Universal, DerAsnEncodingType.Constructed, DerAsnKnownTypeTags.Constructed.Set);
        }
    }
}
