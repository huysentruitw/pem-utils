using System.IO;
using System.Text;
using NUnit.Framework;

namespace PemUtils.Tests
{
    [TestFixture]
    public class PemReaderTests
    {
        [Test]
        public void ReadsPublicKey()
        {
            var pem = "-----BEGIN PUBLIC KEY-----\n"
                + "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAsiLoIxmXaZAFRBKtHYZh\n"
                + "iF8m+pYR+xGIpupvsdDEvKO92D6fIccgVLIW6p6sSNkoXx5J6KDSMbA/chy5M6pR\n"
                + "vJkaCXCI4zlCPMYvPhI8OxN3RYPfdQTLpgPywrlfdn2CAum7o4D8nR4NJacB3NfP\n"
                + "nS9tsJ2L3p5iHviuTB4xm03IKmPPqsaJy+nXUFC1XS9E/PseVHRuNvKa7WmlwSZn\n"
                + "gQzKAVSIwqpgCc+oP1pKEeJ0M3LHFo8ao5SuzhfXUIGrPnkUKEE3m7B0b8xXZfP1\n"
                + "N6ELoonWDK+RMgYIBaZdgBhPfHxF8KfTHvSzcUzWZojuR+ynaFL9AJK+8RiXnB4C\n"
                + "JwIDAQAB\n"
                + "-----END PUBLIC KEY-----\n";

            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(pem)))
            using (var reader = new PemReader(stream))
            {
                var key = reader.ReadRsaKey();
                var modulus = System.Convert.FromBase64String("siLoIxmXaZAFRBKtHYZhiF8m +pYR+xGIpupvsdDEvKO92D6fIccgVLIW6p6sSNkoXx5J6KDSMbA/chy5M6pRvJkaCXCI4zlCPMYvPhI8OxN3RYPfdQTLpgPywrlfdn2CAum7o4D8nR4NJacB3NfPnS9tsJ2L3p5iHviuTB4xm03IKmPPqsaJy+nXUFC1XS9E/PseVHRuNvKa7WmlwSZngQzKAVSIwqpgCc+oP1pKEeJ0M3LHFo8ao5SuzhfXUIGrPnkUKEE3m7B0b8xXZfP1N6ELoonWDK+RMgYIBaZdgBhPfHxF8KfTHvSzcUzWZojuR+ynaFL9AJK+8RiXnB4CJw==");

                Assert.That(key.Modulus, Is.EquivalentTo(modulus));

                var exponent = System.Convert.FromBase64String("AQAB");
                Assert.That(key.Exponent, Is.EquivalentTo(exponent));
            }
        }
    }
}
