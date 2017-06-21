using System;
using System.IO;
using System.Text;
using NUnit.Framework;

namespace PemUtils.Tests
{
    [TestFixture]
    public class PemReaderTests
    {
        [Test]
        public void ReadPublicKey()
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

            // Expected data has been derived from https://superdry.apphb.com/tools/online-rsa-key-converter
            var expectedModulus = Convert.FromBase64String("siLoIxmXaZAFRBKtHYZhiF8m+pYR+xGIpupvsdDEvKO92D6fIccgVLIW6p6sSNkoXx5J6KDSMbA/chy5M6pRvJkaCXCI4zlCPMYvPhI8OxN3RYPfdQTLpgPywrlfdn2CAum7o4D8nR4NJacB3NfPnS9tsJ2L3p5iHviuTB4xm03IKmPPqsaJy+nXUFC1XS9E/PseVHRuNvKa7WmlwSZngQzKAVSIwqpgCc+oP1pKEeJ0M3LHFo8ao5SuzhfXUIGrPnkUKEE3m7B0b8xXZfP1N6ELoonWDK+RMgYIBaZdgBhPfHxF8KfTHvSzcUzWZojuR+ynaFL9AJK+8RiXnB4CJw==");
            var expectedExponent = Convert.FromBase64String("AQAB");

            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(pem)))
            using (var reader = new PemReader(stream))
            {
                var key = reader.ReadRsaKey();
                Assert.That(key.Modulus, Is.EqualTo(expectedModulus));
                Assert.That(key.Exponent, Is.EqualTo(expectedExponent));
            }
        }

        [Test]
        public void ReadPrivateKey()
        {
            var pem = "-----BEGIN RSA PRIVATE KEY-----\n"
                +"MIIEpAIBAAKCAQEAzw2rOycv+DqPhxJ/XZDsALH0WIn/Yyk97TGKhYME6CuybqlJ\n"
                +"BVTbpd2th2Fw/bDTwIHXr2wYJubX9b4G0gfVIwemq1rZyyJu2SxoOEK4BQg5s8po\n"
                +"Qh0Uq5KadP5DVvxk0AkIUySBCvNU+AHxRFJtJ6UZTjtUVYv2Yie+3YWhN3uDZ5ki\n"
                +"mjB7VSZqgdgnMpfx8kDTtGFHpA1oQNt71z3nyXPRTxZXGlYZlgGNEmv+cd3wYuuQ\n"
                +"0LnqZf3thXZfXYd0ASWhfdq0BxGT8WbThk8y6y9/aHsEtaf4QSpc8idVXPu0BwR8\n"
                +"P4p5lGzY11a5YERPOCl5BiaHYPgUJxzPnkFipwIDAQABAoIBAA26MUUNtw91CnkB\n"
                +"D/KrHgp5weJw276+SD3GkBGD+zpNU1ok3RN+acWYad3U5wHazF8x/JPDzeIeYekH\n"
                +"/TnFjSryYelwb4oZMVIysIIyYjLrNbAm1jyz4t/xK05gYSSOPTzRrHyeqfOI6HQ8\n"
                +"5LsL3/LF7mSSaGf3jJE7Y1sadfLQneSUhLMjDksl+o914P/WrV/LGcZ7O6noZ3Vg\n"
                +"e8zPGlPciPVOBqHqtWzxyTxbtycIUPW7GKbZjaWMwRwLnI5U+hcoGQgHsRUUJMcw\n"
                +"iGubcScdlOHCnorxM79sgjnko4RD+DUZGU/if/oNRS6dhIvaWCIrt+4lHhpHhZGI\n"
                +"d16tLCECgYEA+uOWmE91jbACXylQ+GSAchUfAmeqtf2e23TlXzScleEay/elSbf6\n"
                +"/HgUhUiUXCa2r+3d6kbEhoKy4Hed1Jm19rYBBIMSLZR0R3Il987x6WAnx+xrkfSW\n"
                +"I3Z8fHJoCtTfS/hXA+W/Snr0WlFUMgwaEZzd3FEIU62ZizKgjO1c67ECgYEA00V5\n"
                +"L2a7FGa684RWXxNmFX0ROmAn3Dt9urT8lRsfsRARF52dPqSthS/iLchBIGTBUtZD\n"
                +"5EnX5ttu88o5XkjxOLRrPsMBBW4fk1stUgQVifeDrlUG4yMf7lNLrkbmJy+0cl0y\n"
                +"me9l+HMhOPKdLfyebadSy1HlkR8nKSaQW3y3wdcCgYEA8vIL1DWtmaSEx22U0NNR\n"
                +"Zid5vbRxJIYRnGVX75dcwe4XKsgGMJqN2ojVJjOgJpP+d+IY8FHS4IYTfTWXilXG\n"
                +"VL7twVbC9Yw6BS1OAudMbjcEjp4rlEyKTpDf/woyIbr89+3lJQsG77KciBEVPNln\n"
                +"LQL/++Yj8BO9CYPe4FjBkCECgYEAmkKj1YSBHMhVwPDjz8/uPcpwBdunvxqBFw6H\n"
                +"TqfbYAGHOWMQKWk8eX8Y+qy5QNnQfpeMQufYCOw3+zGw6bMAzpKNq+nemQRrccCl\n"
                +"OrlYsMBVGbljqf0/l1iibcG+0uX2L3r1M4ilP99wZpBfS/CkDRSbU3Gc2XWRtm4+\n"
                +"AU7zLUkCgYBFVjQVyvkjNdnpKmFn3NCP5ELDj13dovK/ma75Paw+U9J1+4bIf+0O\n"
                +"hulF6wVUOJKlef7v7S7q4EM7WrbPXpI/Cqyn7B4Q/C0oWL0VCbceUgtvG6qTy8pd\n"
                +"AUxk9W0PnYDK7Sw+jv8iN0zNNr1SqZc37YLy2R1eD0R+3+RQHkl63Q==\n"
                +"-----END RSA PRIVATE KEY-----\n";

            // Expected data has been derived from https://superdry.apphb.com/tools/online-rsa-key-converter
            var expectedModulus = Convert.FromBase64String("zw2rOycv+DqPhxJ/XZDsALH0WIn/Yyk97TGKhYME6CuybqlJBVTbpd2th2Fw/bDTwIHXr2wYJubX9b4G0gfVIwemq1rZyyJu2SxoOEK4BQg5s8poQh0Uq5KadP5DVvxk0AkIUySBCvNU+AHxRFJtJ6UZTjtUVYv2Yie+3YWhN3uDZ5kimjB7VSZqgdgnMpfx8kDTtGFHpA1oQNt71z3nyXPRTxZXGlYZlgGNEmv+cd3wYuuQ0LnqZf3thXZfXYd0ASWhfdq0BxGT8WbThk8y6y9/aHsEtaf4QSpc8idVXPu0BwR8P4p5lGzY11a5YERPOCl5BiaHYPgUJxzPnkFipw==");
            var expectedExponent = Convert.FromBase64String("AQAB");
            var expectedD = Convert.FromBase64String("DboxRQ23D3UKeQEP8qseCnnB4nDbvr5IPcaQEYP7Ok1TWiTdE35pxZhp3dTnAdrMXzH8k8PN4h5h6Qf9OcWNKvJh6XBvihkxUjKwgjJiMus1sCbWPLPi3/ErTmBhJI49PNGsfJ6p84jodDzkuwvf8sXuZJJoZ/eMkTtjWxp18tCd5JSEsyMOSyX6j3Xg/9atX8sZxns7qehndWB7zM8aU9yI9U4Goeq1bPHJPFu3JwhQ9bsYptmNpYzBHAucjlT6FygZCAexFRQkxzCIa5txJx2U4cKeivEzv2yCOeSjhEP4NRkZT+J/+g1FLp2Ei9pYIiu37iUeGkeFkYh3Xq0sIQ==");
            var expectedP = Convert.FromBase64String("+uOWmE91jbACXylQ+GSAchUfAmeqtf2e23TlXzScleEay/elSbf6/HgUhUiUXCa2r+3d6kbEhoKy4Hed1Jm19rYBBIMSLZR0R3Il987x6WAnx+xrkfSWI3Z8fHJoCtTfS/hXA+W/Snr0WlFUMgwaEZzd3FEIU62ZizKgjO1c67E=");
            var expectedQ = Convert.FromBase64String("00V5L2a7FGa684RWXxNmFX0ROmAn3Dt9urT8lRsfsRARF52dPqSthS/iLchBIGTBUtZD5EnX5ttu88o5XkjxOLRrPsMBBW4fk1stUgQVifeDrlUG4yMf7lNLrkbmJy+0cl0yme9l+HMhOPKdLfyebadSy1HlkR8nKSaQW3y3wdc=");
            var expectedDP = Convert.FromBase64String("8vIL1DWtmaSEx22U0NNRZid5vbRxJIYRnGVX75dcwe4XKsgGMJqN2ojVJjOgJpP+d+IY8FHS4IYTfTWXilXGVL7twVbC9Yw6BS1OAudMbjcEjp4rlEyKTpDf/woyIbr89+3lJQsG77KciBEVPNlnLQL/++Yj8BO9CYPe4FjBkCE=");
            var expectedDQ = Convert.FromBase64String("mkKj1YSBHMhVwPDjz8/uPcpwBdunvxqBFw6HTqfbYAGHOWMQKWk8eX8Y+qy5QNnQfpeMQufYCOw3+zGw6bMAzpKNq+nemQRrccClOrlYsMBVGbljqf0/l1iibcG+0uX2L3r1M4ilP99wZpBfS/CkDRSbU3Gc2XWRtm4+AU7zLUk=");
            var expectedInverseQ = Convert.FromBase64String("RVY0Fcr5IzXZ6SphZ9zQj+RCw49d3aLyv5mu+T2sPlPSdfuGyH/tDobpResFVDiSpXn+7+0u6uBDO1q2z16SPwqsp+weEPwtKFi9FQm3HlILbxuqk8vKXQFMZPVtD52Ayu0sPo7/IjdMzTa9UqmXN+2C8tkdXg9Eft/kUB5Jet0=");

            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(pem)))
            using (var reader = new PemReader(stream))
            {
                var key = reader.ReadRsaKey();
                Assert.That(key.Modulus, Is.EqualTo(expectedModulus));
                Assert.That(key.Exponent, Is.EqualTo(expectedExponent));
                Assert.That(key.D, Is.EqualTo(expectedD));
                Assert.That(key.P, Is.EqualTo(expectedP));
                Assert.That(key.Q, Is.EqualTo(expectedQ));
                Assert.That(key.DP, Is.EqualTo(expectedDP));
                Assert.That(key.DQ, Is.EqualTo(expectedDQ));
                Assert.That(key.InverseQ, Is.EqualTo(expectedInverseQ));
            }
        }
        [Test]
        public void CrlfEnding()
        {
            var pem = "-----BEGIN PUBLIC KEY-----\r\n"
                + "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAsiLoIxmXaZAFRBKtHYZh\r\n"
                + "iF8m+pYR+xGIpupvsdDEvKO92D6fIccgVLIW6p6sSNkoXx5J6KDSMbA/chy5M6pR\r\n"
                + "vJkaCXCI4zlCPMYvPhI8OxN3RYPfdQTLpgPywrlfdn2CAum7o4D8nR4NJacB3NfP\r\n"
                + "nS9tsJ2L3p5iHviuTB4xm03IKmPPqsaJy+nXUFC1XS9E/PseVHRuNvKa7WmlwSZn\r\n"
                + "gQzKAVSIwqpgCc+oP1pKEeJ0M3LHFo8ao5SuzhfXUIGrPnkUKEE3m7B0b8xXZfP1\r\n"
                + "N6ELoonWDK+RMgYIBaZdgBhPfHxF8KfTHvSzcUzWZojuR+ynaFL9AJK+8RiXnB4C\r\n"
                + "JwIDAQAB\r\n"
                + "-----END PUBLIC KEY-----\r\n";

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
