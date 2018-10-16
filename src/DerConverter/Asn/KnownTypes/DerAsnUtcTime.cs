using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DerConverter.Asn.KnownTypes
{
    public class DerAsnUtcTime : DerAsnType<DateTimeOffset>
    {
        private const int YearThreshold = 2000;

        internal DerAsnUtcTime(IDerAsnDecoder decoder, DerAsnIdentifier identifier, Queue<byte> rawData)
            : base(decoder, identifier, rawData)
        {
        }

        public DerAsnUtcTime(DerAsnIdentifier identifier, DateTimeOffset value)
            : base(identifier, value)
        {
        }

        public DerAsnUtcTime(DateTimeOffset value)
            : this(DerAsnIdentifiers.Primitive.UtcTime, value)
        {
        }

        protected override DateTimeOffset DecodeValue(IDerAsnDecoder decoder, Queue<byte> rawData)
        {
            var ascii = Encoding.ASCII.GetString(rawData.DequeueAll().ToArray());
            var match = Regex.Match(ascii, @"^(?<year>[0-9]{2})(?<month>[0-9]{2})(?<day>[0-9]{2})(?<hour>[0-9]{2})(?<minute>[0-9]{2})(?<second>[0-9]{2})?(?<offset>(Z|([+-][0-9]{4})))$");
            if (!match.Success) throw new NotSupportedException($"UtcTime format '{ascii}' not supported");

            int year = AdjustYear(int.Parse(match.Groups["year"].Value));
            int month = int.Parse(match.Groups["month"].Value);
            int day = int.Parse(match.Groups["day"].Value);
            int hour = int.Parse(match.Groups["hour"].Value);
            int minute = int.Parse(match.Groups["minute"].Value);
            int second = !string.IsNullOrEmpty(match.Groups["second"].Value) ? int.Parse(match.Groups["second"].Value) : 0;
            TimeSpan offset = ParseOffset(match.Groups["offset"].Value);

            return new DateTimeOffset(year, month, day, hour, minute, second, offset);
        }

        protected override IEnumerable<byte> EncodeValue(IDerAsnEncoder encoder, DateTimeOffset value)
        {
            DateTime dateTime = value.DateTime;
            int year = dateTime.Year % 100;
            int month = dateTime.Month;
            int day = dateTime.Day;
            int hour = dateTime.Hour;
            int minute = dateTime.Minute;
            int second = dateTime.Second;

            var sb = new StringBuilder();
            sb.Append($"{year:00}{month:00}{day:00}{hour:00}{minute:00}");
            if (second > 0) sb.Append($"{second:00}");

            TimeSpan offset = value.Offset;
            if (offset != TimeSpan.Zero)
            {
                if (offset < TimeSpan.Zero)
                {
                    sb.Append("-");
                    offset = offset.Negate();
                }
                else
                {
                    sb.Append("+");
                }

                int offsetHours = offset.Hours;
                int offsetMinutes = offset.Minutes;
                sb.Append($"{offsetHours:00}{offsetMinutes:00}");
            }
            else
            {
                sb.Append("Z");
            }

            return Encoding.ASCII.GetBytes(sb.ToString());
        }

        private static int AdjustYear(int year)
        {
            var n = YearThreshold / 100;
            year += n * 100;
            if (year < YearThreshold) year += 100;
            return year;
        }

        private static TimeSpan ParseOffset(string value)
        {
            if (value == "Z") return TimeSpan.Zero;
            var match = Regex.Match(value, "^(?<sign>[+-])(?<hours>[0-9]{2})(?<minutes>[0-9]{2})$");
            if (!match.Success) throw new NotSupportedException($"UtcTime offset '{value}' not supported");
            string sign = match.Groups["sign"].Value;
            int hours = int.Parse(match.Groups["hours"].Value);
            int minutes = int.Parse(match.Groups["minutes"].Value);
            return sign == "+" ? new TimeSpan(hours, minutes, 0) : - new TimeSpan(hours, minutes, 0);
        }
    }
}
