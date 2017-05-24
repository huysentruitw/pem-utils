namespace PemUtils
{
    public class PemFormat
    {
        private readonly string _type;

        private PemFormat(string type)
        {
            _type = type;
        }

        public string Header => $"-----BEGIN {_type}-----";

        public string Footer => $"-----END {_type}-----";

        public static PemFormat X509CertificateOld => new PemFormat("X509 CERTIFICATE");

        public static PemFormat X509Certificate => new PemFormat("CERTIFICATE");

        public static PemFormat X509Pair => new PemFormat("CERTIFICATE PAIR");

        public static PemFormat X509Trusted => new PemFormat("TRUSTED CERTIFICATE");

        public static PemFormat X509RequestOld => new PemFormat("NEW CERTIFICATE REQUEST");

        public static PemFormat X509Request => new PemFormat("CERTIFICATE REQUEST");

        public static PemFormat X509Crl => new PemFormat("X509 CRL");

        public static PemFormat EvpPkey => new PemFormat("ANY PRIVATE KEY");

        public static PemFormat Public => new PemFormat("PUBLIC KEY");

        public static PemFormat Rsa => new PemFormat("RSA PRIVATE KEY");

        public static PemFormat RsaPublic => new PemFormat("RSA PUBLIC KEY");

        public static PemFormat Dsa => new PemFormat("DSA PRIVATE KEY");

        public static PemFormat DsaPublic => new PemFormat("DSA PUBLIC KEY");

        public static PemFormat Pkcs7 => new PemFormat("PKCS7");

        public static PemFormat Pkcs7Signed => new PemFormat("PKCS #7 SIGNED DATA");

        public static PemFormat Pkcs8 => new PemFormat("ENCRYPTED PRIVATE KEY");

        public static PemFormat Pkcs8Inf => new PemFormat("PRIVATE KEY");

        public static PemFormat DhParameters => new PemFormat("DH PARAMETERS");

        public static PemFormat SslSession => new PemFormat("SSL SESSION PARAMETERS");

        public static PemFormat DsaParameters => new PemFormat("DSA PARAMETERS");

        public static PemFormat EcdsaPublic => new PemFormat("ECDSA PUBLIC KEY");

        public static PemFormat EcParameters => new PemFormat("EC PARAMETERS");

        public static PemFormat EcPrivateKey => new PemFormat("EC PRIVATE KEY");

        public static PemFormat Parameters => new PemFormat("PARAMETERS");

        public static PemFormat Cms => new PemFormat("CMS");
    }
}
