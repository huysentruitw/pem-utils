using System.Collections.Generic;

namespace DerConverter.Tests.Asn.KnownTypes
{
    public class Q : Queue<byte>
    {
        public static Q New => new Q();

        public static Q operator <<(Q q, int d)
        {
            q.Enqueue((byte)d);
            return q;
        }
    }
}
