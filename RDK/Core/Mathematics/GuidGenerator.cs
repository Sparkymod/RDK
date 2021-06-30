using System;

namespace RDK.Core.Mathematics
{
    public static class GuidGenerator
    {
        // Generate a 64 bit Guid
        public static long Long()
        {
            return Math.Abs(BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0));
        }

        // Generate a 32 bit Guid
        public static int Int()
        {
            return Math.Abs(Guid.NewGuid().GetHashCode());
        }

        // Generate a string Guid
        public static string String()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
