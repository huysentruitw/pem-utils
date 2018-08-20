//using System;
//using System.IO;
//using System.Security.Cryptography;
//using System.Text;
//using DerConverter;
//using DerConverter.Asn;

//namespace PemUtils
//{
//    public class PemWriter : IDisposable
//    {
//        private Stream _stream;
//        private int _maximumLineLength;
//        private bool _disposeStream;
//        private Encoding _encoding;

//        public PemWriter(Stream stream, int maximumLineLength = 64, bool disposeStream = false, Encoding encoding = null)
//        {
//            if (stream == null) throw new ArgumentNullException(nameof(stream));
//            if (maximumLineLength < 32) throw new ArgumentOutOfRangeException(nameof(maximumLineLength), "Length should be bigger than or equal to 32");
//            _stream = stream;
//            _maximumLineLength = maximumLineLength;
//            _disposeStream = disposeStream;
//            _encoding = encoding ?? Encoding.UTF8;
//        }

//        public void Dispose()
//        {
//            if (_disposeStream) _stream.Dispose();
//        }

//        public void WritePublicKey(RSA rsa) => WritePublicKey(rsa.ExportParameters(false));

//        public void WritePublicKey(RSAParameters parameters)
//        {
//            var innerSequence = new DerAsnSequence(new DerAsnType[]
//            {
//                new DerAsnInteger(parameters.Modulus, true),
//                new DerAsnInteger(parameters.Exponent, true)
//            });

//            var outerSequence = new DerAsnSequence(new DerAsnType[]
//            {
//                new DerAsnSequence(new DerAsnType[]
//                {
//                    new DerAsnObjectIdentifier("1.2.840.113549.1.1.1"), // rsaEncryption http://www.oid-info.com/get/1.2.840.113549.1.1.1
//                    new DerAsnNull()
//                }),
//                new DerAsnBitString(DerConvert.Encode(innerSequence))
//            });

//            Write(outerSequence, PemFormat.Public);
//        }

//        public void WritePrivateKey(RSA rsa) => WritePrivateKey(rsa.ExportParameters(true));

//        public void WritePrivateKey(RSAParameters parameters)
//        {
//            var sequence = new DerAsnSequence(new DerAsnType[]
//            {
//                new DerAsnInteger(new byte[] { 0x00 }, true),   // Version
//                new DerAsnInteger(parameters.Modulus, true),
//                new DerAsnInteger(parameters.Exponent, true),
//                new DerAsnInteger(parameters.D, true),
//                new DerAsnInteger(parameters.P, true),
//                new DerAsnInteger(parameters.Q, true),
//                new DerAsnInteger(parameters.DP, true),
//                new DerAsnInteger(parameters.DQ, true),
//                new DerAsnInteger(parameters.InverseQ, true)
//            });

//            Write(sequence, PemFormat.Rsa);
//        }

//        private void Write(DerAsnType der, PemFormat format)
//        {
//            var derBytes = DerConvert.Encode(der);

//            var derBase64 = Convert.ToBase64String(derBytes);

//            var pem = new StringBuilder();
//            pem.Append(format.Header + "\n");
//            for (var i = 0; i < derBase64.Length; i += _maximumLineLength)
//            {
//                pem.Append(derBase64.Substring(i, Math.Min(_maximumLineLength, derBase64.Length - i)));
//                pem.Append("\n");
//            }
//            pem.Append(format.Footer + "\n");

//            using (var writer = new StreamWriter(_stream, _encoding, 4096, true))
//                writer.Write(pem.ToString());
//        }
//    }
//}
