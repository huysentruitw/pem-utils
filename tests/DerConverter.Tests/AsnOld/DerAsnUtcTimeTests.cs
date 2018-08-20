//using DerConverter.Asn;
//using NUnit.Framework;
//using System;
//using System.Linq;
//using System.Text;

//namespace DerConverter.Tests.Asn
//{
//    [TestFixture]
//    public class DerAsnUtcTimeTests
//    {
//        [Test]
//        public void DerAsnUtcTime_Parse_ShouldDecodeCorrectly()
//        {
//            var data = new byte[]
//            {
//                0x17, 0x0D,
//                0x31, 0x33, 0x30, 0x32, 0x30, 0x37, 0x32, 0x31, 0x34, 0x38, 0x34, 0x37, 0x5A
//            };

//            var type = DerAsnType.Parse(data);
//            Assert.That(type is DerAsnUtcTime, Is.True);

//            Assert.That(type.Value, Is.EqualTo(data.Skip(2).ToArray()));
//        }

//        [Test]
//        public void DerAsnUtcTime_GetBytes_ShouldEncodeCorrectly()
//        {
//            var type = new DerAsnUtcTime(new byte[] { 0x31, 0x33, 0x30, 0x32, 0x30, 0x37, 0x32, 0x31, 0x34, 0x38, 0x34, 0x37, 0x5A });

//            var data = type.GetBytes();

//            Assert.That((DerAsnTypeTag)data[0], Is.EqualTo(DerAsnTypeTag.UtcTime));
//            Assert.That(data[1], Is.EqualTo(0xD));
//            Assert.That(data.Skip(2).ToArray(), Is.EqualTo(new byte[] { 0x31, 0x33, 0x30, 0x32, 0x30, 0x37, 0x32, 0x31, 0x34, 0x38, 0x34, 0x37, 0x5A }));
//        }

//        [Test]
//        public void DerAsnUtcTime_Constructor_UtcTimeSeconds()
//        {
//            var date = new DateTimeOffset(2013, 02, 07, 21, 48, 47, TimeSpan.Zero);
//            var type = new DerAsnUtcTime(date, includeSeconds: true);

//            var data = type.GetBytes();

//            Assert.That((DerAsnTypeTag)data[0], Is.EqualTo(DerAsnTypeTag.UtcTime));
//            Assert.That(data[1], Is.EqualTo(0xD));
//            Assert.That(Encoding.ASCII.GetString(data.Skip(2).ToArray()), Is.EqualTo("130207214847Z"));
//        }

//        [Test]
//        public void DerAsnUtcTime_Constructor_UtcTimeWithoutSeconds()
//        {
//            var date = new DateTimeOffset(2013, 02, 07, 21, 48, 47, TimeSpan.Zero);
//            var type = new DerAsnUtcTime(date, includeSeconds: false);

//            var data = type.GetBytes();

//            Assert.That((DerAsnTypeTag)data[0], Is.EqualTo(DerAsnTypeTag.UtcTime));
//            Assert.That(data[1], Is.EqualTo(0xB));
//            Assert.That(Encoding.ASCII.GetString(data.Skip(2).ToArray()), Is.EqualTo("1302072148Z"));
//        }

//        [Test]
//        public void DerAsnUtcTime_Constructor_OffsetTimeSeconds()
//        {
//            var date = new DateTimeOffset(2013, 02, 07, 21, 48, 47, new TimeSpan(5, 45, 0));
//            var type = new DerAsnUtcTime(date, includeSeconds: true);

//            var data = type.GetBytes();

//            Assert.That((DerAsnTypeTag)data[0], Is.EqualTo(DerAsnTypeTag.UtcTime));
//            Assert.That(data[1], Is.EqualTo(0x11));
//            Assert.That(Encoding.ASCII.GetString(data.Skip(2).ToArray()), Is.EqualTo("130207214847+0545"));
//        }

//        [Test]
//        public void DerAsnUtcTime_Constructor_OffsetTimeWithoutSeconds()
//        {
//            var date = new DateTimeOffset(2013, 02, 07, 21, 48, 47, new TimeSpan(-5, -45, 0));
//            var type = new DerAsnUtcTime(date, includeSeconds: false);

//            var data = type.GetBytes();

//            Assert.That((DerAsnTypeTag)data[0], Is.EqualTo(DerAsnTypeTag.UtcTime));
//            Assert.That(data[1], Is.EqualTo(0x0F));
//            Assert.That(Encoding.ASCII.GetString(data.Skip(2).ToArray()), Is.EqualTo("1302072148-0545"));
//        }
//    }
//}
