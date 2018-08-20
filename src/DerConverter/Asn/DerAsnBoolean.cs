//using System;
//using System.Collections.Generic;

//namespace DerConverter.Asn
//{
//    public class DerAsnBoolean : DerAsnType<bool>
//    {
//        internal DerAsnBoolean(DerAsnIdentifier identifier, Queue<byte> data)
//            : base(identifier, data)
//        {
//        }

//        public DerAsnBoolean(bool value)
//            : base(identifier, value)
//        {
//        }

//        protected override bool DecodeValue(Queue<byte> data)
//        {
//            throw new NotImplementedException();
//        }

//        protected override byte[] EncodeValue(bool value)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
