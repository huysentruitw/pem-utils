namespace DerConverter.Asn
{
    public enum DerAsnTypeTag : byte
    {
        // Primitive (bit 5: 0)
        Boolean = 0x01,
        Integer = 0x02,
        BitString = 0x03,
        OctetString = 0x04,
        Null = 0x05,
        ObjectIdentifier = 0x06,
        Utf8String = 0x0C,
        PrintableString = 0x13,
        Ia5tring = 0x16,
        BmpString = 0x1E,

        // Constructed (bit 5: 1)
        Sequence = 0x30,
        Set = 0x31
    }
}
