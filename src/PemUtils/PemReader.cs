using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using DerConverter;
using DerConverter.Asn;

namespace PemUtils
{
    public class PemReader : IDisposable
    {
        private readonly Stream _stream;
        private readonly bool _disposeStream;

        public PemReader(Stream stream, bool disposeStream = false)
        {
            if (stream == null) throw new ArgumentNullException(nameof(stream));
            _stream = stream;
            _disposeStream = disposeStream;
        }

        public void Dispose()
        {
            if (_disposeStream) _stream.Dispose();
        }

        public RSAParameters ReadRsaKey()
        {
            using (var reader = new StreamReader(_stream, Encoding.UTF8, true, 4096, true))
            {
                var pem = reader.ReadToEnd();

                var parts = Regex.Match(pem, @"^(?<header>\-+\s?BEGIN[^-]+\-+)\s*(?<body>[^-]+)(?<footer>\-+\s?END[^-]+\-+)$");

                if (!parts.Success) throw new InvalidOperationException("Data on the stream doesn't match the required PEM format");

                // TODO check header / footer

                var derData = Convert.FromBase64String(parts.Groups["body"].Value);
                var outer_der = DerConvert.Decode(derData);

                var modulus_and_exponent = DerConvert.Decode((outer_der.Value as DerAsnType[])[1].Value as byte[]).Value as DerAsnType[];
                return new RSAParameters
                {
                    Modulus = modulus_and_exponent[0].Value as byte[],
                    Exponent = modulus_and_exponent[1].Value as byte[]
                };
                
            }

            throw new NotImplementedException();
        }
    }
}
