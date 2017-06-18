using System.IO;
using System.Text;
using NUnit.Framework;
using System.Linq;

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
        [Test]
        public void ReadPrivateKey()
        {
            var pem = "-----BEGIN RSA PRIVATE KEY-----\n" 
                    + "MIIEpAIBAAKCAQEAzw2rOycv+DqPhxJ/XZDsALH0WIn/Yyk97TGKhYME6CuybqlJ\n"
                    + "BVTbpd2th2Fw/bDTwIHXr2wYJubX9b4G0gfVIwemq1rZyyJu2SxoOEK4BQg5s8po\n"
                    + "Qh0Uq5KadP5DVvxk0AkIUySBCvNU+AHxRFJtJ6UZTjtUVYv2Yie+3YWhN3uDZ5ki\n"
                    + "mjB7VSZqgdgnMpfx8kDTtGFHpA1oQNt71z3nyXPRTxZXGlYZlgGNEmv+cd3wYuuQ\n"
                    + "0LnqZf3thXZfXYd0ASWhfdq0BxGT8WbThk8y6y9/aHsEtaf4QSpc8idVXPu0BwR8\n"
                    + "P4p5lGzY11a5YERPOCl5BiaHYPgUJxzPnkFipwIDAQABAoIBAA26MUUNtw91CnkB\n"
                    + "D/KrHgp5weJw276+SD3GkBGD+zpNU1ok3RN+acWYad3U5wHazF8x/JPDzeIeYekH\n"
                    + "/TnFjSryYelwb4oZMVIysIIyYjLrNbAm1jyz4t/xK05gYSSOPTzRrHyeqfOI6HQ8\n"
                    + "5LsL3/LF7mSSaGf3jJE7Y1sadfLQneSUhLMjDksl+o914P/WrV/LGcZ7O6noZ3Vg\n"
                    + "e8zPGlPciPVOBqHqtWzxyTxbtycIUPW7GKbZjaWMwRwLnI5U+hcoGQgHsRUUJMcw\n"
                    + "iGubcScdlOHCnorxM79sgjnko4RD+DUZGU/if/oNRS6dhIvaWCIrt+4lHhpHhZGI\n"
                    + "d16tLCECgYEA+uOWmE91jbACXylQ+GSAchUfAmeqtf2e23TlXzScleEay/elSbf6\n"
                    + "/HgUhUiUXCa2r+3d6kbEhoKy4Hed1Jm19rYBBIMSLZR0R3Il987x6WAnx+xrkfSW\n"
                    + "I3Z8fHJoCtTfS/hXA+W/Snr0WlFUMgwaEZzd3FEIU62ZizKgjO1c67ECgYEA00V5\n"
                    + "L2a7FGa684RWXxNmFX0ROmAn3Dt9urT8lRsfsRARF52dPqSthS/iLchBIGTBUtZD\n"
                    + "5EnX5ttu88o5XkjxOLRrPsMBBW4fk1stUgQVifeDrlUG4yMf7lNLrkbmJy+0cl0y\n"
                    + "me9l+HMhOPKdLfyebadSy1HlkR8nKSaQW3y3wdcCgYEA8vIL1DWtmaSEx22U0NNR\n"
                    + "Zid5vbRxJIYRnGVX75dcwe4XKsgGMJqN2ojVJjOgJpP+d+IY8FHS4IYTfTWXilXG\n"
                    + "VL7twVbC9Yw6BS1OAudMbjcEjp4rlEyKTpDf/woyIbr89+3lJQsG77KciBEVPNln\n"
                    + "LQL/++Yj8BO9CYPe4FjBkCECgYEAmkKj1YSBHMhVwPDjz8/uPcpwBdunvxqBFw6H\n"
                    + "TqfbYAGHOWMQKWk8eX8Y+qy5QNnQfpeMQufYCOw3+zGw6bMAzpKNq+nemQRrccCl\n"
                    + "OrlYsMBVGbljqf0/l1iibcG+0uX2L3r1M4ilP99wZpBfS/CkDRSbU3Gc2XWRtm4+\n"
                    + "AU7zLUkCgYBFVjQVyvkjNdnpKmFn3NCP5ELDj13dovK/ma75Paw+U9J1+4bIf+0O\n"
                    + "hulF6wVUOJKlef7v7S7q4EM7WrbPXpI/Cqyn7B4Q/C0oWL0VCbceUgtvG6qTy8pd\n"
                    + "AUxk9W0PnYDK7Sw+jv8iN0zNNr1SqZc37YLy2R1eD0R+3+RQHkl63Q==\n"
                    + "-----END RSA PRIVATE KEY-----";
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(pem)))
            using (var reader = new PemReader(stream))
            {
                //RFC 3447
                var key = reader.ReadRsaKey();

                var coefficient = System.Convert.FromBase64String("RVY0Fcr5IzXZ6SphZ9zQj+RCw49d3aLyv5mu+T2sPlPSdfuGyH/tDobpResFVDiSpXn+7+0u6uBDO1q2z16SPwqsp+weEPwtKFi9FQm3HlILbxuqk8vKXQFMZPVtD52Ayu0sPo7/IjdMzTa9UqmXN+2C8tkdXg9Eft/kUB5Jet0=");
                var modulus = System.Convert.FromBase64String("AM8NqzsnL/g6j4cSf12Q7ACx9FiJ/2MpPe0xioWDBOgrsm6pSQVU26XdrYdhcP2w08CB169sGCbm1/W+BtIH1SMHpqta2csibtksaDhCuAUIObPKaEIdFKuSmnT+Q1b8ZNAJCFMkgQrzVPgB8URSbSelGU47VFWL9mInvt2FoTd7g2eZIpowe1UmaoHYJzKX8fJA07RhR6QNaEDbe9c958lz0U8WVxpWGZYBjRJr/nHd8GLrkNC56mX97YV2X12HdAEloX3atAcRk/Fm04ZPMusvf2h7BLWn+EEqXPInVVz7tAcEfD+KeZRs2NdWuWBETzgpeQYmh2D4FCccz55BYqc=");
                var exponent1 = System.Convert.FromBase64String("APLyC9Q1rZmkhMdtlNDTUWYneb20cSSGEZxlV++XXMHuFyrIBjCajdqI1SYzoCaT/nfiGPBR0uCGE301l4pVxlS+7cFWwvWMOgUtTgLnTG43BI6eK5RMik6Q3/8KMiG6/Pft5SULBu+ynIgRFTzZZy0C//vmI/ATvQmD3uBYwZAh");
                var exponent2 = System.Convert.FromBase64String("AJpCo9WEgRzIVcDw48/P7j3KcAXbp78agRcOh06n22ABhzljEClpPHl/GPqsuUDZ0H6XjELn2AjsN/sxsOmzAM6Sjavp3pkEa3HApTq5WLDAVRm5Y6n9P5dYom3BvtLl9i969TOIpT/fcGaQX0vwpA0Um1NxnNl1kbZuPgFO8y1J");
                var prime1 = System.Convert.FromBase64String("APrjlphPdY2wAl8pUPhkgHIVHwJnqrX9ntt05V80nJXhGsv3pUm3+vx4FIVIlFwmtq/t3epGxIaCsuB3ndSZtfa2AQSDEi2UdEdyJffO8elgJ8fsa5H0liN2fHxyaArU30v4VwPlv0p69FpRVDIMGhGc3dxRCFOtmYsyoIztXOux");
                var prime2 = System.Convert.FromBase64String("ANNFeS9muxRmuvOEVl8TZhV9ETpgJ9w7fbq0/JUbH7EQERednT6krYUv4i3IQSBkwVLWQ+RJ1+bbbvPKOV5I8Ti0az7DAQVuH5NbLVIEFYn3g65VBuMjH+5TS65G5icvtHJdMpnvZfhzITjynS38nm2nUstR5ZEfJykmkFt8t8HX");
                var private_exponent = System.Convert.FromBase64String("DboxRQ23D3UKeQEP8qseCnnB4nDbvr5IPcaQEYP7Ok1TWiTdE35pxZhp3dTnAdrMXzH8k8PN4h5h6Qf9OcWNKvJh6XBvihkxUjKwgjJiMus1sCbWPLPi3/ErTmBhJI49PNGsfJ6p84jodDzkuwvf8sXuZJJoZ/eMkTtjWxp18tCd5JSEsyMOSyX6j3Xg/9atX8sZxns7qehndWB7zM8aU9yI9U4Goeq1bPHJPFu3JwhQ9bsYptmNpYzBHAucjlT6FygZCAexFRQkxzCIa5txJx2U4cKeivEzv2yCOeSjhEP4NRkZT+J/+g1FLp2Ei9pYIiu37iUeGkeFkYh3Xq0sIQ==");
                var exponent = System.Convert.FromBase64String("AQAB");

                //Note: skipping buffer 0s where present
                Assert.That(coefficient, Is.EquivalentTo(key.InverseQ));
                Assert.That(modulus.Skip(1), Is.EquivalentTo(key.Modulus));
                Assert.That(exponent1.Skip(1), Is.EquivalentTo(key.DP));
                Assert.That(exponent2.Skip(1), Is.EquivalentTo(key.DQ));
                Assert.That(prime1.Skip(1), Is.EquivalentTo(key.P));
                Assert.That(prime2.Skip(1), Is.EquivalentTo(key.Q));
                Assert.That(private_exponent, Is.EquivalentTo(key.D));
                Assert.That(exponent, Is.EquivalentTo(key.Exponent));
            }
        }
    }
}
