//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text.RegularExpressions;

//namespace DerConverter.AsnOld
//{
//    public class DerAsnObjectIdentifier : DerAsnType
//    {
//        private readonly uint[] _nodes;

//        internal DerAsnObjectIdentifier(Queue<byte> rawData)
//            : base(DerAsnTypeTag.ObjectIdentifier)
//        {
//            if (rawData == null) throw new ArgumentNullException(nameof(rawData));
//            var nodes = new List<uint>();
//            var firstTwoNodes = rawData.Dequeue();
//            nodes.Add((uint)(firstTwoNodes / 40));
//            nodes.Add((uint)(firstTwoNodes % 40));
//            while (rawData.Any()) nodes.Add(DequeueNode(rawData));
//            _nodes = nodes.ToArray();
//        }

//        public DerAsnObjectIdentifier(string value)
//            : base(DerAsnTypeTag.ObjectIdentifier)
//        {
//            if (string.IsNullOrEmpty(value)) throw new ArgumentNullException(nameof(value));
//            if (!Regex.IsMatch(value, "^[0-9]+(.[0-9]+)+$")) throw new ArgumentException("Invalid object identifier format", nameof(value));
//            _nodes = value.Split('.').Select(x => uint.Parse(x)).ToArray();
//            if (_nodes[0] > 5) throw new ArgumentOutOfRangeException(nameof(value), "First value should not be greater than 5");
//            if (_nodes[1] > 39) throw new ArgumentOutOfRangeException(nameof(value), "Second value should not be greater than 39");
//        }

//        public override object Value => string.Join(".", _nodes.Select(x => x.ToString()));

//        protected override byte[] InternalGetBytes()
//        {
//            var result = new List<byte>();
//            result.Add((byte)(_nodes[0] * 40 + _nodes[1]));
//            foreach (var node in _nodes.Skip(2)) result.AddRange(EnqueueNode(node));
//            return result.ToArray();
//        }

//        private static uint DequeueNode(Queue<byte> queue)
//        {
//            uint result = 0;
//            byte data;
//            do
//            {
//                result <<= 7;
//                data = queue.Dequeue();
//                result += (uint)(data & 0x7F);
//            } while (data >= 0x80);
//            return result;
//        }

//        private static byte[] EnqueueNode(uint node)
//        {
//            var result = new List<byte>();
//            result.Add((byte)(node & 0x7F));
//            node >>= 7;
//            while (node > 0)
//            {
//                result.Add((byte)(0x80 | (node & 0x7F)));
//                node >>= 7;
//            }
//            result.Reverse();
//            return result.ToArray();
//        }
//    }
//}
