namespace DerConverter.Asn
{
    public enum DerAsnTypeTag : byte
    {
        Boolean = 0x01,
        Integer = 0x02,
        BitString = 0x03,
        OctetString = 0x04,
        Null = 0x05,
        ObjectIdentifier = 0x06,
        Utf8String = 0x0C,
        PrintableString = 0x13,
        Ia5tring = 0x16,
        UnicodeString = 0x1E,
        Sequence = 0x30,
        Set = 0x31
    }
}
