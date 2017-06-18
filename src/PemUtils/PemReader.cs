using System;
using System.Collections.Generic;
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
                var der_list = new List<DerAsnType>();
                EnqueueDers(outer_der, der_list);

                if (der_list.Count < 8)
                {
                    return new RSAParameters
                    {
                        Modulus = der_list[1].Value as byte[],
                        Exponent = der_list[2].Value as byte[]
                    };
                }
                return new RSAParameters
                {
                    Modulus = der_list[1].Value as byte[],
                    Exponent = der_list[2].Value as byte[],
                    D = der_list[3]?.Value as byte[],
                    P = der_list[4]?.Value as byte[],
                    Q = der_list[5]?.Value as byte[],
                    DP = der_list[6]?.Value as byte[],
                    DQ = der_list[7]?.Value as byte[],
                    InverseQ = der_list[8]?.Value as byte[]
                };
                
            }
        }

        public void EnqueueDers(DerAsnType der, List<DerAsnType> ders)
        {
            switch (der.Tag)
            {
                case DerAsnTypeTag.Sequence:
                    foreach (var type in (der.Value as DerAsnType[]))
                    {
                        EnqueueDers(type, ders);
                    }
                break;
                case DerAsnTypeTag.Null:
                break;
                case DerAsnTypeTag.BitString:
                    var inner_der = DerConvert.Decode(der.Value as byte[]);
                    EnqueueDers(inner_der, ders);
                break;
                default:
                    ders.Add(der);
                break;
            }
        }
    }
}
